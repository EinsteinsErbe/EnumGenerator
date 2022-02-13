using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGenerator
{
    public abstract class CodeWriter
    {
        protected static string INTENDATION = "    ";

        readonly protected StringBuilder sb;
        readonly protected string intendation;
        readonly protected Style style;

        public CodeWriter(StringBuilder sb, Style style, string intendation = "")
        {
            this.sb = sb;
            this.style = style;
            this.intendation = intendation;
        }
        public void WriteLine(string text)
        {
            sb.AppendLine(intendation + text);
        }

        public void NewLine()
        {
            sb.AppendLine();
        }

        public Block SubBlock(string head)
        {
            return new Block(sb, head, style, intendation);
        }
    }

    public class MainWriter : CodeWriter
    {
        public MainWriter(Style style) : base(new StringBuilder(), style, "") { }

        public string Finalize()
        {
            return sb.ToString();
        }
    }

    public class Block : CodeWriter, IDisposable
    {
        readonly string parentIntendation;
        readonly string foot;

        public Block(StringBuilder sb, string head, Style style, string parentIntendation, string foot = "}") : base(sb, style, parentIntendation + INTENDATION)
        {
            this.parentIntendation = parentIntendation;
            this.foot = foot;

            switch (style)
            {
                case Style.DOTNET:
                    sb.AppendLine(parentIntendation + head);
                    sb.AppendLine(parentIntendation + "{");
                    break;
                default:
                    sb.AppendLine(parentIntendation + head + " {");
                    break;
            }
        }

        public void Dispose()
        {
            sb.AppendLine(parentIntendation + foot);
        }

        public Block SubBlock(string head, string foot = "}")
        {
            return new Block(sb, head, style, intendation, foot);
        }
    }

    public enum Style
    {
        DOTNET,
        JAVA
    }
}

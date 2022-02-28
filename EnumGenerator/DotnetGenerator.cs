using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGenerator
{
    public class DotnetGenerator : IGenerator
    {
        public string Namespace { get; set; } = "MyEnum";

        public void Generate(EnumModel model, string path)
        {
            MainWriter mw = new(Style.DOTNET);

            using (Block b = mw.SubBlock("namespace " + Namespace))
            {
                using (Block bb = b.SubBlock("public enum " + model.Name))
                {
                    for (int i = 0; i < model.Members.Count; i++)
                    {
                        bb.WriteLine($"{model.Members[i].Name} = {model.Members[i].Value}" + (i < model.Members.Count - 1 ? "," : ""));
                    }
                }
                b.NewLine();
                using (Block bb = b.SubBlock("public static class " + model.Name + "_Extensions"))
                {
                    using (Block bbb = bb.SubBlock($"public static string GetDescription(this {model.Name} value)"))
                    {
                        using (Block bbbb = bbb.SubBlock("return value switch", "};"))
                        {
                            model.Members.ForEach(m => bbbb.WriteLine($"{model.Name}.{m.Name} => \"{m.Description}\","));
                            bbbb.WriteLine("_ => \"undefined\",");
                        }
                    }
                }
            }

            Directory.CreateDirectory(path);
            File.WriteAllText(Path.Combine(path, model.Name+".cs"), mw.Finalize());
        }
    }
}

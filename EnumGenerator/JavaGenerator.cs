using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGenerator
{
    public class JavaGenerator : IGenerator
    {
        public string Package { get; set; } = "myenum";

        public void Generate(EnumModel model)
        {
            MainWriter mw = new(Style.JAVA);

            mw.WriteLine("package " + Package + ";");
            mw.NewLine();

            using (Block b = mw.SubBlock("public enum " + model.Name))
            {
                for (int i = 0; i < model.Members.Count; i++)
                {
                    b.WriteLine($"{model.Members[i].Name}({model.Members[i].Value}, \"{model.Members[i].Description}\")" + (i < model.Members.Count - 1 ? "," : ";"));
                }

                b.NewLine();
                b.WriteLine("private int value;");
                b.WriteLine("private String description;");
                b.NewLine();

                using (Block bb = b.SubBlock(model.Name + "(int value, String description)"))
                {
                    bb.WriteLine("this.value = value;");
                    bb.WriteLine("this.description = description;");
                }
                b.NewLine();
                using (Block bb = b.SubBlock("public int getValue()"))
                {
                    bb.WriteLine("return value;");
                }
                b.NewLine();
                using (Block bb = b.SubBlock("public String getDescription()"))
                {
                    bb.WriteLine("return description;");
                }
            }

            Console.WriteLine(mw.Finalize());
        }
    }
}

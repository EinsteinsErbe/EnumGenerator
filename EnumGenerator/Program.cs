using EnumGenerator;
using Newtonsoft.Json;

Console.WriteLine("Create Enum");
EnumModel model = new EnumModel
{
    Name = "RECO_TYPE",
    Members = new List<EnumMember> {
        new EnumMember { Value = 0, Name = "CHECKBOX", Description = "Checkbox" },
        new EnumMember { Value = 1, Name = "SIGNATURE", Description = "Signature" }
    }
};

string json = JsonConvert.SerializeObject(model, Formatting.Indented);

Console.WriteLine(json);

new DotnetGenerator().Generate(model);
new JavaGenerator().Generate(model);
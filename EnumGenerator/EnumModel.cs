using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGenerator
{
    public class EnumModel
    {
        public string Name { get; set; } = string.Empty;
        public List<EnumMember> Members { get; set; } = new List<EnumMember>();
    }

    public class EnumMember
    {
        public int Value { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

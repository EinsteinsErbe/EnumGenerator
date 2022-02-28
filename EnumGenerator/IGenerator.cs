using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumGenerator
{
    public interface IGenerator
    {
        public void Generate(EnumModel model, string file);
    }
}

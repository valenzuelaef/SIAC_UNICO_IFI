using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro
{
   public class ItemGeneric
    {
        public ItemGeneric() { }
        public ItemGeneric(string vCode, string vCode2, string vDescription)
        {
            Code = vCode;
            Code2 = vCode2;
            Description = vDescription;
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Code2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetUbigeosType
{
    [DataContract(Name = "UbigeosTypeRequestCommon")]
    public class UbigeosTypeRequest : Claro.Entity.Request
    {
        public int coddep{ get; set; }
        public int codprov { get; set; }
        public int coddist { get; set; }
    }
}

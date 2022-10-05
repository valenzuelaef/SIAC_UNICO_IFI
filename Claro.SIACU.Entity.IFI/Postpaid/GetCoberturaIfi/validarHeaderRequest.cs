using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class validarHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}

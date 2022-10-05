using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class validarCoberturaResponse
    {
        [DataMember(Name = "responseStatus")]
        public responseStatus responseStatus { get; set; }

        [DataMember(Name = "responseData")]
        public responseData responseData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    //jean
    [DataContract(Name = "validarCoberturaRequest")]
    public class validarCoberturaRequest
    {
        [DataMember(Name = "MessageRequest")]
        public validarMessageRequest MessageRequest { get; set; }
     
    }
}

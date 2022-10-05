using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract()]
    public class validarBodyRequest
    {
        [DataMember(Name = "validarCoberturaRequest")]
        public ValidarCoberturaItemRequest validarCoberturaRequest { get; set; }

    }
  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class validarCoberturaMessageResponse
    {
        [DataMember(Name = "MessageResponse")]
        public validarMessageResponse MessageResponse { get; set; }

        public validarCoberturaMessageResponse()
        {
            MessageResponse = new validarMessageResponse();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class validarMessageRequest
    {
        [DataMember(Name = "Header")]
        public validarHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public BodyRequestValidarCobertura Body { get; set; }

        public validarMessageRequest()
        {
            Header = new validarHeaderRequest();
            Body = new BodyRequestValidarCobertura();
        }
    }
}

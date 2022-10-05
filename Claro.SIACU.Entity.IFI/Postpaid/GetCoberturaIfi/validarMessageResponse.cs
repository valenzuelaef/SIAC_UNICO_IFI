using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class validarMessageResponse
    {
        [DataMember(Name = "Header")]
        public validarHeaderResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public validarBodyResponse Body { get; set; }

        public validarMessageResponse()
        {
            Header = new validarHeaderResponse();
            Body = new validarBodyResponse();
        }
    }
}
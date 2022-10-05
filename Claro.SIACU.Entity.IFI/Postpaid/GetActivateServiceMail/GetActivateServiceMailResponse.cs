using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetActivateServiceMail
{
    [DataContract(Name = "GetActivateServiceMailIFIResponse")]
    public class GetActivateServiceMailResponse
    {
        [DataMember]
        public string strMensaje { get; set; }
        [DataMember]
        public string strResult { get; set; }
    }
}

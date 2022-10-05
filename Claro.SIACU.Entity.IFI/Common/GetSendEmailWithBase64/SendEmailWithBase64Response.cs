using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetSendEmailWithBase64
{
    [DataContract]
    public class SendEmailWithBase64Response
    {
        [DataMember]
        public string IdTransaccion { get; set; }

        [DataMember]
        public string ResponseCode { get; set; }

        [DataMember]
        public string ResponseMessage { get; set; }
    }
}

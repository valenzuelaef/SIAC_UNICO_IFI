using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetSendEmailWithBase64
{
    [DataContract]
    public class SendEmailWithBase64Request : Claro.Entity.Request
    {
        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public string To { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string HtmlFlag { get; set; }

        [DataMember]
        public List<AttachedFile> ListAttachedFile { get; set; }

        [DataMember]
        public List<ParameterComplexType> ListParameters { get; set; }
    }
}

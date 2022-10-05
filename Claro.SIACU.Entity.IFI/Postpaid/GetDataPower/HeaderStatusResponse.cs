using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetDataPower
{
    [DataContract(Name = "Status")]
    public class HeaderStatusResponse
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }
        [DataMember(Name = "Code")]
        public string Code { get; set; }
        [DataMember(Name = "Message")]
        public string Message { get; set; }
        [DataMember(Name = "MsgId")]
        public string MsgId { get; set; }
    }
}

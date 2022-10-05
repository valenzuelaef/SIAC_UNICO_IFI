using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetDataPower
{
    [DataContract]
    public class Status
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "msgid")]
        public string Msgid { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}

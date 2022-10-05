using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetDataPower
{
    [DataContract]
    public class HeaderResponse
    {
        [DataMember(Name = "consumer")]
        public string Consumer { get; set; }
        [DataMember(Name = "pid")]
        public string Pid { get; set; }
        [DataMember(Name = "status")]
        public Status Status { get; set; }
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }
        [DataMember(Name = "varArg")]
        public string VarArg { get; set; }
    }
}

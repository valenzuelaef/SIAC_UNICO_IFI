using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetDataPower
{
    [DataContract(Name = "HeaderResponse")]
    public class HeaderResponse
    {
        [DataMember(Name = "Consumer")]
        public string Consumer { get; set; }
        [DataMember(Name = "Pid")]
        public string Pid { get; set; }
        [DataMember(Name = "TimeStamp")]
        public string TimeStamp { get; set; }
        [DataMember(Name = "VarArg")]
        public string VarArg { get; set; }
        [DataMember(Name = "Status")]
        public HeaderStatusResponse Status { get; set; }
    }
}

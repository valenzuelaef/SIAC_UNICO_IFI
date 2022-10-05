using Claro.SIACU.Entity.IFI.Common.IsOkGetKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.UnlockService
{
    [DataContract(Name = "UnlockServiceRequest")]
    public class UnlockServiceRequest : Claro.Entity.Request
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeaderRequest Header { get; set; }
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string ticklerCode { get; set; }
        [DataMember]
        public string ticklerDes { get; set; }
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public string userName { get; set; }
        [DataMember]
        public string strUser { get; set; }
        [DataMember]
        public string strPass { get; set; }
        [DataMember]
        public IsOkGetKeyRequest objIsOkGetKeyRequest { get; set; }
    }
}

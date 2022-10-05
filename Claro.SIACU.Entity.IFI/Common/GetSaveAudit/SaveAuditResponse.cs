using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetSaveAudit
{
    [DataContract(Name = "SaveAuditResponseCommon")]
    public class SaveAuditResponse
    {
        [DataMember]
        public string vResultado { get; set; }
        [DataMember]
        public string vTransaccionResp { get; set; }
        [DataMember]
        public string vestado { get; set; }
        [DataMember]
        public bool respuesta { get; set; }
    }
}

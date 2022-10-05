using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.DeleteLockLine
{
    [DataContract(Name = "DeleteLockLineResponseIFI")]
    public class DeleteLockLineResponse
    {
        [DataMember]
        public bool resul { get; set; }
        [DataMember]
        public string rFlagDelete { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
    }
}

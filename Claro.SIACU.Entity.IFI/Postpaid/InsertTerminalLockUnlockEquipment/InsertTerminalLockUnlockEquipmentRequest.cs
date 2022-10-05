using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.InsertTerminalLockUnlockEquipment
{
    [DataContract(Name = "InsertTerminalLockUnlockEquipmentRequestIFI")]
    public class InsertTerminalLockUnlockEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public Terminal objTerminal { get; set; }
        [DataMember]
        public bool isTransactionLock { get; set; }

    }
}

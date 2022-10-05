

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.InsertTerminalLockUnlockEquipment
{
    [DataContract(Name = "InsertTerminalLockUnlockEquipmentResponseIFI")]
    public class InsertTerminalLockUnlockEquipmentResponse
    {
        [DataMember]
        public int  resTerminal { get; set; }
    }
}

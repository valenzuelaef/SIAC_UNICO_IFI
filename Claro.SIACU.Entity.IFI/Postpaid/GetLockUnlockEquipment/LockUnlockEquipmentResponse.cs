using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetLockUnlockEquipment
{
    [DataContract(Name = "LockUnlockEquipmentResponseIFI")]
    public class LockUnlockEquipmentResponse
    {
        [DataMember]
        public string strMessage { get; set; }
        [DataMember]
        public bool Result { get; set; }
    }
}

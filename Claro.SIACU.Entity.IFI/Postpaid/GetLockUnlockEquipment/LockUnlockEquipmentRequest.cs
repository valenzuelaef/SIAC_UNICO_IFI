using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetLockUnlockEquipment
{
    [DataContract(Name = "LockUnlockEquipmentRequestIFI")]
    public class LockUnlockEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Imei { get; set; }
    }
}

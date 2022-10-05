using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipment
{
    [DataContract(Name = "UpdateUnlockEquipmentRequestIFI")]
    public class UpdateUnlockEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public Lock objLock { get; set; }

    }
}

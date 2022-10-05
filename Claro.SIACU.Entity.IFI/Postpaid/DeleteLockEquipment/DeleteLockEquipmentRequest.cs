using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.DeleteLockEquipment
{
    [DataContract(Name = "DeleteLockEquipmentRequestIFI")]
    public class DeleteLockEquipmentRequest : Claro.Entity.Request
    {
        [DataMember]
        public Lock objLock { get; set; }
    }
}

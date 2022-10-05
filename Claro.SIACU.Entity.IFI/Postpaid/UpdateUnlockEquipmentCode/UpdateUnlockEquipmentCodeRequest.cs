using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipmentCode
{
    [DataContract(Name = "UpdateUnlockEquipmentCodeRequestIFI")]
    public class UpdateUnlockEquipmentCodeRequest : Claro.Entity.Request
    {
        [DataMember]
        public Lock objLock { get; set; }
        [DataMember]
        public string codeUnlock { get; set; }
    }
}

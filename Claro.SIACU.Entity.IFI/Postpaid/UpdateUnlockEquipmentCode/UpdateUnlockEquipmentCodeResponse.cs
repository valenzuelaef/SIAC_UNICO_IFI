using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipmentCode
{
     [DataContract(Name = "UpdateUnlockEquipmentCodeResponseIFI")]
    public class UpdateUnlockEquipmentCodeResponse
    {
        [DataMember]
        public bool resul { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
    }
}

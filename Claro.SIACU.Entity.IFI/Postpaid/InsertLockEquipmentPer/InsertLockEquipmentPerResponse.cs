using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.InsertLockEquipmentPer
{
    [DataContract(Name = "InsertLockEquipmentPerResponseIFI")]
    public class InsertLockEquipmentPerResponse
    {
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public bool Result { get; set; }
    }
}

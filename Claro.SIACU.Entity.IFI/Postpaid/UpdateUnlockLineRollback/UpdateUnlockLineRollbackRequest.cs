using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockLineRollback
{
    [DataContract(Name = "UpdateUnlockLineRollbackRequestIFI")]
    public class UpdateUnlockLineRollbackRequest : Claro.Entity.Request
    {
        [DataMember]
        public Lock objLock { get; set; }
       
    }
}

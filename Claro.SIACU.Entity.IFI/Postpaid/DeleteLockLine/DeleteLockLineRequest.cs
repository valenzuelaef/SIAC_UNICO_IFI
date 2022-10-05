using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.DeleteLockLine
{
    [DataContract(Name = "DeleteLockLineRequestIFI")]
    public class DeleteLockLineRequest : Claro.Entity.Request
    {
        [DataMember]
        public Lock objLock { get; set; }
        
    }
}

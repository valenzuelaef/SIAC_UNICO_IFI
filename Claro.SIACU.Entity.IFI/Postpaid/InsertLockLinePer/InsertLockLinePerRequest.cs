using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.InsertLockLinePer
{
    [DataContract(Name = "InsertLockLinePerRequestIFI")]
    public class InsertLockLinePerRequest : Claro.Entity.Request
    {
        [DataMember]
        public Lock objLock { get; set; }
        [DataMember]
        public string codeLock { get; set; }
    }
}

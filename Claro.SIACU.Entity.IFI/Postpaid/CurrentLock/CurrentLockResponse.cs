using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.CurrentLock
{
    [DataContract(Name = "CurrentLockResponseIFI")]
    public class CurrentLockResponse
    {
        [DataMember]
        public List<Annotation> lstAnnotation { get; set; }

    }
}

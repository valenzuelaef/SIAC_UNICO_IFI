using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetBillingCycle
{
    [DataContract(Name = "BillingCycleResponse")]
    public class BillingCycleResponse
    {
        [DataMember]
        public List<Entity.IFI.Common.BillingCycle> LstBillingCycleResponse { get; set; }
    }
}

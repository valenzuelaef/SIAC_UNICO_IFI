using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetBillingCycle
{
    [DataContract(Name = "BillingCycleRequest")]
    public class BillingCycleRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTypeCustomer { get; set; }
    }
}

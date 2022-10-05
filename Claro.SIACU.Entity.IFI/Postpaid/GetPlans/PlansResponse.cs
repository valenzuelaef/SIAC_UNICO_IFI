using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetPlans
{
    [DataContract(Name = "PlansResponse")]
    public class PlansResponse
    {
        [DataMember]
        public List<ProductPlan> lstPlan { get; set; }
    }
}

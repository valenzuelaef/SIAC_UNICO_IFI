using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetServicesByPlan
{
    public class PlanServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdPlan { get; set; }
        [DataMember]
        public string strProductType { get; set; }
    }
}

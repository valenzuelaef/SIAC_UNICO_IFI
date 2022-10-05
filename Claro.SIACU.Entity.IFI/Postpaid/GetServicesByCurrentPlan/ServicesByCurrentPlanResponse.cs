using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetServicesByCurrentPlan
{
    public class ServicesByCurrentPlanResponse
    {
        [DataMember]
        public List<ServiceByCurrentPlan> lstServicesByCurrentPlan { get; set; }

        [DataMember]
        public string strTmCode { get; set; }
    }
}

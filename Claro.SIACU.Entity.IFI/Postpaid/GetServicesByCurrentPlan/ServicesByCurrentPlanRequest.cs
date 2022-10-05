using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetServicesByCurrentPlan
{
    public class ServicesByCurrentPlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractId { get; set; }
    }
}

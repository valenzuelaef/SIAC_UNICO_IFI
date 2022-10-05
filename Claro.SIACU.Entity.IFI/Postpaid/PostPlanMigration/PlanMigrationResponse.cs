using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationResponse")]
    public class PlanMigrationResponse
    {
        [DataMember(Name = "MessageResponse")]
        public PlanMigrationMessageResponse MessageResponse { get; set; }
    }
}

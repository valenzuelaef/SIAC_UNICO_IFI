using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationMessageResponse")]
    public class PlanMigrationMessageResponse
    {
        [DataMember(Name = "Header")]
        public PlanMigrationHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public PlanMigrationBodyResponse Body { get; set; }
    }
}

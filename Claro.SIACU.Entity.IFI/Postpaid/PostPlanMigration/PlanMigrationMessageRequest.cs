using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationMessageRequest")]
    public class PlanMigrationMessageRequest
    {
        [DataMember(Name = "Header")]
        public PlanMigrationHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public PlanMigrationBodyRequest Body { get; set; }
    }
}

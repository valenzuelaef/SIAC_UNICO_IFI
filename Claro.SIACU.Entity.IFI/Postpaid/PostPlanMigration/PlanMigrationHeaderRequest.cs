using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationHeaderRequest")]
    public class PlanMigrationHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}

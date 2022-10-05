using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    public class PlanMigrationHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public GetDataPower.HeaderResponse HeaderResponse { get; set; }

    }
}

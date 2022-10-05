using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.PostPlanMigration
{
    [DataContract(Name = "PlanMigrationRequest")] 
    public class PlanMigrationRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public PlanMigrationMessageRequest MessageRequest { get; set; }      
    }
}

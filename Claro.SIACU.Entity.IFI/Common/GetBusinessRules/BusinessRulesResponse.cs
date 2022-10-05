using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetBusinessRules
{
    public class BusinessRulesResponse
    {
        [DataMember]
        public Entity.IFI.Common.BusinessRules ObjBusinessRules { get; set; }

        [DataMember]
        public List<Entity.IFI.Common.BusinessRules> ListBusinessRules { get; set; }
    }
}

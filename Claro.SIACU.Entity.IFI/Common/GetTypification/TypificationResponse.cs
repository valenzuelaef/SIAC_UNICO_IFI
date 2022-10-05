using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetTypification
{
    [DataContract(Name = "TypificationResponse")]
    public class TypificationResponse
    {
        [DataMember]
        public Entity.IFI.Common.Typification ObjTypification { get; set; }

        [DataMember]
        public List<Entity.IFI.Common.Typification> ListTypification { get; set; }
    }
}

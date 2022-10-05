using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetEstCivType
{
    [DataContract(Name = "EstCivTypeResponseCommon")]
    public class EstCivTypeResponse
    {
        [DataMember]
        public List<ListItem> EstCivTypes { get; set; }
    }
}

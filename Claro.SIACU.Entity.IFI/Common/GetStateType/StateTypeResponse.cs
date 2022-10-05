using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetStateType
{
    [DataContract(Name = "StateTypeResponseCommon")]
    public class StateTypeResponse
    {
        [DataMember]
        public List<ListItem> StateTypes { get; set; }
    }
}

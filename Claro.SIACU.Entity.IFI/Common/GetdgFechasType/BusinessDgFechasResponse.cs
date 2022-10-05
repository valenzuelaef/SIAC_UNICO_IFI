using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetdgFechasType
{
    [DataContract(Name = "DgFechasTypeResponseCommon")]
    public class DgFechasResponse
    {
        [DataMember]
        public List<ListItem> DgFechasTypes { get; set; }
    }
}

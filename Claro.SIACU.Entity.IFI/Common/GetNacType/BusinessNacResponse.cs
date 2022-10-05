using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetNacType
{
    [DataContract(Name = "NacTypeResponseCommon")]
    public class NacTypeResponse
    {
        [DataMember]
        public List<ListItem> NacTypes { get; set; }
    }
}

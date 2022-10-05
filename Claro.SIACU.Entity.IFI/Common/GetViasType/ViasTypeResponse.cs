using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetViasType
{
    [DataContract(Name = "ViasTypeResponseCommon")]
    public class ViasTypeResponse
    {
        [DataMember]
        public List<ListItem> ViasTypes { get; set; }
    }
}

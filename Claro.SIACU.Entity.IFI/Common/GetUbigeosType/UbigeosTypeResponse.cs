using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetUbigeosType
{
    [DataContract(Name = "UbigeosTypeResponseCommon")]
    public class UbigeosTypeResponse
    {
        [DataMember]
        public List<ListItem> UbigeosTypes { get; set; }
    }
}

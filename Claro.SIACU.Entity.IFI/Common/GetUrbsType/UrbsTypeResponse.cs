using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetUrbsType
{
    [DataContract(Name = "UrbsTypeResponseCommon")]
    public class UrbsTypeResponse
    {
        [DataMember]
        public List<ListItem> UrbsTypes { get; set; }
    }
}

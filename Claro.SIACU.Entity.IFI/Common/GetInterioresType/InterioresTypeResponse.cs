using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetInterioresType
{
    [DataContract(Name = "InterioresTypeResponseCommon")]
    public class InterioresTypeResponse
    {
        [DataMember]
        public List<ListItem> InterioresTypes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetZonesType
{
    [DataContract(Name = "ZonesTypeResponseCommon")]
    public class ZonesTypeResponse
    {
        [DataMember]
        public List<ListItem> ZonesTypes { get; set; }
    }
}


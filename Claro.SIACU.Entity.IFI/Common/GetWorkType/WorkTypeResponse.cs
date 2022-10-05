using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetWorkType
{
    [DataContract(Name = "WorkTypeResponseCommon")]
    public class WorkTypeResponse
    {
        [DataMember]
        public List<ListItem> WorkTypes { get; set; }
    }
}

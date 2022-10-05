using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetDataPower
{
    [DataContract]
    public class HeadersRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public HeaderRequest HeaderRequest { get; set; }
    }
}

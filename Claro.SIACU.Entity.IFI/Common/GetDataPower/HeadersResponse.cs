using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common.GetDataPower
{
    [DataContract]
    public class HeadersResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public HeaderResponse HeaderResponse{get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.UnlockService
{
    [DataContract(Name = "UnlockServiceResponse")]
    public class UnlockServiceResponse
    {
        [DataMember(Name = "Headers")]
        public Common.GetDataPower.HeadersResponse HeadersResponse { get; set; }
        [DataMember(Name = "ResponseStatus")]
        public Common.GetDataPower.ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public bool resul { get; set; }
    }
}

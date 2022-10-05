using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetLinesTelephone
{
    [DataContract(Name = "GetLinesTelephoneIFIResponse")]
    public class GetLinesTelephoneResponse
    {
        [DataMember]
        public List<Line> lstLine { get; set; }
        [DataMember]
        public bool blResult { get; set; }

    }
}

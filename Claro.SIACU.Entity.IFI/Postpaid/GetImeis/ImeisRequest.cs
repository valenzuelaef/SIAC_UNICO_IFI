using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetImeis
{
    [DataContract(Name = "ImeisRequestIFI")]
    public class ImeisRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strLine { get; set; }
    }
}

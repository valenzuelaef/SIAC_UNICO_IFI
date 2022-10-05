using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetLinesTelephone
{
    [DataContract(Name = "GetLinesTelephoneIFIRequest")]
    public class GetLinesTelephoneRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCUSTOMER_ID { get; set; }
        [DataMember]
        public string strTelephone { get; set; }
    }
}

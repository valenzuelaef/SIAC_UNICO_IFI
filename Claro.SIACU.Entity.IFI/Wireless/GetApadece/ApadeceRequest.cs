using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Wireless.GetApadece
{
    [DataContract]
    public class ApadeceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strMsisdn { get; set; }
        [DataMember]
        public string strCoId { get; set; }
    }
}

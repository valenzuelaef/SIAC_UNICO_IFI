using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.GetPCRFConsultation
{
    [DataContract]
    public class PCRFConnectorRequest : Claro.Entity.Request 
    {
        [DataMember]
        public string strLinea { get; set; }
        [DataMember]
        public string strAccountId { get; set; }
    }
}

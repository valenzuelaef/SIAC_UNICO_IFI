using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetClientDataAdd
{
    [DataContract(Name = "ClientDataAddRequest")]
    public class ClientDataAddRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdSession { get; set; }

        [DataMember]
        public string strTransaccion { get; set; }     
        
        [DataMember]
        public string vInteraccionID { get; set; }

        [DataMember]
        public string v_ContactId { get; set; }

        [DataMember]
        public string vFlagModo { get; set; }

        [DataMember]
        public string vFLAG_CONSULTA { get; set; }

        [DataMember]
        public string vMSG_TEXT { get; set; }
    }
}

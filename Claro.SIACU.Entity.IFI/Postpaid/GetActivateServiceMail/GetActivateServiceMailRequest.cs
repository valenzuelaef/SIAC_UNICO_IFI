using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetActivateServiceMail
{
    [DataContract(Name = "GetActivateServiceMailIFIRequest")]
    public class GetActivateServiceMailRequest : Claro.Entity.Request
    {

        [DataMember]
        public string pCustomerID { get; set; }
        [DataMember]
        public string pCuenta { get; set; }
        [DataMember]
        public string pEmail { get; set; }
        [DataMember]
        public string pFlag { get; set; }
        [DataMember]
        public string pTelRef { get; set; }
        [DataMember]
        public string pNumCla { get; set; }
        [DataMember]
        public string pObjID { get; set; }
        [DataMember]
        public string pTelConfSMS { get; set; }
    }
}

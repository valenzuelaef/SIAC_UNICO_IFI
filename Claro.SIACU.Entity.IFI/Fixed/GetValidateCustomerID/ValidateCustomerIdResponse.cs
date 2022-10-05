using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetValidateCustomerID
{
    [DataContract]
    public class ValidateCustomerIdResponse
    {
        [DataMember]
        public int ContactObjID { get; set; }
        [DataMember]
        public string FlgResult { get; set; }
        [DataMember]
        public string MsError { get; set; }
        [DataMember]
        public bool resultado { get; set; }

    }
}

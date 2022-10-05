using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.GetCustomer
{
    [DataContract]
    public class GetCustomerRequest : Claro.Entity.Request
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public string vPhone { get; set; }
        [DataMember]
        public string vAccount { get; set; }
        [DataMember]
        public string vContactobjid1 { get; set; }
        [DataMember]
        public string vFlagReg { get; set; }
    }
}

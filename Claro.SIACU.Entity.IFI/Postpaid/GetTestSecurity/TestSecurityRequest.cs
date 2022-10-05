using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetTestSecurity
{
    [DataContract(Name = "TestSecurityIFIRequest")]
    public class TestSecurityRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTypeCustomer { get; set; }
        [DataMember]
        public string strGroupCustomer { get; set; }
    }
}

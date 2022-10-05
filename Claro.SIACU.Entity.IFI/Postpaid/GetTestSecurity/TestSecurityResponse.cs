using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetTestSecurity
{
    [DataContract(Name = "TestSecurityIFIResponse")]
    public class TestSecurityResponse
    {
        [DataMember]
        public List<TestSecurity> lstTestSecurity { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetCustomer
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

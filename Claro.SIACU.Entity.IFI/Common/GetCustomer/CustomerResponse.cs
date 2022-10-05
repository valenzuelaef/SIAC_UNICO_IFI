using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetCustomer
{
    [DataContract]
    public class CustomerResponse
    {
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public string vFlagConsulta { get;set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public bool Resultado { get; set; }
        
        [DataMember]
        public string contactobjid {get;set;}
        [DataMember]
        public string vFlagInsert {get; set;}
      

    }
}

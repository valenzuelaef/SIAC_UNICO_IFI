using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetCaseLa
{
    [DataContract]
    public class GetCaseLaRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdSession { get; set; }
        [DataMember]
        public string strTransaccion { get; set; }   
        [DataMember]
        public string vPhone { get; set; }
        [DataMember]
        public string vfechaini { get; set; }
        [DataMember]
        public string vfechafin { get; set; }        
        [DataMember]
        public string vFlagReg { get; set; }

    }
}

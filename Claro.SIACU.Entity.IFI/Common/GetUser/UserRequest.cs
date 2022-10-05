using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetUser
{
    [DataContract]
    public class UserRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodeUser { get; set; }
        [DataMember]
        public string CodeRol { get; set; }
        [DataMember]
        public string CodeCac { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetTypification
{
    [DataContract(Name = "TypificationRequest")]
    public class TypificationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TRANSACTION_NAME { get; set; }
    }
}

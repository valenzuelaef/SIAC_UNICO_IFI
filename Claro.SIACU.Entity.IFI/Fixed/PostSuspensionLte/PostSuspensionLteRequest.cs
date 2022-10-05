using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostSuspensionLte
{
    [DataContract]
    public class PostSuspensionLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public SuspensionLte Suspension { get; set; }
    }
}

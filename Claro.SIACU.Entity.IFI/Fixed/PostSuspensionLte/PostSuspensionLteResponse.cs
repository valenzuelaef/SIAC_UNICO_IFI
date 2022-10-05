using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostSuspensionLte
{
    [DataContract]
    public class PostSuspensionLteResponse
    {
        [DataMember]
        public bool ResponseStatus { get; set; }
    }
}

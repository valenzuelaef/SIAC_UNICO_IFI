using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostReconexionLte
{
    [DataContract]
    public class ReconexionLteResponse
    {
        [DataMember]
        public bool ResponseStatus { get; set; }
    }
}

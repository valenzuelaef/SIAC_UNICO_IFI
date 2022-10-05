using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostUpdateProgTaskLte
{
    [DataContract]
    public class UpdateProgTaskLteResponse
    {
        [DataMember]
        public bool ResultStatus { get; set; }
    }
}

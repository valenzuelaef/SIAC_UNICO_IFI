using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostExecuteSuspension
{
    [DataContract]
    public class ExecuteSuspensionRequest : Claro.Entity.Request
    {
        [DataMember]
        public Suspension Suspension { get; set; }
    }
}

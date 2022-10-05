using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetStateType
{
    [DataContract(Name = "StateTypeRequestCommon")]
    public class StateTypeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string IdList { get; set; }
    }
}

using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetCaseLa
{
    [DataContract]
    public class GetCaseLaResponse
    {

        [DataMember]
        public string vFlagConsulta { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public bool Result { get; set; }
    }
}

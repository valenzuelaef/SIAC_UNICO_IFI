using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.ETAFlowValidate
{
    [DataContract(Name = "ETAFlowRequestHfc")]
    public class ETAFlowRequest : Claro.Entity.Request
    {
        [DataMember]
        public string as_origen { get; set; }
        [DataMember]
        public string av_idplano { get; set; }
        [DataMember]
        public string av_ubigeo { get; set; }
        [DataMember]
        public int an_tiptra { get; set; }
        [DataMember]
        public string an_tipsrv { get; set; }
    }
}

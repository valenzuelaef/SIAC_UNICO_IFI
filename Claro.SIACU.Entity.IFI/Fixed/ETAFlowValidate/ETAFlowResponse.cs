using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Fixed.ETAFlowValidate
{
    [DataContract(Name = "ETAFlowResponseHfc")]
    public class ETAFlowResponse
    {
        [DataMember]
        public ETAFlow ETAFlow { get; set; }
    }
}

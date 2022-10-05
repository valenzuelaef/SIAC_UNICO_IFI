using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "PCRFPaquetesAdicResponse")]
    public class PCRFPaquetesAdicResponse
    {
        [DataMember(Name = "MessageResponse")]
        public PCRFPaquetesAdicMessageResponse MessageResponse { get; set; }
    }
}

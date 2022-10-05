using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "PCRFPaquetesAdicMessageResponse")]
    public class PCRFPaquetesAdicMessageResponse
    {
        [DataMember(Name = "Header")]
        public PCRFPaquetesAdicHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public PCRFPaquetesAdicBodyResponse Body { get; set; }
    }
}

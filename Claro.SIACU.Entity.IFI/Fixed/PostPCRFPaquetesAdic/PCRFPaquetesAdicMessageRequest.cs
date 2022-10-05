using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "PCRFPaquetesAdicMessageRequest")]
    public class PCRFPaquetesAdicMessageRequest
    {
        [DataMember(Name = "Header")]
        public PCRFPaquetesAdicHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public PCRFPaquetesAdicBodyRequest Body { get; set; }
    }          
}

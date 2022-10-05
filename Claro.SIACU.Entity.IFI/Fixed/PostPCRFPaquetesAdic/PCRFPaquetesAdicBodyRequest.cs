using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
    //INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "PCRFPaquetesAdicBodyRequest")]
    public class PCRFPaquetesAdicBodyRequest
    {
        [DataMember]
        public PCRFPaquetesAdicConsultarRequest consultarRequest { get; set; }

    }
    public class PCRFPaquetesAdicConsultarRequest
    {
        [DataMember]
        public string msisdn { get; set; }

        [DataMember]
        public string flagHistorico { get; set; }

        [DataMember]
        public string cantDias { get; set; }
        
    }

}

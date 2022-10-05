using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
//INI - RF-04 - RF-05 Evalenzs
    public class RestPCRFPaquetesAdicRequest 
    {
        public RestPCRFPaquetesAdicMessageRequest MessageRequest { get; set; }

    }

    public class RestPCRFPaquetesAdicMessageRequest
    {
        public RestPCRFPaquetesAdicHeader Header { get; set; }

        public RestPCRFPaquetesAdicBodyRequest Body { get; set; }
    }

    public class RestPCRFPaquetesAdicHeader
    {
        public RestPCRFPaquetesAdicHeaderRequest HeaderRequest { get; set; }
    }

    public class RestPCRFPaquetesAdicHeaderRequest
    {
        public string consumer { get; set; }

        public string country { get; set; }

        public string dispositivo { get; set; }

        public string language { get; set; }

        public string modulo { get; set; }

        public string msgType { get; set; }
        public string operation { get; set; }

        public string pid { get; set; }

        public string system { get; set; }

        public string timestamp { get; set; }

        public string userId { get; set; }

        public string wsIp { get; set; }

    }

    public class RestPCRFPaquetesAdicBodyRequest
    {
        public RestPCRFPaquetesAdicConsultarRequest consultarRequest { get; set; }

    }

    public class RestPCRFPaquetesAdicConsultarRequest
    {
        public string msisdn { get; set; }

        public string flagHistorico { get; set; }

        public string cantDias { get; set; }
    }

}

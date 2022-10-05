using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class responseStatus
    {
        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "descripcionRespuesta")]
        public string descripcionRespuesta { get; set; }

        [DataMember(Name = "ubicacionError")]
        public string ubicacionError { get; set; }

        [DataMember(Name = "fecha")]
        public string fecha { get; set; }

        [DataMember(Name = "origen")]
        public string origen { get; set; }

        [DataMember(Name = "detalleError")]
        public string detalleError { get; set; }

        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        public responseStatus()
        {
            status = string.Empty;
            codigoRespuesta = string.Empty;
            descripcionRespuesta = string.Empty;
            ubicacionError = string.Empty;
            fecha = string.Empty;
            origen = string.Empty;
            detalleError = string.Empty;
            idTransaccion = string.Empty;
        }
    }
}

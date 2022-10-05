using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    
    [DataContract()]
    public class ValidarCoberturaItemRequest
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }
        [DataMember(Name = "codAplicacion")]
        public string codAplicacion { get; set; }
        [DataMember(Name = "latitud")]
        public string latitud { get; set; }
        [DataMember(Name = "longitud")]
        public string longitud { get; set; }
        [DataMember(Name = "tipoTecnologia")]
        public string tipoTecnologia { get; set; }
        [DataMember(Name = "motivo")]
        public string motivo { get; set; }
        [DataMember(Name = "cliente")]
        public validarBodyRequestCliente cliente { get; set; }
        [DataMember(Name = "direccion")]
        public validarBodyRequestDireccion direccion { get; set; }
        [DataMember(Name = "solicitud")]
        public validarBodyRequestSolicitud solicitud { get; set; }

        public ValidarCoberturaItemRequest()
        {
            idTransaccion = string.Empty;
            codAplicacion = string.Empty;
            latitud = string.Empty;
            longitud = string.Empty;
            tipoTecnologia = string.Empty;
            motivo = string.Empty;
            cliente = new validarBodyRequestCliente();
            direccion = new validarBodyRequestDireccion();
            solicitud = new validarBodyRequestSolicitud();
        }
     
    }
}

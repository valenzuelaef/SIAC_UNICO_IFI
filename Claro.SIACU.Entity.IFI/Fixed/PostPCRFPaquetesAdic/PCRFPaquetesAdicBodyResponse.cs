using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic
{
    //INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "PCRFPaquetesAdicBodyResponse")]
    public class PCRFPaquetesAdicBodyResponse
    {
        [DataMember(Name = "consultarPaquetesResponseType")]
        public consultarPaquetesResponse consultarPaquetesResponseType { get; set; }

    }

    public class consultarPaquetesResponse
    {
        [DataMember(Name = "responseData")]
        public responseData responseData { get; set; }

        [DataMember(Name = "responseStatus")]
        public responseStatus responseStatus { get; set; }
    }

    public class responseData
    {
          [DataMember(Name = "listaFact")]
        public List<PCRFPaquetesAdqueridos> listaFact { get; set; }
      
        [DataMember(Name = "dataPCRF")]
          public responsedataPCRF dataPCRF { get; set; }

    }
    public class responseStatus
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }

        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }

        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; }
    }
    public class responsedataPCRF
    {
        [DataMember(Name = "datosConsumidos")]
        public string datosConsumidos { get; set; }

        [DataMember(Name = "motivoDegradacion")]
        public string motivoDegradacion { get; set; }

        [DataMember(Name = "paqueteComprado")]
        public string paqueteComprado { get; set; }

        [DataMember(Name = "velocidadDegradada")]
        public string velocidadDegradada { get; set; }

    }

    public class PCRFPaquetesAdqueridos
      {
         [DataMember(Name = "msisdn")]
        public string msisdn { get; set; }
         [DataMember(Name = "customerId")]
         public string customerId { get; set; }
         [DataMember(Name = "tipoCliente")]
         public string tipoCliente { get; set; }
         [DataMember(Name = "tipoProducto")]
         public string tipoProducto { get; set; }
         [DataMember(Name = "estado")]
         public string estado { get; set; }
         [DataMember(Name = "estadoEntrega")]
         public string estadoEntrega { get; set; }
         [DataMember(Name = "cicloFact")]
         public string cicloFact { get; set; }

         [DataMember(Name = "planBase")]
         public string planBase { get; set; }

         [DataMember(Name = "motivoDeg")]
         public string motivoDeg { get; set; }

         [DataMember(Name = "paquete")]
         public string paquete { get; set; }
         [DataMember(Name = "cargoFijo")]
         public string cargoFijo { get; set; }

         [DataMember(Name = "tipoPago")]
         public string tipoPago { get; set; }

         [DataMember(Name = "monto")]
         public string monto { get; set; }

         [DataMember(Name = "canal")]
         public string canal { get; set; }

         [DataMember(Name = "departamento")]
         public string departamento { get; set; }

         [DataMember(Name = "provincia")]
         public string provincia { get; set; }

         [DataMember(Name = "distrito")]
         public string distrito { get; set; }
         [DataMember(Name = "fechaCPR")]
         public string fechaCPR { get; set; }
         [DataMember(Name = "fechaAct")]
         public string fechaAct { get; set; }

      }
}

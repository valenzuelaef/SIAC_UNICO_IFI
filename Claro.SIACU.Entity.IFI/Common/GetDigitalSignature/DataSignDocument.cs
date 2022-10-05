using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetDigitalSignature
{
    [DataContract]
    public class DataSignDocument
    {
        [DataMember]
        public string RutaArchivo { get; set; }
        [DataMember]
        public string IdTransaccion { get; set; }
        [DataMember]
        public string FechaInicio { get; set; }
        [DataMember]
        public string FechaFin { get; set; }
        [DataMember]
        public string CodigoRespuesta { get; set; }
        [DataMember]
        public string MensajeRespuesta { get; set; }
        [DataMember]
        public string DescripcionRespuesta { get; set; }
    }
}

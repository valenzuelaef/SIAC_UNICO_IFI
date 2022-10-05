using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common.GetDigitalSignature
{
    [DataContract]
    public class DigitalSignatureRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodigoPDV { get; set; }
        [DataMember]
        public string NombrePDV { get; set; }
        //[DataMember]
        //public string TipoFirma { get; set; }
        //[DataMember]
        //public string TipoArchivo { get; set; }
        //[DataMember]
        //public string Negocio { get; set; }
        //[DataMember]
        //public string TipoContrato { get; set; }
        //[DataMember]
        //public string DatFirma { get; set; }
        //[DataMember]
        //public string OrigenArchivo { get; set; }
        //[DataMember]
        //public string CodigoAplicacion { get; set; }
        //[DataMember]
        //public string PosFirma { get; set; }
        [DataMember]
        public string NombreArchivo { get; set; }
        //[DataMember]
        //public string IpAplicacion { get; set; }
        [DataMember]
        public string NumeroArchivo { get; set; }
        //[DataMember]
        //public string SegmentoOferta { get; set; }
        //[DataMember]
        //public string PlantillaBRMS { get; set; }
        //[DataMember]
        //public string TipoOperacion { get; set; }
        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string ContenidoArchivo { get; set; }
        //[DataMember]
        //public string RutaArchivoDestino { get; set; }
        //[DataMember]
        //public string RutaArchivoOrigen { get; set; }
        [DataMember]
        public string CanalAtencion { get; set; }
    }
}

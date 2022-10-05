using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.IFI.Common.GetDigitalSignature
{
    [DataContract]
    public class ResponseStatus
    {
        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public string CodigoRespuesta { get; set; }
        [DataMember]
        public string DescripcionRespuesta { get; set; }
        [DataMember]
        public string UbicacionError { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string Origen { get; set; }
    }
}

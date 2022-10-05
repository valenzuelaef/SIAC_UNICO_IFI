using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract(Name = "solicitud")]
    public class validarBodyRequestSolicitud
    {
        [DataMember(Name = "tipoProducto")]
        public string tipoProducto { get; set; }
        [DataMember(Name = "modalidad")]
        public string modalidad { get; set; }
        [DataMember(Name = "venta")]
        public string venta { get; set; }
        [DataMember(Name = "monto")]
        public string monto { get; set; }
        [DataMember(Name = "equipo")]
        public string equipo { get; set; }
        [DataMember(Name = "plan")]
        public string plan { get; set; }
    }
}

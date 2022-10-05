using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract(Name = "direccion")]
    public class validarBodyRequestDireccion
    {
        [DataMember(Name = "departamento")]
        public string departamento { get; set; }
        [DataMember(Name = "provincia")]
        public string provincia { get; set; }
        [DataMember(Name = "distrito")]
        public string distrito { get; set; }
        [DataMember(Name = "direccion")]
        public string direccion { get; set; }
    }
}

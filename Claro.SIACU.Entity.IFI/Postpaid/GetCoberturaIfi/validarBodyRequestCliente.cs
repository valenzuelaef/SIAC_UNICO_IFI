using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract(Name = "cliente")]
    public class validarBodyRequestCliente
    {
        [DataMember(Name = "tipoDoc")]
        public string tipoDoc { get; set; }
        [DataMember(Name = "numeroDoc")]
        public string numeroDoc { get; set; }
        [DataMember(Name = "nombres")]
        public string nombres { get; set; }
        [DataMember(Name = "apellidos")]
        public string apellidos { get; set; }
        [DataMember(Name = "correo")]
        public string correo { get; set; }
        [DataMember(Name = "telefonoContacto")]
        public string telefonoContacto { get; set; }
    }
}

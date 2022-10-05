using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class ListaTecnologiasDisponibles
    {
        [DataMember(Name = "tipo")]
        public string tipo { get; set; }
        [DataMember(Name = "codPlano")]
        public string codPlano { get; set; }

        public ListaTecnologiasDisponibles()
        {
            tipo = string.Empty;
            codPlano = string.Empty;
        }
    }
}

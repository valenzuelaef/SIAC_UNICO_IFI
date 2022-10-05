using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class obtenerTipoTecnologiaResponse
    {
        [DataMember(Name = "olisObtenerTipoTecnologia")]
        public List<ObtenerTipoTecnologia> olisObtenerTipoTecnologia { get; set; }
    }
}

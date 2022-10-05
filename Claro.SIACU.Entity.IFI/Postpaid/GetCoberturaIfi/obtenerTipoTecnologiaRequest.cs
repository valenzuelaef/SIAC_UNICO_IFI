using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    [DataContract]
    public class obtenerTipoTecnologiaRequest : Claro.Entity.Request
    {
        [DataMember(Name = "strParamGrupo")]
        public string strParamGrupo { get; set; }

        [DataMember(Name = "strPlanActual")]
        public string strPlanActual { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi
{
    public class ObtenerTipoTecnologia
    {
        public Int64 PARAN_GRUPO { get; set; }
        public Int64 PARAN_CODIGO { get; set; }
        public string PARAV_DESCRIPCION { get; set; }
        public string PARAV_VALOR { get; set; }
        public string PARAV_VALOR1 { get; set; }
        public string PARAN_FLAG_SISTEMA { get; set; }
    }
}
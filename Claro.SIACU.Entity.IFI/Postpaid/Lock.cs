using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    public class Lock
    {
        public Int64 _BLO_ID_MOV { get; set; }
        public string _BLO_CUENTA { get; set; }
        public string _BLO_TELEFONO { get; set; }
        public string _BLO_COD_APLICA { get; set; }
        public string _BLO_USUARIO { get; set; }
        public string _BLO_AUTORIZA { get; set; }
        public DateTime _BLO_FECHA_BLOQ { get; set; }
        public string _BLO_ESTADO { get; set; }
        public string _BLO_CODIGO { get; set; }
        public DateTime _BLO_FECHA_DESBLOQ { get; set; }
        public string _BLO_USUARIO_DESBLOQ { get; set; }
        public string _BLO_TIPO { get; set; }
        public string _BLO_IMEI { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeServiceAddress
{
    public class responseStatus
    {
        public int estado { get; set; }
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public string ubicacionError { get; set; }
        public string fecha { get; set; }
        public string origen { get; set; }
        public string idTransaccion { get; set; }
    }
}

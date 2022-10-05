using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Postpaid
{
    public class Annotation
    {
        public  string _Codigo_Cliente {get; set;}
        public string _Codigo { get; set; }
        public string _Estado { get; set; }
        public string _Prioridad { get; set; }
        public string _Descripcion { get; set; }
        public string _Usuario_Seguimiento { get; set; }
        public DateTime _Fecha_Seguimiento { get; set; }
        public string _sFecha { get; set; }
        public DateTime _Fecha { get; set; }
        public string _Accion_Seguimiento { get; set; }
        public DateTime _Fecha_Apertura { get; set; }
        public DateTime _Fecha_Cierre { get; set; }
        public string _desc_tipo { get; set; }
        public string _tipo { get; set; }
        public string _Nro_Tickler { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class SolicitudModel
    {
        public string tipoProducto { get; set; }
        public string modalidad { get; set; }
        public string venta { get; set; }
        public string monto { get; set; }
        public string equipo { get; set; }
        public string plan { get; set; }
        
    }
}
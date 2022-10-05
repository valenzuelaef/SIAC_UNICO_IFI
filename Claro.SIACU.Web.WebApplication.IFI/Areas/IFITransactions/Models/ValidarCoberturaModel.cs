using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ValidarCoberturaModel
    {
        public string idTransaccion { get; set; }
        public string codAplicacion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string tipoTecnologia { get; set; }
        public string motivo { get; set; }
    }
}
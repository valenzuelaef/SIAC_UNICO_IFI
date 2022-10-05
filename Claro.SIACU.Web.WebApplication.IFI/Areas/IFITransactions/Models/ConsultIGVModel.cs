using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ConsultIGVModel
    {
        public double igv { get; set; }
        public double igvD { get; set; }
        public string impudFecFinVigencia { get; set; }
        public string impudFecIniVigencia { get; set; }
        public string impudFecRegistro { get; set; }
        public string impunTipDoc { get; set; }
        public string imputId { get; set; }
        public string impuvDes { get; set; }
    }
}
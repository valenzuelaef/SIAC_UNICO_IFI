using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class WebResponse
    {
        public bool Error { get; set; }
        public bool exiteCobertura { get; set; }
        public string Mensaje { get; set; }
    }
}
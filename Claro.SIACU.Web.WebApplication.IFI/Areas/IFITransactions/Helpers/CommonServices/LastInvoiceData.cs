using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices
{
    public class LastInvoiceData
    {
        public string FECHA_VENCIMIENTO { get; set; }
        public string MTO_ULT_FACTURA { get; set; }
        public string PERIODO { get; set; }
        public string INVOICENUMBER { get; set; }
    }
}
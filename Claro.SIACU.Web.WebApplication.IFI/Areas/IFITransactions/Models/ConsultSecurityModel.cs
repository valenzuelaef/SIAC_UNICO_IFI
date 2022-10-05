using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ConsultSecurityModel
    {
        public string ErrMessage { get; set; }
        public string CodeErr { get; set; }
        public List<SecurityModel> ListConsultSecurity { get; set; }
    }
}
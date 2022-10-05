using System;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices
{
    [Serializable]
    public class TimeZoneVM
    {
        public string vUbigeo { get; set; }
        public string vJobTypes { get; set; }
        public string vCommitmentDate { get; set; }
        public string vSubJobTypes { get; set; }
        public string vValidateETA { get; set; }
        public string vHistoryETA { get; set; }

        public string vTimeZone { get; set; }
        public string vMotiveSot { get; set; }
        public string vIdTipoServicio { get; set; }
        public string vCantidad { get; set; }
    }
}
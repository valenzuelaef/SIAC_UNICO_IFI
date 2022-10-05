using System.Collections.Generic;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class CommonServices
    {
        public List<HELPER_ITEM.CacDacTypeVM> CacDacTypes { get; set; }
        public List<HELPER_ITEM.EstCivTypeVM> EstCivTypes { get; set; }
        public List<HELPER_ITEM.NacTypeVM> NacTypes { get; set; }
        public List<HELPER_ITEM.ViaType> ViaTypes { get; set; }
        public List<HELPER_ITEM.ManzanaType> ManzanaTypes { get; set; }
        public List<HELPER_ITEM.InteriorType> InteriorTypes { get; set; }
        public List<HELPER_ITEM.UrbType> UrbTypes { get; set; }
        public List<HELPER_ITEM.ZoneType> ZoneTypes { get; set; }
        public List<HELPER_ITEM.UbigeoType> UbigeoTypes { get; set; }
        public List<HELPER_ITEM.LastInvoiceData> LastInvoiceDatas { get; set; }
        public List<HELPER_ITEM.DgFechasTypeVM> DgFechasTypes { get; set; }
    }
}
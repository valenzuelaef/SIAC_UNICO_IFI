using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using System;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class SuspensionReconnectionModel
    {
        public string firmaDigital { get; set; }
        public string dateSuspension { get; set; }
        public string dateReactivation { get; set; }
        public string msisdn { get; set; }
        public string contractId { get; set; }
        public string currentUser { get; set; }
        public bool chkRetencion { get; set; }
        public string customerId { get; set; }
        public string codPlanoIns { get; set; }
        public string fullNameUser { get; set; }

        public string impPagar { get; set; }
        public string montoRet { get; set; }

        public bool montoRetEnabled { get; set; }
        public bool impPagarEnabled { get; set; }
        public bool chkRetencionEnabled { get; set; }
        public bool TotalimpPagarEnabled { get; set; }
        public bool dateSuspensionEnabled { get; set; }
        public bool dateReactivationEnabled { get; set; }


        #region tipificacion
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string notes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        #endregion

        public DateTime dFechaSuspension { get; set; }
        public DateTime dFechaReactivacion { get; set; }
        public string strCodigoAplicativo { get; set; }
        public string strPasswordAplicativo { get; set; }
        public string idSession { get; set; }
        public string idTransaccion { get; set; }
        public string account { get; set; }

        #region Datos del Cliente
        public string name { get; set; }
        public string lastName { get; set; }
        public string businessName { get; set; }
        public string documentType { get; set; }
        public string documentNumber { get; set; }
        public string address { get; set; }
        public string district { get; set; }
        public string departament { get; set; }
        public string province { get; set; }
        public string codPlano { get; set; }
        public string fullName { get; set; }
        public string legalAgent { get; set; }
        public string tipoCliente { get; set; }
        public string cboCACDAC { get; set; }
        public string email { get; set; }
        public bool chkEmail { get; set; }

        public string diasMaxConRetencion { get; set; }
        public string diasMaxSinRetencion { get; set; }
        public string diasMinSinRetencion { get; set; }

        public string cicloFacturacion { get; set; }

        public string strProgramDate { get; set; }
        public string strAction { get; set; }
        #endregion

        public ScheduledTransaction objScheduledTransactionModel { get; set; }
       
    }
}
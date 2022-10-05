using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using System;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class DuplicateReceiptsModel
    {
        public string Destinatarios { get; set; }
        public string arrPaths { get; set; }
        public string arrDates { get; set; }
        public bool ischkEmail { get; set; }
        public string strIdSession { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool blnInteract { get; set; }
        public string strFullPathPDF { get; set; }
        public FixedIFIService.JobType[] JobTypes { get; set; }
        public string strCustomerId { get; set; }
        public string JobTypeComplementarySalesHFC { get; set; }

        public string MessageSelectJobTypes { get; set; }
        public string MessageSelectDate { get; set; }
        public string MessageErrorUbigeo { get; set; }
        public string MessageValidate { get; set; }
        public string MessageNoValidate { get; set; }
        public string MessageNsTimeZone { get; set; }
        public string DNI_RUC { get; set; }
        public string strTelReferencia { get; set; }
        public string strRepresentante { get; set; }
        public string strDocRepresentante { get; set; }
        public string strCiudad { get; set; }
        public string strEmail { get; set; }
        public bool strchkEmail { get; set; }
        public string strEmailMedio { get; set; }
        public string strMeses { get; set; }
        public string strFechActivacion { get; set; }
        //

        public string strObjidContacto { get; set; }
        public string strCacDac { get; set; }
        public string strCacDacId { get; set; }
        public string Customer { get; set; }
        public string Accion { get; set; }
        public string strCalle { get; set; }
        public string strReferencia { get; set; }
        public string strDepartamento { get; set; }
        public string strProvincia { get; set; }
        public string strDistrito { get; set; }
        public string strFechaDistribucion { get; set; }
        public string strFechaEmision { get; set; }
        public string CodeTipification { get; set; }
        public string OBJID_SITE { get; set; }
        public string hidSupJef { get; set; }
        public string DesAccion { get; set; }
        public string strCodigoAplicativo { get; set; }
        public string strPasswordAplicativo { get; set; }
        public string strTransaccion { get; set; }
        public string account { get; set; }
        public string strNote { get; set; }
        public string strCheckfecha { get; set; }
        public string strchkReciboFedateado { get; set; }     
        public string strTelefono { get; set; }
        public string strMailChange { get; set; }
        public string strApellidos { get; set; }
        public string strNombres { get; set; }
        public string strNombresCompleto { get; set; }
        public string strContactoCliente { get; set; }
        public string CurrentUser { get; set; }
        public string ContractId { get; set; }
        public string Plan { get; set; }
        public string strMedio { get; set; }
        public string strMedioDes { get; set; } 
        public string strTipoocumento { get; set; }
        public string AdressDespatch { get; set; }
        public string fullNameUser { get; set; }
        public string strPais { get; set; }
        public string cola { get; set; }
        #region tipificacion
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string notes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        #endregion
    }
}
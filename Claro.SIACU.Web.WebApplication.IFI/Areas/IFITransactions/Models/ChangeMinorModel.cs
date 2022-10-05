using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using System;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ChangeMinorModel
    {
        public string strIdSession { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool blnInteract { get; set; }
        public string strFullPathPDF { get; set; }
        public FixedIFIService.JobType[] JobTypes { get; set; }
        public string strCustomerId { get; set; }
        public string JobTypeComplementarySalesHFC { get; set; }
        public bool chkEmail { get; set; }
        public string MessageSelectJobTypes { get; set; }
        public string MessageSelectDate { get; set; }
        public string MessageErrorUbigeo { get; set; }
        public string MessageValidate { get; set; }
        public string MessageNoValidate { get; set; }
        public string MessageNsTimeZone { get; set; }
        public string DNI_RUC { get; set; }
        public string strTelReferencia { get; set; }
        public bool Flag_Email { get; set; }
        public string fullNameUser { get; set; } 
        public string strObjidContacto { get; set; } 
        public string strCacDac { get; set; }
        public string strCacDacId { get; set; }
        public string Customer { get; set; }
        public string Accion { get; set; }
        public string strCargo { get; set; }
        public string strDNI { get; set; }
        public string strPhone { get; set; }
        public string strMovil { get; set; }
        public string strFax { get; set; }
        public string strMail { get; set; }
        public string strNombreComercial { get; set; }
        public string strContactoCliente { get; set; }
        public DateTime dateFechaNacimiento { get; set; }
        public string strNacionalidad { get; set; }
        public string strNacionalidadId { get; set; }
        public string strSexo { get; set; }
        public string strEstadoCivil { get; set; }
        public string strEstadoCivilId { get; set; }
        public string hidSupJef { get; set; }
        public string DesAccion { get; set; }
        public string strNameComplet { get; set; }
        public string strCodigoAplicativo { get; set; }
        public string strPasswordAplicativo { get; set; }
        public string strTipoocumento { get; set; }
        public string strTransaccion { get; set; }
        public string account { get; set; }
        public string RepresentLegal { get; set; }
        public string strNote { get; set; }
        public string AdressDespatch { get; set; }
        public string strTelefono { get; set; }
        public string strMailChange { get; set; }
        public string strApellidos { get; set; }
        public string strNombres { get; set; }
        public string CurrentUser { get; set; }
        public string StrEmailEnviar { get; set; }

        #region tipificacion
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string notes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        #endregion
        // VARIABLES PARA DATOS ADICIONALES
        public string StrADDRESS { get; set; }
        public string StrCOUNTRY_DES { get; set; }
        public string StrCOUNTRY_ID { get; set; }
        public string StrDEPARTAMENTO { get; set; }
        public string StrDEPARTAMENTO_ID { get; set; }
        public string StrDISTRITO { get; set; }
        public string StrDISTRITO_ID { get; set; }
        public string StrEMAIL1 { get; set; }
        public string StrEMAIL2 { get; set; }
        public string StrINTERACT2CONTACT { get; set; }
        public string StrOBJID_INTERACT { get; set; }
        public string StrPHONE1 { get; set; }
        public string StrPHONE2 { get; set; }
        public string StrPROVINCIA { get; set; }  
        public string StrPROVINCIA_ID { get; set; }  

    }
}
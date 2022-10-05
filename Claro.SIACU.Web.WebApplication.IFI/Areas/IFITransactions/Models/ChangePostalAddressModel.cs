using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using System;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models
{
    public class ChangePostalAddressModel
    {
        public string strIdSession { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool blnInteract { get; set; }
        public string strFullPathPDF { get; set; }
        public FixedIFIService.JobType[] JobTypes { get; set; }
        public string strCustomerId { get; set; }
        public string strDireccion { get; set; }
        public string strReferencia { get; set; }
        public string strPais { get; set; }
        public string strDepartamento { get; set; }
        public string strProvincia { get; set; }
        public string strDistrito { get; set; }
        public string strCodPostal { get; set; }
        public string strFecVto { get; set; }
        public double strMontoUltFac { get; set; }
        public string JobTypeComplementarySalesHFC { get; set; }
        public string MessageSelectJobTypes { get; set; }
        public string MessageSelectDate { get; set; }
        public string MessageErrorUbigeo { get; set; }
        public string MessageValidate { get; set; }
        public string MessageNoValidate { get; set; }
        public string MessageNsTimeZone { get; set; }
        public string DNI_RUC { get; set; }
        public string strTelReferencia { get; set; }

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

        public string strCodigoAplicativo { get; set; }
        public string strPasswordAplicativo { get; set; }

        public string strTransaccion { get; set; }
        public string account { get; set; }
        public string strNote { get; set; }
        public string strTelefono { get; set; }
        public string strMailChange { get; set; }
        public string strApellidos { get; set; }
        public string strNombres { get; set; }

        public string CurrentUser { get; set; }

        #region tipificacion
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string notes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        #endregion
        public bool Flag_Email { get; set; }
        //test constancia
        public string fullNameUser { get; set; }
        public string strNameComplet { get; set; }
        public string RepresentLegal { get; set; }
        public string strTipoDocumento { get; set; }
        public bool chkEmail { get; set; }
    }
}
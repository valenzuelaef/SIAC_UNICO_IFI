using System;
using System.Linq;
using KEY = Claro.ConfigurationManager;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AuditRequest;
using Newtonsoft.Json;
using HELPERS = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers;
using Claro.SIACU.Web.WebApplication.IFI.Controllers;
using System.Web.Mvc;
using System.Collections;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using System.Collections.Generic;
using Claro.Web;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Common = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class ChangePostalAddressController : CommonServicesController
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient oServicePostpaid = new PostIFIService.PostIFIServiceClient();
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        #region Variables para obtener valores y mensajes de archivos (.config)
        public readonly string vArchivoHFCPOSTConfig = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig");
        public readonly string vArchivoHFCPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
        public readonly string vArchivoINTPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
        #endregion


        ///<summary>Metodo que retorna la vista del controlador</summary>
        ///<returns>View<returns>
        ///<remarks>Index</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public ActionResult Index()
        {
            return View("~/Areas/IFITransactions/Views/ChangePostalAddress/ChangePostalAddress.cshtml");
        }

        ///<summary>Metodo que se ejecuta al cargar la vista</summary>
        ///<param name="strIdSession">/param>
        ///<returns>JsonResult<returns>
        ///<remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        public JsonResult PageLoad(string strIdSession)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            Common.Typification typificationLoad = null;
            string lblMensaje = "";
            try
            {
                typificationLoad = LoadTypificationChangePostalAddress(audit, ref lblMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return Json(new { data = typificationLoad });

        }

        ///<summary>Metodo necesario para cargar la tipificacion de cambiar direccion postal</summary>
        ///<param name="audit">/param>
        ///<param name="lblMensaje">/param>
        ///<returns>Common.Typification<returns>
        ///<remarks>LoadTypificationChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>23/10/2018.</FecAct></item></list>
        public Common.Typification LoadTypificationChangePostalAddress(CommonIFIService.AuditRequest audit,
                                                    ref string lblMensaje)
        {
            Common.Typification oTypification = null;





            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("TransaccionChangePostalAddress");
                objTypificationRequest.audit = audit;

                Common.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<Common.TypificationResponse>(
                    () => { return oCommonService.GetTypification(objTypificationRequest); });




                oTypification = objResponse.ListTypification.First();


                if (oTypification == null)
                {

                    lblMensaje = "No se cargo las tipificaciones";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = "No se cargo las tipificaciones";
            }

            return oTypification;
        }


        #region Guardar Transacción

        ///<summary>Realiza la actualizacion de direccion de facturación</summary>
        ///<param name="oModel">/param>
        ///<returns>JsonResult<returns>
        ///<remarks>SaveTransactionChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/10/2018.</FecCrea></item></list>
        public JsonResult SaveTransactionChangePostalAddress(Model.ChangePostalAddressModel oModel)
        {
            string vDesInteraction = string.Empty;
            string strRutaArchivo = string.Empty;
            string vInteractionId = string.Empty;
            string strNombreArchivo = string.Empty;
            string strInteraccionId = string.Empty;
            string strPathFileHPxtream = string.Empty;
            string RutaArchivo = string.Empty;

            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            bool blnRetorno = false;
            bool blnRetorno2 = false;
            oModel.blnInteract = false;

            try
            {
                blnRetorno = SaveChangePostalAddress(oModel);
                if (blnRetorno)
                {
                    blnRetorno2 = SaveInteraccionChangePostalAddress(oModel, ref strInteraccionId, ref strPathFileHPxtream);

                    if (!blnRetorno2)
                    {
                        vDesInteraction = "Error en la transacción : Ocurrió un error al actualizar los datos.";

                    }
                    else {
                        vDesInteraction = "La transacción se realizó con éxito.";
                        try
                        {

                            if (oModel.chkEmail)
                            {
                                if (GetSendEmailChangePostalAddress(oModel, strInteraccionId, strPathFileHPxtream) == Claro.SIACU.Constants.OK)
                                {
                                    vDesInteraction = vDesInteraction + " Se enviará un correo de notificación.";
                                }
                                else
                                {
                                    vDesInteraction = vDesInteraction + "Pero no se pudo procesar el envío del correo de notificación.";
                                }
                            }
                            else
                            {

                                vDesInteraction = vDesInteraction + "No se envió un correo de notificación.";
                            }
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Info(oModel.strIdSession, "GetConstancyPDF-GetSendEmail", "ERROR: " + ex.Message);
                    }
                }

                    strRutaArchivo = strPathFileHPxtream;

                }
                else
                {
                    vDesInteraction = "Error en la transacción : Ocurrió un error al actualizar los datos.";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }

            return Json(new { data = oModel, vDesInteraction, strRutaArchivo });
        }


        /// <summary>Método que permite Actualizar la dirección de facturación de un cliente</summary>
        /// <param name="oModel">      
        /// <returns>bool</returns>
        /// <remarks>SaveChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/10/2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>12/11/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Solicitud del cliente</Mot></item></list>
        public bool SaveChangePostalAddress(Model.ChangePostalAddressModel oModel)
        {
            bool FlatResultado = false;
            SaveChangePostalAddressResponse objRes = new SaveChangePostalAddressResponse();
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            try
            {
                SaveChangePostalAddressRequest objRequest = new SaveChangePostalAddressRequest()
                {
                    audit = audit,
                    strSession = oModel.strIdSession,
                    strTransaction = audit.transaction,
                    objCliente = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client()
                    {
                        CUSTOMER_ID = oModel.strCustomerId,
                        DOMICILIO = oModel.strDireccion,
                        REFERENCIA = oModel.strReferencia,
                        DISTRITO = oModel.strDistrito,
                        PROVINCIA = oModel.strProvincia,
                        ZIPCODE= oModel.strCodPostal,
                        DEPARTAMENTO= oModel.strDepartamento,
                        PAIS_FAC= oModel.strPais

                    },
                };

                SaveChangePostalAddressResponse objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetSaveChangePostalAddress(objRequest);
                });

                FlatResultado = objResponse.StrResult;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return FlatResultado;
        }

        #endregion

        #region Interacción

        ///<summary>Registra la interacción al cambiar la direccion de facturacion</summary>
        ///<param name="oModel"></param>
        ///<returns>List<returns>
        ///<remarks>SaveInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/10/2018.</FecCrea></item></list>
        public List<string> SaveInteraction(Model.ChangePostalAddressModel oModel)
        {
            var strUserSession = string.Empty;
            var strUserSystem = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = oModel.strPhone;
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion = new Model.InteractionModel();


            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            try
            {


                oInteraccion = DatosInteraccion(oModel);
                oPlantillaDat = GetDataTemplateInteraction(oModel);


                var resultInteraction = InsertInteraction(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.strIdSession, oModel.strCustomerId);

                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != Claro.SIACU.Constants.OK && lstaDatTemplate[3] == string.Empty)
                {

                    Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                    throw new Exception(Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));

                }

            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);

            }


            return lstaDatTemplate;
        }


        /// <summary>Método que obtiene los datos de la interacción </summary>
        /// <param name="oModel"></param>
        /// <returns>Model.InteractionModel</returns>
        /// <remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/10/2018</FecCrea></item></list>
        public Model.InteractionModel DatosInteraccion(Model.ChangePostalAddressModel oModel)
        {

            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest();


            try
            {

                oInteraccion.Telephone = oModel.strTelefono;
                oInteraccion.ObjidContacto = oModel.strObjidContacto;
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);          
                oInteraccion.Type = oModel.tipo;
                oInteraccion.Class = oModel.claseDes;
                oInteraccion.SubClass = oModel.subClaseDes;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.Constants.NumberZeroString;
                oInteraccion.Note = oModel.strNote;
                oInteraccion.FlagCase = "0";
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;

            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }



            return oInteraccion;

        }


        /// <summary>Método que obtiene los datos de la plantilla interacción</summary>
        /// <param name="oModel"></param>
        /// <returns>Model.TemplateInteractionModel</returns>
        /// <remarks>GetDataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>23/01/2019.</FecAct></item>
        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.ChangePostalAddressModel oModel)
        {
            var oPlantillaCampoData = new Model.TemplateInteractionModel();

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            try
            {


                oPlantillaCampoData.NOMBRE_TRANSACCION = KEY.AppSettings("TransaccionChangePostalAddress");
                oPlantillaCampoData.X_CLARO_NUMBER = oModel.strTelefono;
                oPlantillaCampoData.X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                oPlantillaCampoData.X_FIRST_NAME = oModel.strNombres;
                oPlantillaCampoData.X_LAST_NAME = oModel.strApellidos;
                oPlantillaCampoData.X_ADDRESS = oModel.strDireccion;
                oPlantillaCampoData.X_EMAIL = oModel.strMailChange;
                oPlantillaCampoData.X_REFERENCE_ADDRESS = oModel.strReferencia;
                oPlantillaCampoData.X_OTHER_FIRST_NAME = oModel.strContactoCliente;
                oPlantillaCampoData.X_DISTRICT = oModel.strDistrito;
                oPlantillaCampoData.X_CITY = oModel.strProvincia;
                oPlantillaCampoData.X_DEPARTMENT = oModel.strDepartamento;
                oPlantillaCampoData.X_INTER_1 = oModel.strNacionalidad;
                oPlantillaCampoData.X_ZIPCODE = oModel.strCodPostal;
                if (oModel.strFecVto == "")
                {
                    oPlantillaCampoData.X_ICCID ="No Indicado";
                }
                else
                {
                    oPlantillaCampoData.X_ICCID =oModel.strFecVto;
                }

                if (Convert.ToString(oModel.strMontoUltFac) == "")
                {
                    oPlantillaCampoData.X_CHARGE_AMOUNT = 0.0;
                }
                else
                {
                    oPlantillaCampoData.X_CHARGE_AMOUNT = oModel.strMontoUltFac;
                }

                oPlantillaCampoData.X_INTER_15 = oModel.strCacDac;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, ex.Message);
            }


            return oPlantillaCampoData;

        }
        #endregion

        #region PDF-EMAIL


        ///<summary>Método que registra la interacción al cambiar la direccion de facturacion</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strPathFileHPxtream"></param>
        ///<returns>bool<returns>
        ///<remarks>SaveInteraccionChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/10/2018.</FecCrea></item></list>
        public bool SaveInteraccionChangePostalAddress(Model.ChangePostalAddressModel objModel, ref string strInteraccionId, ref string strPathFileHPxtream)
        {
            string strUSUARIO_SISTEMA = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
            string strUSUARIO_APLICACION = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strPASSWORD_USUARIO = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            var oInteraction = DataInteractionChangePostalAddress(objModel);

            if (oInteraction == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(objModel.claseDes))
            {
                return false;
            }

            var oTemplateInteraction = DataTemplateInteractionChangePostalAddress(objModel);

            var resultInteraction = InsertInteracChangePostalAddress(
                    oInteraction,
                    oTemplateInteraction,
                    objModel.strTelefono,
                    strUSUARIO_SISTEMA,
                    strUSUARIO_APLICACION,
                    strPASSWORD_USUARIO,
                    true,
                    objModel.strIdSession,
                    objModel.strCustomerId);

            strInteraccionId = resultInteraction.rInteraccionId.ToString();
            var strFlagInsercion = resultInteraction.rFlagInsercion.ToString();
            var strFlagInsercionInteraccion = resultInteraction.rFlagInsercionInteraccion.ToString();

            if (strFlagInsercion.Trim().ToUpper() != Claro.SIACU.Constants.OK.Trim().ToUpper() && strFlagInsercion != string.Empty)
            {
                return false;
            }

            if (strFlagInsercionInteraccion.Trim().ToUpper() != Claro.SIACU.Constants.OK.Trim().ToUpper() && strFlagInsercionInteraccion != string.Empty)
            {
                return false;
            }


            if (strFlagInsercion.Trim().ToUpper() == Claro.SIACU.Constants.OK.Trim().ToUpper() && strFlagInsercionInteraccion.Trim().ToUpper() == Claro.SIACU.Constants.OK.Trim().ToUpper())
            {
                strPathFileHPxtream = GetConstancyPDFChangePostalAddress(objModel.strIdSession, objModel, strInteraccionId);
            }

            return true;
        }


        /// <summary>Método que permite generar la constancia de Cambios de dirección de facturación mediante un archivo Pdf</summary>
        /// <param name="strInteraction"></param>  
        /// <param name="objModel"></param>  
        /// <returns>string</returns>
        /// <remarks>GetConstancyPDFChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/11/2018</FecCrea></item></list>
        ///<item><FecActu>03/12/2018</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>cambio de plantilla de prueba a original</Mot></item></list>
        public string GetConstancyPDFChangePostalAddress(string strIdSession, Model.ChangePostalAddressModel objModel, string strInteraction)
        {
            string NAME_PDF = string.Empty;


            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            try
            {
                if (objModel.chkEmail)
                {
                CommonIFIService.ParametersGeneratePDF oParameter = new CommonIFIService.ParametersGeneratePDF()
                {
                        StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoCambioDirecFactu"),
                        StrCentroAtencionArea = objModel.strCacDac,
                        StrTitularCliente = objModel.strNameComplet,
                        StrRepresLegal = objModel.RepresentLegal,
                        StrTipoDocIdentidad = objModel.strTipoDocumento,
                        strFechaHoraAtención = (DateTime.Now).ToString("dd/MM/yyyy"),
                        StrCasoInter = strInteraction,
                    StrNroServicio = objModel.strTelefono,
                    strNroDoc = objModel.DNI_RUC,
                        StrDireccion = objModel.strDireccion,
                        StrNotasDireccion = objModel.strReferencia,
                        StrTelfReferencia = objModel.strCodPostal,
                        strEnvioCorreo = Claro.SIACU.Constants.Yes,
                        StrPais = objModel.strPais,
                        StrDepartamento = objModel.strDepartamento,
                        StrProvincia = objModel.strProvincia,
                        StrDistrito = objModel.strDistrito,
                        strCorreoCliente = objModel.strMailChange,
                        StrCodUsuario = objModel.CurrentUser,
                        StrNombreAgenteUsuario = objModel.fullNameUser,
                        strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital"),
                        StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionCambioDirecFactu")
                    };
                    CommonIFIService.GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(objAuditRequest.Session, oParameter);
                    NAME_PDF = response.FullPathPDF;
                }
                else
                {
                    CommonIFIService.ParametersGeneratePDF oParameter = new CommonIFIService.ParametersGeneratePDF()
                    {
                        StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoCambioDirecFactu"),
                        StrCentroAtencionArea = objModel.strCacDac,
                    StrTitularCliente = objModel.strNameComplet,
                    StrRepresLegal = objModel.RepresentLegal,
                    StrTipoDocIdentidad = objModel.strTipoDocumento,
                        strFechaHoraAtención = (DateTime.Now).ToString("dd/MM/yyyy"),
                    StrCasoInter = strInteraction,
                        StrNroServicio = objModel.strTelefono,
                        strNroDoc = objModel.DNI_RUC,
                        StrDireccion = objModel.strDireccion,
                        StrNotasDireccion = objModel.strReferencia,
                        StrTelfReferencia = objModel.strCodPostal,
                        strEnvioCorreo = Claro.SIACU.Constants.Not,
                        StrPais = objModel.strPais,
                        StrDepartamento = objModel.strDepartamento,
                        StrProvincia = objModel.strProvincia,
                        StrDistrito = objModel.strDistrito,
                        StrCodUsuario = objModel.CurrentUser,
                        StrNombreAgenteUsuario = objModel.fullNameUser,
                        strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital"),
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionCambioDirecFactu")
                };
                CommonIFIService.GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(objAuditRequest.Session, oParameter);
                NAME_PDF = response.FullPathPDF;
                }



            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }


        /// <summary>Método que obtiene los datos de la interacción </summary>
        ///<param name="objModel"></param>
        /// <returns>CommonIFIService.Iteraction</returns>
        /// <remarks>DataInteractionChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/11/2018</FecCrea></item></list>
        public CommonIFIService.Iteraction DataInteractionChangePostalAddress(Model.ChangePostalAddressModel objModel)
        {
            var responseModel = new CommonIFIService.Iteraction();
            try
            {
                var tipo = ConfigurationManager.AppSettings("gConstTipoINT");
                responseModel.OBJID_CONTACTO = objModel.strObjidContacto;
                responseModel.START_DATE = DateTime.UtcNow.ToString("dd/MM/yyyy");
                responseModel.TELEFONO = objModel.strTelefono;
                responseModel.TIPO = objModel.tipo;
                responseModel.CLASE = objModel.claseDes;
                responseModel.SUBCLASE = objModel.subClaseDes;
                responseModel.TIPO_CODIGO = objModel.tipoCode;
                responseModel.CLASE_CODIGO = objModel.claseCode;
                responseModel.SUBCLASE_CODIGO = objModel.subClaseCode;
                responseModel.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                responseModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                responseModel.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                responseModel.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                responseModel.NOTAS = objModel.strNote;
                responseModel.FLAG_CASO = Claro.Constants.NumberZeroString;
                responseModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                responseModel.AGENTE = objModel.CurrentUser;
                
            }
            catch (Exception ex)
            {
                Logging.Error(objModel.strIdSession, objModel.strIdSession, ex.Message);
            }

            return responseModel;
        }


        /// <summary>Método que obtiene los datos de la plantilla interacción</summary>
        /// <param name="oModel"></param>
        /// <returns>CommonIFIService.InsertTemplateInteraction</returns>
        /// <remarks>DataTemplateInteractionChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/11/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>23/01/2019.</FecAct></item>responseModel._NOMBRE_TRANSACCION obtenido por KEY para que sea tomado en el nuevo flujo</list>
        public CommonIFIService.InsertTemplateInteraction DataTemplateInteractionChangePostalAddress(Model.ChangePostalAddressModel objModel)
        {
            var responseModel = new CommonIFIService.InsertTemplateInteraction();
            var strImporte = string.Empty;
            try
            {
                responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("TransaccionChangePostalAddress");
                responseModel._X_CLARO_NUMBER = objModel.strTelefono;
                responseModel._X_DOCUMENT_NUMBER = objModel.DNI_RUC;
                responseModel._X_FIRST_NAME = objModel.strNombres;
                responseModel._X_LAST_NAME = objModel.strApellidos;
                responseModel._X_ADDRESS = objModel.strDireccion;
                responseModel._X_REFERENCE_ADDRESS = objModel.strReferencia;
                responseModel._X_OTHER_FIRST_NAME = objModel.strContactoCliente;
                responseModel._X_DISTRICT = objModel.strDistrito;
                responseModel._X_CITY = objModel.strProvincia;
                responseModel._X_DEPARTMENT = objModel.strDepartamento;
                responseModel._X_INTER_1 = objModel.strPais;
                responseModel._X_ZIPCODE = objModel.strCodPostal;
                responseModel._X_NAME_LEGAL_REP = objModel.fullNameUser;

                if (objModel.Flag_Email == true)
                {
                    responseModel._X_EMAIL = objModel.strMailChange;
                 }
                 else
                 {
                     responseModel._X_EMAIL = "";
                 }

                responseModel._X_INTER_15 = objModel.strCacDac;


            }
            catch (Exception ex)
            {
                Logging.Error(objModel.strIdSession, objModel.strIdSession, ex.Message);
            }

            return responseModel;
        }


        /// <summary>Método que inserta los datos enviados en la interacción </summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="oPlantillaDat"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        /// <returns>CommonIFIService.InsertGeneralResponse</returns>
        /// <remarks>InsertInteracChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/11/2018</FecCrea></item></list>
        public CommonIFIService.InsertGeneralResponse InsertInteracChangePostalAddress(CommonIFIService.Iteraction objInteractionModel, Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            CommonIFIService.InsertGeneralRequest objRequest;
            CommonIFIService.InsertGeneralResponse objResult = null;
            Common.AuditRequest audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(strIdSession);
            try
            {
                objRequest = new Common.InsertGeneralRequest()
                {
                    Interaction = objInteractionModel,
                    InteractionTemplate = oPlantillaDat,
                    vNroTelefono = strNroTelephone,
                    vPASSWORD_USUARIO = strPassUser,
                    vUSUARIO_APLICACION = strUserSession,
                    vUSUARIO_SISTEMA = strUserAplication,
                    vEjecutarTransaccion = boolEjecutTransaction,
                    audit = audit,

                };

                objResult = Logging.ExecuteMethod(() =>
                {
                    return oCommonService.GetinsertInteractionGeneral(objRequest);
                });

            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return objResult;
        }


        /// <summary> metodo para enviar un correo electronico al email ingresado </summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strRutaArchivo"></param>
        /// <returns>CommonIFIService.InsertGeneralResponse</returns>
        /// <remarks>GetSendEmailChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>22/11/2018</FecCrea></item></list>
        public string GetSendEmailChangePostalAddress(Model.ChangePostalAddressModel objModel,
                           string strInteraccionId,
                           string strRutaArchivo)
        {

            WirelessIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(objModel.strIdSession);
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            byte[] attachFile = null;
            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

            if (DisplayFileFromServerSharedFile(objModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile))
            {
                Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                {
                    strIdSession = objModel.strIdSession,
                    strTo = objModel.strMailChange,
                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailChangePostalAdress", vArchivoINTPOSTConfigMsg),
                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailChangePostalAdress", vArchivoINTPOSTConfigMsg),
                    strAttached = strAdjunto,
                    byteAttached = attachFile
                };
                MensajeEmail = GetSendEmailConst2(objSendEmail);
                return Claro.SIACU.Constants.OK;
            }
            else
            {
                return null;
            }

        }

        #endregion

    }
}
using System;
using System.Linq;
using KEY = Claro.ConfigurationManager;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;
using Claro.Web;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AuditRequest;
using Common = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class ChangeMinorController : CommonServicesController
    {
        public readonly CommonIFIService.CommonIFIServiceClient oCommonIFIService = new CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient oServicePostpaid = new PostIFIService.PostIFIServiceClient();
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient();


        /// <summary>Método que permite mostrar la vista del cambio de datos menores</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/09/2018.</FecCrea></item></list>
        public ActionResult Index()
        {
            return View("~/Areas/IFITransactions/Views/ChangeMinor/ChangeMinor.cshtml");
        }


        /// <summary>Método que se ejecuta al cargar la página</summary>
        /// <param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>PageLoad</remarks>
        /// <list type="PageLoad">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/09/2018.</FecCrea></item></list>
        public JsonResult PageLoad(string strIdSession)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            Common.Typification typificationLoad = null;
            string lblMensaje = "";
            try
            {
                typificationLoad = LoadTypificationChangeMinor(audit, ref lblMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return Json(new { data = typificationLoad });

        }

        #region Tipificacion
        /// <summary>Método que obtiene los datos de la tipificación</summary>
        /// <param name="audit"></param>  
        /// <param name="lblMensaje"></param>
        /// <returns>Common.Typification</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018</FecCrea></item></list>
        public Common.Typification LoadTypificationChangeMinor(CommonIFIService.AuditRequest audit,
                                                            ref string lblMensaje)
        {
            Common.Typification oTypification = null;



            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("TransaccionChangeMinor");
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
        #endregion

        #region Transacción

        /// <summary>Método que realiza la actualizacion de datos menores</summary>
        /// <param name="oModel"></param>  
        /// <returns>JsonResult</returns>
        /// <remarks>SaveTransactionCambioDatosM</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public JsonResult SaveTransactionCambioDatosM(Model.ChangeMinorModel oModel)
        {
            string vDesInteraction = string.Empty;
            string strRutaArchivo = string.Empty;
            string vInteractionId = string.Empty;
            string strNombreArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            ClientDataAddResponse objClientDataAddResponse = new ClientDataAddResponse();
            ClientDataAddRequest objRequestClientDataAdd = new ClientDataAddRequest();

            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            bool blnRetorno = false;
            oModel.blnInteract = false;
            string strFlgRegistrado = Claro.Constants.NumberOneString;

            try
            {
                DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
                DataCustomerResponsePostPaid objResponse = null;

                objRequestCustomer = new DataCustomerRequestPostPaid()
                {
                    audit = audit,
                    strIdSession = oModel.strIdSession,
                    strTransaccion = audit.transaction,
                    strtelefono = oModel.strTelefono,
                    strcustomerid = oModel.strCustomerId
                };

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetDataCustomer(objRequestCustomer);
                });

                CustomerResponse objCustomerResponse;
                AuditRequestFixed audit2 = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
                GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                {
                    audit = audit2,
                    vPhone = oModel.strTelefono,
                    vAccount = string.Empty,
                    vContactobjid1 = string.Empty,
                    vFlagReg = strFlgRegistrado
                };
                objCustomerResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCustomerClf(objGetCustomerRequest);
                });

                oModel.strObjidContacto = objCustomerResponse.contactobjid;
                objRequestClientDataAdd = new ClientDataAddRequest()
                {
                    audit = audit,
                    strIdSession = oModel.strIdSession,
                    strTransaccion = audit.transaction,
                    vInteraccionID = "",
                    v_ContactId = oModel.strObjidContacto,
                    vFlagModo = ConfigurationManager.AppSettings("strFlagConsultaByCliente")
                };

                objClientDataAddResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetClientDataAdd(objRequestClientDataAdd);
                });

                blnRetorno = ActualizarDatosMenores(oModel);
                if (blnRetorno)
                {
                    actualizaDatosClarify(oModel);
                    actualizaTelefonoFijoClfy(oModel);

                    if (oModel.strObjidContacto != null)
                    {
                        List<string> strInteractionId = SaveInteraction(oModel, objResponse, objClientDataAddResponse);
                        vInteractionId = strInteractionId[3].ToString();

                        if (strInteractionId[0] == Claro.SIACU.Constants.OK)
                        {
                            try
                            {
                                #region CONSTANCY PDF
                                vDesInteraction = Claro.Utils.GetValueFromConfigFileIFI("strMsgExitoTrans", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                                GetConstancyPDF(oModel.strIdSession, vInteractionId, oModel, objResponse, objClientDataAddResponse);
                                strRutaArchivo = oModel.strFullPathPDF;
                                #endregion

                                if (oModel.Flag_Email)
                                {

                                    #region SEND EMAIL
                                    byte[] attachFile = null;
                                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                                    if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile))
                                    {
                                        List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                                        if (attachFile != null)
                                        {
                                            AttachedFile objFile = new AttachedFile();

                                            objFile.file = attachFile;
                                            objFile.Name = strAdjunto;
                                            objFile.HeadBoard = strAdjunto;
                                            lstAdjuntos.Add(objFile);
                                            MensajeEmail = GetSendEmail2(oModel, lstAdjuntos);
                                        }
                                        else
                                        {
                                            MensajeEmail = "Pero no se pudo procesar el envío del correo de notificación.";
                                        }
                                    }
                                    else
                                    {
                                        MensajeEmail = "Pero no se pudo procesar el envío del correo de notificación.";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    MensajeEmail = "No se envió un correo de notificación.";
                                }
                            }
                            catch (Exception ex)
                            {
                                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                            }
                        }
                        else
                        {
                            vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");
                        }
                    }
                    else
                    {
                        vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");
                    }
                }
                else
                {
                    vDesInteraction = "Error en la transacción : Ocurrió un error al actualizar los datos";
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }

            return Json(new { data = oModel, vDesInteraction, strRutaArchivo, vInteractionId, MensajeEmail });
        }
        #endregion

        #region CONSTANCY PDF - SEND EMAIL
        /// <summary>Método que permite generar la constancia de Cambios de Datos Menores mediante un archivo Pdf</summary>
        /// <param name="strIdSession"></param>  
        /// <param name="strIdInteraction"></param>  
        /// <param name="oModel"></param>  
        /// <param name="objResponse"></param> 
        /// <param name="objClientDataAddResponse"></param> 
        /// <returns>Dictionary</returns>
        /// <remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>24/10/2018</FecCrea></item></list>        
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.ChangeMinorModel oModel, DataCustomerResponsePostPaid objResponse, ClientDataAddResponse objClientDataAddResponse)
        {

            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;

            InteractionServiceRequestHfc objInteractionServiceRequest = new InteractionServiceRequestHfc();
            ParametersGeneratePDF parameters = null;

            try
            {
                strTexto = Claro.Utils.GetValueFromConfigFileIFI("strMsgDatosMenoresConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

                if (oModel.Accion.ToUpper() == Claro.Constants.LetterR)
                {

                    parameters = new ParametersGeneratePDF();

                    //------Nuevos Datos del Cliente

                    if (oModel.strContactoCliente != objResponse.Cliente.CONTACTO_CLIENTE)
                    {
                        parameters.StrContactoCliente = oModel.strContactoCliente;
                    }

                    if (oModel.dateFechaNacimiento != objResponse.Cliente.FECHA_NAC)
                    {
                        parameters.StrFechaNac = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");
                    }

                    if (oModel.strPhone != objResponse.Cliente.TELEF_REFERENCIA)
                    {
                        parameters.StrTelfReferencia = oModel.strPhone;
                    }


                    if (oModel.strNacionalidad != (objResponse.Cliente.LUGAR_NACIMIENTO_DES).ToUpper())
                    {
                        parameters.StrNacionalidad = oModel.strNacionalidad;
                    }

                    if (oModel.strMailChange != objResponse.Cliente.EMAIL)
                    {

                        parameters.strCorreoCliente = oModel.strMailChange;
                    }

                    if (oModel.strEstadoCivil.ToUpper() != objResponse.Cliente.ESTADO_CIVIL.ToUpper())
                    {
                        parameters.StrEstadoCivil = oModel.strEstadoCivil;
                    }


                    if (oModel.strSexo != objResponse.Cliente.SEXO)
                    {
                        parameters.StrSexo = oModel.strSexo;

                        if (parameters.StrSexo == "F")
                        {
                            parameters.StrSexo = "FEMENINO";
                        }
                        else if (parameters.StrSexo == "M")
                        {
                            parameters.StrSexo = "MASCULINO";
                        }
                        else if (parameters.StrSexo == "U")
                        {
                            parameters.StrSexo = "NA";
                        }
                    }

                    //----Datos Adicionales

                    if (oModel.strCargo != objResponse.Cliente.CARGO)
                    {
                        parameters.StrCargo = oModel.strCargo;
                    }

                    if (oModel.strNombreComercial != objResponse.Cliente.NOMBRE_COMERCIAL)
                    {
                        parameters.StrContenidoComercial = oModel.strNombreComercial;
                    }

                    if (oModel.StrPHONE1 != objClientDataAddResponse.Cliente.PHONE1)
                    {
                        parameters.StrPhone1 = oModel.StrPHONE1;

                    }

                    if (oModel.StrPHONE2 != objClientDataAddResponse.Cliente.PHONE2)
                    {
                        parameters.StrPhone2 = oModel.StrPHONE2;

                    }

                    if (oModel.StrEMAIL1 != objClientDataAddResponse.Cliente.EMAIL1)
                    {

                        parameters.StrEmail1 = oModel.StrEMAIL1;
                    }

                    if (oModel.StrEMAIL2 != objClientDataAddResponse.Cliente.EMAIL2)
                    {

                        parameters.StrEmail2 = oModel.StrEMAIL2;
                    }

                    if (oModel.strFax != objResponse.Cliente.FAX)
                    {
                        parameters.StrNumeroFax = oModel.strFax;
                    }

                    //----Direccion Alternativa

                    if (oModel.StrCOUNTRY_ID != objClientDataAddResponse.Cliente.COUNTRY_ID)
                    {
                        parameters.StrPais = oModel.StrCOUNTRY_DES;
                    }

                    if (oModel.StrDEPARTAMENTO_ID != objClientDataAddResponse.Cliente.DEPARTAMENTO)
                    {
                        parameters.StrDepartamento = oModel.StrDEPARTAMENTO;
                    }

                    if (oModel.StrPROVINCIA_ID != objClientDataAddResponse.Cliente.PROVINCIA)
                    {
                        parameters.StrProvincia = oModel.StrPROVINCIA;
                    }

                    if (oModel.StrDISTRITO_ID != objClientDataAddResponse.Cliente.DISTRITO)
                    {
                        parameters.StrDistrito = oModel.StrDISTRITO;
                    }

                    if (oModel.StrADDRESS != objClientDataAddResponse.Cliente.ADDRESS)
                    {
                        parameters.StrDireccion = oModel.StrADDRESS;
                    }

                    if (oModel.strCacDac != objResponse.Cliente.PUNTO_VENTA)
                    {
                        parameters.StrCentroAtencionArea = oModel.strCacDac;
                    }

                    parameters.StrNombreAgenteUsuario = oModel.fullNameUser;
                    parameters.strEnvioCorreo = oModel.chkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
                    parameters.strEmailEnviar = oModel.StrEmailEnviar;
                    parameters.StrNroServicio = oModel.strTelefono;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.strNroDoc = oModel.DNI_RUC;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.StrTitularCliente = oModel.strNameComplet;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrCustomerId = oModel.strCustomerId;
                    parameters.StrFechaTransaccionProgram = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.strFechaTransaccion = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.StrTipoDocIdentidad = oModel.strTipoocumento;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrAccion = oModel.DesAccion;
                    parameters.StrTipoTransaccion = ConfigurationManager.AppSettings("strNombreArchivo_DatosMenores");
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionCambioDatosMenores");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoCambioDatosMenores");
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrContenidoComercial2 = strTexto;
                }
                else
                {

                    parameters = new ParametersGeneratePDF();
                    parameters.StrTitularCliente = oModel.strNombres;
                    parameters.StrTipoDocIdentidad = oModel.strTipoocumento;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.StrTelfReferencia = oModel.strPhone;
                    parameters.StrMovil = oModel.strMovil;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrContenidoComercial = oModel.strContactoCliente;
                    parameters.strNroDoc = oModel.DNI_RUC;
                    parameters.StrPuntoAtencion = oModel.strCacDac;
                    parameters.strDireccionInstalcion = oModel.AdressDespatch;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.strFechaTransaccion = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.StrTipoTransaccion = ConfigurationManager.AppSettings("strNombreArchivo_DatosMenores");
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionCambioDatosMenores");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoCambioDatosMenores");
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.strFechaHoraAtención = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.StrContenidoComercial2 = strTexto;

                }

                GenerateConstancyResponseCommon response = GenerateContancyPDF(strIdSession, parameters);
                nombrepath = response.FullPathPDF;
                var generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", strNombreArchivo);


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }
            return listResponse;
        }

        /// <summary>Método que permite el envio de la constancia por email</summary>
        /// <param name="strInteraccionId"></param>  
        /// <param name="strAdjunto"></param>  
        /// <param name="model"></param>  
        /// <param name="strNombreArchivoPDF"></param>  
        /// <param name="attachFile"></param>  
        /// <returns>string</returns>
        /// <remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>24/10/2018</FecCrea></item></list>
        public string GetSendEmail(string strInteraccionId, string strAdjunto, Model.ChangeMinorModel model, string strNombreArchivoPDF, byte[] attachFile)
        {
            string strResul = string.Empty;
            string strDestinatarios = model.StrEmailEnviar;
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailCambiodatosMenores",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                Claro.Web.Logging.Info(AuditRequest.Session, AuditRequest.transaction, "Método : GetSendEmail strInteraccionId: " + strInteraccionId);

                #region "Body Email"

                strMessage = "<html>";
                strMessage += " <head>";
                strMessage += "     <style type='text/css'>";
                strMessage += "     <!--";
                strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "      -->";
                strMessage += "      </style>";
                strMessage += " </head>";
                strMessage += "<body>";
                strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + " </td></tr>";
                if (model.Accion == Claro.Constants.LetterR)
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailMinorChanges", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                }
                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";

                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailConsultaLlameGratis", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse = null;
                objGetSendEmailRequest =
                    new CommonIFIService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strMessage,
                        strAttached = strAdjunto,
                        strSubject = strAsunto,
                        AttachedByte = attachFile
                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return oCommonService.GetSendEmailFixed(objGetSendEmailRequest); });

                if (objGetSendEmailResponse.Exit == Claro.SIACU.Constants.OK)
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                }
                else
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.strIdSession, AuditRequest.transaction, ex.Message);

                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }
            return strResul;
        }

        /// <summary>Método que permite el envio de la constancia por email</summary>
        /// <param name="model"></param>  
        /// <param name="lstFileAdjunto"></param>  
        /// <returns>string</returns>
        /// <remarks>GetSendEmail2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/03/2019</FecCrea></item></list>
        public string GetSendEmail2(Model.ChangeMinorModel model, List<CommonIFIService.AttachedFile> lstFileAdjunto)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            try
            {
                string strMessage = string.Empty;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailCambiodatosMenores",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                Claro.Web.Logging.Info(objAudit.Session, objAudit.transaction, "Método : GetSendEmail strInteraccionId: " + objAudit);
                string strDestinatarios = model.StrEmailEnviar;

                #region "Body Email"

                strMessage = "<html>";
                strMessage += " <head>";
                strMessage += "     <style type='text/css'>";
                strMessage += "     <!--";
                strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "      -->";
                strMessage += "      </style>";
                strMessage += " </head>";
                strMessage += "<body>";
                strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + " </td></tr>";
                if (model.Accion == Claro.Constants.LetterR)
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailMinorChanges", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                }
                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";

                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailConsultaLlameGratis", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                objSendEmailWithBase64Request.audit = objAudit;
                objSendEmailWithBase64Request.Sender = strRemitente;
                objSendEmailWithBase64Request.To = strDestinatarios;
                objSendEmailWithBase64Request.HtmlFlag = KEY.AppSettings("strHtmlFlagCorreoSB").ToString();
                objSendEmailWithBase64Request.Subject = strAsunto;
                objSendEmailWithBase64Request.Message = strMessage;
                if (lstFileAdjunto != null && lstFileAdjunto.Count > 0)
                {
                    objSendEmailWithBase64Request.ListAttachedFile = lstFileAdjunto;
                }

                objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                {
                    return oCommonIFIService.SendEmailWithBase64(objSendEmailWithBase64Request);
                });
                if (objSendEmailWithBase64Response != null && objSendEmailWithBase64Response.ResponseCode == Claro.Constants.NumberZeroString)
                {
                    strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.strIdSession, objAudit.transaction, ex.Message);
            }
            return strResul;
        }
        #endregion



        /// <summary>Método que obtiene datos del cliente y de los datos adicionales.</summary>
        /// <param name="oModel"></param>  
        /// <returns>JsonResult</returns>
        /// <remarks>GetCustomerChangeMinor</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/10/2018</FecCrea></item></list>
        public JsonResult GetCustomerChangeMinor(Model.ChangeMinorModel oModel)
        {

            string cambiarfecha = "";
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            ClientDataAddResponse objClientDataAddResponse = new ClientDataAddResponse();
            ClientDataAddRequest objRequestClientDataAdd = new ClientDataAddRequest();

            DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
            DataCustomerResponsePostPaid objResponse = new DataCustomerResponsePostPaid();
            CustomerResponse objCustomerResponse = new CustomerResponse();
            try
            {
                objRequestCustomer = new DataCustomerRequestPostPaid()
                {
                    audit = audit,
                    strIdSession = oModel.strIdSession,
                    strTransaccion = audit.transaction,
                    strtelefono = oModel.strTelefono,
                    strcustomerid = oModel.strCustomerId
                };

                objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetDataCustomer(objRequestCustomer);
                });

                cambiarfecha = (objResponse.Cliente.FECHA_NAC).ToString("dd/MM/yyyy");
                CustomerResponse objCustomerResponse2;
                AuditRequestFixed audit2 = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
                GetCustomerRequest objGetCustomerRequest2 = new GetCustomerRequest()
                {
                    audit = audit2,
                    vPhone = oModel.strTelefono,
                    vAccount = string.Empty,
                    vContactobjid1 = string.Empty,
                    vFlagReg = strFlgRegistrado
                };

                objCustomerResponse2 = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCustomerClf(objGetCustomerRequest2);
                });

                oModel.strObjidContacto = objCustomerResponse2.contactobjid;

                //Metodos para Datos Adicionales

                objRequestClientDataAdd = new ClientDataAddRequest()
                {
                    audit = audit,
                    strIdSession = oModel.strIdSession,
                    strTransaccion = audit.transaction,
                    vInteraccionID = "",
                    v_ContactId = oModel.strObjidContacto,
                    vFlagModo = ConfigurationManager.AppSettings("strFlagConsultaByCliente")
                };

                objClientDataAddResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.GetClientDataAdd(objRequestClientDataAdd);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }

            return Json(new { objCus = objResponse.Cliente, cambiarfecha, objclientDataAdd = objClientDataAddResponse.Cliente });
        }


        /// <summary>Método que permite Actualizar los datos menores de un cliente en la BD (BSCS70_DESA)</summary>
        /// <param name="oModel"></param>   
        /// <returns>bool</returns>
        /// <remarks>ActualizarDatosMenores</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public bool ActualizarDatosMenores(Model.ChangeMinorModel oModel)
        {
            bool FlatResultado = false;
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            try
            {
                SaveChangeMinorRequest objRequest = new SaveChangeMinorRequest()
                {
                    audit = audit,
                    strSession = oModel.strIdSession,
                    strTransaction = audit.transaction,
                    objCliente = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client()
                    {
                        CUSTOMER_ID = oModel.strCustomerId,
                        CARGO = oModel.strCargo,
                        TELEF_REFERENCIA = oModel.strPhone,
                        TELEFONO = oModel.strMovil,
                        FAX = oModel.strFax,
                        EMAIL = oModel.strMailChange,
                        NOMBRE_COMERCIAL = oModel.strNombreComercial,
                        CONTACTO_CLIENTE = oModel.strContactoCliente,
                        FECHA_NAC = oModel.dateFechaNacimiento,
                        LUGAR_NACIMIENTO_ID = oModel.strNacionalidadId,
                        LUGAR_NACIMIENTO_DES = oModel.strNacionalidad,
                        SEXO = oModel.strSexo,
                        ESTADO_CIVIL = oModel.strEstadoCivil,
                        ESTADO_CIVIL_ID = oModel.strEstadoCivilId

                    },
                };

                SaveChangeMinorResponse objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.SaveChangeMinor(objRequest);
                });

                FlatResultado = objResponse.StrResult;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return FlatResultado;
        }


        /// <summary>Método que permite Actualizar los datos menores de un cliente en la BD de Clarify (TIMPRB)</summary>
        /// <param name="oModel"></param> 
        /// <remarks>actualizaDatosClarify</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public void actualizaDatosClarify(Model.ChangeMinorModel oModel)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            try
            {

                Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client objClient = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client()
                {

                    OBJID_CONTACTO = oModel.strObjidContacto,
                    TELEF_REFERENCIA = oModel.strPhone,
                    FAX = oModel.strFax,
                    EMAIL = oModel.strMailChange,
                    FECHA_NAC = oModel.dateFechaNacimiento,
                    SEXO = oModel.strSexo,
                    ESTADO_CIVIL_ID = oModel.strEstadoCivilId,
                    CARGO = oModel.strCargo,
                    NOMBRE_COMERCIAL = oModel.strNombreComercial,
                    CONTACTO_CLIENTE = oModel.strContactoCliente,
                    LUGAR_NACIMIENTO_DES = oModel.strNacionalidadId

                };


                SaveChangeMinorClarifyRequest objRequest = new SaveChangeMinorClarifyRequest()
                {
                    audit = audit,
                    strSession = oModel.strIdSession,
                    strTransaction = audit.transaction,
                    objCliente = objClient

                };

                Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.SaveChangeMinorClarify(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
        }

        #region Interaccion

        /// <summary>Método que permite que registra la interacción , dentro de este metodo existe un evento la cual permitira el registro de Datos Adicionales.</summary>
        /// <returns>List</returns>
        /// <remarks>SaveInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecActu>23/01/2019</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Se creo un nuevo metodo de creacion de interacion para realizarlo por servicio y ya no por StoreProcedure sin quitar la opcion de un furuto cambio a SP otra vez</Mot></item>nuevo metodo para crear interaccion por SP o Servicio "InsertInteractionChangeMinor"</list></remarks>
        public List<string> SaveInteraction(Model.ChangeMinorModel oModel, DataCustomerResponsePostPaid objResponse, ClientDataAddResponse objClientDataAddResponse)
        {

            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = oModel.strPhone;
            Model.TemplateInteractionModel oPlantillaDat = null;
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion = null;

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            try
            {

                oInteraccion = DatosInteraccion(oModel);
                oPlantillaDat = GetDataTemplateInteraction(oModel, objResponse, objClientDataAddResponse);


                var resultInteractionService = InsertInteractionChangeMinor(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.strIdSession, oModel.strCustomerId);

                foreach (KeyValuePair<string, object> par in resultInteractionService)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != Claro.SIACU.Constants.OK && lstaDatTemplate[3] == string.Empty)
                {
                    Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                }
                var vInteractionId = lstaDatTemplate[3].ToString();
                Model.ClientDataAddModel oClientDataAdd = null;
                oClientDataAdd = DatosClienteDatosAdic(oModel);
                string idObjidContacto = oModel.strObjidContacto;
                SaveClientDataAdd(oClientDataAdd, oModel, vInteractionId, idObjidContacto);
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            return lstaDatTemplate;
        }


        /// <summary>Método que obtiene los datos de la interacción.</summary>
        /// <returns>Model.InteractionModel</returns>
        /// <remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>23/01/2019</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Se actualizo los ultimos datos para que sean tomados por el servicio o el sp sin afectar por donde valla el registro de interaccion</Mot></item>ClassCode,SubClassCode</list></remarks>
        public Model.InteractionModel DatosInteraccion(Model.ChangeMinorModel oModel)
        {

            var oInteraccion = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Inicio Método : DatosInteraccion");
            try
            {
                oInteraccion.ObjidContacto = oModel.strObjidContacto;
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = oModel.strTelefono;
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
                oInteraccion.ClassCode = oModel.claseCode;
                oInteraccion.SubClassCode = oModel.subClaseCode;
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }


            return oInteraccion;

        }


        /// <summary>Método que obtiene los datos de la plantilla interacción</summary>
        /// <param name="oModel"></param>
        /// <param name="objResponse"></param>
        /// <param name="objClientDataAddResponse"></param>
        /// <returns>Model.TemplateInteractionModel</returns>
        /// <remarks>GetDataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>23/01/20109</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Se adaptaron algunos datos para que fueran tomados tambien por el nuevo proceso de creacion de interaccion por servicio sin afectar el flujo por SP</Mot></item></list></remarks>
        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.ChangeMinorModel oModel, DataCustomerResponsePostPaid objResponse, ClientDataAddResponse objClientDataAddResponse)
        {
            var oPlantillaCampoData = new Model.TemplateInteractionModel();

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            try
            {

                //------Nuevos Datos del Cliente

                if (oModel.strContactoCliente != objResponse.Cliente.CONTACTO_CLIENTE)
                {
                    oPlantillaCampoData.X_OTHER_FIRST_NAME = oModel.strContactoCliente;
                }

                if (oModel.dateFechaNacimiento != objResponse.Cliente.FECHA_NAC)
                {
                    oPlantillaCampoData.X_CLAROLOCAL5 = oModel.dateFechaNacimiento.ToString("dd/MM/yyyy");

                }

                if (oModel.strPhone != objResponse.Cliente.TELEF_REFERENCIA)
                {
                    oPlantillaCampoData.X_FIXED_NUMBER = oModel.strPhone;
                }

                if (oModel.strMovil != objResponse.Cliente.TELEFONO_CONTACTO)
                {

                    oPlantillaCampoData.X_CLAROLOCAL4 = oModel.strMovil;
                }

                if (oModel.strNacionalidad != (objResponse.Cliente.LUGAR_NACIMIENTO_DES).ToUpper())
                {
                    oPlantillaCampoData.X_INTER_1 = oModel.strNacionalidad;
                }

                if (oModel.strMailChange != objResponse.Cliente.EMAIL)
                {

                    oPlantillaCampoData.X_EMAIL = oModel.strMailChange;
                }

                if (oModel.strEstadoCivil.ToUpper() != objResponse.Cliente.ESTADO_CIVIL.ToUpper())
                {
                    oPlantillaCampoData.X_MARITAL_STATUS = oModel.strEstadoCivil;
                }


                if (oModel.strSexo != objResponse.Cliente.SEXO)
                {
                    oPlantillaCampoData.X_CONTACT_SEX = oModel.strSexo;

                    if (oPlantillaCampoData.X_CONTACT_SEX == "F")
                    {
                        oPlantillaCampoData.X_CONTACT_SEX = "FEMENINO";
                    }
                    else if (oPlantillaCampoData.X_CONTACT_SEX == "M")
                    {
                        oPlantillaCampoData.X_CONTACT_SEX = "MASCULINO";
                    }
                    else if (oPlantillaCampoData.X_CONTACT_SEX == "U")
                    {
                        oPlantillaCampoData.X_CONTACT_SEX = "NA";
                    }
                }

                //----Datos Adicionales

                if (oModel.strCargo != objResponse.Cliente.CARGO)
                {
                    oPlantillaCampoData.X_POSITION = oModel.strCargo;
                }

                if (oModel.strNombreComercial != objResponse.Cliente.NOMBRE_COMERCIAL)
                {
                    oPlantillaCampoData.X_INTER_29 = oModel.strNombreComercial;
                }

                if (oModel.StrPHONE1 != objClientDataAddResponse.Cliente.PHONE1)
                {
                    oPlantillaCampoData.X_OST_NUMBER = oModel.StrPHONE1;

                }

                if (oModel.StrPHONE2 != objClientDataAddResponse.Cliente.PHONE2)
                {
                    oPlantillaCampoData.X_OTHER_PHONE = oModel.StrPHONE2;

                }

                if (oModel.StrEMAIL1 != objClientDataAddResponse.Cliente.EMAIL1)
                {

                    oPlantillaCampoData.X_CLAROLOCAL1 = oModel.StrEMAIL1;
                }

                if (oModel.StrEMAIL2 != objClientDataAddResponse.Cliente.EMAIL2)
                {

                    oPlantillaCampoData.X_CLAROLOCAL2 = oModel.StrEMAIL2;
                }

                if (oModel.strFax != objResponse.Cliente.FAX)
                {
                    oPlantillaCampoData.X_FAX = oModel.strFax;
                }

                //----Direccion Alternativa

                if (oModel.StrCOUNTRY_ID != objClientDataAddResponse.Cliente.COUNTRY_ID)
                {
                    oPlantillaCampoData.X_REASON = oModel.StrCOUNTRY_DES;
                }

                if (oModel.StrDEPARTAMENTO_ID != objClientDataAddResponse.Cliente.DEPARTAMENTO)
                {
                    oPlantillaCampoData.X_DEPARTMENT = oModel.StrDEPARTAMENTO;
                }

                if (oModel.StrPROVINCIA_ID != objClientDataAddResponse.Cliente.PROVINCIA)
                {
                    oPlantillaCampoData.X_CITY = oModel.StrPROVINCIA;
                }

                if (oModel.StrDISTRITO_ID != objClientDataAddResponse.Cliente.DISTRITO)
                {
                    oPlantillaCampoData.X_DISTRICT = oModel.StrDISTRITO;
                }

                if (oModel.StrADDRESS != objClientDataAddResponse.Cliente.ADDRESS)
                {
                    oPlantillaCampoData.X_ADDRESS = oModel.StrADDRESS;
                }

                if (oModel.strCacDac != objResponse.Cliente.PUNTO_VENTA)
                {
                    oPlantillaCampoData.X_INTER_15 = oModel.strCacDac;
                }


                oPlantillaCampoData.X_CLAROLOCAL6 = oModel.chkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;

                oPlantillaCampoData.X_INTER_16 = oModel.StrEmailEnviar;
                oPlantillaCampoData.NOMBRE_TRANSACCION = KEY.AppSettings("TransaccionChangeMinor");
                oPlantillaCampoData.X_CLARO_NUMBER = oModel.strTelefono;
                oPlantillaCampoData.X_FIRST_NAME = oModel.strNombres;
                oPlantillaCampoData.X_LAST_NAME = oModel.strApellidos;
                oPlantillaCampoData.X_FLAG_REGISTERED = "1";

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            return oPlantillaCampoData;

        }
        #endregion

        #region Datos Adicionales

        /// <summary>Método que permite Actualizar el telefono fijo en la BD de Clarify (TIMPRB)</summary>
        /// <param name="oModel"></param> 
        /// <remarks>actualizaTelefonoFij8oClfy</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>12/11/2018</FecCrea></item></list>
        public void actualizaTelefonoFijoClfy(Model.ChangeMinorModel oModel)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            try
            {
                Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client objClient = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client()
                {
                    TELEFONO = oModel.strTelefono,
                    CARGO = oModel.strCargo,
                    NOMBRE_COMERCIAL = oModel.strNombreComercial,
                    FAX = oModel.strFax,
                    EMAIL = oModel.strMailChange,
                    FECHA_NAC = oModel.dateFechaNacimiento,
                    SEXO = oModel.strSexo
                };

                SaveChangeMinorFijoClarifyRequest objRequest = new SaveChangeMinorFijoClarifyRequest()
                {
                    audit = audit,
                    strSession = oModel.strIdSession,
                    strTransaction = audit.transaction,
                    objCliente = objClient

                };

                SaveChangeMinorFijoClarifyResponse objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.SaveChangeMinorFijoClarify(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
        }


        /// <summary>Método que permite almacenar los datos del cliente adicionales.</summary>
        /// <param name="oModel"></param>
        /// <returns>Model.ClientDataAddModel</returns>
        /// <remarks>DatosClienteDatosAdic</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>12/11/2018</FecCrea></item></list>
        public Model.ClientDataAddModel DatosClienteDatosAdic(Model.ChangeMinorModel oModel)
        {
            var oClientDataAdd = new Model.ClientDataAddModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);

            try
            {
                oClientDataAdd.PHONE1 = oModel.StrPHONE1;
                oClientDataAdd.PHONE2 = oModel.StrPHONE2;
                oClientDataAdd.EMAIL1 = oModel.StrEMAIL1;
                oClientDataAdd.EMAIL2 = oModel.StrEMAIL2;
                oClientDataAdd.COUNTRY_ID = oModel.StrCOUNTRY_ID;
                oClientDataAdd.COUNTRY_DES = oModel.StrCOUNTRY_DES;
                oClientDataAdd.DEPARTAMENTO_ID = oModel.StrDEPARTAMENTO_ID;
                oClientDataAdd.DEPARTAMENTO = oModel.StrDEPARTAMENTO;
                oClientDataAdd.PROVINCIA_ID = oModel.StrPROVINCIA_ID;
                oClientDataAdd.PROVINCIA = oModel.StrPROVINCIA;
                oClientDataAdd.DISTRITO_ID = oModel.StrDISTRITO_ID;
                oClientDataAdd.DISTRITO = oModel.StrDISTRITO;
                oClientDataAdd.ADDRESS = oModel.StrADDRESS;
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }


            return oClientDataAdd;

        }


        /// <summary>Método que permite guardar los Datos Adicionales e Direccion Alternativa en la BD de Clarify (TIMPRB)</summary>
        /// <param name="oModel"></param>  
        /// <remarks>SaveClientDataAdd</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>12/11/2018</FecCrea></item></list>
        public void SaveClientDataAdd(Model.ClientDataAddModel oClientDataAdd, Model.ChangeMinorModel oModel, string vInteractionId, string idObjidContacto)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            try
            {

                Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client objClient = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.Client()
                {
                    OBJID_CONTACTO = oModel.strObjidContacto,
                    INTERACT2CONTACT = vInteractionId,
                    PHONE1 = oClientDataAdd.PHONE1,
                    PHONE2 = oClientDataAdd.PHONE2,
                    EMAIL1 = oClientDataAdd.EMAIL1,
                    EMAIL2 = oClientDataAdd.EMAIL2,
                    COUNTRY_ID = oClientDataAdd.COUNTRY_ID,
                    DEPARTAMENTO = oClientDataAdd.DEPARTAMENTO_ID,
                    PROVINCIA = Convert.ToString(oClientDataAdd.PROVINCIA_ID),
                    DISTRITO = oClientDataAdd.DISTRITO_ID,
                    ADDRESS = oClientDataAdd.ADDRESS

                };


                SaveChangeMinorClientDataAddRequest objRequest = new SaveChangeMinorClientDataAddRequest()
                {
                    audit = audit,
                    strSession = oModel.strIdSession,
                    strTransaction = audit.transaction,
                    objCliente = objClient

                };

                Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return oServicePostpaid.SaveChangeMinorClientDataAdd(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
        }

        #endregion

        ///<summary>Metodo que inserta los datos enviados en la  datos de la interacción y plantilla para cambios de datos menores</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="oPlantillaDat"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        ///<returns>Dictionary<returns>
        ///<remarks>InsertInteractionChangeM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>17/01/2018.</FecCrea></item></list>
        public Dictionary<string, object> InsertInteractionChangeM(Model.InteractionModel objInteractionModel,
                                                    Model.TemplateInteractionModel oPlantillaDat,
                                                    string strNroTelephone,
                                                    string strUserSession,
                                                    string strUserAplication,
                                                    string strPassUser,
                                                    bool boolEjecutTransaction,
                                                    string strIdSession,
                                                    string strCustomerId)
        {
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");

            var strTelefono = (strNroTelephone == objInteractionModel.Telephone) ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };

            CommonIFIService.InsertGeneralRequest objInsertGeneralRequest = new CommonIFIService.InsertGeneralRequest()
            {

                audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession),
                vNroTelefono = strTelefono,
                Interaction = new CommonIFIService.Iteraction()
                {
                    OBJID_CONTACTO = objInteractionModel.ObjidContacto,
                    FECHA_CREACION = objInteractionModel.DateCreaction,
                    TELEFONO = objInteractionModel.Telephone,
                    TIPO = objInteractionModel.Type,
                    CLASE = objInteractionModel.Class,
                    SUBCLASE = objInteractionModel.SubClass,
                    CLASE_CODIGO = objInteractionModel.ClassCode,
                    SUBCLASE_CODIGO = objInteractionModel.SubClassCode,
                    TIPO_INTER = objInteractionModel.TypeInter,
                    METODO = objInteractionModel.Method,
                    RESULTADO = objInteractionModel.Result,
                    HECHO_EN_UNO = objInteractionModel.MadeOne,
                    NOTAS = objInteractionModel.Note,
                    CONTRACT = objInteractionModel.Contract,
                    PLANO = objInteractionModel.Plan,
                    FLAG_CASO = objInteractionModel.FlagCase,
                    USUARIO_PROCESO = objInteractionModel.UserProces,
                    AGENTE = objInteractionModel.Agenth

                },
                InteractionTemplate = new CommonIFIService.InsertTemplateInteraction()
                {
                    _X_OTHER_FIRST_NAME = oPlantillaDat.X_OTHER_FIRST_NAME,
                    _X_CLAROLOCAL5 = oPlantillaDat.X_CLAROLOCAL5,
                    _X_FIXED_NUMBER = oPlantillaDat.X_FIXED_NUMBER,
                    _X_CLAROLOCAL4 = oPlantillaDat.X_CLAROLOCAL4,
                    _X_INTER_1 = oPlantillaDat.X_INTER_1,
                    _X_EMAIL = oPlantillaDat.X_EMAIL,
                    _X_MARITAL_STATUS = oPlantillaDat.X_MARITAL_STATUS,
                    _X_CONTACT_SEX = oPlantillaDat.X_CONTACT_SEX,
                    _X_POSITION = oPlantillaDat.X_POSITION,
                    _X_INTER_29 = oPlantillaDat.X_INTER_29,
                    _X_OST_NUMBER = oPlantillaDat.X_OST_NUMBER,
                    _X_OTHER_PHONE = oPlantillaDat.X_OTHER_PHONE,
                    _X_CLAROLOCAL1 = oPlantillaDat.X_CLAROLOCAL1,
                    _X_CLAROLOCAL2 = oPlantillaDat.X_CLAROLOCAL2,
                    _X_FAX = oPlantillaDat.X_FAX,
                    _X_REASON = oPlantillaDat.X_REASON,
                    _X_DEPARTMENT = oPlantillaDat.X_DEPARTMENT,
                    _X_CITY = oPlantillaDat.X_CITY,
                    _X_DISTRICT = oPlantillaDat.X_DISTRICT,
                    _X_ADDRESS = oPlantillaDat.X_ADDRESS,
                    _X_INTER_15 = oPlantillaDat.X_INTER_15,
                    _X_CLAROLOCAL6 = oPlantillaDat.X_CLAROLOCAL6,
                    _X_INTER_16 = oPlantillaDat.X_INTER_16,
                    _NOMBRE_TRANSACCION = oPlantillaDat.NOMBRE_TRANSACCION,
                    _X_CLARO_NUMBER = oPlantillaDat.X_CLARO_NUMBER,
                    _X_FIRST_NAME = oPlantillaDat.X_FIRST_NAME,
                    _X_LAST_NAME = oPlantillaDat.X_LAST_NAME,
                    _X_FLAG_REGISTERED = oPlantillaDat.X_FLAG_REGISTERED

                }

            };

            var objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            //Validacion de Contingencia
            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            CommonIFIService.InsertGeneralResponse objInsertGeneralResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.InsertGeneralResponse>(() => { return oCommonIFIService.GetinsertInteractionGeneral(objInsertGeneralRequest); });

            var dictionaryResponse = new Dictionary<string, object>()
            {
                {"rFlagInsercion", objInsertGeneralResponse.rFlagInsercion},
                {"rMsgText", objInsertGeneralResponse.rMsgText},
                {"rResult", objInsertGeneralResponse.rResult.ToString()},
                {"rInteraccionId", objInsertGeneralResponse.rInteraccionId},
            };

            var rInteraccionId = objInsertGeneralResponse.rInteraccionId;

            return dictionaryResponse;

        }

        ///<summary>Metodo para obtener la tipificacion de afiliación por correo electrónico IFI</summary>
        ///<param name="audit"></param>
        ///<param name="lblMensaje"></param>
        ///<param name="strStateEmail"></param>
        ///<returns>Common.Typification<returns>
        ///<remarks>LoadTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/01/2019.</FecCrea></item></list>      
        public Common.Typification LoadTypification(CommonIFIService.AuditRequest audit,
                                                            ref string lblMensaje, string strStateEmail)
        {
            Common.Typification oTypification = null;
            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("TransaccionChangeMinor");
                objTypificationRequest.audit = audit;

                Common.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<Common.TypificationResponse>(
                    () => { return oCommonIFIService.GetTypification(objTypificationRequest); });
                if (objResponse.ListTypification != null)
                {
                    oTypification = objResponse.ListTypification.First();
                }
                else
                {

                    lblMensaje = "No se cargo las tipificaciones";
                }



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

        /// <summary>Registra la interacción , dentro de este metodo existe un evento la cual permitira el registro de Datos Adicionales.</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="oPlantillaDat"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        ///<returns>Dictionary<returns>
        ///<remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/01/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>23/01/2019.</FecAct></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Este nuevo metodo utiliza el registro por SP o por servicio segun el KEY "TransaccionChangeMinor"</Mot></item>KEY "TransaccionChangeMinor" SI=ProcesoPorServicio NO=ProcesoPorSP</list></remarks>
        public Dictionary<string, object> InsertInteractionChangeMinor(Model.InteractionModel objInteractionModel, Model.TemplateInteractionModel oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");

            var strTelefono = (strNroTelephone == objInteractionModel.Telephone) ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };

            CommonIFIService.InsertGeneralRequest objInsertGeneralRequest = new CommonIFIService.InsertGeneralRequest()
            {

                audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession),
                vNroTelefono = strTelefono,
                Interaction = new CommonIFIService.Iteraction()
                {
                    AGENTE = objInteractionModel.Agenth,
                    USUARIO_PROCESO = objInteractionModel.UserProces,
                    FLAG_CASO = objInteractionModel.FlagCase,
                    PLANO = objInteractionModel.Plan,
                    CONTRACT = objInteractionModel.Contract,
                    NOTAS = objInteractionModel.Note,
                    HECHO_EN_UNO = objInteractionModel.MadeOne,
                    RESULTADO = objInteractionModel.Result,
                    METODO = objInteractionModel.Method,
                    TIPO_INTER = objInteractionModel.TypeInter,
                    CLASE = objInteractionModel.Class,
                    SUBCLASE = objInteractionModel.SubClass,
                    TIPO = objInteractionModel.Type,
                    CLASE_CODIGO = objInteractionModel.ClassCode,
                    SUBCLASE_CODIGO = objInteractionModel.SubClassCode,
                    TELEFONO = objInteractionModel.Telephone,
                    FECHA_CREACION = objInteractionModel.DateCreaction,
                    OBJID_CONTACTO = objInteractionModel.ObjidContacto

                },
                InteractionTemplate = new CommonIFIService.InsertTemplateInteraction()
                {
                    _X_OTHER_FIRST_NAME = oPlantillaDat.X_OTHER_FIRST_NAME,
                    _X_CLAROLOCAL5 = oPlantillaDat.X_CLAROLOCAL5,
                    _X_FIXED_NUMBER = oPlantillaDat.X_FIXED_NUMBER,
                    _X_CLAROLOCAL4 = oPlantillaDat.X_CLAROLOCAL4,
                    _X_INTER_1 = oPlantillaDat.X_INTER_1,
                    _X_EMAIL = oPlantillaDat.X_EMAIL,
                    _X_MARITAL_STATUS = oPlantillaDat.X_MARITAL_STATUS,
                    _X_CONTACT_SEX = oPlantillaDat.X_CONTACT_SEX,
                    _X_POSITION = oPlantillaDat.X_POSITION,
                    _X_INTER_29 = oPlantillaDat.X_INTER_29,
                    _X_OST_NUMBER = oPlantillaDat.X_OST_NUMBER,
                    _X_OTHER_PHONE = oPlantillaDat.X_OTHER_PHONE,
                    _X_CLAROLOCAL1 = oPlantillaDat.X_CLAROLOCAL1,
                    _X_CLAROLOCAL2 = oPlantillaDat.X_CLAROLOCAL2,
                    _X_FAX = oPlantillaDat.X_FAX,
                    _X_REASON = oPlantillaDat.X_REASON,
                    _X_DEPARTMENT = oPlantillaDat.X_DEPARTMENT,
                    _X_CITY = oPlantillaDat.X_CITY,
                    _X_DISTRICT = oPlantillaDat.X_DISTRICT,
                    _X_ADDRESS = oPlantillaDat.X_ADDRESS,
                    _X_INTER_15 = oPlantillaDat.X_INTER_15,
                    _X_CLAROLOCAL6 = oPlantillaDat.X_CLAROLOCAL6,
                    _X_INTER_16 = oPlantillaDat.X_INTER_16,
                    _NOMBRE_TRANSACCION = oPlantillaDat.NOMBRE_TRANSACCION,
                    _X_CLARO_NUMBER = oPlantillaDat.X_CLARO_NUMBER,
                    _X_FIRST_NAME = oPlantillaDat.X_FIRST_NAME,
                    _X_LAST_NAME = oPlantillaDat.X_LAST_NAME,
                    _X_FLAG_REGISTERED = oPlantillaDat.X_FLAG_REGISTERED

                }
            };



            var objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            //Validacion de contingencia
            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            CommonIFIService.InsertGeneralResponse objInsertGeneralResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.InsertGeneralResponse>(() => { return oCommonIFIService.GetinsertInteractionGeneral(objInsertGeneralRequest); });

            var dictionaryResponse = new Dictionary<string, object>()
            {
                {"rFlagInsercion", objInsertGeneralResponse.rFlagInsercion},
                {"rMsgText", objInsertGeneralResponse.rMsgText},
                {"rResult", objInsertGeneralResponse.rResult.ToString()},
                {"rInteraccionId", objInsertGeneralResponse.rInteraccionId},
            };

            var rInteraccionId = objInsertGeneralResponse.rInteraccionId;


            return dictionaryResponse;

        }
    }
}
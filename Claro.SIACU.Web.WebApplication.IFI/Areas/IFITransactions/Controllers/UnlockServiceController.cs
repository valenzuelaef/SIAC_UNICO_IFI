using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class UnlockServiceController : Controller
    {
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        public readonly PostIFIService.PostIFIServiceClient oPostpaidService = new PostIFIService.PostIFIServiceClient();
        public readonly string vArchivoINTPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");


        /// <summary>inicia la pantalla de desbloqueo de linea y/o equipo</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>Index</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>obtiene permisos de desbloqueo de equipo</summary>
        /// <param name="cadenaOpciones"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetPermisionUnLockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetPermisionUnLockService(string cadenaOpciones)
        {
            bool result = false;
            if (cadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfDesBloquearServicioIFI")) >= 0)
            {
                result = true;
            }
            return Json(new { data = result });
        }


        /// <summary>obtiene la relacion de pariente </summary>
        /// <param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetRelationParient</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetRelationParient(string strIdSession)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();
            try
            {
                lstGenericItem = App_Code.Common.GetXMLList("ListaRelacionTitular");


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }

            return Json(new { data = lstGenericItem });
        }


        /// <summary>obtiene tipo de documentos</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdList"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetDocumentType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetDocumentType(string strIdSession, string strIdList)
        {
            var audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var objResponse = new CommonIFIService.StateTypeResponseCommon();
            try
            {
                var objRequest = new CommonIFIService.StateTypeRequestCommon()
                {
                    audit = audit,
                    IdList = strIdList
                };
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.StateTypeResponseCommon>(
                    () => { return oCommonService.GetStateType(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            return Json(new { data = objResponse });
        }


        /// <summary>guarda la transaccion de desblqoueo</summary>
        /// <param name="objModel"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>SaveTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult SaveTransaction(ServiceLockModel objModel)
        {
            bool blnRetorno;
            string TipoInter = string.Empty;

            string outMsg = string.Empty;
            string strPathFileHPxtream = "", strInteraccionId = "";
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);

            audit.userName = objModel.currentUser;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : SaveTransaction");
            Typification typificationLoad = null;
            string lblMensaje = "";
            switch (objModel.TipoTransaccion)
            {
                case Claro.Constants.NumberOneString: TipoInter = Claro.ConfigurationManager.AppSettings("strTipiUnLockLine"); break;
                case Claro.Constants.NumberTwoString: TipoInter = Claro.ConfigurationManager.AppSettings("strTipiUnLockEquipment"); break;
                case Claro.Constants.NumberThreeString: TipoInter = Claro.ConfigurationManager.AppSettings("strTipiUnLockLineEquipment"); break;

            }
            try
            {
                bool xres = false;
                typificationLoad = LoadTypificationUnLock(audit, TipoInter, ref lblMensaje);

                switch (objModel.TipoTransaccion)
                {
                    case Claro.Constants.NumberOneString: UnLockLine(objModel, audit, out xres); break;
                    case Claro.Constants.NumberTwoString: UnLockEquipment(objModel, audit, out xres); break;
                    case Claro.Constants.NumberThreeString: UnLockLineEquipment(objModel, audit, out xres); break;
                }

                if (xres)
                {
                    //registra interaccion
                    blnRetorno = SaveInteraccion(objModel, ref strInteraccionId, ref strPathFileHPxtream, typificationLoad);
                    if (!string.IsNullOrEmpty(strInteraccionId) && blnRetorno)
                    {//genera la constancia 
                        outMsg = "Se registro satisfactoriamente el Desbloqueo de linea y/o Equipo con el codigo de desbloqueo : " + objModel.codeLock;
                        strPathFileHPxtream = GetConstancyPDF(objModel.idSession, objModel, strInteraccionId, objModel.codeLock);

                        var nombreArchivo = strPathFileHPxtream.Substring(strPathFileHPxtream.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                        Claro.Web.Logging.Info(audit.Session, audit.transaction, "El nombre del archivo es: " + nombreArchivo);

                        var flagFirmaDigital = KEY.AppSettings("strKeyFlagFirmaDigital").ToString();
                        var stateFirmaDigital = flagFirmaDigital == Constants.ZeroNumber ? true : FirmaDigital(objModel, nombreArchivo);

                        if (stateFirmaDigital)
                        {
                        if (objModel.EmailEnvio == Claro.Constants.LetterS)
                        {
                            if (!string.IsNullOrEmpty(strPathFileHPxtream))
                            {
                                if (GetSendEmailUnLock2(objModel, strPathFileHPxtream) == Claro.SIACU.Constants.OK)
                                {
                                    outMsg += " y se envio la constancia por correo";
                                }
                                else
                                {
                                    outMsg += " pero no se pudo enviar la constancia por correo";
                                }

                            }
                            else
                            {
                                outMsg += " pero no se pudo enviar la constancia por correo";
                            }
                        }
                    }
                    else
                    {
                            outMsg += ". " + KEY.AppSettings("strMensajeErrorFirmaDigital");
                            Claro.Web.Logging.Error(audit.Session, audit.transaction, "Ocurrió un error al Generar el certificado digital del documento " + nombreArchivo);
                        }
                    }
                    else
                    {
                        outMsg = "No se pudo registrar satisfactoriamente el Desbloqueo de linea y/o Equipo";
                    }
                }
                else
                {
                    outMsg = "No se pudo registrar el Desbloqueo ";
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return Json(new { outMsg, strPathFileHPxtream });
        }

        /// <summary>envia email desbloqueo</summary>
        /// <param name="objModel"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strRutaArchivo"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetSendEmailUnLock(ServiceLockModel objModel,
                                string strInteraccionId,
                                string strRutaArchivo)
        {

            WirelessIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(objModel.idSession);
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            byte[] attachFile = null;
            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

            if (new CommonServicesController().DisplayFileFromServerSharedFile(objModel.idSession, audit.transaction, strRutaArchivo, out attachFile))
            {
                Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                {
                    strIdSession = objModel.idSession,
                    strTo = objModel.Email,
                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailUnLock", vArchivoINTPOSTConfigMsg),
                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailUnLock", vArchivoINTPOSTConfigMsg),
                    strAttached = strAdjunto,
                    byteAttached = attachFile
                };
                MensajeEmail = GetSendEmailConstUnLock(objSendEmail);
                return Claro.SIACU.Constants.OK;
            }
            else
            {
                return null;
            }

        }

        /// <summary>Método que permite el envio de la constancia por email</summary>
        /// <param name="objModel"></param>  
        /// <param name="strRutaArchivo"></param>  
        /// <returns>string</returns>
        /// <remarks>GetSendEmail2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/03/2019</FecCrea></item></list>
        public string GetSendEmailUnLock2(ServiceLockModel objModel,
                               string strRutaArchivo)
        {

            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);

            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            try
            {
                string strNombreArchivo = string.Empty;
                byte[] attachFile = null;
                string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                if (new CommonServicesController().DisplayFileFromServerSharedFile(objModel.idSession, objAudit.transaction, strRutaArchivo, out attachFile))
                {

                    if (attachFile != null)
                    {
                        List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                        AttachedFile objFile = new AttachedFile();

                        objFile.file = attachFile;
                        objFile.Name = strAdjunto;
                        objFile.HeadBoard = strAdjunto;
                        lstAdjuntos.Add(objFile);

                        objSendEmailWithBase64Request.audit = objAudit;
                        objSendEmailWithBase64Request.Sender = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                        objSendEmailWithBase64Request.To = objModel.Email;
                        objSendEmailWithBase64Request.Subject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailUnLock", vArchivoINTPOSTConfigMsg);
                        objSendEmailWithBase64Request.Message = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailUnLock", vArchivoINTPOSTConfigMsg);
                        objSendEmailWithBase64Request.HtmlFlag = KEY.AppSettings("strHtmlFlagCorreoSB").ToString();

                        if (lstAdjuntos != null && lstAdjuntos.Count > 0)
                        {
                            objSendEmailWithBase64Request.ListAttachedFile = lstAdjuntos;
                        }

                        objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                        {
                            return objSendEmailWithBase64Response = oCommonService.SendEmailWithBase64(objSendEmailWithBase64Request);
                        });
                        if (objSendEmailWithBase64Response != null && objSendEmailWithBase64Response.ResponseCode == Claro.Constants.NumberZeroString)
                        {
                            strResul = Claro.SIACU.Constants.OK;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.idSession, objAudit.transaction, ex.Message);
            }
            return strResul;
        }


        /// <summary>seteo de valores para envio de email</summary>
        /// <param name="objSendEmail"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailConstUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetSendEmailConstUnLock(Model.SendEmailModel objSendEmail)
        {
            CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse = new CommonIFIService.SendEmailResponseCommon();
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objSendEmail.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonIFIService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = ConfigurationManager.AppSettings("CorreoServicioAlCliente"),
                    strTo = objSendEmail.strTo,
                    strSubject = objSendEmail.strSubject,
                    strMessage = TemplateEmailUnLock(objSendEmail.strMessage),
                    strAttached = objSendEmail.strAttached,
                    AttachedByte = objSendEmail.byteAttached
                };
            try
            {
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return oCommonService.GetSendEmailFixed(objGetSendEmailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSendEmail.strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }

            string strResul = string.Empty;

            if (objGetSendEmailResponse.Exit == Claro.SIACU.Constants.OK)
            {
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }
            else
            {
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
            }

            return strResul;
        }


        /// <summary>arma la plantilla de email</summary>
        /// <param name="strMessage"></param>
        /// <returns>string</returns>
        /// <remarks>TemplateEmailUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string TemplateEmailUnLock(string strMessage)
        {
            string strmessage = string.Empty;
            var strHtml = new System.Text.StringBuilder();

            strHtml.Append("<html>");
            strHtml.Append("<head>");
            strHtml.Append("<style type='text/css'>");
            strHtml.Append("<!--");
            strHtml.Append(".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}");
            strHtml.Append(".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}");
            strHtml.Append("-->");
            strHtml.Append("</style>");
            strHtml.Append("<body>");
            strHtml.Append("<table width='100%' border='0' cellpadding='0' cellspacing='0'>");
            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strEstimadoCliente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + " </td></tr>");

            strHtml.Append("<tr><td width='180' class='Estilo1' height='22'>");
            strHtml.Append(strMessage);
            strHtml.Append("</td></tr>");

            strHtml.Append("<tr>");
            strHtml.Append("<td align='center'>");
            strHtml.Append("</td></tr>");

            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>&nbsp;</td></tr>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgCordialmente", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>");
            strHtml.Append("<tr><td class='Estilo1'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailFirma", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>");

            strHtml.Append("<tr><td height='10'></td>");
            strHtml.Append("<tr><td height='10'></td>");

            strHtml.Append("</table>");
            strHtml.Append("</body>");
            strHtml.Append("</html>");

            return strHtml.ToString();

        }


        /// <summary>genera constancia</summary>
        /// <param name="strIdSession"></param>
        /// <param name="objModel"></param>
        /// <param name="strInteraction"></param>
        /// <param name="strCode"></param>
        /// <returns>string</returns>
        /// <remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetConstancyPDF(string strIdSession, ServiceLockModel objModel, string strInteraction, string strCode)
        {
            string NAME_PDF = string.Empty;


            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            try
            {

                CommonIFIService.ParametersGeneratePDF oParameter = new CommonIFIService.ParametersGeneratePDF()
                {
                    //SOLICITUD_DESBLOQUEO_LINEA_EQUIPO
                    StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreTransaccionDesbloqueoIFI"),
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaDesbloqueoIFI"),
                    StrCentroAtencionArea = objModel.cac,
                    StrIdInteraccion = strInteraction,
                    StrCasoInter = strInteraction,
                    StrRepresLegal = objModel.LegalRep,
                    StrNroServicio = objModel.msisdn,
                    StrTipoDocIdentidad = objModel.TypeDocument,
                    StrNroDocIdentidad = objModel.nroDocumento,
                    strCodigoBloqueo = strCode,
                    strSolicitante = objModel.Solicitante,
                    strMotivo = objModel.TipoTransaccion == Claro.Constants.NumberOneString ? objModel.TipoBloqueo : objModel.TipoTransaccion == Claro.Constants.NumberTwoString ? objModel.TipoBloqueoEquipo : objModel.TipoBloqueo,
                    strSolicitanteNombre = objModel.strSolicitanteNombre,
                    strSolicitanteApellido = objModel.strSolicitanteApellido,
                    strSolicitanteTipoDoc = objModel.strSolicitanteTipoDoc,
                    strSolicitanteNroDoc = objModel.strSolicitanteNroDoc,
                    strSolicitanteNro = objModel.TelefonoReference,
                    strLinea = objModel.msisdn,
                    StrImei = objModel.IMEI,
                    strMarcaModelo = objModel.Marca,
                    strEnvioCorreo = objModel.EmailEnvio,
                    strCorreoCliente = objModel.Email,
                    strFlagBloqueo = objModel.TipoTransaccion,
                    strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital"),
                    StrCodUsuario = objModel.currentUser,
                    StrNombreAgenteUsuario = objModel.fullNameUser,
                    StrTitularCliente = objModel.StrTitularCliente,
                    strFirmaDigitalHuella = objModel.strFirmaDigitalHuella,
                    strHuellaEncode = objModel.strHuellaEncode,
                    strHuellaFechaHora = DateTime.Today.ToString("dd/MM/yyyy"),
                    strHuellaMinucia = objModel.strHuellaMinucia,
                    strHuellaNomApeCli = objModel.strHuellaNomApeCli,
                    strHuellaNroDoc = objModel.strHuellaNroDoc,
                    strHuellaTipDoc = objModel.strHuellaTipDoc,
                   

                };
                if (oParameter.strSolicitante == "1") oParameter.strSolicitante = "Titular";
                if (oParameter.strSolicitante == "2") oParameter.strSolicitante = "Carta Poder";
                if (oParameter.strSolicitante == "3") oParameter.strSolicitante = "RR/LL";

                CommonIFIService.GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "NAME_PDF:" + NAME_PDF);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }


        /// <summary>desbloqueo de linea y equipo</summary>
        /// <param name="objModel"></param>
        /// <param name="auditCommon"></param>
        /// <param name="xres"></param>
        /// <remarks>UnLockLineEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public void UnLockLineEquipment(ServiceLockModel objModel, CommonIFIService.AuditRequest auditCommon, out bool xres)
        {
            xres = false;
            UnLockLine(objModel, auditCommon, out xres);
            if (xres)
            {
                xres = false;
                UnLockEquipment(objModel, auditCommon, out xres);
            }
        }


        /// <summary>desbloqueo de equipo</summary>
        /// <param name="objModel"></param>
        /// <param name="auditCommon"></param>
        /// <param name="xres"></param>
        /// <remarks>UnLockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public void UnLockEquipment(ServiceLockModel objModel, CommonIFIService.AuditRequest auditCommon, out bool xres)
        {
            xres = false;
            int resTerminal;
            string strCode = string.Empty;
            PostIFIService.AuditRequest auditPost = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(objModel.idSession);
            auditPost.userName = objModel.currentUser;
            LockUnlockEquipmentRequestIFI objLockUnlockEquipmentRequestIFI = new LockUnlockEquipmentRequestIFI()
            {
                audit = auditPost,
                Imei = objModel.IMEI
            };
            LockUnlockEquipmentResponseIFI objResponse =
                                           Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponseIFI>(
                                          () =>
                                          {
                                              return oPostpaidService.GetEquipmentUnLock(objLockUnlockEquipmentRequestIFI);
                                          });

            if (objResponse.Result)
            {
                strCode = GetSequenceCode(auditCommon, true);
                objModel.codeLock = strCode;
                if (!string.IsNullOrEmpty(strCode))
                {
                    PostIFIService.UpdateUnlockEquipmentCodeRequestIFI objRequest = new UpdateUnlockEquipmentCodeRequestIFI()
                    {
                        audit = auditPost,
                        codeUnlock = strCode,
                        objLock = new Lock()
                        {
                            _BLO_CUENTA = objModel.cuenta,
                            _BLO_TELEFONO = objModel.msisdn,
                            _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                            _BLO_USUARIO = auditPost.userName,
                            _BLO_AUTORIZA = "",
                            _BLO_ESTADO = "DESBLOQUEADO",
                            _BLO_TIPO = objModel.CodigoBloqueo,
                            _BLO_CODIGO = strCode,
                            _BLO_IMEI = objModel.IMEI

                        },

                    };
                    bool resultPer = UpdateUnlockEquipmentCode(objRequest);
                    xres = resultPer;
                    if (!resultPer)
                    {
                        xres = false;
                        LockUnlockEquipmentRequestIFI objLockUnlockEquipmentRequestIFI2 = new LockUnlockEquipmentRequestIFI()
                        {
                            audit = auditPost,
                            Imei = objModel.IMEI
                        };
                        LockUnlockEquipmentResponseIFI objResponse2 =
                                                       Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponseIFI>(
                                                      () =>
                                                      {
                                                          return oPostpaidService.GetEquipmentLock(objLockUnlockEquipmentRequestIFI2);
                                                      });
                        if (objResponse2.Result) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el Rollback del desbloqueo de equipo");
                        else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el Rollback del desbloqueo de equipo");
                    }
                    else
                    {
                        //made13
                        resTerminal = new CommonServicesController().InsertTerminalLockUnlockEquipment(false, auditPost, objModel);
                        if (resTerminal > 0) { xres = true; }
                        else
                        {
                            xres = false;
                            UpdateUnlockEquipmentRequestIFI objUpdateUnlockEquipmentRequestIFI = new UpdateUnlockEquipmentRequestIFI()
                            {
                                audit = auditPost,

                                objLock = new Lock()
                                {
                                    _BLO_CUENTA = objModel.cuenta,
                                    _BLO_TELEFONO = objModel.msisdn,
                                    _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                                    _BLO_USUARIO = auditPost.userName,
                                    _BLO_AUTORIZA = "",
                                    _BLO_ESTADO = "DESBLOQUEADO",
                                    _BLO_TIPO = objModel.CodigoBloqueo,
                                    _BLO_CODIGO = strCode,
                                    _BLO_IMEI = objModel.IMEI

                                },
                            };
                            UpdateUnlockEquipmentResponseIFI objResponseUpd =
                                                          Claro.Web.Logging.ExecuteMethod<UpdateUnlockEquipmentResponseIFI>(
                                                         () =>
                                                         {
                                                             return oPostpaidService.UpdateUnlockEquipment(objUpdateUnlockEquipmentRequestIFI);
                                                         });

                            if (objResponseUpd.resul) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el UpdateUnlockEquipment del desbloqueo de equipo");
                            else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el UpdateUnlockEquipment del desbloqueo de equipo");
                            LockUnlockEquipmentRequestIFI objLockUnlockEquipmentRequestIFI2 = new LockUnlockEquipmentRequestIFI()
                            {
                                audit = auditPost,
                                Imei = objModel.IMEI
                            };
                            LockUnlockEquipmentResponseIFI objResponse2 =
                                                           Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponseIFI>(
                                                          () =>
                                                          {
                                                              return oPostpaidService.GetEquipmentLock(objLockUnlockEquipmentRequestIFI2);
                                                          });
                            if (objResponse2.Result) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el Rollback del desbloqueo de equipo");
                            else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el Rollback del desbloqueo de equipo");
                        }
                    }
                }
                else
                {
                    xres = false;
                    LockUnlockEquipmentRequestIFI objLockUnlockEquipmentRequestIFI2 = new LockUnlockEquipmentRequestIFI()
                    {
                        audit = auditPost,
                        Imei = objModel.IMEI
                    };
                    LockUnlockEquipmentResponseIFI objResponse2 =
                                                   Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponseIFI>(
                                                  () =>
                                                  {
                                                      return oPostpaidService.GetEquipmentLock(objLockUnlockEquipmentRequestIFI2);
                                                  });
                    if (objResponse2.Result) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el Rollback del 2desbloqueo de equipo");
                    else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el Rollback del 2desbloqueo de equipo");
                }

            }
            else
            {
                Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el desbloqueo de equipo");
                xres = false;
            }

        }


        /// <summary>actualizar desbloqueo de equipo codigo</summary>
        /// <param name="objRequest"></param>
        /// <returns>bool</returns>
        /// <remarks>UpdateUnlockEquipmentCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public bool UpdateUnlockEquipmentCode(PostIFIService.UpdateUnlockEquipmentCodeRequestIFI objRequest)
        {
            UpdateUnlockEquipmentCodeResponseIFI objResponse =
            Claro.Web.Logging.ExecuteMethod<UpdateUnlockEquipmentCodeResponseIFI>(
                () => { return oPostpaidService.UpdateUnlockEquipmentCode(objRequest); });
            return objResponse.resul;
        }


        /// <summary>desbloqueo de linea</summary>
        /// <param name="objModel"></param>
        /// <param name="auditCommon"></param>
        /// <param name="xres"></param>
        /// <remarks>UnLockLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public void UnLockLine(ServiceLockModel objModel, CommonIFIService.AuditRequest auditCommon, out bool xres)
        {
            xres = false;
            string strCode = string.Empty;
            PostIFIService.AuditRequest auditPost = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(objModel.idSession);
            auditPost.userName = objModel.currentUser;
            Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "Ingreso metodo: UnLockLine");
            Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El codigo de desbloqueo es: " + strCode);
            strCode = GetSequenceCode(auditCommon, true);
            objModel.codeLock = strCode;
            if (!string.IsNullOrEmpty(strCode))
            {
                PostIFIService.UpdateUnlockLineCodeRequestIFI objRequest = new UpdateUnlockLineCodeRequestIFI()
                {
                    audit = auditPost,
                    codeUnlock = strCode,
                    objLock = new Lock()
                    {
                        _BLO_CUENTA = objModel.cuenta,
                        _BLO_TELEFONO = objModel.msisdn,
                        _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                        _BLO_USUARIO = auditPost.userName,
                        _BLO_AUTORIZA = "",
                        _BLO_ESTADO = "DESBLOQUEADO",
                        _BLO_TIPO = objModel.CodigoBloqueo,
                        _BLO_CODIGO = strCode
                    },

                };
                bool resultPer = UpdateUnLockLineCode(objRequest);
                Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El resultado del metodo UpdateUnLockLineCode es: " + resultPer);
                xres = resultPer;
                if (resultPer)
                {
                    UnlockServiceRequest objServiceLockRequest = new UnlockServiceRequest()
               {
                   audit = auditPost,
                   coId = objModel.contractId,
                   objIsOkGetKeyRequest = new PostIFIService.IsOkGetKeyRequestCommon()
                   {
                       audit = auditPost,

                   },
                   ticklerCode = objModel.CodigoBloqueo

               };
                    UnlockServiceResponse objResponse =
                                                   Claro.Web.Logging.ExecuteMethod<UnlockServiceResponse>(
                                                  () =>
                                                  {
                                                      return oPostpaidService.GetUnlockService(objServiceLockRequest);
                                                  });
                    Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El resultado del metodo GetUnlockService es: " + objResponse.resul);
                    xres = objResponse.resul;
                    if (objResponse.resul)
                    {
                        PostIFIService.UpdateUnlockLineCodeRequestIFI objRequest2 = new UpdateUnlockLineCodeRequestIFI()
                        {
                            audit = auditPost,
                            codeUnlock = strCode,
                            objLock = new Lock()
                            {
                                _BLO_CUENTA = objModel.cuenta,
                                _BLO_TELEFONO = objModel.msisdn,
                                _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                                _BLO_USUARIO = auditPost.userName,
                                _BLO_AUTORIZA = "",
                                _BLO_ESTADO = "DESBLOQUEADO",
                                _BLO_TIPO = objModel.CodigoBloqueo,
                                _BLO_CODIGO = strCode
                            },

                        };
                        bool resultPer2 = UpdateUnLockLineCode(objRequest2);
                        Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El resultado del metodo2 UpdateUnLockLineCode es: " + resultPer2);

                        xres = resultPer2;
                    }
                    else
                    {
                        xres = false;
                        UpdateUnlockLineRollbackRequestIFI objUpdateUnlockLineRollbackRequestIFI = new UpdateUnlockLineRollbackRequestIFI()
                        {
                            audit = auditPost,
                            objLock = new Lock()
                            {
                                _BLO_CUENTA = objModel.cuenta,
                                _BLO_TELEFONO = objModel.msisdn,
                                _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                                _BLO_USUARIO = auditPost.userName,
                                _BLO_AUTORIZA = "",
                                _BLO_ESTADO = "DESBLOQUEADO",
                                _BLO_TIPO = objModel.CodigoBloqueo,
                                _BLO_CODIGO = strCode
                            }
                        };
                        UpdateUnlockLineRollbackResponseIFI objUpdateUnlockLineRollbackResponseIFI =
                                                  Claro.Web.Logging.ExecuteMethod<UpdateUnlockLineRollbackResponseIFI>(
                                                 () =>
                                                 {
                                                     return oPostpaidService.UpdateUnlockLineRollback(objUpdateUnlockLineRollbackRequestIFI);
                                                 });
                        if (objUpdateUnlockLineRollbackResponseIFI.resul) Claro.Web.Logging.Info(objUpdateUnlockLineRollbackRequestIFI.audit.Session, objUpdateUnlockLineRollbackRequestIFI.audit.transaction, "se realizó el Rollback correctamente");
                        else Claro.Web.Logging.Info(objUpdateUnlockLineRollbackRequestIFI.audit.Session, objUpdateUnlockLineRollbackRequestIFI.audit.transaction, "ocurrío un error al realizar el Rollback");

                    }
                }
                else
                {
                    Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "El codigo de desbloqueo no se inserto en el metodo: UpdateUnLockLineCode ");

                    xres = resultPer;
                }

            }
            else
            {
                Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "El codigo de desbloqueo no se genero ");

                xres = false;
            }


        }


        /// <summary>actualizacion de desbloqueo de linea de codigo</summary>
        /// <param name="objRequest"></param>
        /// <returns>bool</returns>
        /// <remarks>UpdateUnLockLineCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public bool UpdateUnLockLineCode(PostIFIService.UpdateUnlockLineCodeRequestIFI objRequest)
        {
            UpdateUnlockLineCodeResponseIFI objResponse =
            Claro.Web.Logging.ExecuteMethod<UpdateUnlockLineCodeResponseIFI>(
                () => { return oPostpaidService.UpdateUnlockLineCode(objRequest); });
            return objResponse.resul;
        }


        /// <summary>carga tipificacion desbloqueo</summary>
        /// <param name="audit"></param>
        /// <param name="Transaction_Name"></param>
        /// <param name="lblMensaje"></param>
        /// <returns>Typification</returns>
        /// <remarks>LoadTypificationUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public Typification LoadTypificationUnLock(CommonIFIService.AuditRequest audit, string Transaction_Name,
                                                           ref string lblMensaje)
        {
            Typification oTypification = null;


            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypificationUnLock");


            try
            {
                TypificationRequest objTypificationRequest = new TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = Transaction_Name;
                objTypificationRequest.audit = audit;

                TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<TypificationResponse>(
                    () => { return oCommonService.GetTypification(objTypificationRequest); });




                oTypification = objResponse.ListTypification.First();


                if (oTypification == null)
                {
                    lblMensaje = "No se cargo las tipificaciones";
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, lblMensaje);

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = "No se cargo las tipificaciones";
            }

            return oTypification;
        }


        /// <summary>obtiene secuencia</summary>
        /// <param name="audit"></param>
        /// <param name="isflagUnlock"></param>
        /// <returns>string</returns>
        /// <remarks>GetSequenceCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetSequenceCode(CommonIFIService.AuditRequest audit, bool isflagUnlock)
        {

            SequenceCodeRequest objSequenceCodeRequest = new SequenceCodeRequest()
            {
                audit = audit,
                isFlagUnlock = isflagUnlock
            };
            SequenceCodeResponse objResponse =
             Claro.Web.Logging.ExecuteMethod<SequenceCodeResponse>(
                 () => { return oCommonService.GetSequenceCode(objSequenceCodeRequest); });
            return objResponse.code;
        }


        #region Guardar Interaccion
        /// <summary>guarda interaccion desbloqueo</summary>
        /// <param name="objModel"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strPathFileHPxtream"></param>
        /// <param name="objTypification"></param>
        /// <returns>bool</returns>
        /// <remarks>SaveInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public bool SaveInteraccion(ServiceLockModel objModel, ref string strInteraccionId, ref string strPathFileHPxtream, Typification objTypification)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);
            objModel.idTransaccion = audit.transaction;

            audit.userName = objModel.currentUser;
            string strUSUARIO_SISTEMA = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
            string strUSUARIO_APLICACION = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strPASSWORD_USUARIO = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            var oInteraction = DataInteraction(objModel, objTypification);

            if (oInteraction == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(objTypification.CLASE))
            {
                return false;
            }

            var oTemplateInteraction = DataTemplateInteraction(objModel);

            var resultInteraction = InsertInterac(
                    oInteraction,
                    oTemplateInteraction,
                    objModel.msisdn,
                    strUSUARIO_SISTEMA,
                    strUSUARIO_APLICACION,
                    strPASSWORD_USUARIO,
                    true,
                    objModel.idSession,
                    objModel.customerId);

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




            return true;
        }


        /// <summary>inserta interaccion</summary>
        /// <param name="objInteractionModel"></param>
        /// <param name="oPlantillaDat"></param>
        /// <param name="strNroTelephone"></param>
        /// <param name="strUserSession"></param>
        /// <param name="strUserAplication"></param>
        /// <param name="strPassUser"></param>
        /// <param name="boolEjecutTransaction"></param>
        /// <param name="strIdSession"></param>
        /// <param name="strCustomerId"></param>
        /// <returns>CommonIFIService.InsertGeneralResponse</returns>
        /// <remarks>InsertInterac</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public CommonIFIService.InsertGeneralResponse InsertInterac(CommonIFIService.Iteraction objInteractionModel, InsertTemplateInteraction oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            CommonIFIService.InsertGeneralRequest objRequest;
            CommonIFIService.InsertGeneralResponse objResult = null;
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            try
            {
                objRequest = new CommonIFIService.InsertGeneralRequest()
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

                objResult = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oCommonService.GetinsertInteractionGeneral(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return objResult;
        }


        /// <summary>setea datos interaccion</summary>
        /// <param name="objModel"></param>
        /// <param name="objTypification"></param>
        /// <returns>CommonIFIService.Iteraction</returns>
        /// <remarks>DataInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public CommonIFIService.Iteraction DataInteraction(ServiceLockModel objModel, Typification objTypification)
        {
            var responseModel = new CommonIFIService.Iteraction();
            try
            {
                var tipo = ConfigurationManager.AppSettings("gConstTipoINT");
                responseModel.START_DATE = DateTime.UtcNow.ToString("dd/MM/yyyy");
                responseModel.TELEFONO = objModel.msisdn;
                responseModel.TIPO = objTypification.TIPO;
                responseModel.CLASE = objTypification.CLASE;
                responseModel.SUBCLASE = objTypification.SUBCLASE;
                responseModel.TIPO_CODIGO = objTypification.TIPO_CODE;
                responseModel.CLASE_CODIGO = objTypification.CLASE_CODE;
                responseModel.SUBCLASE_CODIGO = objTypification.SUBCLASE_CODE;
                responseModel.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                responseModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                responseModel.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                responseModel.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                responseModel.NOTAS = objModel.notes;
                responseModel.FLAG_CASO = Claro.Constants.NumberZeroString;
                responseModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                responseModel.AGENTE = objModel.currentUser;
                responseModel.CONTRACT = objModel.contractId;
                responseModel.PLANO = objModel.codPlano ?? string.Empty;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.idSession, objModel.idTransaccion, ex.Message);
            }

            return responseModel;
        }


        /// <summary>setea plantilla iteraccion</summary>
        /// <param name="objModel"></param>
        /// <returns>CommonIFIService.InsertTemplateInteraction</returns>
        /// <remarks>DataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public CommonIFIService.InsertTemplateInteraction DataTemplateInteraction(ServiceLockModel objModel)
        {
            var responseModel = new CommonIFIService.InsertTemplateInteraction();
            var strImporte = string.Empty;
            try
            {
                responseModel._X_INTER_29 = objModel.codeLock;
                //DesBloqueo de Linea
                if (objModel.TipoTransaccion == Claro.Constants.NumberOneString)
                {
                    responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strNombreTransaccionDesbloqueoLinea");
                    responseModel._X_NAME_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_DNI_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_BIRTHDAY = DateTime.Now;

                    //datos de la transaccion
                    responseModel._X_INTER_5 = KEY.AppSettings("strNombreDesbloqueoLinea");
                    responseModel._X_INTER_6 = objModel.Solicitante;
                    responseModel._X_FIRST_NAME = objModel.firstName;
                    responseModel._X_LAST_NAME = objModel.LastName;
                    responseModel._X_INTER_4 = objModel.TipoBloqueo;
                    responseModel._X_REASON = ((objModel.CodigoBloqueo != null) && (objModel.CodigoBloqueo.Length >= 8)) ? objModel.CodigoBloqueo.Substring(0, 8) : "";
                    responseModel._X_INTER_7 = objModel.TelefonoReference;
                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;
                    responseModel._X_TYPE_DOCUMENT = objModel.TypeDocument;
                    //datos de la linea y/o equipo
                    responseModel._X_CLARO_NUMBER = objModel.msisdn;
                    responseModel._X_IMEI = objModel.IMEI;
                    responseModel._X_INTER_21 = objModel.Marca;
                    responseModel._X_EMAIL = objModel.Email;
                    responseModel._X_EMAIL_CONFIRMATION = objModel.EmailEnvio;
                    responseModel._X_INTER_2 = objModel.cac;
                    responseModel._X_INTER_20 = objModel.notes;

                }
                //DesBloqueo de Equipo
                if (objModel.TipoTransaccion == Claro.Constants.NumberTwoString)
                {
                    responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strNombreTransaccionDesbloqueoEquipo");

                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;

                    responseModel._X_NAME_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_DNI_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_BIRTHDAY = DateTime.Now;

                    //datos de la transaccion
                    responseModel._X_INTER_5 = KEY.AppSettings("strNombreDesbloqueoEquipo");
                    responseModel._X_INTER_6 = objModel.Solicitante;
                    responseModel._X_FIRST_NAME = objModel.firstName;
                    responseModel._X_LAST_NAME = objModel.LastName;
                    responseModel._X_INTER_4 = objModel.TipoBloqueoEquipo;
                    responseModel._X_REASON = objModel.CodigoBloqueoEquipo.Length >= 8 ? objModel.CodigoBloqueoEquipo.Substring(0, 8) : "";
                    responseModel._X_INTER_7 = objModel.TelefonoReference;
                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;
                    responseModel._X_TYPE_DOCUMENT = objModel.TypeDocument;
                    //datos de la linea y/o equipo
                    responseModel._X_CLARO_NUMBER = objModel.msisdn;
                    responseModel._X_IMEI = objModel.IMEI;
                    responseModel._X_INTER_21 = objModel.Marca;
                    responseModel._X_EMAIL = objModel.Email;
                    responseModel._X_EMAIL_CONFIRMATION = objModel.EmailEnvio;
                    responseModel._X_INTER_2 = objModel.cac;
                    responseModel._X_INTER_20 = objModel.notes;

                }
                //DesBloqueo de Linea y Equipo
                if (objModel.TipoTransaccion == Claro.Constants.NumberThreeString)
                {
                    responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strNombreTransaccionDesbloqueoLineaEquipo");

                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;

                    responseModel._X_NAME_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_DNI_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_BIRTHDAY = DateTime.Now;

                    //datos de la transaccion
                    responseModel._X_INTER_5 = KEY.AppSettings("strNombreDesbloqueoLineaEquipo");
                    responseModel._X_INTER_6 = objModel.Solicitante;
                    responseModel._X_FIRST_NAME = objModel.firstName;
                    responseModel._X_LAST_NAME = objModel.LastName;
                    responseModel._X_INTER_4 = objModel.TipoBloqueo;
                    responseModel._X_REASON = objModel.CodigoBloqueo.Length >= 8 ? objModel.CodigoBloqueo.Substring(0, 8) : "";
                    responseModel._X_INTER_7 = objModel.TelefonoReference;
                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;
                    responseModel._X_TYPE_DOCUMENT = objModel.TypeDocument;
                    //datos de la linea y/o equipo
                    responseModel._X_CLARO_NUMBER = objModel.msisdn;
                    responseModel._X_IMEI = objModel.IMEI;
                    responseModel._X_INTER_21 = objModel.Marca;
                    responseModel._X_EMAIL = objModel.Email;
                    responseModel._X_EMAIL_CONFIRMATION = objModel.EmailEnvio;
                    responseModel._X_INTER_2 = objModel.cac;
                    responseModel._X_INTER_20 = objModel.notes;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.idSession, objModel.idSession, ex.Message);
            }

            return responseModel;
        }
        #endregion

        #region FirmaDigital
        /// <summary>Método que genera el certificado digital a un documento existente</summary>
        /// <param name="objModel"></param>   
        /// <param name="nombreArchivo">nombre de archivo pdf</param> 
        /// <returns>bool</returns>
        /// <remarks>FirmarDocumento</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/04/2019</FecCrea></item></list>
        public bool FirmaDigital(ServiceLockModel objModel, string nombreArchivo)
        {
            bool resp = false;
            var objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);

            try
            {
                CommonIFIService.DigitalSignatureResponse objDigitalSignatureResponse = null;
                CommonIFIService.DigitalSignatureRequest objDigitalSignatureRequest = new CommonIFIService.DigitalSignatureRequest()
                {
                    audit = objAudit,
                    NombreArchivo = nombreArchivo,
                    ContenidoArchivo = String.Empty,
                    CanalAtencion = "CAC",
                    CodigoPDV = objModel.strIdCac,
                    NombrePDV = objModel.cac,
                    NumeroDocumento = objModel.nroDocumento,
                    TipoDocumento = objModel.TypeDocument,
                    NumeroArchivo = Claro.Constants.NumberOneString

                };
                objDigitalSignatureResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.DigitalSignatureResponse>(() =>
                {
                    return oCommonService.FirmarDocumento(objDigitalSignatureRequest);
                });
                if (objDigitalSignatureResponse.ResponseStatus != null && objDigitalSignatureResponse.ResponseStatus.CodigoRespuesta == Claro.Constants.NumberZeroString)
                {
                    resp = true;
                    Claro.Web.Logging.Info(objAudit.Session, objAudit.transaction, "Se generó con éxito el certificado digital del documento " + nombreArchivo);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.transaction, ex.Message);
            }

            return resp;
        }
        #endregion
    }
}
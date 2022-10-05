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
    public class ServiceLockController : Controller
    {
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        public readonly PostIFIService.PostIFIServiceClient oPostpaidService = new PostIFIService.PostIFIServiceClient();
        public readonly string vArchivoINTPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");

        /// <summary>Método que abre a pantallla de bloqueo linea y/o equipo</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>Index</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>Método que valida permiso de bloqueo de linea</summary>
        /// <param name="cadenaOpciones"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetPermisionLockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetPermisionLockService(string cadenaOpciones)
        {
            bool result = false;
            if (cadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfBloquearServicioIFI")) >= 0)
            {
                result = true;

            }

            return Json(new { data = result });
        }


        /// <summary>Método que obtiene el tipo de customer id</summary>
        /// <param name="strCustomerType"></param>
        /// <param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetCodeCustomerType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public JsonResult GetCodeCustomerType(string strCustomerType, string strIdSession)
        {
            string strCodeCustomerType = string.Empty;
            string strConstBusiness = ConfigurationManager.AppSettings("ConstBusiness");
            string strConstConsumer = ConfigurationManager.AppSettings("ConstConsumer");
            string strConstB2E = ConfigurationManager.AppSettings("ConstB2E");
            string strConstEmpleadoClaro = ConfigurationManager.AppSettings("ConstEmpleadoClaro");
            string strConstPrepago = ConfigurationManager.AppSettings("ConstPrepago");

            if (strCustomerType.ToUpper() == strConstBusiness)
            {
                strCodeCustomerType = ConfigurationManager.AppSettings("ConstCodBusiness");
            }
            else if (strCustomerType.ToUpper() == strConstConsumer)
            {
                strCodeCustomerType = ConfigurationManager.AppSettings("ConstCodConsumer");
            }
            else if (strCustomerType.ToUpper() == strConstB2E)
            {
                strCodeCustomerType = ConfigurationManager.AppSettings("ConstCodB2E");
            }
            else if (strCustomerType.ToUpper() == strConstEmpleadoClaro)
            {
                strCodeCustomerType = ConfigurationManager.AppSettings("ConstCodEmpleadoClaro");
            }
            else if (strCustomerType.ToUpper() == strConstPrepago)
            {
                strCodeCustomerType = ConfigurationManager.AppSettings("ConstCodPrepago");
            }

            return Json(new { data = strCodeCustomerType });
        }


        /// <summary>Método que obtiene codigo de modalidad</summary>
        /// <param name="strModality"></param>
        /// <param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetCodeModality</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetCodeModality(string strModality, string strIdSession)
        {
            string strCodeModality = string.Empty;
            string strConstCorporativo = ConfigurationManager.AppSettings("ConstCorporativo");
            string strConstMasivo = ConfigurationManager.AppSettings("ConstMasivo");


            if (strModality.ToUpper() == strConstCorporativo)
            {
                strCodeModality = ConfigurationManager.AppSettings("ConstCodCorporativo");
            }
            else if (strModality.ToUpper() == strConstMasivo)
            {
                strCodeModality = ConfigurationManager.AppSettings("ConstCodMasivo");
            }


            return Json(new { data = strCodeModality });
        }


        /// <summary>Método que valida preguntas de seguridad</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strCodeCustomerType"></param>
        /// <param name="strCodeModality"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>TestSecurity</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult TestSecurity(string strIdSession, string strCodeCustomerType, string strCodeModality)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();
            Claro.Web.Logging.Info(strIdSession, "Transaccion: Bloqueo De Servicio", "Inicio Método : TestSecurity");
            var objResponse = new PostIFIService.TestSecurityIFIResponse();
            try
            {
                PostIFIService.TestSecurityIFIRequest objRequest = new PostIFIService.TestSecurityIFIRequest()
                {
                    audit = audit,
                    strGroupCustomer = strCodeModality,
                    strTypeCustomer = strCodeCustomerType
                };
                objResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.TestSecurityIFIResponse>(
                    () => { return oPostpaidService.GetTestSecurity(objRequest); });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Error : " + ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, "Transaccion: Bloqueo De Servicio", "fin Método : TestSecurity");
            return Json(new { data = objResponse });
        }

        /// <summary>Método que obtiene preguntas de seguridad</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>QuestionSecurity</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public ActionResult QuestionSecurity()
        {
            return View();
        }


        /// <summary>Método que obtiene relacion de parientes</summary>
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
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Inicio Método : GetRelationParient");
            try
            {
                lstGenericItem = App_Code.Common.GetXMLList("ListaRelacionTitular");


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Error : " + ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "fin Método : GetRelationParient");
            return Json(new { data = lstGenericItem });
        }


        /// <summary>Método que obtiene tipo de documentos</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdList"></param>
        /// <returns></returns>
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


        /// <summary>Método que guarda transaccion bloqueo</summary>
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

            Typification typificationLoad = null;
            string lblMensaje = "";
            switch (objModel.TipoTransaccion)
            {
                case Claro.Constants.NumberOneString: TipoInter = Claro.ConfigurationManager.AppSettings("strTipiLockLine"); break;
                case Claro.Constants.NumberTwoString: TipoInter = Claro.ConfigurationManager.AppSettings("strTipiLockEquipment"); break;
                case Claro.Constants.NumberThreeString: TipoInter = Claro.ConfigurationManager.AppSettings("strTipiLockLineEquipment"); break;

            }
            try
            {
                bool xres = false;
                typificationLoad = LoadTypificationLock(audit, TipoInter, ref lblMensaje);




                switch (objModel.TipoTransaccion)
                {
                    case Claro.Constants.NumberOneString: LockLine(objModel, audit, out xres); break;
                    case Claro.Constants.NumberTwoString: LockEquipment(objModel, audit, out xres); break;
                    case Claro.Constants.NumberThreeString: LockLineEquipment(objModel, audit, out xres); break;

                }



                if (xres)
                {//inserta interaccion

                    blnRetorno = SaveInteraccion(objModel, ref strInteraccionId, ref strPathFileHPxtream, typificationLoad);
                    if (!string.IsNullOrEmpty(strInteraccionId) && blnRetorno)
                    {//genera la constancia 
                        outMsg = "Se registro satisfactoriamente el bloqueo de linea y/o Equipo con el codigo de bloqueo : " + objModel.codeLock;
                        strPathFileHPxtream = GetConstancyPDF(objModel.idSession, objModel, strInteraccionId, objModel.codeLock);


                        if (objModel.EmailEnvio == Claro.Constants.LetterS)
                        {
                            if (!string.IsNullOrEmpty(strPathFileHPxtream))
                            {
                                if (GetSendEmailLock2(objModel, strPathFileHPxtream) == Claro.SIACU.Constants.OK)
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
                        outMsg = "No se pudo registrar satisfactoriamente el bloqueo de linea y/o Equipo";
                    }


                }
                else
                {
                    outMsg = "No se pudo registrar el bloqueo ";
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            return Json(new { outMsg, strPathFileHPxtream });
        }


        /// <summary>Método que envia email bloqueo</summary>
        /// <param name="objModel"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strRutaArchivo"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetSendEmailLock(ServiceLockModel objModel,
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
                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailLock", vArchivoINTPOSTConfigMsg),
                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailLock", vArchivoINTPOSTConfigMsg),
                    strAttached = strAdjunto,
                    byteAttached = attachFile
                };
                MensajeEmail = GetSendEmailConstLock(objSendEmail);
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
        public string GetSendEmailLock2(ServiceLockModel objModel,
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
                        objSendEmailWithBase64Request.Subject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailLock", vArchivoINTPOSTConfigMsg);
                        objSendEmailWithBase64Request.Message = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailLock", vArchivoINTPOSTConfigMsg);
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

        /// <summary>Méetodo que envia email bloqueo con adjunto</summary>
        /// <param name="objSendEmail"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailConstLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetSendEmailConstLock(Model.SendEmailModel objSendEmail)
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
                    strMessage = TemplateEmailLock(objSendEmail.strMessage),
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


        /// <summary>Método que arma plantilla bloqueo</summary>
        /// <param name="strMessage"></param>
        /// <returns>string</returns>
        /// <remarks>TemplateEmailLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string TemplateEmailLock(string strMessage)
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


        /// <summary>Méetodo que genera constancia</summary>
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

            Claro.Web.Logging.Info(strIdSession, "", "IN GetConstancyPDF- HPXTREAM ");
            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            try
            {

                CommonIFIService.ParametersGeneratePDF oParameter = new CommonIFIService.ParametersGeneratePDF()
                {
                    //SOLICITUD_BLOQUEO_LINEA_EQUIPO
                    StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreTransaccionBloqueoIFI"),
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaBloqueoIFI"),
                    StrCentroAtencionArea = objModel.cac,
                    StrIdInteraccion = strInteraction,
                    StrCasoInter = strInteraction,
                    StrRepresLegal = objModel.LegalRep,
                    StrNroServicio = objModel.msisdn,
                    StrTipoDocIdentidad = objModel.TypeDocument,
                    StrNroDocIdentidad = objModel.nroDocumento,
                    strCodigoBloqueo = strCode,
                    strSolicitante = objModel.Solicitante,
                    strMotivo = objModel.TipoBloqueo,
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
                    StrTitularCliente = objModel.StrTitularCliente

                };
                if (oParameter.strSolicitante == "1") oParameter.strSolicitante = "Titular";
                if (oParameter.strSolicitante == "2") oParameter.strSolicitante = "Carta Poder";
                if (oParameter.strSolicitante == "3") oParameter.strSolicitante = "RR/LL";
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: ", "oParameter.StrFechaTransaccionProgram" + oParameter.StrFechaTransaccionProgram);

                CommonIFIService.GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "OUT GetConstancyPDF- DATOS MENORES - INT()  NAME_PDF:   " + NAME_PDF);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }


        /// <summary>Método que permite el bloqueo de linea</summary>
        /// <param name="objModel"></param>
        /// <param name="auditCommon"></param>
        /// <param name="xres"></param>
        /// <remarks>LockLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public void LockLine(ServiceLockModel objModel, CommonIFIService.AuditRequest auditCommon, out bool xres)
        {

            xres = false;
            string strCode = string.Empty;
            PostIFIService.AuditRequest auditPost = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(objModel.idSession);
            auditPost.userName = objModel.currentUser;
            Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "Ingreso metodo: LockLine");
            Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El codigo de bloqueo es: " + strCode);
            strCode = GetSequenceCode(auditCommon, false);
            objModel.codeLock = strCode;
            if (!string.IsNullOrEmpty(strCode))
            {

                PostIFIService.InsertLockLinePerRequestIFI objRequest = new InsertLockLinePerRequestIFI()
                {
                    audit = auditPost,
                    codeLock = strCode,
                    objLock = new Lock()
                    {
                        _BLO_CUENTA = objModel.cuenta,
                        _BLO_TELEFONO = objModel.msisdn,
                        _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                        _BLO_USUARIO = auditPost.userName,
                        _BLO_AUTORIZA = "",
                        _BLO_ESTADO = "BLOQUEADO",
                        _BLO_TIPO = objModel.CodigoBloqueo,
                        _BLO_CODIGO = strCode
                    },

                };
                bool resultPer = InsertLockLinePer(objRequest);

                Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El resultado del metodo InsertLockLinePer es: " + resultPer);
                xres = resultPer;
                if (resultPer)
                {
                    ServiceLockRequest objServiceLockRequest = new ServiceLockRequest()
                    {
                        audit = auditPost,
                        coId = objModel.contractId,
                        objIsOkGetKeyRequest = new PostIFIService.IsOkGetKeyRequestCommon()
                        {
                            audit = auditPost,

                        },
                        ticklerCode = objModel.CodigoBloqueo

                    };
                    ServiceLockResponse objResponse =
                                                   Claro.Web.Logging.ExecuteMethod<ServiceLockResponse>(
                                                  () =>
                                                  {
                                                      return oPostpaidService.GetServiceLock(objServiceLockRequest);
                                                  });
                    Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El resultado del metodo GetServiceLock es: " + objResponse.resul);
                    xres = objResponse.resul;
                    if (objResponse.resul)
                    {
                        PostIFIService.InsertLockLinePerRequestIFI objRequest2 = new InsertLockLinePerRequestIFI()
                        {
                            audit = auditPost,
                            codeLock = strCode,
                            objLock = new Lock()
                            {
                                _BLO_CUENTA = objModel.cuenta,
                                _BLO_TELEFONO = objModel.msisdn,
                                _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                                _BLO_USUARIO = auditPost.userName,
                                _BLO_AUTORIZA = "",
                                _BLO_ESTADO = "BLOQUEADO",
                                _BLO_TIPO = objModel.CodigoBloqueo,
                                _BLO_CODIGO = strCode
                            },

                        };
                        bool resultPer2 = InsertLockLinePer(objRequest2);
                        Claro.Web.Logging.Info(auditPost.Session, auditPost.transaction, "El resultado del metodo2 InsertLockLinePer es: " + resultPer2);

                        xres = resultPer2;

                    }
                    else
                    {
                        xres = false;
                        DeleteLockLineRequestIFI objDeleteLockLineRequestIFI = new DeleteLockLineRequestIFI()
                        {
                            audit = auditPost,
                            objLock = new Lock()
                            {
                                _BLO_CUENTA = objModel.cuenta,
                                _BLO_TELEFONO = objModel.msisdn,
                                _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                                _BLO_USUARIO = auditPost.userName,
                                _BLO_AUTORIZA = "",
                                _BLO_ESTADO = "BLOQUEADO",
                                _BLO_TIPO = objModel.CodigoBloqueo,
                                _BLO_CODIGO = strCode
                            }
                        };
                        DeleteLockLineResponseIFI objDeleteLockLineResponseIFI =
                                                  Claro.Web.Logging.ExecuteMethod<DeleteLockLineResponseIFI>(
                                                 () =>
                                                 {
                                                     return oPostpaidService.DeleteLockLine(objDeleteLockLineRequestIFI);
                                                 });
                        if (objDeleteLockLineResponseIFI.resul) Claro.Web.Logging.Info(objDeleteLockLineRequestIFI.audit.Session, objDeleteLockLineRequestIFI.audit.transaction, "se realizó el Rollback correctamente");
                        else Claro.Web.Logging.Info(objDeleteLockLineRequestIFI.audit.Session, objDeleteLockLineRequestIFI.audit.transaction, "ocurrío un error al realizar el Rollback");
                    }
                }
                else
                {
                    Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "El codigo de bloqueo no se inserto en el metodo: InsertLockLinePer ");

                    xres = resultPer;
                }

            }
            else
            {
                Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "El codigo de bloqueo no se genero ");

                xres = false;
            }

        }


        /// <summary>Método que hace el bloqueo de equipo</summary>
        /// <param name="objModel"></param>
        /// <param name="auditCommon"></param>
        /// <param name="xres"></param>
        /// <remarks>LockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public void LockEquipment(ServiceLockModel objModel, CommonIFIService.AuditRequest auditCommon, out bool xres)
        {
            xres = false;
            string strCode = string.Empty;
            int resTerminal;
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
                                              return oPostpaidService.GetEquipmentLock(objLockUnlockEquipmentRequestIFI);
                                          });

            if (objResponse.Result)
            {
                strCode = GetSequenceCode(auditCommon, false);
                objModel.codeLock = strCode;
                if (!string.IsNullOrEmpty(strCode))
                {
                    PostIFIService.InsertLockEquipmentPerRequestIFI objRequest = new InsertLockEquipmentPerRequestIFI()
                    {
                        audit = auditPost,
                        codeLock = strCode,
                        objLock = new Lock()
                        {
                            _BLO_CUENTA = objModel.cuenta,
                            _BLO_TELEFONO = objModel.msisdn,
                            _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                            _BLO_USUARIO = auditPost.userName,
                            _BLO_AUTORIZA = "",
                            _BLO_ESTADO = "BLOQUEADO",
                            _BLO_TIPO = objModel.CodigoBloqueo,
                            _BLO_CODIGO = strCode,
                            _BLO_IMEI = objModel.IMEI

                        },

                    };

                    bool resultPer = InsertLockEquipmentPer(objRequest);
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
                                                          return oPostpaidService.GetEquipmentUnLock(objLockUnlockEquipmentRequestIFI2);
                                                      });
                        if (objResponse2.Result) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el Rollback del bloqueo de equipo");
                        else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el Rollback del bloqueo de equipo");
                    }
                    else
                    {
                        resTerminal = new CommonServicesController().InsertTerminalLockUnlockEquipment(true, auditPost, objModel);
                        if (resTerminal > 0) { xres = true; }
                        else
                        {
                            xres = false;
                            DeleteLockEquipmentRequestIFI objDeleteLockEquipmentRequestIFI = new DeleteLockEquipmentRequestIFI()
                            {
                                audit = auditPost,

                                objLock = new Lock()
                                {
                                    _BLO_CUENTA = objModel.cuenta,
                                    _BLO_TELEFONO = objModel.msisdn,
                                    _BLO_COD_APLICA = Claro.ConfigurationManager.AppSettings("ApplicationCode"),
                                    _BLO_USUARIO = auditPost.userName,
                                    _BLO_AUTORIZA = "",
                                    _BLO_ESTADO = "BLOQUEADO",
                                    _BLO_TIPO = objModel.CodigoBloqueo,
                                    _BLO_CODIGO = strCode,
                                    _BLO_IMEI = objModel.IMEI


                                },
                            };
                            DeleteLockEquipmentResponseIFI objResponseUpd =
                                                        Claro.Web.Logging.ExecuteMethod<DeleteLockEquipmentResponseIFI>(
                                                       () =>
                                                       {
                                                           return oPostpaidService.DeleteLockEquipment(objDeleteLockEquipmentRequestIFI);
                                                       });

                            if (objResponseUpd.resul) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el DeleteLockEquipment del desbloqueo de equipo");
                            else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el DeleteLockEquipment del desbloqueo de equipo");
                            LockUnlockEquipmentRequestIFI objLockUnlockEquipmentRequestIFI2 = new LockUnlockEquipmentRequestIFI()
                            {
                                audit = auditPost,
                                Imei = objModel.IMEI
                            };
                            LockUnlockEquipmentResponseIFI objResponse2 =
                                                           Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponseIFI>(
                                                          () =>
                                                          {
                                                              return oPostpaidService.GetEquipmentUnLock(objLockUnlockEquipmentRequestIFI2);
                                                          });
                            if (objResponse2.Result) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el Rollback del bloqueo de equipo");
                            else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el Rollback del bloqueo de equipo");
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
                                                      return oPostpaidService.GetEquipmentUnLock(objLockUnlockEquipmentRequestIFI2);
                                                  });
                    if (objResponse2.Result) Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "Se realizo el Rollback del 2bloqueo de equipo");
                    else Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el Rollback del 2bloqueo de equipo");

                }

            }
            else
            {
                Claro.Web.Logging.Error(auditPost.Session, auditPost.transaction, "No se pudo realizar el bloqueo de equipo");
                xres = false;
            }

        }


        /// <summary>Método que permite insertar terminal bloqueo desbloqueo equipo</summary>
        /// <param name="isLock"></param>
        /// <param name="auditPost"></param>
        /// <param name="objModel"></param>
        /// <returns>int</returns>
        /// <remarks>InsertTerminalLockUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public int InsertTerminalLockUnlockEquipment(bool isLock, PostIFIService.AuditRequest auditPost, ServiceLockModel objModel)
        {
            InsertTerminalLockUnlockEquipmentRequestIFI objInsertTerminalLockUnlockEquipmentRequestIFI = new InsertTerminalLockUnlockEquipmentRequestIFI()
            {
                audit = auditPost,
                isTransactionLock = true,
                objTerminal = new Terminal()
                {
                    _strMarca = objModel.Marca,
                    _strNumeroIMEI = objModel.IMEI,
                    _strNumeroLinea = objModel.msisdn,
                    _strTipDocumento = objModel.strSolicitanteTipoDoc,
                    _strTipoMovimiento = objModel.TipoBloqueo,
                    _strReportante = "",
                    _strAsesorServicio = auditPost.userName,
                    _strModelo = ""

                }
            };

            InsertTerminalLockUnlockEquipmentResponseIFI objResponseTerminal =
                                         Claro.Web.Logging.ExecuteMethod<InsertTerminalLockUnlockEquipmentResponseIFI>(
                                        () =>
                                        {
                                            return oPostpaidService.InsertTerminalLockUnlockEquipment(objInsertTerminalLockUnlockEquipmentRequestIFI);
                                        });
            return objResponseTerminal.resTerminal;

        }


        /// <summary>Método que permite insertar bloqueo de equipo per</summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>InsertLockEquipmentPer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public bool InsertLockEquipmentPer(PostIFIService.InsertLockEquipmentPerRequestIFI objRequest)
        {
            InsertLockEquipmentPerResponseIFI objResponse =
            Claro.Web.Logging.ExecuteMethod<InsertLockEquipmentPerResponseIFI>(
                () => { return oPostpaidService.InsertLockEquipmentPer(objRequest); });
            return objResponse.Result;
        }


        /// <summary>Método que permtite el bloqueo de linea y equipo</summary>
        /// <param name="objModel"></param>
        /// <param name="auditCommon"></param>
        /// <param name="xres"></param>
        /// <remarks>LockLineEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public void LockLineEquipment(ServiceLockModel objModel, CommonIFIService.AuditRequest auditCommon, out bool xres)
        {
            xres = false;
            LockLine(objModel, auditCommon, out xres);
            if (xres)
            {
                xres = false;
                LockEquipment(objModel, auditCommon, out xres);
            }



        }


        /// <summary>Método que inserta bloqueo linea per</summary>
        /// <param name="objRequest"></param>
        /// <returns>bool</returns>
        /// <remarks>InsertLockLinePer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public bool InsertLockLinePer(PostIFIService.InsertLockLinePerRequestIFI objRequest)
        {
            Claro.Web.Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, "Ingreso metodo: InsertLockLinePer ");

            InsertLockLinePerResponseIFI objResponse =
            Claro.Web.Logging.ExecuteMethod<InsertLockLinePerResponseIFI>(
                () => { return oPostpaidService.InsertLockLinePer(objRequest); });
            return objResponse.Result;
        }

        ///<summary>Método que permite obtener el codigo de desbloque</summary>
        /// <param name="audit"></param>
        /// <param name="isFlagUnlock"></param>
        /// <returns>string</returns>
        /// <remarks>GetSequenceCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public string GetSequenceCode(CommonIFIService.AuditRequest audit, bool isFlagUnlock)
        {

            SequenceCodeRequest objSequenceCodeRequest = new SequenceCodeRequest()
            {
                audit = audit,
                isFlagUnlock = isFlagUnlock
            };
            SequenceCodeResponse objResponse =
             Claro.Web.Logging.ExecuteMethod<SequenceCodeResponse>(
                 () => { return oCommonService.GetSequenceCode(objSequenceCodeRequest); });
            return objResponse.code;
        }


        /// <summary>Método que permite la carga tipificacion bloqueo</summary>
        /// <param name="audit"></param>
        /// <param name="Transaction_Name"></param>
        /// <param name="lblMensaje"></param>
        /// <returns>Typification</returns>
        /// <remarks>LoadTypificationLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public Typification LoadTypificationLock(CommonIFIService.AuditRequest audit, string Transaction_Name,
                                                           ref string lblMensaje)
        {
            Typification oTypification = null;


            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypificationChangeMinor");


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
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypification ingreso validacion nula");
                    lblMensaje = "No se cargo las tipificaciones";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Error LoadTypifications --> " + ex.Message);
                lblMensaje = "No se cargo las tipificaciones";
            }

            return oTypification;
        }
        #region Guardar Interaccion


        /// <summary>Método que guarda la interaccion</summary>
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


        /// <summary>Método que setea datos de la interaccion</summary>
        /// <param name="objModel"></param>
        /// <param name="objTypification"></param>
        /// <returns>CommonIFIService.Iteraction </returns>
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


        /// <summary>Método que setea datos de la plantilla</summary>
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
                //Bloqueo de Linea
                if (objModel.TipoTransaccion == Claro.Constants.NumberOneString)
                {
                    responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strNombreTransaccionBloqueoLinea");
                    responseModel._X_NAME_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_DNI_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_BIRTHDAY = DateTime.Now;

                    //datos de la transaccion
                    responseModel._X_INTER_5 = KEY.AppSettings("strNombreBloqueoLinea");
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
                //Bloqueo de Equipo
                if (objModel.TipoTransaccion == Claro.Constants.NumberTwoString)
                {
                    responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strNombreTransaccionBloqueoEquipo");

                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;

                    responseModel._X_NAME_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_DNI_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_BIRTHDAY = DateTime.Now;

                    //datos de la transaccion
                    responseModel._X_INTER_5 = KEY.AppSettings("strNombreBloqueoEquipo");
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
                //Bloqueo de Linea y Equipo
                if (objModel.TipoTransaccion == Claro.Constants.NumberThreeString)
                {
                    responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strNombreTransaccionBloqueoLineaEquipo");

                    responseModel._X_DOCUMENT_NUMBER = objModel.nroDocumento;

                    responseModel._X_NAME_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_DNI_LEGAL_REP = objModel.LegalRep;
                    responseModel._X_BIRTHDAY = DateTime.Now;

                    //datos de la transaccion
                    responseModel._X_INTER_5 = KEY.AppSettings("strNombreBloqueoLineaEquipo");
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

        /// <summary>Método que obtiene actual bloqueo</summary>
        /// <param name="strIdSession"></param>
        /// <param name="codId"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>CurrentBlock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult CurrentBlock(string strIdSession, string codId)
        {
            var audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            var objResponse = new PostIFIService.CurrentLockResponseIFI();
            try
            {
                var objRequest = new PostIFIService.CurrentLockRequestIFI()
                {
                    audit = audit,
                    codId = codId


                };
                objResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.CurrentLockResponseIFI>(
                    () => { return oPostpaidService.CurrentBlock(objRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }
            return Json(new { data = objResponse.lstAnnotation });
        }


        /// <summary>Método que obtiene imeis</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strLine"></param>
        /// <returns></returns>
        /// <remarks>GetImeis</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetImeis(string strIdSession, string strLine)
        {
            var audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            var objResponse = new PostIFIService.ImeisResponseIFI();
            try
            {
                var objRequest = new PostIFIService.ImeisRequestIFI()
                {
                    audit = audit,
                    strLine = strLine


                };
                objResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.ImeisResponseIFI>(
                    () => { return oPostpaidService.GetImeis(objRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            return Json(new { data = objResponse.lstTerminal });
        }
    }
}
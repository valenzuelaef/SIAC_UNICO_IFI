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
    public class DuplicateReceiptsController : CommonServicesController
    {
        public readonly CommonIFIService.CommonIFIServiceClient oCommonIFIService = new CommonIFIService.CommonIFIServiceClient();
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient oServicePostpaid = new PostIFIService.PostIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient();


        /// <summary>Método que muestra la vista para el formulario de duplicado de recibos</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>DuplicateReceipts</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018</FecCrea></item></list>
        public ActionResult DuplicateReceipts()
        {
            return View();
        }


        /// <summary>Método que se ejecuta al cargarse la página</summary>
        /// <returns>JsonResult</returns>
        /// <remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018</FecCrea></item></list>
        public JsonResult PageLoad(string strIdSession)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Método PageLoad Duplicado de Recibo IFI");
            Common.Typification typificationLoad = null;
            string lblMensaje = "";
            try
            {
                typificationLoad = LoadTypificationDuplicateReceipts(audit, ref lblMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message.ToString());
            }
            return Json(new { data = typificationLoad });

        }


        /// <summary>Método que obtiene los datos de la tipificación</summary>
        /// <param name="audit"></param>  
        /// <param name="lblMensaje"></param>
        /// <returns>Common.Typification</returns>
        /// <remarks>LoadTypificationDuplicateReceipts</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018</FecCrea></item></list>
        public Common.Typification LoadTypificationDuplicateReceipts(CommonIFIService.AuditRequest audit, ref string lblMensaje)
        {
            Common.Typification oTypification = null;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypificationDuplicateReceipts IFI");
            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("TransaccionDuplicateReceipts");
                objTypificationRequest.audit = audit;

                Common.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<Common.TypificationResponse>(
                    () => { return oCommonService.GetTypification(objTypificationRequest); });
                oTypification = objResponse.ListTypification.First();
                if (oTypification == null)
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypification IFI ingreso validacion nula");
                    lblMensaje = "No se cargo las tipificaciones";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, "Message Error : " + ex.Message);
                lblMensaje = "No se cargo las tipificaciones";
            }

            return oTypification;
        }


        /// <summary>Método que devuelve los datos de la transaccion guardada para pasarlo a la session</summary>
        /// <param name="oModel"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetCustomerDuplicateReceipts</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public JsonResult GetCustomerDuplicateReceipts(Model.DuplicateReceiptsModel oModel)
        {
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.strIdSession);
            ClientDataAddResponse objClientDataAddResponse = new ClientDataAddResponse();
            ClientDataAddRequest objRequestClientDataAdd = new ClientDataAddRequest();
            DataCustomerRequestPostPaid objRequestCustomer = new DataCustomerRequestPostPaid();
            DataCustomerResponsePostPaid objResponse = new DataCustomerResponsePostPaid();
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

            return Json(new { objCus = objResponse.Cliente, objclientDataAdd = objClientDataAddResponse.Cliente });
        }


        /// <summary>Método que realiza el guardado de la transaccion de duplicado de recibos</summary>
        /// <param name="oModel"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>SaveTransactionDuplicateReceipts</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public JsonResult SaveTransactionDuplicateReceipts(Model.DuplicateReceiptsModel oModel)
        {
            string vDesInteraction = string.Empty;
            var hidCasoId = "";
            string strEstadoForm = Claro.Constants.LetterN;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.strIdSession);
            oModel.blnInteract = false;
            string strRutaArchivo = string.Empty;
            string vInteractionId = string.Empty;
            string strNombreArchivo = string.Empty;
            string GeneroCaso = string.Empty;
            oModel.strTransaccion = audit.transaction;
            oModel.CodeTipification = ConfigurationManager.AppSettings("TransaccionDuplicateReceipts");
            bool flagResult = false;
            string Rpta = "";
            string resulMsg = "";

            try
            {
                if (oModel.strMedio == "3" || oModel.strMedio == "4")
                {
                    GetMedioIdCaso(oModel, strEstadoForm, GeneroCaso, out flagResult, out  vDesInteraction, out strNombreArchivo, out hidCasoId, out strRutaArchivo, audit, AuditRequest, out vInteractionId, out  Rpta, out resulMsg);
                }
                else
                {
                    List<string> strInteractionId = SaveInteraction(oModel);
                    vInteractionId = strInteractionId[3].ToString();
                    if (strInteractionId[0] == Claro.SIACU.Constants.OK)
                    {
                        flagResult = true;

                        #region CONSTANCY PDF
                        vDesInteraction = Claro.Utils.GetValueFromConfigFileIFI("strMsgTranGrabSatisDuplicateReceipts", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        GetConstancyPDF(oModel.strIdSession, vInteractionId, oModel);
                        strRutaArchivo = oModel.strFullPathPDF;
                        #endregion
                    }
                    else
                    {
                        vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");
                    }
                }

                if (flagResult)
                {
                    GetMedioEnviarCorreo(oModel, vInteractionId, out  Rpta, strRutaArchivo, audit);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "MensajeFedaDomi " + vDesInteraction);
            return Json(new { data = oModel, vDesInteraction, strRutaArchivo, hidCasoId, vInteractionId, resulMsg, Rpta });
        }






        /// <summary>Método que ejecuta el envio de email concatenando los recibos y la constancia</summary>
        /// <param name="cadena"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strAdjunto"></param>
        /// <param name="model"></param>
        /// <param name="Rpta"></param>
        /// <param name="attachFile"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailAltDuplicate</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetSendEmailAltDuplicate(string cadena, string strInteraccionId, string strAdjunto, Model.DuplicateReceiptsModel model, out string Rpta, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.Destinatarios;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");


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

                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDuplicadoRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";



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

                CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse;
                objGetSendEmailRequest =
                    new CommonIFIService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strMessage,
                        strAttached = cadena + "|" + strAdjunto,
                        strSubject = strAsunto,
                        AttachedByte = attachFile

                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return oCommonService.GetSendEmailAltFixed(objGetSendEmailRequest); });

                Rpta = objGetSendEmailResponse.Exit;
                if (Rpta == Claro.SIACU.Constants.OK)
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
                Claro.Web.Logging.Error(AuditRequest.Session, AuditRequest.transaction, ex.Message);
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                Rpta = Claro.SIACU.Constants.NO_OK;
            }
            return strResul;
        }


        /// <summary>Método que ejecuta el envio de email - Solo recibos</summary>
        /// <param name="cadena"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strAdjunto"></param>
        /// <param name="model"></param>
        /// <param name="Rpta"></param>
        /// <param name="attachFile"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailAltDuplicatebill</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetSendEmailAltDuplicatebill(string cadena, string strInteraccionId, string strAdjunto, Model.DuplicateReceiptsModel model, out string Rpta, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.strEmailMedio;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

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

                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDuplicadoRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";



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

                CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse;
                objGetSendEmailRequest =
                    new CommonIFIService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strMessage,
                        strAttached = cadena,
                        strSubject = strAsunto,
                        AttachedByte = attachFile

                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return oCommonService.GetSendEmailAltFixed(objGetSendEmailRequest); });

                Rpta = objGetSendEmailResponse.Exit;
                if (Rpta == Claro.SIACU.Constants.OK)
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
                Claro.Web.Logging.Error(AuditRequest.Session, AuditRequest.transaction, ex.Message);
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                Rpta = Claro.SIACU.Constants.NO_OK;
            }
            return strResul;
        }


        /// <summary>Método que ejecuta el envio de email - Solo constancia</summary>
        /// <param name="cadena"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strAdjunto"></param>
        /// <param name="model"></param>
        /// <param name="Rpta"></param>
        /// <param name="attachFile"></param>
        /// <returns>string</returns>
        /// <remarks>GetSendEmailAltDuplicatebill2</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetSendEmailAltDuplicatebill2(string cadena, string strInteraccionId, string strAdjunto, Model.DuplicateReceiptsModel model, out string Rpta, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.Destinatarios;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");


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

                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDuplicadoRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";



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

                CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse;
                objGetSendEmailRequest =
                    new CommonIFIService.SendEmailRequestCommon()
                    {
                        audit = AuditRequest,
                        strSender = strRemitente,
                        strTo = strDestinatarios,
                        strMessage = strMessage,
                        strAttached = cadena,
                        strSubject = strAsunto,
                        AttachedByte = attachFile

                    };
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return oCommonService.GetSendEmailAltFixed(objGetSendEmailRequest); });

                Rpta = objGetSendEmailResponse.Exit;
                if (Rpta == Claro.SIACU.Constants.OK)
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
                Claro.Web.Logging.Error(AuditRequest.Session, AuditRequest.transaction, ex.Message);
                strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                Rpta = Claro.SIACU.Constants.NO_OK;
            }
            return strResul;
        }


        /// <summary>Método que obtiene la plantilla para almacenar los datos del caso.</summary>
        /// <param name="oModel"></param>
        /// <returns>FixedIFIService.CaseTemplate</returns>
        /// <remarks>DatosPlantillaCaso</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public FixedIFIService.CaseTemplate DatosPlantillaCaso(Model.DuplicateReceiptsModel oModel)
        {

            FixedIFIService.CaseTemplate oPlantillaCampoData = new FixedIFIService.CaseTemplate();

            oPlantillaCampoData.NOMBRE_TRANSACCION = oModel.strTransaccion;
            oPlantillaCampoData.NRO_TELEFONO = oModel.strTelefono;
            oPlantillaCampoData.X_CELLULAR_NUMBER = oModel.strTelefono;
            oPlantillaCampoData.X_DOCUMENT_NUMBER = oModel.DNI_RUC;
            oPlantillaCampoData.X_CUSTOMER_NAME = oModel.strNombres;
            oPlantillaCampoData.X_CAS_1 = oModel.strApellidos;
            oPlantillaCampoData.X_CAS_2 = oModel.strRepresentante;
            oPlantillaCampoData.X_CAS_3 = oModel.strDocRepresentante;
            oPlantillaCampoData.X_CAS_4 = oModel.strNombresCompleto;
            oPlantillaCampoData.X_CAS_5 = oModel.strContactoCliente;
            oPlantillaCampoData.X_CAS_6 = oModel.strPais;
            oPlantillaCampoData.X_ADDRESS = oModel.strCalle;
            oPlantillaCampoData.X_CAS_29 = oModel.strDepartamento;
            oPlantillaCampoData.X_CAS_16 = oModel.strProvincia;
            oPlantillaCampoData.X_CAS_17 = oModel.strDistrito;
            oPlantillaCampoData.X_CAS_15 = oModel.strCacDac;
            oPlantillaCampoData.X_CAS_7 = oModel.strEmailMedio;
            oPlantillaCampoData.X_CLAROLOCAL6 = oModel.strchkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
            oPlantillaCampoData.X_FLAG_VARIATION = "1";
            oPlantillaCampoData.X_CLARO_LDN4 = oModel.strMedio;
            oPlantillaCampoData.X_MODEL = oModel.strMedioDes;
            if (oModel.strFechActivacion == "")
            {

                oPlantillaCampoData.X_REACTIVATION_DATE = Convert.ToDate(DateTime.Now);
            }
            else
            {
                oPlantillaCampoData.X_REACTIVATION_DATE = Convert.ToDate(oModel.strFechActivacion);
            }
            if (oModel.arrDates != null)
            {
                oPlantillaCampoData.X_CONCLUSIONS = oModel.arrDates.TrimEnd('|');
            }
            oPlantillaCampoData.X_CLARO_LDN1 = oModel.strMeses;
            oPlantillaCampoData.X_STORE = oModel.strEmail;
            oPlantillaCampoData.X_REFERENCE_ADDRESS = oModel.strReferencia;
            oPlantillaCampoData.X_REPOSITION_REASON = oModel.strMeses;


            return oPlantillaCampoData;
        }


        /// <summary>Método que registra los datos de la interacción.</summary>
        /// <param name="oModel"></param>
        /// <returns>List</returns>
        /// <remarks>SaveInteraction</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecActu>16/01/2019</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Se creo un nuevo metodo de creacion de interacion para realizarlo por servicio y ya no por StoreProcedure sin quitar la opcion de un furuto cambio a SP otra vez</Mot></item>nuevo metodo para crear interaccion por SP o Servicio "InsertInteractionDuplicateReceipts"</list></remarks>
        public List<string> SaveInteraction(Model.DuplicateReceiptsModel oModel)
        {

            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = oModel.strTelefono;
            Model.TemplateInteractionModel oPlantillaDat;
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion;

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            try
            {

                oInteraccion = DatosInteraccion(oModel);
                oPlantillaDat = GetDataTemplateInteraction(oModel);

                var resultInteraction = InsertInteractionDuplicateReceipts(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.strIdSession, oModel.strCustomerId);

                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != Claro.SIACU.Constants.OK && lstaDatTemplate[3] == string.Empty)
                {

                    Claro.Web.Logging.Info(audit.Session, audit.transaction, Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                    throw new Exception(Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);

            }


            return lstaDatTemplate;
        }


        /// <summary>Método que obtiene los datos de la interacción.</summary>
        /// <param name="oModel"></param>
        /// <returns>Model.InteractionModel</returns>
        /// <remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>11/10/2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>23/01/2019</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Se adaptaron algunos datos para que fueran tomados tambien por el nuevo proceso de creacion de interaccion por servicio sin afectar el flujo por SP</Mot></item></list></remarks>
        public Model.InteractionModel DatosInteraccion(Model.DuplicateReceiptsModel oModel)
        {

            var oInteraccion = new Model.InteractionModel();

            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);



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
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Plan;
                oInteraccion.ClassCode = oModel.claseCode;
                oInteraccion.SubClassCode = oModel.subClaseCode;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }



            return oInteraccion;

        }


        /// <summary>Método que obtiene los datos de la plantilla interacción</summary>
        /// <param name="oModel"></param>
        /// <returns>Model.InteractionModel</returns>
        /// <remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>11/10/2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>23/01/2019</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Se adaptaron algunos datos para que fueran tomados tambien por el nuevo proceso de creacion de interaccion por servicio sin afectar el flujo por SP</Mot></item></list></remarks>
        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.DuplicateReceiptsModel oModel)
        {
            var oPlantillaCampoData = new Model.TemplateInteractionModel();

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            try
            {

                oPlantillaCampoData.NOMBRE_TRANSACCION = KEY.AppSettings("TransaccionDuplicateReceipts");
                oPlantillaCampoData.X_CLARO_NUMBER = oModel.strTelefono;
                oPlantillaCampoData.X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                oPlantillaCampoData.X_FIRST_NAME = oModel.strNombres;
                oPlantillaCampoData.X_LAST_NAME = oModel.strApellidos;
                oPlantillaCampoData.X_NAME_LEGAL_REP = oModel.strRepresentante;
                oPlantillaCampoData.X_DNI_LEGAL_REP = oModel.DNI_RUC;
                oPlantillaCampoData.X_OLD_FIRST_NAME = oModel.strNombres;
                oPlantillaCampoData.X_OTHER_FIRST_NAME = oModel.strApellidos;
                oPlantillaCampoData.X_REASON = oModel.strPais;
                oPlantillaCampoData.X_DEPARTMENT = oModel.strDepartamento;
                oPlantillaCampoData.X_CITY = oModel.strProvincia;
                oPlantillaCampoData.X_DISTRICT = oModel.strDistrito;
                oPlantillaCampoData.X_ADDRESS = oModel.strCalle;
                oPlantillaCampoData.X_INTER_16 = oModel.strEmailMedio;
                oPlantillaCampoData.X_CLAROLOCAL6 = oModel.strchkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
                oPlantillaCampoData.X_FLAG_REGISTERED = "1";
                oPlantillaCampoData.X_EMAIL = oModel.strEmail;
                oPlantillaCampoData.X_RECHARGE_DATE = Claro.Utils.CheckDate(oModel.strFechActivacion);
                oPlantillaCampoData.X_REFERENCE_ADDRESS = oModel.strReferencia;
                oPlantillaCampoData.X_CLARO_LDN1 = oModel.strMeses;
                oPlantillaCampoData.X_INTER_15 = oModel.strCacDac;
                oPlantillaCampoData.X_INTER_6 = oModel.strMedio;

                oPlantillaCampoData.X_MODEL = oModel.strMedioDes;

                if (oModel.arrDates != null)
                {
                    oPlantillaCampoData.X_INTER_30 = oModel.arrDates.TrimEnd('|');
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }


            return oPlantillaCampoData;

        }


        /// <summary>Método que obtiene los datos de caso</summary>
        /// <param name="model"></param>
        /// <returns>FixedIFIService.Interaction</returns>
        /// <remarks>DatosCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018.</FecCrea></item></list>
        public FixedIFIService.Interaction DatosCaso(Model.DuplicateReceiptsModel model)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            FixedIFIService.Interaction objInteractionModel = new FixedIFIService.Interaction();
            Model.InteractionModel objInteraction;
            objInteraction = CargarTipificacion(model.strIdSession, model.CodeTipification);

            try
            {
                if (objInteraction.FlagCase == Claro.SIACU.Constants.OK)
                {
                    //ObtenerCliente
                    var strNroTelephone = model.strTelefono;
                    objInteractionModel.OBJID_CONTACTO = GetCustomer(strNroTelephone, model.strIdSession);  //Get Customer = strObjId
                    objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
                    objInteractionModel.TELEFONO = model.strTelefono;
                    objInteractionModel.TIPIFICACION = objInteraction.Type;
                    objInteractionModel.CLASE = objInteraction.Class;
                    objInteractionModel.SUBCLASE = objInteraction.SubClass;
                    objInteractionModel.PRIORIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                    objInteractionModel.SEVERIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                    objInteractionModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                    objInteractionModel.TIPO_INTERACCION = ConfigurationManager.AppSettings("AtencionDefault");
                    objInteractionModel.NOTAS = model.strNote;
                    objInteractionModel.COLA = model.cola;
                    objInteractionModel.FLAG_INTERACCION = Claro.Constants.NumberOneString;
                    objInteractionModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                    objInteractionModel.USUARIO_ID = model.CurrentUser;

                }
                else
                {
                    objInteractionModel.RESULTADO = objInteraction.Result;
                    objInteractionModel.FLAG_INSERCION = objInteraction.FlagCase;

                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

            return objInteractionModel;
        }


        /// <summary>Método que inserta los datos de caso</summary>
        /// <param name="oCaso"></param>
        /// <param name="oPlantillaCaso"></param>
        /// <param name="oModel"></param>
        /// <param name="strEstadoForm"></param>
        /// <returns>FixedIFIService.Interaction</returns>
        /// <remarks>InsertCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018.</FecCrea></item></list>
        public FixedIFIService.Interaction InsertCaso(FixedIFIService.Interaction oCaso, FixedIFIService.CaseTemplate oPlantillaCaso, Model.DuplicateReceiptsModel oModel, string strEstadoForm)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            FixedIFIService.Interaction oResponseCase = new FixedIFIService.Interaction();
            FixedIFIService.CaseTemplate oPlantillaResponse = new FixedIFIService.CaseTemplate();
            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            bool resultado = false;
            string IdCaso = string.Empty;
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            string ID_CASO = string.Empty;
            string FLAG_INSERCION = string.Empty;
            string MESSAGE = string.Empty;
            try
            {
                if (oCaso.OBJID_CONTACTO == null || oCaso.OBJID_CONTACTO == "0" || oCaso.OBJID_CONTACTO == "")
                {
                    var phone = oModel.strTelefono;
                    CustomerResponse objCustomerResponse;

                    GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                    {
                        audit = audit,
                        vPhone = phone,
                        vAccount = string.Empty,
                        vContactobjid1 = string.Empty,
                        vFlagReg = strFlgRegistrado
                    };
                    objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() =>
                    {
                        return GetCustomerData(objGetCustomerRequest, oModel.strIdSession);
                    });
                    if (objCustomerResponse != null)
                    {

                        oCaso.OBJID_CONTACTO = objCustomerResponse.contactobjid;
                        oCaso.OBJID_SITE = objCustomerResponse.Customer.SiteCode;

                    }

                }
                if (oCaso.OBJID_SITE == null || oCaso.OBJID_SITE == Claro.Constants.NumberZeroString || oCaso.OBJID_SITE == String.Empty)
                {
                    var phone = oModel.strTelefono;
                    CustomerResponse objCustomerResponse;
                    GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
                    {
                        audit = audit,
                        vPhone = phone,
                        vAccount = string.Empty,
                        vContactobjid1 = oModel.ContractId,
                        vFlagReg = strFlgRegistrado
                    };
                    objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() =>
                    {
                        return GetCustomerData(objGetCustomerRequest, oModel.strIdSession);
                    });

                    if (objCustomerResponse != null)
                    {
                        oCaso.OBJID_SITE = objCustomerResponse.Customer.SiteCode;
                    }
                }
                if (strEstadoForm == Claro.Constants.LetterN)
                {
                    if (ContingenciaClarify.ToUpper() != Claro.SIACU.Constants.Yes)
                    {
                        oResponseCase = GetCreateCase(oCaso);
                        IdCaso = oResponseCase.CASO_ID;
                        resultado = true;
                    }
                    else
                    {
                        oResponseCase = GetInsertCase(oCaso);
                        IdCaso = oResponseCase.CASO_ID;
                        resultado = true;
                    }
                }
                else
                {
                    resultado = true;
                }
                if (resultado)
                {

                    GetResultado(oPlantillaCaso, oPlantillaResponse, resultado, IdCaso, strEstadoForm, out ID_CASO, out  FLAG_INSERCION, out  MESSAGE);

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            oResponseCase.CASO_ID = ID_CASO;
            oResponseCase.FLAG_INSERCION_CASO = FLAG_INSERCION;
            oResponseCase.MESSAGE_CASO = MESSAGE;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fín InsertCaso - CASO_ID : " + oResponseCase.CASO_ID + "FLAG_INSERCION : " + oResponseCase.FLAG_INSERCION + "oResponseCase.MESSAGE_CASO :" + oResponseCase.MESSAGE_CASO);
            return oResponseCase;
        }

        public string GetResultado(CaseTemplate oPlantillaCaso, CaseTemplate oPlantillaResponse, bool resultado, string IdCaso, string strEstadoForm, out string ID_CASO, out string FLAG_INSERCION, out string MESSAGE)
        {
            string strResul = string.Empty;
            ID_CASO = "";
            FLAG_INSERCION = "";
            MESSAGE = "";
            if (!string.IsNullOrEmpty(IdCaso) && oPlantillaCaso != null)
            {

                oPlantillaCaso.ID_CASO = IdCaso;
                oPlantillaResponse = GuardarPlantillaCaso(oPlantillaCaso, strEstadoForm);
                ID_CASO = oPlantillaResponse.ID_CASO;
                FLAG_INSERCION = oPlantillaResponse.FLAG_INSERCION;
                MESSAGE = oPlantillaResponse.MESSAGE;


            }
            return strResul;
        }

        /// <summary>Método para guardar plantilla caso</summary>       
        /// <param name="oPlantilla"></param>
        /// <param name="vEstadoForm"></param>       
        /// <returns>FixedIFIService.CaseTemplate</returns>
        /// <remarks>GuardarPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018</FecCrea></item></list>
        public FixedIFIService.CaseTemplate GuardarPlantillaCaso(FixedIFIService.CaseTemplate oPlantilla, string vEstadoForm)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oPlantilla.audit.Session);
            string ContingenciaClarify = string.Empty;
            ContingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            FixedIFIService.CaseTemplate oResponse = new FixedIFIService.CaseTemplate();

            try
            {
                if (vEstadoForm == Claro.Constants.LetterN)
                {
                    if (ContingenciaClarify.ToUpper() != Claro.SIACU.Constants.Yes)
                    {
                        oResponse = GetInsertTemplateCase(oPlantilla);
                    }
                    else
                    {
                        oResponse = GetInsertTemplateCaseContingent(oPlantilla);
                    }
                }
                else
                {
                    if (ContingenciaClarify.ToUpper() != Claro.SIACU.Constants.Yes)
                    {
                        oResponse = ActualizaPlantillaCaso(oPlantilla);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

            return oResponse;
        }


        #region LOAD TIPIFICATION

        /// <summary>Método que permite cargar la tipificación en la vista</summary>       
        /// <param name="IdSession"></param>
        /// <param name="CodeTipification"></param>       
        /// <returns>Model.InteractionModel</returns>
        /// <remarks>CargarTipificacion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16/10/2018</FecCrea></item></list>
        public Model.InteractionModel CargarTipificacion(string IdSession, string CodeTipification)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(IdSession);
            var objInteraction = new Model.InteractionModel();
            try
            {
                var tipification = GetTypificationHFC(IdSession, CodeTipification);
                if (tipification != null)
                {
                    tipification.ToList().ForEach(x =>
                    {
                        objInteraction.Type = x.Type;
                        objInteraction.Class = x.Class;
                        objInteraction.SubClass = x.SubClass;
                        objInteraction.InteractionCode = x.InteractionCode;
                        objInteraction.TypeCode = x.TypeCode;
                        objInteraction.ClassCode = x.ClassCode;
                        objInteraction.SubClassCode = x.SubClassCode;
                        objInteraction.FlagCase = Claro.SIACU.Constants.OK;
                    });
                }
                else
                {
                    objInteraction.Result = Claro.SIACU.Constants.strNotTypification;
                    objInteraction.FlagCase = Claro.SIACU.Constants.NO_OK;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);

            }

            return objInteraction;
        }

        #endregion


        #region CONSTANCY PDF - SEND EMAIL

        /// <summary>Método que permite generar la constancia de Duplicado de recibos mediante un archivo Pdf</summary>
        /// <param name="strIdSession"></param>  
        /// <param name="strIdInteraction"></param>  
        /// <param name="oModel"></param>  
        /// <returns>Dictionary</returns>
        /// <remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>24/10/2018</FecCrea></item></list>
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.DuplicateReceiptsModel oModel)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;
            bool test = false;

            ParametersGeneratePDF parameters = null;

            try
            {
                strTexto = Claro.Utils.GetValueFromConfigFileIFI("strMsgDuplicadoReciboConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));


                parameters = new ParametersGeneratePDF();
                //------Campos de Duplicado de Recibos.

                if (oModel.strMedio == "3" || oModel.strMedio == "4")
                {
                    parameters.StrCentroAtencionArea = oModel.strCacDac;
                    parameters.StrContactoCliente = oModel.strContactoCliente;
                    var separadas = oModel.arrDates.TrimEnd('|');
                    parameters.StrFechaEmisionDocFact = separadas;
                    parameters.StrMedio = oModel.strMedioDes;
                    parameters.StrReferenciaActual = oModel.strReferencia;
                    parameters.StrPais = oModel.strPais;
                    parameters.StrDepartamento = oModel.strDepartamento;
                    parameters.StrProvincia = oModel.strProvincia;
                    parameters.StrDistrito = oModel.strDistrito;
                    parameters.StrDireccion = oModel.strCalle;
                    parameters.StrNombreAgenteUsuario = oModel.fullNameUser;
                    parameters.strEnvioCorreo = oModel.ischkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
                    parameters.strCorreoCliente = oModel.strEmail;
                    parameters.StrNroServicio = oModel.strTelefono;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.strNroDoc = oModel.DNI_RUC;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.StrTitularCliente = oModel.strNombresCompleto;
                    parameters.StrRepresLegal = oModel.strRepresentante;
                    parameters.StrCustomerId = oModel.strCustomerId;
                    parameters.StrFechaTransaccionProgram = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.strFechaTransaccion = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.StrTipoDocIdentidad = oModel.strTipoocumento;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrAccion = oModel.DesAccion;
                    parameters.StrTipoTransaccion = ConfigurationManager.AppSettings("strNombreArchivo_DuplicadoRecibo");
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionDuplicadoRecibo");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoDuplicadoRecibo");
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrContenidoComercial2 = strTexto;


                }
                else
                {
                    test = true;

                }


                if (test)
                {
                    parameters.StrCentroAtencionArea = oModel.strCacDac;
                    parameters.StrEmail = oModel.strEmailMedio;
                    parameters.StrContactoCliente = oModel.strContactoCliente;
                    var separadas = oModel.arrDates.TrimEnd('|');
                    parameters.StrFechaEmisionDocFact = separadas;
                    parameters.StrMedio = (oModel.strMedio == "1" ? "EMAIL" : oModel.strMedioDes);
                    parameters.StrNombreAgenteUsuario = oModel.fullNameUser;
                    parameters.strEnvioCorreo = oModel.ischkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
                    parameters.strCorreoCliente = oModel.strEmail;
                    parameters.StrNroServicio = oModel.strTelefono;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.strNroDoc = oModel.DNI_RUC;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.StrTitularCliente = oModel.strNombresCompleto;
                    parameters.StrRepresLegal = oModel.strRepresentante;
                    parameters.StrCustomerId = oModel.strCustomerId;
                    parameters.StrFechaTransaccionProgram = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.strFechaTransaccion = (DateTime.Now).ToString("dd/MM/yyyy");
                    parameters.StrTipoDocIdentidad = oModel.strTipoocumento;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrAccion = oModel.DesAccion;
                    parameters.StrTipoTransaccion = ConfigurationManager.AppSettings("strNombreArchivo_DuplicadoRecibo");
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionDuplicadoRecibo");
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoDuplicadoRecibo");
                    parameters.StrCasoInter = strInteraccionId;
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
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Nombrepath: " + nombrepath);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Generado: " + generado);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "strNombreArchivo : " + strNombreArchivo);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return listResponse;
        }

        #endregion

        /// <summary>Método que registra la interacción de duplicado de recibos</summary>
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
        ///<remarks>InsertInteractionDuplicateReceipts</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>16/01/2019.</FecAct></item></list>
        /// <item><Resp></Resp></item>
        /// <item><Mot>En este nuevo metodo se hace el llamado a "GetinsertInteractionGeneral" para ralizar el registro de interaccion por SP o por servicio segun el KEY "TransaccionDuplicateReceipts"</Mot></item>KEY "TransaccionDuplicateReceipts" SI=ProcesoPorServicio NO=ProcesoPorSP</list></remarks>
        public Dictionary<string, object> InsertInteractionDuplicateReceipts(Model.InteractionModel objInteractionModel, Model.TemplateInteractionModel oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {

            var strTelefono = (strNroTelephone == objInteractionModel.Telephone) ? strNroTelephone : objInteractionModel.Telephone;
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
                    PLANO = objInteractionModel.Plan,
                    CONTRACT = objInteractionModel.Contract,
                    CLASE_CODIGO = objInteractionModel.ClassCode,
                    SUBCLASE_CODIGO = objInteractionModel.SubClassCode,
                    OBJID_CONTACTO = objInteractionModel.ObjidContacto,
                    FECHA_CREACION = objInteractionModel.DateCreaction,
                    TELEFONO = objInteractionModel.Telephone,
                    TIPO = objInteractionModel.Type,
                    CLASE = objInteractionModel.Class,
                    SUBCLASE = objInteractionModel.SubClass,
                    TIPO_INTER = objInteractionModel.TypeInter,
                    METODO = objInteractionModel.Method,
                    RESULTADO = objInteractionModel.Result,
                    HECHO_EN_UNO = objInteractionModel.MadeOne,
                    NOTAS = objInteractionModel.Note,
                    FLAG_CASO = objInteractionModel.FlagCase,
                    USUARIO_PROCESO = objInteractionModel.UserProces,
                    AGENTE = objInteractionModel.Agenth

                },
                InteractionTemplate = new CommonIFIService.InsertTemplateInteraction()
                {

                    _NOMBRE_TRANSACCION = oPlantillaDat.NOMBRE_TRANSACCION,
                    _X_CLARO_NUMBER = oPlantillaDat.X_CLARO_NUMBER,
                    _X_DOCUMENT_NUMBER = oPlantillaDat.X_DOCUMENT_NUMBER,
                    _X_FIRST_NAME = oPlantillaDat.X_FIRST_NAME,
                    _X_LAST_NAME = oPlantillaDat.X_LAST_NAME,
                    _X_NAME_LEGAL_REP = oPlantillaDat.X_NAME_LEGAL_REP,
                    _X_DNI_LEGAL_REP = oPlantillaDat.X_DNI_LEGAL_REP,
                    _X_OLD_FIRST_NAME = oPlantillaDat.X_OLD_FIRST_NAME,
                    _X_OTHER_FIRST_NAME = oPlantillaDat.X_OTHER_FIRST_NAME,
                    _X_REASON = oPlantillaDat.X_REASON,
                    _X_DEPARTMENT = oPlantillaDat.X_DEPARTMENT,
                    _X_CITY = oPlantillaDat.X_CITY,
                    _X_DISTRICT = oPlantillaDat.X_DISTRICT,
                    _X_ADDRESS = oPlantillaDat.X_ADDRESS,
                    _X_INTER_16 = oPlantillaDat.X_INTER_16,
                    _X_CLAROLOCAL6 = oPlantillaDat.X_CLAROLOCAL6,
                    _X_FLAG_REGISTERED = oPlantillaDat.X_FLAG_REGISTERED,
                    _X_EMAIL = oPlantillaDat.X_EMAIL,
                    _X_RECHARGE_DATE = oPlantillaDat.X_RECHARGE_DATE,
                    _X_REFERENCE_ADDRESS = oPlantillaDat.X_REFERENCE_ADDRESS,
                    _X_CLARO_LDN1 = oPlantillaDat.X_CLARO_LDN1,
                    _X_INTER_15 = oPlantillaDat.X_INTER_15,
                    _X_INTER_6 = oPlantillaDat.X_INTER_6,
                    _X_MODEL = oPlantillaDat.X_MODEL,
                    _X_INTER_30 = oPlantillaDat.X_INTER_30

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




            return dictionaryResponse;
        }


        /// <summary>Método que realiza el envio de correo</summary>
        /// <param name="oModel"></param>
        /// <param name="vInteractionId"></param>
        /// <param name="Rpta"></param>
        /// <param name="strRutaArchivo"></param>
        /// <param name="audit"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetMedioEnviarCorreo</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetMedioEnviarCorreo(Model.DuplicateReceiptsModel oModel, string vInteractionId, out string Rpta, string strRutaArchivo, AuditRequestFixed audit)
        {
            string strResul = string.Empty;
            Rpta = "";

            if (oModel.strEmailMedio != oModel.strEmail && oModel.strMedio == "1")
            {
                GetMedioEmail(oModel, vInteractionId, out  Rpta, strRutaArchivo, audit);
            }
            else
            {
                if (oModel.ischkEmail)
                {
                    GetCheckEmail(oModel, vInteractionId, out  Rpta, strRutaArchivo, audit);
                }
            }
            if (oModel.strEmailMedio == oModel.strEmail && oModel.strMedio == "1" && !oModel.ischkEmail)
            {
                GetNotChekEmail(oModel, vInteractionId, out  Rpta, audit);
            }
            return strResul;
        }

        /// <summary>Método que envia email sin seleccionar el check </summary>
        /// <param name="oModel"></param>
        /// <param name="vInteractionId"></param>
        /// <param name="Rpta"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetNotChekEmail</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetNotChekEmail(Model.DuplicateReceiptsModel oModel, string vInteractionId, out string Rpta, AuditRequestFixed audit)
        {
            string strResul = string.Empty;
            string cadena = "";
            Rpta = "";

            #region SEND EMAIL
            if (oModel.arrPaths != null)
            {
                cadena = oModel.arrPaths.Substring(0, oModel.arrPaths.Length - 1);
            }

            List<CommonIFIService.AttachedFile> lstAdjuntos1 = new List<AttachedFile>();

            string[] Files = cadena.Split('|');

            foreach (var item in Files)
            {
                byte[] attachFile1 = null;
                string strAdjunto1 = string.IsNullOrEmpty(item) ? string.Empty : item.Substring(item.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                if (DisplayFileFromServerSharedInvoice(oModel.strIdSession, audit.transaction, item, out attachFile1))
                {
                    if (attachFile1 != null)
                    {
                        AttachedFile objFile = new AttachedFile();
                        objFile.file = attachFile1;
                        objFile.Name = strAdjunto1;
                        objFile.HeadBoard = strAdjunto1;
                        lstAdjuntos1.Add(objFile);
                    }
                }
            }
            if (lstAdjuntos1.Count > 0)
            {
                if (GetSendEmail(oModel.strIdSession, oModel, lstAdjuntos1) == Claro.SIACU.Constants.OK)
                {
                    vInteractionId = "Se enviará un correo de notificación.";
                }
                else
                {
                    vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                }
            }
            else
            {
                vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
            }

            #endregion
            // GetSendEmailAltDuplicatebill(cadena, vInteractionId, strAdjunto1, oModel, out Rpta, attachFile1);
            Claro.Web.Logging.Info("", audit.transaction, vInteractionId);
            return strResul;
        }

        /// <summary>Método que envia email al seleccionar el check </summary>
        /// <param name="oModel"></param>
        /// <param name="vInteractionId"></param>
        /// <param name="Rpta"></param>
        /// <param name="strRutaArchivo"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetCheckEmail</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetCheckEmail(Model.DuplicateReceiptsModel oModel, string vInteractionId, out string Rpta, string strRutaArchivo, AuditRequestFixed audit)
        {

            string strResul = string.Empty;
            string cadena = "";
            Rpta = "";
            #region SEND EMAIL
            if (oModel.strMedio == "1")
            {

                #region Facturas
                if (oModel.arrPaths != null)
                {
                    cadena = oModel.arrPaths.Substring(0, oModel.arrPaths.Length - 1);
                }

                List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                string[] Files = cadena.Split('|');

                foreach (var item in Files)
                {
                    byte[] attachFile1 = null;
                    string strAdjunto1 = string.IsNullOrEmpty(item) ? string.Empty : item.Substring(item.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                    if (DisplayFileFromServerSharedInvoice(oModel.strIdSession, audit.transaction, item, out attachFile1))
                    {
                        if (attachFile1 != null)
                        {
                            AttachedFile objFile = new AttachedFile();
                            objFile.file = attachFile1;
                            objFile.Name = strAdjunto1;
                            objFile.HeadBoard = strAdjunto1;
                            lstAdjuntos.Add(objFile);
                        }
                    }
                }

                #endregion

                #region Constancia
                byte[] attachConstancy = null;
                string strConstancy = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachConstancy))
                {
                    if (attachConstancy != null)
                    {
                        AttachedFile objFile = new AttachedFile();
                        string nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                        objFile.file = attachConstancy;
                        objFile.Name = nombre + ".pdf";
                        objFile.HeadBoard = nombre;
                        lstAdjuntos.Add(objFile);
                    }
                }
                #endregion

                if (lstAdjuntos.Count > 0)
                {
                    if (GetSendEmail2(oModel.strIdSession, oModel, lstAdjuntos) == Claro.SIACU.Constants.OK)
                    {
                        vInteractionId = "Se enviará un correo de notificación.";
                    }
                    else
                    {
                        vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                    }
                }
                else
                {
                    vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                }


            }
            else
            {
                byte[] attachFile2 = null;
                string strAdjunto2 = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);


                if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile2))
                {
                    List<CommonIFIService.AttachedFile> lstAdjuntos2 = new List<AttachedFile>();

                    if (attachFile2 != null)
                    {
                        AttachedFile objFile = new AttachedFile();
                        string nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                        objFile.file = attachFile2;
                        objFile.Name = nombre + ".pdf";
                        objFile.HeadBoard = nombre;

                        lstAdjuntos2.Add(objFile);
                    }
                    // GetSendEmailAltDuplicatebill2(strAdjunto, vInteractionId, strAdjunto, oModel, out Rpta, attachFile);
                    if (GetSendEmail2(oModel.strIdSession, oModel, lstAdjuntos2) == Claro.SIACU.Constants.OK)
                    {
                        vInteractionId = "Se enviará un correo de notificación.";
                    }
                    else
                    {
                        vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                    }
                }
                else
                {
                    vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                }
            }
            #endregion

            //#region SEND EMAIL
            //byte[] attachFile = null;
            //string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

            //if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile))
            //    GetSendEmailAltDuplicate(cadena, vInteractionId, strAdjunto, oModel, out Rpta, attachFile);

            //#endregion

            Claro.Web.Logging.Info("", audit.transaction, vInteractionId);
            return strResul;

        }

        /// <summary>Método que ingresa cuando selecciona el medio Email </summary>
        /// <param name="oModel"></param>
        /// <param name="vInteractionId"></param>
        /// <param name="Rpta"></param>
        /// <param name="strRutaArchivo"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetMedioEmail</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetMedioEmail(Model.DuplicateReceiptsModel oModel, string vInteractionId, out string Rpta, string strRutaArchivo, AuditRequestFixed audit)
        {
            string strResul = string.Empty;
            string cadena = "";
            Rpta = "";
            #region SEND EMAIL FACTURAS
            if (oModel.arrPaths != null)
            {

                cadena = oModel.arrPaths.Substring(0, oModel.arrPaths.Length - 1);
            }

            List<CommonIFIService.AttachedFile> lstAdjuntos1 = new List<AttachedFile>();

            string[] Files = cadena.Split('|');

            foreach (var item in Files)
            {
                byte[] attachFile1 = null;
                string strAdjunto1 = string.IsNullOrEmpty(item) ? string.Empty : item.Substring(item.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                if (DisplayFileFromServerSharedInvoice(oModel.strIdSession, audit.transaction, item, out attachFile1))
                {
                    if (attachFile1 != null)
                    {
                        AttachedFile objFile = new AttachedFile();
                        string nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                        objFile.file = attachFile1;
                        objFile.Name = strAdjunto1;
                        objFile.HeadBoard = strAdjunto1;

                        lstAdjuntos1.Add(objFile);
                    }
                }
            }
            // GetSendEmailAltDuplicatebill(cadena, vInteractionId, strAdjunto1, oModel, out Rpta, attachFile1);
            if (lstAdjuntos1.Count > 0)
            {
                if (GetSendEmail(oModel.strIdSession, oModel, lstAdjuntos1) == Claro.SIACU.Constants.OK)
                {
                    vInteractionId = "Se enviará un correo de notificación.";
                }
                else
                {
                    vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                }
            }
            else
            {
                vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
            }

            #endregion

            #region SEND EMAIL CONSTANCIA
            if (oModel.strEmail != "" && oModel.ischkEmail)
            {
                byte[] attachFile2 = null;
                string strAdjunto2 = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);


                if (DisplayFileFromServerSharedFile(oModel.strIdSession, audit.transaction, strRutaArchivo, out attachFile2))
                {
                    List<CommonIFIService.AttachedFile> lstAdjuntos2 = new List<AttachedFile>();

                    if (attachFile2 != null)
                    {
                        AttachedFile objFile = new AttachedFile();
                        string nombre = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")).ToString();
                        objFile.file = attachFile2;
                        objFile.Name = nombre + ".pdf";
                        objFile.HeadBoard = nombre;

                        lstAdjuntos2.Add(objFile);
                    }

                    // GetSendEmailAltDuplicatebill2(strAdjunto, vInteractionId, strAdjunto, oModel, out Rpta, attachFile);
                    if (GetSendEmail2(oModel.strIdSession, oModel, lstAdjuntos2) == Claro.SIACU.Constants.OK)
                    {
                        vInteractionId = "Se enviará un correo de notificación.";
                    }
                    else
                    {
                        vInteractionId = "Pero no se pudo procesar el envío del correo de notificación.";
                    }

                }
                else
                {
                    vInteractionId = "No se envió un correo de notificación.";
                }

            }
            else
            {
                vInteractionId = "No se envió un correo de notificación.";
            }
            #endregion

            Claro.Web.Logging.Info("", audit.transaction, vInteractionId);
            return strResul;
        }

        /// <summary>Método que envia el correo  </summary>
        /// <param name="strSession"></param>
        /// <param name="oModel"></param>
        /// <param name="lstFileAdjunto"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        private string GetSendEmail(string strSession, Model.DuplicateReceiptsModel oModel, List<AttachedFile> lstFileAdjunto)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strSession);
            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            if (objSendEmailWithBase64Request != null)
            {
                try
                {
                    string strMessage = string.Empty;
                    string strDestinatarios = oModel.strEmailMedio;
                    string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                        ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

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

                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDuplicadoRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";



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
                        strResul = Claro.SIACU.Constants.OK;

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error("", objAudit.transaction, ex.Message);
                }
            }

            return strResul;
        }

        /// <summary>Método que envia el correo  </summary>
        /// <param name="strSession"></param>
        /// <param name="oModel"></param>
        /// <param name="lstFileAdjunto"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetSendEmail2</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetSendEmail2(string strSession, Model.DuplicateReceiptsModel oModel, List<CommonIFIService.AttachedFile> lstFileAdjunto)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strSession);
            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            if (objSendEmailWithBase64Request != null)
            {
                try
                {
                    string strMessage = string.Empty;
                    string strDestinatarios = oModel.Destinatarios;
                    string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailDuplicadoRecibo",
                        ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                    string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

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

                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDuplicadoRecibo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";



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
                        strResul = Claro.SIACU.Constants.OK;

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error("", objAudit.transaction, ex.Message);
                }
            }

            return strResul;
        }

        /// <summary>Método que ingresa cuando selecciona el medio domicilio o fedateo </summary>
        /// <param name="oModel"></param>
        /// <param name="strEstadoForm"></param>
        /// <param name="GeneroCaso"></param>
        /// <param name="flagResult"></param>
        /// <param name="vDesInteraction"></param>
        /// <param name="strNombreArchivo"></param>
        /// <param name="hidCasoId"></param>
        /// <param name="strRutaArchivo"></param>
        /// <param name="audit"></param>
        /// <param name="AuditRequest"></param>
        /// <param name="vInteractionId"></param>
        /// <param name="Rpta"></param>
        /// <param name="resulMsg"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetMedioIdCaso</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetMedioIdCaso(Model.DuplicateReceiptsModel oModel, string strEstadoForm, string GeneroCaso, out bool flagResult, out string vDesInteraction, out string strNombreArchivo, out string hidCasoId, out string strRutaArchivo, AuditRequestFixed audit, Common.AuditRequest AuditRequest, out  string vInteractionId, out string Rpta, out string resulMsg)
        {
            string strResul = string.Empty;
            hidCasoId = "";
            strRutaArchivo = "";
            Rpta = "";
            resulMsg = "";
            flagResult = false;
            vDesInteraction = "";
            strNombreArchivo = "";
            vInteractionId = "";


            CommonIFIService.GetCaseLaRequest objCaseLaRequest;
            CommonIFIService.GetCaseLaResponse objGetCaseLaResponse;
            objCaseLaRequest =
                new CommonIFIService.GetCaseLaRequest()
                {
                    audit = AuditRequest,
                    strIdSession = oModel.strIdSession,
                    strTransaccion = audit.transaction,
                    vPhone = oModel.strTelefono,
                    vfechaini = (DateTime.Now).ToString("dd/MM/yyyy"),
                    vfechafin = (DateTime.Now).AddDays(1).ToString("dd/MM/yyyy")

                };
            objGetCaseLaResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.GetCaseLaResponse>(() => { return oCommonService.GetCaseLa(objCaseLaRequest); });

            if (objGetCaseLaResponse != null && (objGetCaseLaResponse.rMsgText == "" || objGetCaseLaResponse.rMsgText == "null"))
            {
                QueuesCaseResponse objQueuesCaseResponse;
                CommonIFIService.AuditRequest audit3 = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.strIdSession);
                QueuesCaseRequest objGetQueuesCaseRequest = new QueuesCaseRequest()
                {
                    audit = audit3,
                    SubClase = oModel.subClaseCode,
                    Des_Title = string.Empty,
                    Flag_Buscar = "1",
                    Usuario = ""
                };
                objQueuesCaseResponse = Logging.ExecuteMethod(() =>
                {
                    return oCommonService.GetQueuesCase(objGetQueuesCaseRequest);
                });

                var hidCola = "";
                var array = objQueuesCaseResponse.ListQueues;

                if (oModel.strMedio == "3")
                {
                    GetMedioDomicilio(oModel, array, out hidCola);

                }
                else if (oModel.strMedio == "4")
                {
                    GetFedateado(oModel, array, out hidCola);

                }

                FixedIFIService.Interaction objInteractionModel;
                objInteractionModel = DatosCaso(oModel);
                objInteractionModel.audit = audit;

                FixedIFIService.CaseTemplate oPlantillaCaso;
                oPlantillaCaso = DatosPlantillaCaso(oModel);
                oPlantillaCaso.audit = audit;

                var objInsertCaso = InsertCaso(objInteractionModel, oPlantillaCaso, oModel, strEstadoForm);

                hidCasoId = objInsertCaso.CASO_ID;

                if (objInsertCaso.FLAG_INSERCION == Claro.SIACU.Constants.OK)
                {
                    flagResult = true;
                    vDesInteraction = Claro.Utils.GetValueFromConfigFileIFI("strMsgTranGrabSatisDuplicateReceipts", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "<br/>"
                        + "Se ha generado el Caso   " + hidCasoId.Trim() + "   y fue enviada a la Cola " + hidCola.Trim() + ".";

                    #region CONSTANCY PDF

                    GetConstancyPDF(oModel.strIdSession, hidCasoId, oModel);
                    strRutaArchivo = oModel.strFullPathPDF;
                    #endregion
                }
                else
                {
                    vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");
                }
            }
            else
            {
                vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeErrorDuplicated");
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "MensajeFedaDomi " + vDesInteraction);
            return strResul;
        }

        /// <summary>Método que muesar el mensaje del fedateo </summary>
        /// <param name="oModel"></param>
        /// <param name="array"></param>
        /// <param name="hidCola"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetFedateado</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetFedateado(Model.DuplicateReceiptsModel oModel, List<QueuesCase> array, out string hidCola)
        {
            string strResul = string.Empty;
            hidCola = "";
            foreach (var item in array)
            {
                if (item.TITLE_COLA.Equals(ConfigurationManager.AppSettings("strConfigMedioFedateado")) && item.FLAG_ACTIVO.Equals("1"))
                {
                    oModel.cola = item.TITLE_COLA;
                    hidCola = oModel.cola;
                    break;
                }
            }
            return strResul;
        }

        /// <summary>Método que muesar el mensaje del domicilio </summary>
        /// <param name="oModel"></param>
        /// <param name="array"></param>
        /// <param name="hidCola"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetFedateado</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetMedioDomicilio(Model.DuplicateReceiptsModel oModel, List<QueuesCase> array, out string hidCola)
        {
            string strResul = string.Empty;
            hidCola = "";
            if (oModel.DNI_RUC.Length == Claro.Constants.NumberEleven)
            {
                GetRuc(oModel, out  hidCola, array);
            }
            else
            {
                GetDNI(oModel, out  hidCola, array);
            }
            return strResul;
        }

        /// <summary>Método que muestra el mensaje del DNI </summary>
        /// <param name="oModel"></param>
        /// <param name="array"></param>
        /// <param name="hidCola"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetDNI</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetDNI(Model.DuplicateReceiptsModel oModel, out string hidCola, List<QueuesCase> array)
        {
            string strResul = string.Empty;
            hidCola = "";

            foreach (var item in array)
            {
                if (item.TITLE_COLA.Equals(ConfigurationManager.AppSettings("strConfigMedioDomicilioMas")) && item.FLAG_ACTIVO.Equals("1")) //DISTRIBUCIÓN DE RECIBO
                {
                    oModel.cola = item.TITLE_COLA;
                    hidCola = oModel.cola;
                    break;
                }
            }
            return strResul;
        }

        /// <summary>Método que muestra el mensaje del RUC </summary>
        /// <param name="oModel"></param>
        /// <param name="array"></param>
        /// <param name="hidCola"></param>
        /// <returns>strResul</returns>
        /// <remarks>GetRuc</remarks>
        /// <list type="bullet">
        ///<item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        public string GetRuc(Model.DuplicateReceiptsModel oModel, out string hidCola, List<QueuesCase> array)
        {
            string strResul = string.Empty;
            hidCola = "";
            foreach (var item in array)
            {
                if (item.TITLE_COLA.Equals(ConfigurationManager.AppSettings("strConfigMedioDomicilioCorp")) && item.FLAG_ACTIVO.Equals("1")) //DISTRIBUCIÓN DE RECIBO
                {
                    oModel.cola = item.TITLE_COLA;
                    hidCola = oModel.cola;
                    break;
                }
            }
            return strResul;
        }
    }
}

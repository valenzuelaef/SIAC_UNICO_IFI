using Claro.SIACU.Web.WebApplication.IFI.App_Code;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.Web;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonService = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using FunctionsSIACU = Claro.Utils;
using MODELS = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using APPCODE = Claro.SIACU.Web.WebApplication.IFI.App_Code;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{

    public class ScheduledTasksController : CommonServicesController
    {
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        private readonly FixedIFIService.FixedIFIServiceClient oFixedService = new FixedIFIService.FixedIFIServiceClient();
        public readonly PostIFIService.PostIFIServiceClient oServicePostPaid = new PostIFIService.PostIFIServiceClient();
        private readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();

        #region Variables para obtener valores y mensajes de archivos (.config)
        public readonly string vArchivoHFCPOSTConfig = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig");
        public readonly string vArchivoHFCPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
        public readonly string vArchivoINTPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");

        #endregion

        ///<summary>Metodo que muestra la vista para tareas programadas</summary>
        ///<returns>View<returns>
        ///<remarks>ScheduledTasks</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public ActionResult ScheduledTasks()
        {
            return View();
        }


        ///<summary>Metodo que permite editar la transacción</summary>
        ///<param name="strIdSession"></param>
        ///<param name="objParams"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>EditTransaccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult EditTransaccion(string strIdSession, SuspensionReconnectionModel objParams)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objParams.idSession);
            Claro.Web.Logging.Info(objParams.idSession, objParams.idTransaccion, "Iniciando EditTransaccion");
            objParams.strCodigoAplicativo = FunctionsSIACU.CheckStr(ConfigurationManager.AppSettings("strConsCodAplicSuReTemp"));
            objParams.strPasswordAplicativo = FunctionsSIACU.CheckStr(ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago"));
            objParams.dFechaSuspension = FunctionsSIACU.GetDDMMYYYYAsDateTime(objParams.dateSuspension);
            objParams.dFechaReactivacion = FunctionsSIACU.GetDDMMYYYYAsDateTime(objParams.dateReactivation);
            objParams.idTransaccion = audit.transaction;
            string msgError = string.Empty;
            bool lblMsgView = false;
            string PathFileHPxtream = string.Empty;

            var dictionarySaveTransaccion = new Dictionary<string, object>
            {
                { "Result", ValidateEditTransaccion(strIdSession,objParams, ref msgError, ref lblMsgView, ref PathFileHPxtream)},
                { "MsgError", msgError},
                { "LblMsgView", lblMsgView},
                { "PathFileHPxtream", PathFileHPxtream}
            };

            return Json(dictionarySaveTransaccion, JsonRequestBehavior.AllowGet);
        }


        ///<summary>Metodo que permite validar la edición de la transacción</summary>
        ///<param name="strIdSession"></param>
        ///<param name="objModel"></param>
        ///<param name="outMsg"></param>
        ///<param name="lblMsgView"></param>
        ///<param name="strPathFileHPxtream"></param>
        ///<returns>bool<returns>
        ///<remarks>ValidateEditTransaccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool ValidateEditTransaccion(string strIdSession, SuspensionReconnectionModel objModel,
                                           ref string outMsg,
                                           ref bool lblMsgView,
                                           ref string strPathFileHPxtream)
        {
            lblMsgView = false;
            bool result1 = false;
            bool result2 = false;
            bool result = false;
            bool res = false;
            var audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            var objResponseDelete = new FixedIFIService.DeleteScheduledTransactionResponse();
            var objResponseDelete2 = new FixedIFIService.DeleteScheduledTransactionResponse();
            var objResponseDelete3 = new FixedIFIService.DeleteScheduledTransactionResponse();
            string strSusxReten = Claro.Utils.GetValueFromConfigFileIFI("strMensajeSusxReten", vArchivoHFCPOSTConfigMsg);
            string strMinSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMinSus", vArchivoINTPOSTConfigMsg), objModel.diasMinSinRetencion);
            string strMaxSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMaxSus", vArchivoINTPOSTConfigMsg), objModel.diasMaxSinRetencion);
            string strMenFin = Claro.Utils.GetValueFromConfigFileIFI("strMensajeFinale", vArchivoHFCPOSTConfigMsg);
            int intDias = 0;
            bool blnRetorno = false;
            string strInteraccionId = string.Empty;
            strPathFileHPxtream = string.Empty;
            string strNombreArchivo = string.Empty;
            string strRutaArchivo = string.Empty;


            if (objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) > Claro.Utils.CheckInt(objModel.diasMaxConRetencion))
                {

                    outMsg = strSusxReten;
                    return false;
                }
            }

            if (!objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) < 1)
                {

                    outMsg = strMinSus;
                    return false;
                }
            }

            if (!objModel.chkRetencion)
            {


                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) > Claro.Utils.CheckInt(objModel.diasMaxSinRetencion))
                {

                    outMsg = strMaxSus;
                    return false;
                }
            }

            intDias = Claro.Utils.CheckInt(DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion));

            if (!GetValidateCustomerId(objModel))
            {

                lblMsgView = true;
                outMsg = Claro.Utils.GetValueFromConfigFileIFI("gConstKeyNoValidaCustomerID", vArchivoHFCPOSTConfigMsg);
                return false;
            }


            blnRetorno = SaveInteraccion(objModel, ref strInteraccionId, ref strPathFileHPxtream);

            if (!blnRetorno)
            {

                lblMsgView = true;
                outMsg = Claro.Utils.GetValueFromConfigFileIFI("strMensajeGrabarInteraccionError", vArchivoHFCPOSTConfigMsg);
                return false;
            }
            if (strInteraccionId.Length > 0)
            {
                var objRequestDelete = new FixedIFIService.DeleteScheduledTransactionRequest()
                {
                    audit = audit,
                    codId = objModel.objScheduledTransactionModel.CO_ID,
                    strServiCoId = objModel.objScheduledTransactionModel.SERVI_COD,
                    strServiceState = objModel.objScheduledTransactionModel.SERVC_STATE
                };

                res = DeleteScheduled(objRequestDelete);

                if (res)
                {

                    if (objModel.objScheduledTransactionModel.SERVI_COD.Equals(Claro.Constants.NumberThreeString))
                    {
                        result1 = SuspendService(objModel.customerId,
                                              objModel.contractId,
                                              objModel.account,
                                              objModel.dFechaSuspension,
                                              intDias,
                                              objModel.strCodigoAplicativo,
                                              objModel.strPasswordAplicativo,
                                              objModel.currentUser,
                                              strInteraccionId,
                                              objModel.chkRetencion,
                                              objModel.idSession);

                        objRequestDelete.strServiCoId = Claro.Constants.NumberFourString;
                        if (DeleteScheduled(objRequestDelete))
                        {
                            result2 = ReactivateService(objModel.contractId,
                                                    objModel.dFechaReactivacion,
                                                    objModel.strCodigoAplicativo,
                                                    objModel.currentUser,
                                                    objModel.impPagar,
                                                    strInteraccionId,
                                                    objModel.customerId,
                                                    objModel.account,
                                                    objModel.idSession);
                        }
                        if (result1 && result2) result = true;
                    }
                    else
                    {
                        result = ReactivateService(objModel.contractId,
                                                   objModel.dFechaReactivacion,
                                                   objModel.strCodigoAplicativo,
                                                   objModel.currentUser,
                                                   objModel.impPagar,
                                                   strInteraccionId,
                                                   objModel.customerId,
                                                   objModel.account,
                                                   objModel.idSession);
                    }
                    if (result)
                    {
                        string strMsg = "Monto: " + objModel.impPagar + Claro.SIACU.Constants.gstrContrato + objModel.contractId + Claro.SIACU.Constants.gstrFechaHora + DateTime.Now;
                        InsertAudit(objModel, strMsg);
                        outMsg = Claro.Utils.GetValueFromConfigFileIFI("strMensajeFinale", vArchivoHFCPOSTConfigMsg);
                        if (objModel.chkEmail)
                        {

                            if (GetSendEmailEdit2(objModel, strPathFileHPxtream) == Claro.SIACU.Constants.OK)
                            {
                                outMsg = outMsg + " Se enviará un correo de notificación.";
                            }
                            else
                            {
                                outMsg = outMsg + " Pero no se pudo procesar el envío del correo de notificación.";
                            }
                        }
                        else
                        {
                            outMsg = outMsg + " No se envío un correo de notificación.";

                        }
                        return true;
                    }
                    else
                    {
                        var UpdateInter29Request = new FixedIFIService.UpdateInter29Request
                      {
                          audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(objModel.idSession),
                          p_objid = strInteraccionId,
                          p_texto = Claro.Utils.GetValueFromConfigFileIFI("strMensajeErrorEnTranActNota", vArchivoHFCPOSTConfigMsg)
                      };

                        var objResponseGetUpdateInter29 = oFixedService.GetUpdateInter29(UpdateInter29Request);
                        outMsg = Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", vArchivoHFCPOSTConfigMsg);
                        return false;
                    }

                }
                else
                {
                    var UpdateInter29Request = new FixedIFIService.UpdateInter29Request
                    {
                        audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(objModel.idSession),
                        p_objid = strInteraccionId,
                        p_texto = Claro.Utils.GetValueFromConfigFileIFI("strMensajeErrorEnTranActNota", vArchivoHFCPOSTConfigMsg)
                    };

                    var objResponseGetUpdateInter29 = oFixedService.GetUpdateInter29(UpdateInter29Request);
                    outMsg = Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", vArchivoHFCPOSTConfigMsg);

                    return false;
                }
            }
            else
            {
                outMsg = Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", vArchivoHFCPOSTConfigMsg);
                return false;
            }

        }


        ///<summary>Metodo que permite eliminar una tarea programada</summary>
        ///<param name="objRequestDelete"></param>
        ///<returns>bool<returns>
        ///<remarks>DeleteScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        private bool DeleteScheduled(DeleteScheduledTransactionRequest objRequestDelete)
        {
            var objResponseDelete = new FixedIFIService.DeleteScheduledTransactionResponse();
            var objResponseDelete2 = new FixedIFIService.DeleteScheduledTransactionResponse();
            bool result = false;
            try
            {


                objResponseDelete = Claro.Web.Logging.ExecuteMethod<FixedIFIService.DeleteScheduledTransactionResponse>(
                () => { return oFixedService.GetDeleteScheduledTransaction(objRequestDelete); });
                result = objResponseDelete.CorrectProcess;
                if (result)
                {
                    if (objRequestDelete.strServiceState.Equals(Claro.Constants.NumberThreeString))
                    {
                        objRequestDelete.strServiceState = Claro.Constants.NumberFourString;
                        objResponseDelete2 = Claro.Web.Logging.ExecuteMethod<FixedIFIService.DeleteScheduledTransactionResponse>(
                () => { return oFixedService.GetDeleteScheduledTransaction(objRequestDelete); });
                        result = objResponseDelete2.CorrectProcess;
                    }
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequestDelete.audit.Session, objRequestDelete.audit.transaction, "ERROR DeleteScheduled :" + ex.Message);
            }
            return result;
        }


        ///<summary>Metodo que permite insertar la auditoría</summary>
        ///<param name="strText"></param>
        ///<remarks>InsertAudit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        private void InsertAudit(SuspensionReconnectionModel objModel, string strText)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "SuspensionReconnectionCustomerBaseController", "InsertAudit", "strWebServiceSeguridad");
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Message" + msg);
            string strTransac = ConfigurationManager.AppSettings("strCodTranSuspReactTempIFI");
            string strService = ConfigurationManager.AppSettings("gConstEvtServicio");
            string strIpClient = Claro.Utils.CheckStr(HttpContext.Request.UserHostAddress);
            string strNameClient = objModel.fullName;
            string strIpServer = App_Code.Common.GetApplicationIp();
            string strNameServer = App_Code.Common.GetApplicationName();
            string strAccuntUser = objModel.currentUser;
            string strAmount = Claro.Constants.NumberZeroString;
            string strPhone = objModel.msisdn;
            bool result = false;



            try
            {
                CommonIFIService.SaveAuditRequestCommon objRequest = new CommonIFIService.SaveAuditRequestCommon()
                {
                    vCuentaUsuario = strAccuntUser,
                    vIpCliente = strIpClient,
                    vIpServidor = strIpServer,
                    vMonto = strAmount,
                    vNombreCliente = strNameClient,
                    vNombreServidor = strNameServer,
                    vServicio = strService,
                    vTelefono = strPhone,
                    vTexto = strText,
                    vTransaccion = strTransac,
                    audit = audit
                };


                CommonIFIService.SaveAuditResponseCommon SaveAudit = SaveResponse(objRequest);
                result = SaveAudit.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

        }


        ///<summary>Metodo que permite guardar la auditoría</summary>
        ///<param name="objRequest"></param>
        ///<returns>CommonIFIService.SaveAuditResponseCommon<returns>
        ///<remarks>SaveResponse</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public CommonIFIService.SaveAuditResponseCommon SaveResponse(CommonIFIService.SaveAuditRequestCommon objRequest)
        {
            Claro.Web.Logging.Configure();

            CommonIFIService.SaveAuditResponseCommon objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SaveAuditResponseCommon>(() =>
                {
                    return oCommonService.SaveAudit(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, ex.Message);
            }

            return objResponse;
        }


        ///<summary>Metodo que permite guardar la auditoría</summary>
        ///<param name="strCodId"></param>
        ///<param name="dtDatReactivation"></param>
        ///<param name="strCodAplic"></param>
        ///<param name="strCodUsuario"></param>
        ///<param name="importeAPagar"></param>
        ///<param name="interaccion"></param>
        ///<param name="strCustomerId"></param>
        ///<param name="strAcount"></param>
        ///<param name="strIdSession"></param>
        ///<returns>bool<returns>
        ///<remarks>ReactivateService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool ReactivateService(string strCodId,
                                    DateTime dtDatReactivation,
                                    string strCodAplic,
                                    string strCodUsuario,
                                    string importeAPagar,
                                    string interaccion,
                                    string strCustomerId,
                                    string strAcount,
                                    string strIdSession)
        {



            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            FixedIFIService.ReconexionLteResponse objResponse = new FixedIFIService.ReconexionLteResponse();
            FixedIFIService.ReconexionLteRequest objRequest = new FixedIFIService.ReconexionLteRequest();

            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "Metodo ReactivateService");

            try
            {
                var objRec = new FixedIFIService.ReconexionLte
                {
                    codigoAplicacion = strCodAplic,
                    nombreAplicacion = strCodAplic,
                    ipAplicacion = HttpContext.Request.UserHostAddress,
                    flagAccion = Convert.ToInt(Claro.Utils.GetValueFromConfigFileIFI("gConstFlagAccSusRetTempo", vArchivoHFCPOSTConfig)),
                    fechaProgramacion = dtDatReactivation,
                    coId = strCodId,
                    reason = Claro.Utils.GetValueFromConfigFileIFI("gConstReasonReconexion", vArchivoHFCPOSTConfigMsg),
                    coState = Claro.Utils.GetValueFromConfigFileIFI("gConstCoStateReconexion", vArchivoHFCPOSTConfigMsg),
                    usuario = strCodUsuario,
                    codCliente = strCustomerId,
                    montoOCC = Claro.Utils.CheckDbl(importeAPagar),
                    interaccion = interaccion,
                    telefono = string.Empty,
                    tipoServicio = string.Empty,
                    coSer = string.Empty,
                    tipoRegistro = string.Empty,
                    usuarioSistema = ConfigurationManager.AppSettings("USRProceso"),
                    usuarioApp = strCodUsuario,
                    emailUsuarioApp = string.Empty,
                    desCoSer = string.Empty,
                    codigoInteraccion = interaccion,
                    nroCuenta = strAcount,
                    ticklerCode = ConfigurationManager.AppSettings("gConstTicklerNormalSSLTE")
                };

                objRequest = new FixedIFIService.ReconexionLteRequest
                {
                    audit = objAuditRequest,
                    ReconexionLte = objRec
                };


                objResponse = Logging.ExecuteMethod(() =>
                {
                    return oFixedService.EjecutaReconexionDeServicioLte(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }

            return objResponse.ResponseStatus;
        }


        ///<summary>Metodo que permite editar las tareas programadas</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCodId"></param>
        ///<param name="dtFechaProgramacion"></param>
        ///<param name="currentUser"></param>
        ///<returns>bool<returns>
        ///<remarks>EditTaskProgram</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool EditTaskProgram(string strIdSession,
                                   string strCodId,
                                   DateTime dtFechaProgramacion,
                                   string currentUser)
        {
            bool result = false;
            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            var objRequest = new FixedIFIService.UpdateProgTaskLteRequest
            {
                audit = objAuditRequest,
                CodigoAplicacion = ConfigurationManager.AppSettings("strConsCodAplicSuReTemp"),
                NombreAplicacion = ConfigurationManager.AppSettings("strConsCodAplicSuReTemp"),
                IpAplicacion = HttpContext.Request.UserHostAddress,
                UsuarioApp = currentUser,
                ServiCod = ConfigurationManager.AppSettings("strConsServiCodSuspension"),
                ConId = strCodId,
                ServiEstado = Claro.Constants.NumberFourString,
                FechaProg = dtFechaProgramacion.ToString("yyyy-MM-dd")
            };

            var objResponse = Logging.ExecuteMethod(() =>
            {
                return oFixedService.UpdateProgTaskLte(objRequest);
            });

            if (objResponse != null)
            {
                result = objResponse.ResultStatus;
            }

            return result;
        }


        ///<summary>Metodo que permite suspender el servicio</summary>
        ///<param name="vCustomerId"></param>
        ///<param name="vContract"></param>
        ///<param name="vAccount"></param>
        ///<param name="dtFechaSuspension"></param>
        ///<param name="intDias"></param>
        ///<param name="strCodAplic"></param>
        ///<param name="strPasswordAplic"></param>
        ///<param name="strCodUsuario"></param>
        ///<param name="interaccion"></param>
        ///<param name="chkRetencion"></param>
        ///<param name="strIdSession"></param>
        ///<returns>bool<returns>
        ///<remarks>SuspendService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool SuspendService(string vCustomerId,
                                     string vContract,
                                     string vAccount,
                                     DateTime dtFechaSuspension,
                                     int intDias,
                                     string strCodAplic,
                                     string strPasswordAplic,
                                     string strCodUsuario,
                                     string interaccion,
                                     bool chkRetencion,
                                     string strIdSession)
        {
            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            FixedIFIService.PostSuspensionLteRequest objRequest = new FixedIFIService.PostSuspensionLteRequest();
            FixedIFIService.PostSuspensionLteResponse objSuspensionResponse = new FixedIFIService.PostSuspensionLteResponse();

            Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "Metodo SuspendService");
            try
            {
                FixedIFIService.SuspensionLte objSuspensionLte = new FixedIFIService.SuspensionLte
                {
                    codigoAplicacion = strCodAplic,
                    nombreAplicacion = strCodAplic,
                    codigoInteraccion = interaccion,
                    ipAplicacion = HttpContext.Request.UserHostAddress,

                    coId = vContract,
                    coSer = string.Empty,
                    desCoSer = string.Empty,
                    codCliente = vCustomerId,
                    desTickler = Claro.Utils.GetValueFromConfigFileIFI("gConstDesTicklerNormal", vArchivoHFCPOSTConfigMsg),
                    emailUsuarioApp = string.Empty,
                    fechaProgramacion = dtFechaSuspension,
                    fechaSuspension = dtFechaSuspension,
                    fideliza = chkRetencion ? Claro.Constants.NumberOne : Claro.Constants.NumberZero,
                    flagAccion = Convert.ToInt(Claro.Utils.GetValueFromConfigFileIFI("gConstFlagAccSusRetTempo", vArchivoHFCPOSTConfig)),
                    nroCuenta = vAccount,
                    nroDias = intDias,
                    reason = Claro.Utils.GetValueFromConfigFileIFI("gConstReasonSuspencion", vArchivoHFCPOSTConfigMsg),
                    coState = Claro.Utils.GetValueFromConfigFileIFI("gConstCoStateSuspension", vArchivoHFCPOSTConfigMsg),
                    telefono = string.Empty,
                    ticklerCode = ConfigurationManager.AppSettings("gConstTicklerNormalSSLTE"),
                    tipoRegistro = string.Empty,
                    tipoServicio = string.Empty,
                    usuario = strCodUsuario,
                    usuarioApp = strCodUsuario,
                    usuarioSistema = ConfigurationManager.AppSettings("USRProceso")
                };

                objRequest = new FixedIFIService.PostSuspensionLteRequest
                {
                    audit = objAuditRequest,
                    Suspension = objSuspensionLte
                };

                objSuspensionResponse = Logging.ExecuteMethod(() => oFixedService.EjecutaSuspensionDeServicioLte(objRequest));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }

            return objSuspensionResponse.ResponseStatus;
        }



        ///<summary>Metodo que permite obtener los datos de la plantilla de la interacción</summary>
        ///<param name="objModel"></param>
        ///<returns>CommonIFIService.InsertTemplateInteraction<returns>
        ///<remarks>DataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public CommonIFIService.InsertTemplateInteraction DataTemplateInteraction(SuspensionReconnectionModel objModel)
        {
            var responseModel = new CommonIFIService.InsertTemplateInteraction();
            var strImporte = string.Empty;
            try
            {
                responseModel._X_CLARO_NUMBER = objModel.msisdn;
                responseModel._X_INTER_1 = objModel.dateSuspension;
                responseModel._X_INTER_2 = objModel.dateReactivation;
                responseModel._X_INTER_3 = objModel.chkRetencion ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
                responseModel._X_BASKET = objModel.contractId;
                responseModel._X_ADDRESS5 = objModel.fullName;
                responseModel._X_BIRTHDAY = DateTime.UtcNow;
                responseModel._X_INTER_17 = objModel.fullName;
                responseModel._X_NAME_LEGAL_REP = objModel.legalAgent;
                responseModel._X_EMAIL_CONFIRMATION = objModel.chkEmail ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
                responseModel._X_CLARO_LDN1 = objModel.documentType;
                responseModel._X_CLARO_LDN2 = objModel.documentNumber;
                responseModel._X_INTER_20 = objModel.account;
                responseModel._X_INTER_16 = objModel.contractId;
                responseModel._X_INTER_21 = objModel.tipoCliente;

                responseModel._X_INTER_5 = objModel.impPagar;
                responseModel._X_INTER_6 = objModel.notes;
                responseModel._X_INTER_7 = objModel.montoRet;
                responseModel._X_INTER_15 = objModel.cboCACDAC;
                responseModel._X_INTER_30 = objModel.notes;
                responseModel._X_EMAIL = objModel.email;










               
                
            }
            catch (Exception ex)
            {
                Logging.Error(objModel.idSession, objModel.idSession, ex.Message);
            }

            return responseModel;
        }


        ///<summary>Metodo que permite obtener los datos de la interacción</summary>
        ///<param name="objModel"></param>
        ///<returns>CommonIFIService.Iteraction<returns>
        ///<remarks>DataInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>     
        public CommonIFIService.Iteraction DataInteraction(SuspensionReconnectionModel objModel)
        {
            var responseModel = new CommonIFIService.Iteraction();
            try
            {
                var tipo = ConfigurationManager.AppSettings("gConstTipoINT");
                responseModel.START_DATE = DateTime.UtcNow.ToString("dd/MM/yyyy");
                responseModel.TELEFONO = objModel.msisdn;
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
                responseModel.NOTAS = objModel.notes;
                responseModel.FLAG_CASO = Claro.Constants.NumberZeroString;
                responseModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                responseModel.AGENTE = objModel.currentUser;
                responseModel.CONTRACT = objModel.contractId;
                responseModel.PLANO = objModel.codPlano ?? string.Empty;

            }
            catch (Exception ex)
            {
                Logging.Error(objModel.idSession, objModel.idTransaccion, ex.Message);
            }

            return responseModel;
        }


        ///<summary>Metodo que permite insertar la interacción</summary>
        ///<param name="objInteractionModel"></param>
        ///<param name="oPlantillaDat"></param>
        ///<param name="strNroTelephone"></param>
        ///<param name="strUserSession"></param>
        ///<param name="strUserAplication"></param>
        ///<param name="strPassUser"></param>
        ///<param name="boolEjecutTransaction"></param>
        ///<param name="strIdSession"></param>
        ///<param name="strCustomerId"></param>
        ///<returns>CommonIFIService.InsertGeneralResponse<returns>
        ///<remarks>InsertInterac</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public CommonIFIService.InsertGeneralResponse InsertInterac(CommonIFIService.Iteraction objInteractionModel, Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            CommonIFIService.InsertGeneralRequest objRequest;
            CommonIFIService.InsertGeneralResponse objResult = null;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            try
            {
                objRequest = new CommonService.InsertGeneralRequest()
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


        ///<summary>Metodo que permite guardar la interacción</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strPathFileHPxtream"></param>
        ///<returns>bool<returns>
        ///<remarks>SaveInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public bool SaveInteraccion(SuspensionReconnectionModel objModel, ref string strInteraccionId, ref string strPathFileHPxtream)
        {
            string strUSUARIO_SISTEMA = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
            string strUSUARIO_APLICACION = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strPASSWORD_USUARIO = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            var oInteraction = DataInteraction(objModel);

            if (oInteraction == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(objModel.claseDes))
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


            if (strFlagInsercion.Trim().ToUpper() == Claro.SIACU.Constants.OK.Trim().ToUpper() && strFlagInsercionInteraccion.Trim().ToUpper() == Claro.SIACU.Constants.OK.Trim().ToUpper())
            {
                strPathFileHPxtream = GetConstancyPDF(objModel.idSession, objModel, strInteraccionId);
            }

            return true;
        }


        ///<summary>Metodo que permite generar la constancia en formato PDF para el desistimiento de un servicio</summary>
        ///<param name="strIdSession"></param>
        ///<param name="objModel"></param>
        ///<param name="TypeTransaction"></param>
        ///<param name="strInteraction"></param>
        ///<returns>string<returns>
        ///<remarks>GetConstancyPDFDelete</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public string GetConstancyPDFDelete(string strIdSession, MODELS.SuspensionReconnectionModel objModel, string TypeTransaction, string strInteraction)
        {
            string NAME_PDF = string.Empty;
            string strTransactionConstancy = string.Empty;
            string strTransactionType = string.Empty;
            string strTransactionFolder = string.Empty;
            string strAction = string.Empty;
            string strSendEmail = string.Empty;

            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            switch (TypeTransaction)
            {
                case Claro.Constants.NumberThreeString:
                    strTransactionConstancy = ConfigurationManager.AppSettings("strDatosSuspDesis");
                    strTransactionType = "Suspensión";
                    strTransactionFolder = ConfigurationManager.AppSettings("strCarpetaSuspDesisIFI");
                    strSendEmail = Claro.SIACU.Constants.Yes;
                    break;
                case Claro.Constants.NumberOneString:
                    strTransactionConstancy = ConfigurationManager.AppSettings("strPlanMigrationIFIDesConstancy");
                    strTransactionType = ConfigurationManager.AppSettings("strPlanMigrationIFIOperation");
                    strAction = ConfigurationManager.AppSettings("strPlanMigrationIFIDesistimiento");
                    strTransactionFolder = ConfigurationManager.AppSettings("strPlanMigrationIFIDesConstancyFolder");
                    strSendEmail = objModel.email.Trim() != string.Empty ?  Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
                    break;
                default:
                    strTransactionConstancy = ConfigurationManager.AppSettings("strDatosRetDesis");
                    strTransactionType = "Cancelación";
                    strTransactionFolder = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoDesisIFI");
                    strSendEmail = Claro.SIACU.Constants.Yes;
                    break;
            }

            try
            {
                CommonIFIService.ParametersGeneratePDF oParameter = new CommonIFIService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = strTransactionConstancy,
                    StrTitularCliente = objModel.fullName,
                    StrRepresLegal = objModel.legalAgent,
                    StrTipoDocIdentidad = objModel.documentType,
                    strNroDoc = objModel.documentNumber,
                    StrCuentaPostpago = objModel.account,
                    strContrato = objModel.contractId,
                    strTipoCliente = objModel.tipoCliente,
                    strEnvioCorreo = strSendEmail,
                    strCorreoCliente = objModel.email,
                    StrCarpetaTransaccion = strTransactionFolder,
                    strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital"),
                    StrCodUsuario = objModel.currentUser,
                    StrTipoTransaccion = strTransactionType,
                    StrFecDesistimiento = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                    StrAccionIFI = "Borrar Programación",
                    StrCentroAtencionArea = objModel.cboCACDAC,
                    StrNroServicio = objModel.msisdn,
                    StrIdInteraccion = strInteraction,
                    StrNombreAgenteUsuario = objModel.fullNameUser,
                    StrCasoInter=strInteraction,
                    strFechaTransaccion = DateTime.Now.ToShortDateString(),
                    StrCicloFacturacion = objModel.cicloFacturacion,
                    StrFechaEjecucion = objModel.strProgramDate,
                    StrAccion = strAction,
                    StrNroDocIdentidad = objModel.documentNumber
                };


                CommonIFIService.GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "NAME_PDF:  " + NAME_PDF);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }


        ///<summary>Metodo que permite generar la constancia en formato PDF</summary>
        ///<param name="strIdSession"></param>
        ///<param name="objModel"></param>
        ///<param name="strInteraction"></param>
        ///<returns>string<returns>
        ///<remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public string GetConstancyPDF(string strIdSession, MODELS.SuspensionReconnectionModel objModel, string strInteraction)
        {
            string NAME_PDF = string.Empty;

            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            try
            {

                CommonIFIService.ParametersGeneratePDF oParameter = new CommonIFIService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strDatosSuspReconTransac"),
                    StrCentroAtencionArea = objModel.cboCACDAC,
                    StrTitularCliente = objModel.fullName,
                    StrTipoDocIdentidad = objModel.documentType,
                    StrNroDocIdentidad = objModel.documentNumber,
                    StrCasoInter = strInteraction,
                    StrCuentaPostpago = objModel.account,
                    StrFechaTransaccionProgram = DateTime.Today.ToString("dd/MM/yyyy"),
                    strTipoCliente = objModel.tipoCliente,
                    StrFechaSuspension = objModel.dateSuspension,
                    StrFechaActivacion = objModel.dateReactivation,
                    StrCostoReactivacion = objModel.impPagar,
                    strEnvioCorreo = objModel.chkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not,
                    strCorreoCliente = objModel.chkEmail ? objModel.email : string.Empty,

                    StrEmail = objModel.email,
                    strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital"),
                    StrRepresLegal = objModel.legalAgent,
                    strContrato = objModel.msisdn,
                    StrCodUsuario = objModel.currentUser,
                    StrNombreAgenteUsuario = objModel.fullNameUser,

                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaSuspReconINT")
                };


                CommonIFIService.GenerateConstancyResponseCommon response = new CommonServicesController().GenerateContancyPDF(objAuditRequest.Session, oParameter);

                NAME_PDF = response.FullPathPDF;

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "NAME_PDF:   " + NAME_PDF);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }


        ///<summary>Metodo que permite validar el id del cliente</summary>
        ///<param name="objModel"></param>
        ///<returns>bool<returns>
        ///<remarks>GetValidateCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public bool GetValidateCustomerId(SuspensionReconnectionModel objModel)
        {
            FixedIFIService.ValidateCustomerIdResponse objRegionResponse = null;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(objModel.idSession);
            FixedIFIService.ValidateCustomerIdRequest objRegionRequest = new FixedIFIService.ValidateCustomerIdRequest();
            objRegionRequest.audit = audit;
            objRegionRequest.Phone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + objModel.customerId;
            bool valueCustomer = true;

            try
            {
                objRegionResponse = Logging.ExecuteMethod<FixedIFIService.ValidateCustomerIdResponse>(() =>
                {
                    return oFixedService.GetValidateCustomerId(objRegionRequest);
                });

                if (objRegionResponse.FlgResult != Claro.Constants.NumberZeroString)
                {
                    FixedIFIService.CustomerResponse objCustomerResponse = null;
                    FixedIFIService.Customer objCustomerRequest = new FixedIFIService.Customer();
                    objCustomerRequest.audit = audit;
                    objCustomerRequest.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + objModel.customerId;
                    objCustomerRequest.User = objModel.currentUser;
                    objCustomerRequest.Name = objModel.name;
                    objCustomerRequest.LastName = objModel.lastName;
                    objCustomerRequest.BusinessName = objModel.businessName;
                    objCustomerRequest.DocumentType = objModel.documentType;
                    objCustomerRequest.DocumentNumber = objModel.documentNumber;
                    objCustomerRequest.Address = objModel.address;
                    objCustomerRequest.District = objModel.district;
                    objCustomerRequest.Departament = objModel.departament;
                    objCustomerRequest.Province = objModel.province;
                    objCustomerRequest.Modality = ConfigurationManager.AppSettings("gConstKeyStrModalidad");


                    try
                    {
                        objCustomerResponse = Logging.ExecuteMethod<FixedIFIService.CustomerResponse>(() =>
                        {
                            return oFixedService.GetRegisterCustomerId(objCustomerRequest);
                        });
                        if (objCustomerResponse.Resultado)
                        {
                            if (objCustomerResponse.vFlagConsulta.Trim() == ConfigurationManager.AppSettings("gConstKeyStrResultInsertCusID"))
                            {
                                valueCustomer = false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logging.Error(audit.Session, audit.transaction, e.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            return valueCustomer;
        }



        ///<summary>Metodo para obtener valores para editar una tarea programada</summary>
        /// <param name="strIdSession"></param>
        /// <param name="objScheduledTransaction"></param>
        ///<returns>Json<returns>
        ///<remarks>GetEditScheduledTasks</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult GetEditScheduledTasks(string strIdSession, ScheduledTransaction objScheduledTransaction)
        {

            ScheduledTransaction oSuspension;
            ScheduledTransaction oReactivation;
            ScheduledTransaction objGeneralScheduled;
            SuspensionReconnectionModel objSuspensionReconnectionModel = new SuspensionReconnectionModel();
            objSuspensionReconnectionModel.TotalimpPagarEnabled = true;
            objSuspensionReconnectionModel.impPagarEnabled = true;
            objSuspensionReconnectionModel.montoRetEnabled = true;
            objSuspensionReconnectionModel.chkRetencionEnabled = true;
            bool result = true;
            var audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            var auditCommonIFI = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            var objResponse = new FixedIFIService.ListScheduledTransactionsResponse();

            var objResponseDataTemplate = new CommonIFIService.DatTempInteractionResponse();
            var objRequest = new FixedIFIService.ListScheduledTransactionsRequest()
            {
                audit = audit,
                ServiCoId = objScheduledTransaction.CO_ID == null ? "" : objScheduledTransaction.CO_ID,
                Account = "",
                StrStartDate = "",
                StrEndDate = "",
                ServiceState = "",
                Advisor = "",
                TransactionType = "",
                CodeInteraction = "",
                NameCACDAC = ""
            };

            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedIFIService.ListScheduledTransactionsResponse>(
                    () => { return oFixedService.GetListScheduledTransactions(objRequest); });


                if (objResponse.LstTransactions.Count > 0)
                {

                    oSuspension = objResponse.LstTransactions.Where(s => s.SERVI_COD == Claro.Constants.NumberThreeString)
                                            .OrderByDescending(s => s.SERVC_CODE_INTERACTION)
                                            .FirstOrDefault();

                    oReactivation = objResponse.LstTransactions.Where(s => s.SERVI_COD == Claro.Constants.NumberFourString)
                                                .OrderByDescending(s => s.SERVC_CODE_INTERACTION)
                                                .FirstOrDefault();

                    if (objScheduledTransaction.TServ.Equals(Claro.Constants.NumberFourString))
                    {
                        objSuspensionReconnectionModel.dateReactivation = oReactivation.SERVD_DATEPROG;
                        objSuspensionReconnectionModel.dateSuspension = oSuspension.SERVD_DATEPROG;
                        objSuspensionReconnectionModel.dateSuspensionEnabled = false;
                        objSuspensionReconnectionModel.dateReactivationEnabled = true;
                        objGeneralScheduled = oReactivation;

                    }
                    else
                    {
                        objSuspensionReconnectionModel.dateReactivation = oReactivation.SERVD_DATEPROG;
                        objSuspensionReconnectionModel.dateSuspension = oSuspension.SERVD_DATEPROG;
                        objSuspensionReconnectionModel.dateReactivationEnabled = false;
                        objSuspensionReconnectionModel.dateSuspensionEnabled = true;
                        objGeneralScheduled = oSuspension;
                    }

                    InteractionTemplate objInteractionTemplate;
                    var objRequestDataTemplate = new CommonIFIService.DatTempInteractionRequest()
                    {
                        audit = auditCommonIFI,
                        vInteraccionID = objGeneralScheduled.SERVC_CODE_INTERACTION
                    };
                    if (!string.IsNullOrEmpty(objGeneralScheduled.SERVC_CODE_INTERACTION))
                    {
                        objResponseDataTemplate = Claro.Web.Logging.ExecuteMethod<CommonIFIService.DatTempInteractionResponse>(
                     () => { return oCommonService.GetInfoInteractionTemplate(objRequestDataTemplate); });
                        objInteractionTemplate = objResponseDataTemplate.InteractionTemplate;

                        if (objInteractionTemplate != null)
                        {


                            if (objInteractionTemplate.X_INTER_3 != null && objInteractionTemplate.X_INTER_3.Equals(Claro.Constants.NumberOneString))
                            {
                                objSuspensionReconnectionModel.chkRetencion = true;
                                objSuspensionReconnectionModel.montoRet = !string.IsNullOrEmpty(objInteractionTemplate.X_INTER_5) ? objInteractionTemplate.X_INTER_5 : "";
                            }
                            else
                            {
                                objSuspensionReconnectionModel.chkRetencion = false;
                                objSuspensionReconnectionModel.montoRet = Claro.Constants.NumberZeroString;
                            }

                            objSuspensionReconnectionModel.impPagar = !string.IsNullOrEmpty(objInteractionTemplate.X_INTER_5) ? objInteractionTemplate.X_INTER_5 : "";
                            objSuspensionReconnectionModel.TotalimpPagarEnabled = false;
                            objSuspensionReconnectionModel.impPagarEnabled = false;
                            objSuspensionReconnectionModel.montoRetEnabled = false;
                            objSuspensionReconnectionModel.chkRetencionEnabled = false;
                        }
                    }
                    objSuspensionReconnectionModel.objScheduledTransactionModel = objGeneralScheduled;
                }
                return Json(new { data = objSuspensionReconnectionModel, result = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = false;

                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);
                return Json(new { data = objSuspensionReconnectionModel, result = result }, JsonRequestBehavior.AllowGet);
            }

        }


        ///<summary>Eliminar una tarea programada</summary>
        /// <param name="strIdSession"></param>
        ///<param name="objModel"></param>
        /// <param name="StrStartDate"></param>
        /// <param name="StrEndDate"></param>
        ///<returns>Json<returns>
        ///<remarks>DeleteScheduledTasks</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult DeleteScheduledTasks(string strIdSession, SuspensionReconnectionModel objModel, string StrStartDate, string StrEndDate)
        {
            ScheduledTransaction obj;
            string strInteraccionId = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            #region Variables para obtener valores y mensajes de archivos (.config)
            string vArchivoHFCPOSTConfig = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig");
            string vArchivoHFCPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
            string vArchivoINTPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
            #endregion
            var objResponse = new FixedIFIService.ListScheduledTransactionsResponse();
            var objResponseDelete = new FixedIFIService.DeleteScheduledTransactionResponse();

            var objRequest = new FixedIFIService.ListScheduledTransactionsRequest()
                       {
                           audit = audit,
                           ServiCoId = objModel.objScheduledTransactionModel.CO_ID == null ? "" : objModel.objScheduledTransactionModel.CO_ID,
                           StrStartDate = StrStartDate == null ? "" : StrStartDate,
                           StrEndDate = StrEndDate == null ? "" : StrEndDate,
                           ServiceState = objModel.objScheduledTransactionModel.SERVC_STATE == null ? "" : objModel.objScheduledTransactionModel.SERVC_STATE,
                           TransactionType = objModel.objScheduledTransactionModel.SERVI_COD == null ? "" : objModel.objScheduledTransactionModel.SERVI_COD
                       };

            var objRequestDelete = new FixedIFIService.DeleteScheduledTransactionRequest()
            {
                audit = audit,
                codId = objModel.objScheduledTransactionModel.CO_ID,
                strServiCoId = objModel.objScheduledTransactionModel.SERVI_COD,
                strServiceState = objModel.objScheduledTransactionModel.SERVC_STATE
            };
            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedIFIService.ListScheduledTransactionsResponse>(
                    () => { return oFixedService.GetListScheduledTransactions(objRequest); });


                if (objResponse.LstTransactions.Count > 1)
                {
                    return Json(new { data = Claro.Utils.GetValueFromConfigFileIFI("strMsgErrSEMD1TAE", vArchivoHFCPOSTConfigMsg) });  //GeneradorLog.ObtenerValorConfig("strMsgErrSEMD1TAE", vArchivoHFCPOSTConfigMsg)
                }
                if (objResponse.LstTransactions.Count == 1)
                {
                    obj = objResponse.LstTransactions.FirstOrDefault();
                    if (obj.DESC_SERVICE.Equals(Claro.Utils.GetValueFromConfigFileIFI("strConstTipoReact", vArchivoHFCPOSTConfigMsg)))
                    {
                        return Json(new { data = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoPueEliReact", vArchivoHFCPOSTConfigMsg) });
                    }
                    else
                    {
                        if (obj.SERVC_STATE.Equals(Claro.Constants.NumberThreeString) || obj.SERVC_STATE.Equals(Claro.Constants.NumberFourString))
                        {
                            return Json(new { data = Claro.Utils.GetValueFromConfigFileIFI("strMsgTranYaRealOError", vArchivoHFCPOSTConfigMsg) });
                        }
                        else
                        {

                            objResponseDelete = Claro.Web.Logging.ExecuteMethod<FixedIFIService.DeleteScheduledTransactionResponse>(
                   () => { return oFixedService.GetDeleteScheduledTransaction(objRequestDelete); });

                            if (obj.SERVI_COD.Equals(Claro.Constants.NumberThreeString) && (objResponseDelete.CorrectProcess))
                            {
                                objRequestDelete.strServiCoId = Claro.Constants.NumberFourString;
                                objResponseDelete = Claro.Web.Logging.ExecuteMethod<FixedIFIService.DeleteScheduledTransactionResponse>(
                   () => { return oFixedService.GetDeleteScheduledTransaction(objRequestDelete); });
                            }
                        }
                    }
                }
                else
                {
                    return Json(new { data = Claro.Utils.GetValueFromConfigFileIFI("strMsgErroOtbTraPro", vArchivoHFCPOSTConfigMsg) });
                }

                if (objResponseDelete.CorrectProcess)
                {
                    string msgSendEmail = String.Empty;
                    bool blresult = SaveInteraccionDeleteScheduled(objModel.objScheduledTransactionModel.SERVI_COD, objModel, ref strInteraccionId, ref msgSendEmail, objModel.objScheduledTransactionModel.SERVC_CODE_INTERACTION);

                    InsertAudit(objModel, Claro.Utils.GetValueFromConfigFileIFI("strMsgAuditTranProgE", vArchivoHFCPOSTConfigMsg));

                    return Json(new { data = (Claro.Utils.GetValueFromConfigFileIFI("strMsgExitoElimProgra", vArchivoHFCPOSTConfigMsg) + ". " + msgSendEmail) });
                }
                else
                {
                    return Json(new { data = Claro.Utils.GetValueFromConfigFileIFI("strMsgErrorElimProgra", vArchivoHFCPOSTConfigMsg) });
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);
                return Json(new { data = Claro.SIACU.Constants.NO_OK });
            }

        }



        ///<summary>Metodo que permite actualizar el parametro inter30 de la plantilla de la interacción</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strInteractionId"></param>
        ///<param name="pText"></param>
        ///<remarks>ExecuteUpdateInter30</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public void ExecuteUpdateInter30(string strIdSession, string strTransaction, string strInteractionId, string pText)
        {
            CommonIFIService.AuditRequest audit = Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            UpdatexInter30Request objUpdateInter30Request = new UpdatexInter30Request
            {
                audit = audit,
                p_objid = strInteractionId,
                p_texto = pText
            };

            Logging.ExecuteMethod(() => { return oCommonService.GetUpdatexInter30(objUpdateInter30Request); });
        }



        ///<summary>Registra la  Interaccion al borrar una tarea programada</summary>
        ///<param name="TypeTransaction"></param>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<returns>Bool<returns>
        ///<remarks>SaveInteraccionDeleteScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        private bool SaveInteraccionDeleteScheduled(string TypeTransaction, SuspensionReconnectionModel objModel, ref string strInteraccionId, ref string msgSendEmail, string strInteractionID)
        {
            var strFlagInsercion = "";
            var audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);
            try
            {

                string strUSUARIO_SISTEMA = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                string strUSUARIO_APLICACION = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                string strPASSWORD_USUARIO = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                if (TypeTransaction == Claro.Constants.NumberOneString)
                {
                    TemplateInteractionModel oTemplateInterPlus = new TemplateInteractionModel();
                    oTemplateInterPlus = GetInfoInteractionTemplate(audit.Session, strInteractionID);
                    objModel.notes = ConfigurationManager.AppSettings("strPlanMigrationIFINotes");
                    objModel.businessName = oTemplateInterPlus.X_CLAROLOCAL1;
                    objModel.fullName = oTemplateInterPlus.X_CLAROLOCAL2;
                    objModel.documentNumber = oTemplateInterPlus.X_DOCUMENT_NUMBER;
                    objModel.email = oTemplateInterPlus.X_EMAIL;
                    objModel.cboCACDAC = oTemplateInterPlus.X_INTER_6;
                    objModel.strProgramDate = oTemplateInterPlus.X_INTER_5;
                    objModel.documentType = oTemplateInterPlus.X_TYPE_DOCUMENT;
                    objModel.legalAgent = oTemplateInterPlus.X_NAME_LEGAL_REP;
                    objModel.cicloFacturacion = oTemplateInterPlus.X_AMOUNT_UNIT;
                    objModel.msisdn = oTemplateInterPlus.X_CLARO_NUMBER;
                    objModel.strAction = ConfigurationManager.AppSettings("strPlanMigrationIFIDesistimiento");
                }
                
                var oInteraction = DataInteraction(objModel);

                if (oInteraction == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(objModel.claseDes))
                {
                    return false;
                }

                var oTemplateInteraction = DataTemplateInteractionDeleteScheduled(TypeTransaction, objModel);
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
                strFlagInsercion = resultInteraction.rFlagInsercion.ToString();
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
                    string strRutaArchivoDelete = GetConstancyPDFDelete(objModel.idSession, objModel, TypeTransaction, strInteraccionId);
                    string strNombreArchivo = string.Empty;
                    #region SEND EMAIL
                    msgSendEmail = GetSendEmailDelete2(objModel, strRutaArchivoDelete, TypeTransaction);
                    if (msgSendEmail == Claro.SIACU.Constants.OK)
                    {
                        msgSendEmail = Claro.Utils.GetValueFromConfigFileIFI("strTextoEnvioCorreo", vArchivoINTPOSTConfigMsg);
                        Logging.Info(objModel.idSession, audit.transaction, "Se logro enviar el correo ");
                    }
                    else
                    {
                        msgSendEmail = Claro.Utils.GetValueFromConfigFileIFI("strTextoNoEnvioCorreo", vArchivoINTPOSTConfigMsg);
                        Logging.Error(objModel.idSession, audit.transaction, "No se logro enviar el correo ");
                    }

                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {

                if (strFlagInsercion.Length > 1)
                {//mg13
                    ExecuteUpdateInter30(objModel.idSession, audit.transaction, strInteraccionId, Claro.Utils.GetValueFromConfigFileIFI("strMensajeErrorparaNotasClfy", vArchivoHFCPOSTConfigMsg));
                }
                Logging.Error(objModel.idSession, audit.transaction, ex.Message);

                return false;
            }


        }



        #region enviar correo


        ///<summary>Metodo que permite enviar un correo cuando se hace un desistimiento de un servicio</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strRutaArchivo"></param>
        ///<param name="TypeTransaction"></param>
        ///<returns>string</returns>
        ///<remarks>GetSendEmailDelete</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public string GetSendEmailDelete(SuspensionReconnectionModel objModel, string strInteraccionId, string strRutaArchivo, string TypeTransaction)
        {
            CommonServicesController objCommonServicesController = new CommonServicesController();
            WirelessIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(objModel.idSession);
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            byte[] attachFile = null;
            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

            if (objCommonServicesController.DisplayFileFromServerSharedFile(objModel.idSession, audit.transaction, strRutaArchivo, out attachFile))
            {
                Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                {
                    strIdSession = objModel.idSession,
                    strTo = objModel.email,
                    strSubject = TypeTransaction.Equals(Claro.Constants.NumberThreeString) ? Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionDesistimiento", vArchivoINTPOSTConfigMsg) : Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailCancelacionDesistimiento", vArchivoINTPOSTConfigMsg),
                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDesistimiento", vArchivoINTPOSTConfigMsg),
                    strAttached = strAdjunto,
                    byteAttached = attachFile

                };
                MensajeEmail = GetSendEmailConstDelete(objSendEmail);
                return Claro.SIACU.Constants.OK;
            }
            else
            {
                return null;
            }

        }
        #endregion


        ///<summary>Enviar Correo con la constancia generada al eliminar una tarea programada</summary>
        ///<param name="objSendEmail"></param>
        ///<returns>string<returns>
        ///<remarks>GetSendEmailConstDelete</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public string GetSendEmailConstDelete(Model.SendEmailModel objSendEmail)
        {
            CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse = new CommonIFIService.SendEmailResponseCommon();
            CommonIFIService.AuditRequest AuditRequest = APPCODE.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objSendEmail.strIdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest =
                new CommonIFIService.SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = ConfigurationManager.AppSettings("CorreoServicioAlCliente"),
                    strTo = objSendEmail.strTo,
                    strSubject = objSendEmail.strSubject,
                    strMessage = TemplateEmailDelete(objSendEmail.strMessage, objSendEmail.strMsgEmailCall),
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

        ///<summary>Plantilla para enviar correo al eliminar una tarea programada</summary>
        ///<param name="strMessage"></param>
        ///<param name="strMsgEmailCall"></param>
        ///<returns>string<returns>
        ///<remarks>TemplateEmailDelete</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public string TemplateEmailDelete(string strMessage, string strMsgEmailCall)
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
            strHtml.Append(strMsgEmailCall);
            strHtml.Append("</table>");
            strHtml.Append("</body>");
            strHtml.Append("</html>");

            return strHtml.ToString();

        }


        ///<summary>Plantilla para guardar datos en la interaccion al eliminar una tarea programada</summary>
        ///<param name="TypeTransaction"></param>
        ///<param name="objModel"></param>
        ///<returns>CommonIFIService.InsertTemplateInteraction<returns>
        ///<remarks>DataTemplateInteractionDeleteScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public CommonIFIService.InsertTemplateInteraction DataTemplateInteractionDeleteScheduled(string TypeTransaction, SuspensionReconnectionModel objModel)
        {
            var responseModel = new CommonIFIService.InsertTemplateInteraction();
            var strImporte = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);

            var objResponse = new CommonIFIService.TransactionTypeResponseCommon();
            var objRequest = new CommonIFIService.TransactionTypeRequestCommon()
            {
                audit = audit
            };
            try
            {
                responseModel._X_CLARO_NUMBER = objModel.msisdn;
                responseModel._X_CLARO_LDN1 = objModel.documentType;
                responseModel._X_OPERATION_TYPE = string.Empty;
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.TransactionTypeResponseCommon>(
                    () => { return oCommonService.GetTransactionType(objRequest); });

                foreach (var item in objResponse.TransactionTypes)
                {
                    if (TypeTransaction.Equals(item.Code))
                    {
                        responseModel._X_OPERATION_TYPE = item.Description;
                    }
                }

                responseModel._X_INTER_1 = objModel.dateSuspension;
                responseModel._X_INTER_2 = objModel.dateReactivation;
                responseModel._X_INTER_3 = objModel.chkRetencion ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
                responseModel._X_EMAIL_CONFIRMATION = objModel.chkEmail ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
                responseModel._X_BASKET = objModel.contractId;
                responseModel._X_ADDRESS5 = objModel.fullName;
                responseModel._X_BIRTHDAY = DateTime.Now;
                responseModel._X_INTER_17 = objModel.fullName;
                responseModel._X_NAME_LEGAL_REP = objModel.legalAgent;

                responseModel._X_INTER_19 = TypeTransaction;
                responseModel._X_CLARO_LDN2 = objModel.documentNumber;
                responseModel._X_INTER_20 = objModel.account;
                responseModel._X_INTER_16 = objModel.contractId;
                responseModel._X_INTER_21 = objModel.tipoCliente;

                responseModel._X_INTER_5 = objModel.impPagar;
                responseModel._X_INTER_6 = objModel.notes;
                responseModel._X_INTER_7 = objModel.montoRet;
                responseModel._X_INTER_15 = objModel.cboCACDAC;
                responseModel._X_INTER_30 = objModel.notes;
                responseModel._X_EMAIL = objModel.email;
                responseModel._X_CLAROLOCAL1 = objModel.businessName;
                responseModel._X_CLAROLOCAL2 = objModel.fullName;
                responseModel._X_DOCUMENT_NUMBER = objModel.documentNumber;
                responseModel._X_MODEL = objModel.strAction;
                responseModel._X_REASON = objModel.cicloFacturacion;
                responseModel._X_CLARO_LDN3 = objModel.strProgramDate;
                responseModel._X_CLAROLOCAL5 = objModel.currentUser;
                responseModel._X_ICCID = objModel.fullNameUser;
            }
            catch (Exception ex)
            {
                Logging.Error(objModel.idSession, objModel.idSession, ex.Message);
            }

            return responseModel;
        }


        ///<summary>Retorna el listado de las Tareas programadas según los parámetros enviados.</summary>
        /// <param name="strIdSession"></param>
        /// <param name="oScheduledTasksRequest"></param>
        ///<returns>Models.ScheduledTasksModel<returns>
        ///<remarks>GetTransactionScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public Models.ScheduledTasksModel GetTransactionScheduled(string strIdSession, FixedIFIService.ListScheduledTransactionsRequest oScheduledTasksRequest)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            var objResponse = new FixedIFIService.ListScheduledTransactionsResponse();
            var objRequest = new FixedIFIService.ListScheduledTransactionsRequest()
            {
                audit = audit,
                ServiCoId = oScheduledTasksRequest.ServiCoId == null ? "" : oScheduledTasksRequest.ServiCoId,
                Account = oScheduledTasksRequest.Account == null ? "" : oScheduledTasksRequest.Account,
                StrStartDate = oScheduledTasksRequest.StrStartDate == null ? "" : oScheduledTasksRequest.StrStartDate,
                StrEndDate = oScheduledTasksRequest.StrEndDate == null ? "" : oScheduledTasksRequest.StrEndDate,
                ServiceState = oScheduledTasksRequest.ServiceState == null ? "" : oScheduledTasksRequest.ServiceState,
                Advisor = oScheduledTasksRequest.Advisor == null ? "" : oScheduledTasksRequest.Advisor,
                TransactionType = oScheduledTasksRequest.TransactionType == null ? "" : oScheduledTasksRequest.TransactionType,
                CodeInteraction = oScheduledTasksRequest.CodeInteraction == null ? "" : oScheduledTasksRequest.CodeInteraction,
                NameCACDAC = oScheduledTasksRequest.NameCACDAC == null ? "" : oScheduledTasksRequest.NameCACDAC
            };


            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod<FixedIFIService.ListScheduledTransactionsResponse>(
                    () => { return oFixedService.GetListScheduledTransactions(objRequest); });

                ;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            Models.ScheduledTasksModel objScheduledTasksModel = new Models.ScheduledTasksModel();

            if (objResponse.LstTransactions != null)
            {
                List<Helpers.CommonServices.ScheduledTasksHelper.ScheduledTransaction> listScheduledTransactions = new List<Helpers.CommonServices.ScheduledTasksHelper.ScheduledTransaction>();

                foreach (ScheduledTransaction item in objResponse.LstTransactions)
                {
                    listScheduledTransactions.Add(new Helpers.CommonServices.ScheduledTasksHelper.ScheduledTransaction()
                    {
                        CoId = item.CO_ID,
                        CustomerId = item.CUSTOMER_ID,
                        ServdDateProg = item.SERVD_DATEPROG,
                        ServdDateReg = item.SERVD_DATE_REG,
                        ServdDateEjec = item.SERVD_DATE_EJEC,
                        ServcState = item.SERVC_STATE,
                        DescState = item.DESC_STATE,
                        ServcIsBatch = item.SERVC_ISBATCH,
                        ServvMenError = item.SERVV_MEN_ERROR,
                        ServvCodeError = item.SERVV_COD_ERROR,
                        ServUserSystem = item.SERVV_USER_SYSTEM,
                        ServvIdEaiSw = item.SERVV_ID_EAI_SW,
                        ServiCode = item.SERVI_COD,
                        DescServi = item.DESC_SERVICE,
                        ServvMsisdn = item.SERVV_MSISDN,
                        ServvIdBatch = item.SERVV_ID_BATCH,
                        ServvUserAplication = item.SERVV_USER_APLICATION,
                        ServvEmailUserApp = item.SERVV_EMAIL_USER_APP,
                        ServvXmlEntry = item.SERVV_XMLENTRY,
                        ServcNumberAccount = item.SERVC_NUMBERACCOUNT,
                        ServcCodeInteraction = item.SERVC_CODE_INTERACTION,
                        ServcSellingPoint = item.SERVC_POINTSALE,
                        ServcTypeServ = item.SERVC_TYPE_SERV,
                        ServcCoSer = item.SERVC_CO_SER,
                        ServcTypeReg = item.SERVC_TYPE_REG,
                        ServcDesCoSer = item.SERVC_DES_CO_SER
                    });
                }
                objScheduledTasksModel.ScheduledTransactions = listScheduledTransactions;
            }


            return objScheduledTasksModel;
        }


        ///<summary>Convierte el resultado de la función GetTransactionScheduled a un valor JSON.</summary>
        /// <param name="strIdSession"></param>
        ///<param name="oScheduledTasksRequest"></param>
        ///<returns>Json<returns>
        ///<remarks>GetScheduledTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult GetScheduledTransaction(string strIdSession, FixedIFIService.ListScheduledTransactionsRequest oScheduledTasksRequest)
        {
            return Json(new { data = GetTransactionScheduled(strIdSession, oScheduledTasksRequest) });
        }


        ///<summary>Método que retorna el tipo de Estado de la solicitud.</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strIdList"></param>
        ///<returns>Json<returns>
        ///<remarks>GetStateType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult GetStateType(string strIdSession, string strIdList)
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


        ///<summary>Método que retorna el tipo de transacción de la solicitud.</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTransaction"></param>
        ///<returns>Json<returns>
        ///<remarks>GetTypeTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult GetTypeTransaction(string strIdSession)
        {
            var audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            var objResponse = new CommonIFIService.TransactionTypeResponseCommon();
            var objRequest = new CommonIFIService.TransactionTypeRequestCommon()
            {
                audit = audit
            };

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.TransactionTypeResponseCommon>(
                    () => { return oCommonService.GetTransactionType(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Claro.MessageException(audit.transaction);
            }

            return Json(new { data = objResponse });
        }


        ///<summary>Función que permite exportar una lista de tareas programadas hacia un documento excel.</summary>
        /// <param name="strIdSession"></param>
        /// <param name="objScheduledTransactionRequestPostPaid"></param>
        ///<returns>Json<returns>
        ///<remarks>ScheduledExportTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult ScheduledExportTransaction(string strIdSession, FixedIFIService.ListScheduledTransactionsRequest oScheduledTasksRequest)
        {
            try
            {
                Models.ScheduledTasksModel oScheduledTasksModel = new Models.ScheduledTasksModel();
                string strPathExcel = string.Empty;

                oScheduledTasksModel = GetTransactionScheduled(strIdSession, oScheduledTasksRequest);
                strPathExcel = ConfigurationManager.AppSettings("CONST_SCHEDULEDTRANSACTIONLTE");

                return Json(oExcelHelper.ExportExcel(oScheduledTasksModel, strPathExcel));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oScheduledTasksRequest.audit.Session, oScheduledTasksRequest.audit.transaction, ex.Message);
                return Json(new { data = ex.Message });
            }
        }


        #region enviar correo



        ///<summary>Metodo que permite enviar un correo electrónico al actualizar una tarea programada</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strRutaArchivo"></param>
        ///<returns>string</returns>
        ///<remarks>GetSendEmailEdit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>  
        public string GetSendEmailEdit(SuspensionReconnectionModel objModel,
                                   string strInteraccionId,
                                   string strRutaArchivo)
        {

            WirelessIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(objModel.idSession);
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            byte[] attachFile = null;
            string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

            if (DisplayFileFromServerSharedFile(objModel.idSession, audit.transaction, strRutaArchivo, out attachFile))
            {
                Model.SendEmailModel objSendEmail = new Model.SendEmailModel
                {
                    strIdSession = objModel.idSession,
                    strTo = objModel.email,
                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionEdit", vArchivoINTPOSTConfigMsg),
                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailSuspensionReconexionChangesEdit", vArchivoINTPOSTConfigMsg),
                    strAttached = strAdjunto,
                    byteAttached = attachFile
                };
                MensajeEmail = GetSendEmailConst(objSendEmail);
                return Claro.SIACU.Constants.OK;
            }
            else
            {
                return null;
            }

        }
        #endregion

        /// <summary>Método que permite el envio de la constancia por email</summary>
        /// <param name="objModel"></param>  
        /// <param name="strRutaArchivo"></param>  
        /// <returns>string</returns>
        /// <remarks>GetSendEmail2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/03/2019</FecCrea></item></list>
        public string GetSendEmailEdit2(SuspensionReconnectionModel objModel, string strRutaArchivo)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);

            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            if (objModel != null)
            {
                try
                {
                    byte[] attachFile = null;
                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                    List<CommonIFIService.AttachedFile> lstFileAdjunto = new List<AttachedFile>();
                    if (DisplayFileFromServerSharedFile(objModel.idSession, objModel.idTransaccion, strRutaArchivo, out attachFile))
                    {
                        if (attachFile != null)
                        {
                            CommonIFIService.AttachedFile objFile = new CommonIFIService.AttachedFile();
                            objFile.file = attachFile;
                            objFile.Name = strAdjunto;
                            objFile.HeadBoard = strAdjunto;
                            lstFileAdjunto.Add(objFile);
                            string MensajeEmail = string.Empty;
                            string strNombreArchivo = string.Empty;
                            string strDestinatarios = objModel.email;
                            string strSubject = String.Empty;
                            string strMessage = String.Empty;

                            strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionEdit", vArchivoINTPOSTConfigMsg);
                            strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailSuspensionReconexionChangesEdit", vArchivoINTPOSTConfigMsg);

                            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                            objSendEmailWithBase64Request.audit = objAudit;
                            objSendEmailWithBase64Request.Sender = strRemitente;
                            objSendEmailWithBase64Request.To = strDestinatarios;
                            objSendEmailWithBase64Request.HtmlFlag = KEY.AppSettings("strHtmlFlagCorreoSB").ToString();
                            objSendEmailWithBase64Request.Subject = strSubject;
                            objSendEmailWithBase64Request.Message = strMessage;
                            if (lstFileAdjunto != null && lstFileAdjunto.Count > 0)
                            {
                                objSendEmailWithBase64Request.ListAttachedFile = lstFileAdjunto;
                            }

                            objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                            {
                                return oCommonService.SendEmailWithBase64(objSendEmailWithBase64Request);
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

            }
            return strResul;

        }

        public string GetSendEmailDelete2(SuspensionReconnectionModel objModel, string strRutaArchivo, string TypeTransaction)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);

            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            Model.SendEmailModel objSendEmail = new Model.SendEmailModel();

            if (objModel != null)
            {
                try
                {
                    byte[] attachFile = null;
                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);
                    List<CommonIFIService.AttachedFile> lstFileAdjunto = new List<AttachedFile>();
                    if (DisplayFileFromServerSharedFile(objModel.idSession, objModel.idTransaccion, strRutaArchivo, out attachFile))
                    {
                        if (attachFile != null)
                        {
                            CommonIFIService.AttachedFile objFile = new CommonIFIService.AttachedFile();
                            objFile.file = attachFile;
                            objFile.Name = strAdjunto;
                            objFile.HeadBoard = strAdjunto;
                            lstFileAdjunto.Add(objFile);
                            string MensajeEmail = string.Empty;
                            string strNombreArchivo = string.Empty;
                            string strDestinatarios = objModel.email;
                            string strSubject = String.Empty;
                            string strMessage = String.Empty;

                            switch (TypeTransaction)
                            {
                                case Claro.Constants.NumberThreeString:
                                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionDesistimiento", vArchivoINTPOSTConfigMsg);
                                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDesistimiento", vArchivoINTPOSTConfigMsg);
                                    break;
                                case Claro.Constants.NumberOneString:
                                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionDesistimiento", vArchivoINTPOSTConfigMsg);
                                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strPlanMigrationDesBodyEmail", vArchivoINTPOSTConfigMsg);
                                    break;
                                default:
                                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailCancelacionDesistimiento", vArchivoINTPOSTConfigMsg);
                            strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailDesistimiento", vArchivoINTPOSTConfigMsg);
                                    break;
                            }

                            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                            objSendEmailWithBase64Request.audit = objAudit;
                            objSendEmailWithBase64Request.Sender = strRemitente;
                            objSendEmailWithBase64Request.To = strDestinatarios;
                            objSendEmailWithBase64Request.HtmlFlag = KEY.AppSettings("strHtmlFlagCorreoSB").ToString();
                            objSendEmailWithBase64Request.Subject = strSubject;
                            objSendEmailWithBase64Request.Message = strMessage;
                            if (lstFileAdjunto != null && lstFileAdjunto.Count > 0)
                            {
                                objSendEmailWithBase64Request.ListAttachedFile = lstFileAdjunto;
                            }

                            objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                            {
                                return oCommonService.SendEmailWithBase64(objSendEmailWithBase64Request);
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

            }
            return strResul;

        }


    }
}
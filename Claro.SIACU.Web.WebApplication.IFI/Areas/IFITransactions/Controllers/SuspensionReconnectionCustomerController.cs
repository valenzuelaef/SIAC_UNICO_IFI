
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CommonService = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using FunctionsSIACU = Claro.Utils;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class SuspensionReconnectionCustomerController : SuspensionReconnectionCustomerBaseController
    {

        ///<summary>Metodo que retorna la vista del controlador</summary>
        ///<returns>View<returns>
        ///<remarks>Index</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/09/2018.</FecCrea></item></list>
        public ActionResult SuspensionReconnectionCustomer()
        {
            return PartialView("~/Areas/IFITransactions/Views/SuspensionReconnectionCustomer.cshtml");
        }


        ///<summary>Metodo que retorna la vista del controlador para la actualización de suspensión/reconexión.</summary>
        ///<returns>View<returns>
        ///<remarks>Index</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/09/2018.</FecCrea></item></list>
        public ActionResult EditSuspensionReconnectionCustomer()
        {
            return PartialView("~/Areas/IFITransactions/Views/EditSuspensionReconnectionCustomer/EditSuspensionReconnectionCustomer.cshtml");
        }


        ///<summary>Metodo para obtener la tipificación de desistimiento</summary>
        ///<param name="idSession"></param>
        ///<returns>Json<returns>
        ///<remarks>GetTypificationDelete</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public JsonResult GetTypificationDelete(string idSession)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);
            CommonService.Typification typificationLoad = null;
            string lblMensaje = string.Empty;
            try
            {
                string typeProduct = ConfigurationManager.AppSettings("gConstTipoINTDelete");


                typificationLoad = LoadTypificationSusRecon(audit.Session, typeProduct, ref lblMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            var dictionaryLoadTipy = new Dictionary<string, object>
            {                
                { 
                    "typificationLoad",                     
                    new Dictionary<string, object> 
                    {
                        {"result", string.IsNullOrEmpty(lblMensaje) ? true : false},
                        {"objResult",typificationLoad},
                        {"lblMensaje", lblMensaje}
                    }
                }
            };
            return Json(dictionaryLoadTipy, JsonRequestBehavior.AllowGet);
        }
        public bool ValidateLineLock(PostIFIService.CurrentLockRequestIFI objRequest)
        {

            var objResponse = new PostIFIService.CurrentLockResponseIFI();
            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.CurrentLockResponseIFI>(
                    () => { return oServicePostPaid.CurrentBlock(objRequest); });

                if (objResponse != null && objResponse.lstAnnotation != null && objResponse.lstAnnotation.Count > 0)
                {
                    if (objResponse.lstAnnotation[0]._tipo.StartsWith("BLOQ"))
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objRequest.audit.transaction);
            }
            return true;
        }

        ///<summary>Metodo que se ejecuta al cargar la vista</summary>
        ///<param name="strIdSession">/param>
        ///<param name="currentUser">/param>
        ///<param name="contractId">/param>
        ///<param name="CadenaOpciones">/param>
        ///<param name="strCustomerId">/param>
        ///<returns>Json<returns>
        ///<remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public JsonResult PageLoad(string idSession, string currentUser, string contractId, string CadenaOpciones, string strCustomerId)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);
            var auditPost = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(idSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Iniciando PageLoad");
            Dictionary<string, object> msgHiddensLoad = null;
            CommonService.Typification typificationLoad = null;
            Dictionary<string, object> checkStatusScheduledLoad = null;
            string msisdn = string.Empty;
            string typeProduct = ConfigurationManager.AppSettings("gConstTipoINT");
            string lblMensaje = string.Empty;
            bool habilitaRetenido = true;
            bool bldebtor = false;
            bool resulValidateLineBlock = true;

            try
            {
                var objRequest = new PostIFIService.CurrentLockRequestIFI()
                {
                    audit = auditPost,
                    codId = contractId


                };
                resulValidateLineBlock = ValidateLineLock(objRequest);

                msgHiddensLoad = LoadMsgAndHiddens(audit.Session);
                typificationLoad = LoadTypificationSusRecon(audit.Session, typeProduct, ref lblMensaje);
                checkStatusScheduledLoad = CheckStatusScheduledTransactions(audit.Session, contractId, currentUser);
                msisdn = GetMSISDNByContractId(contractId, audit.Session);
                bldebtor = GetStatedebt(audit.Session, strCustomerId);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }
            if (!(CadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfSuspHabilitaRetenido")) > -1))
            {
                habilitaRetenido = false;
            }
            var dictionaryPageLoad = new Dictionary<string, object>
            {
                { "msgHiddensLoad", msgHiddensLoad},
                {"resulValidateLineBlock",resulValidateLineBlock},
                { 
                    "typificationLoad",                     
                    new Dictionary<string, object> 
                    {
                        {"result", string.IsNullOrEmpty(lblMensaje) ? true : false},
                        {"objResult",typificationLoad},
                        {"lblMensaje", lblMensaje}
                    }
                },
                { "checkStatusScheduledLoad", checkStatusScheduledLoad},
                { "msisdn", msisdn},
                {"habilitaRetenido",habilitaRetenido},
                {"NotDebtor",bldebtor}
            };

            return Json(dictionaryPageLoad, JsonRequestBehavior.AllowGet);
        }


        ///<summary>Metodo que guarda la transacción para suspensión/reconexión</summary>
        ///<param name="objParams"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>SaveTransaccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public JsonResult SaveTransaccion(SuspensionReconnectionModel objParams)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(objParams.idSession);
            Claro.Web.Logging.Info(objParams.idSession, objParams.idTransaccion, "Iniciando SaveTransaccion");
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
                { "Result", ValidateSaveTransaccion(objParams, ref msgError, ref lblMsgView, ref PathFileHPxtream)},
                { "MsgError", msgError},
                { "LblMsgView", lblMsgView},
                { "PathFileHPxtream", PathFileHPxtream}
            };

            return Json(dictionarySaveTransaccion, JsonRequestBehavior.AllowGet);
        }


        ///<summary>Metodo que valida y habilita el rango de fechas segun el tipo de retención</summary>
        ///<param name="objModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>ValidateRangesDates</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public JsonResult ValidateRangesDates(SuspensionReconnectionModel objModel)
        {
            if (string.IsNullOrEmpty(objModel.dateSuspension))
                return Json(new { outMsg = "Necesita seleccionar las fechas de suspensión y reactivación", state = false }, JsonRequestBehavior.AllowGet);
            if (string.IsNullOrEmpty(objModel.dateReactivation))
                return Json(new { outMsg = "Necesita seleccionar las fechas de suspensión y reactivación", state = false }, JsonRequestBehavior.AllowGet);

            objModel.dFechaSuspension = FunctionsSIACU.GetDDMMYYYYAsDateTime(objModel.dateSuspension);
            objModel.dFechaReactivacion = FunctionsSIACU.GetDDMMYYYYAsDateTime(objModel.dateReactivation);
            string strSusxReten = Claro.Utils.GetValueFromConfigFileIFI("strMensajeSusxReten", vArchivoHFCPOSTConfigMsg);
            string strMinSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMinSus", vArchivoINTPOSTConfigMsg), objModel.diasMinSinRetencion);
            string strMaxSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMaxSus", vArchivoINTPOSTConfigMsg), objModel.diasMaxSinRetencion);
            string strMenFin = Claro.Utils.GetValueFromConfigFileIFI("strMensajeFinal", vArchivoHFCPOSTConfigMsg);
            string strInteraccionId = string.Empty;
            string outMsg = "";


            if (objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) > Claro.Utils.CheckInt(objModel.diasMaxConRetencion))
                {

                    outMsg = strSusxReten;

                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) < Claro.Utils.CheckInt(objModel.diasMinSinRetencion))
                {

                    outMsg = strMinSus;

                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (!objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) < Claro.Utils.CheckInt(objModel.diasMinSinRetencion))
                {

                    outMsg = strMinSus;

                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (!objModel.chkRetencion)
            {


                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) > Claro.Utils.CheckInt(objModel.diasMaxSinRetencion))
                {

                    outMsg = strMaxSus;
                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(new { outMsg = outMsg, state = true }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ValidateRangesDatesEdit(SuspensionReconnectionModel objModel)
        {
            if (string.IsNullOrEmpty(objModel.dateSuspension))
                return Json(new { outMsg = "Necesita seleccionar las fechas de suspensión y reactivación", state = false }, JsonRequestBehavior.AllowGet);
            if (string.IsNullOrEmpty(objModel.dateReactivation))
                return Json(new { outMsg = "Necesita seleccionar las fechas de suspensión y reactivación", state = false }, JsonRequestBehavior.AllowGet);

            objModel.dFechaSuspension = FunctionsSIACU.GetDDMMYYYYAsDateTime(objModel.dateSuspension);
            objModel.dFechaReactivacion = FunctionsSIACU.GetDDMMYYYYAsDateTime(objModel.dateReactivation);
            string strSusxReten = Claro.Utils.GetValueFromConfigFileIFI("strMensajeSusxReten", vArchivoHFCPOSTConfigMsg);
            string strMinSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMinSus", vArchivoINTPOSTConfigMsg), objModel.diasMinSinRetencion);
            string strMaxSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMaxSus", vArchivoINTPOSTConfigMsg), objModel.diasMaxSinRetencion);
            string strMenFin = Claro.Utils.GetValueFromConfigFileIFI("strMensajeFinal", vArchivoHFCPOSTConfigMsg);
            string strInteraccionId = string.Empty;
            string outMsg = "";


            if (objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) > Claro.Utils.CheckInt(objModel.diasMaxConRetencion))
                {

                    outMsg = strSusxReten;

                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) < 1)
                {

                    outMsg = strMinSus;

                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (!objModel.chkRetencion)
            {

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) < 1)
                {

                    outMsg = strMinSus;

                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }

            if (!objModel.chkRetencion)
            {


                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) > Claro.Utils.CheckInt(objModel.diasMaxSinRetencion))
                {

                    outMsg = strMaxSus;
                    return Json(new { outMsg = outMsg, state = false }, JsonRequestBehavior.AllowGet);
                }
            }


            return Json(new { outMsg = outMsg, state = true }, JsonRequestBehavior.AllowGet);

        }

        ///<summary>Metodo que valida el rango de fechas para </summary>
        ///<param name="objModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>PageLoadEdit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public JsonResult PageLoadEdit(string idSession, string currentUser, string contractId, string vServType)
        {
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Iniciando PageLoad");
            Dictionary<string, object> msgHiddensLoad = null;
            CommonService.Typification typificationLoad = null;
            Dictionary<string, object> ScheduledTransactionDataEdit = null;
            string msisdn = string.Empty;
            string typeProduct = ConfigurationManager.AppSettings("gConstTipoINT");
            string lblMensaje = string.Empty;

            try
            {

                msgHiddensLoad = LoadMsgAndHiddens(audit.Session);
                typificationLoad = LoadTypifications(audit.Session, typeProduct, ref lblMensaje);
                ScheduledTransactionDataEdit = LoadScheduledTransactionDataEdit(audit.Session, contractId, currentUser, vServType);
                msisdn = GetMSISDNByContractId(contractId, audit.Session);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);

            }

            var dictionaryPageLoad = new Dictionary<string, object>
            {
                { "msgHiddensLoad", msgHiddensLoad},
                { 
                    "typificationLoad",                     
                    new Dictionary<string, object> 
                    {
                        {"result", string.IsNullOrEmpty(lblMensaje) ? true : false},
                        {"objResult",typificationLoad},
                        {"lblMensaje", lblMensaje}
                    }
                },

                { "msisdn", msisdn},
                {"ScheduledTransactionDataEdit", ScheduledTransactionDataEdit}
            };

            return Json(dictionaryPageLoad, JsonRequestBehavior.AllowGet);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MODELS = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using CommonService = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.Web;
using Microsoft.VisualBasic;
using Claro.SIACU.Web.WebApplication.IFI.Controllers;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.WirelessIFIService;
using KEY = Claro.ConfigurationManager;



namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class SuspensionReconnectionCustomerBaseController : CommonServicesController
    {

        #region Instance Service
        public readonly CommonIFIService.CommonIFIServiceClient oCommonService = new CommonIFIService.CommonIFIServiceClient();
        public readonly WirelessIFIService.WirelessIFIServiceClient oWirelessIFIService = new WirelessIFIService.WirelessIFIServiceClient();
        public readonly FixedIFIService.FixedIFIServiceClient oFixedService = new FixedIFIService.FixedIFIServiceClient();
        public readonly PostIFIService.PostIFIServiceClient oServicePostPaid = new PostIFIService.PostIFIServiceClient();
        #endregion

        #region Variables para obtener valores y mensajes de archivos (.config)
        public readonly string vArchivoHFCPOSTConfig = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig");
        public readonly string vArchivoHFCPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
        public readonly string vArchivoINTPOSTConfigMsg = ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg");
        #endregion

        #region Metodos de Inicio (LOAD)

        ///<summary>Metodo que carga mensajes y ocultos en el formulario</summary>
        ///<param name="objModel"></param>
        ///<returns>strIdSession<returns>
        ///<remarks>LoadMsgAndHiddens</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public Dictionary<string, object> LoadMsgAndHiddens(string strIdSession)
        {
            PostIFIService.AmountIncomingCallResponse objResponseAmount = null;

            PostIFIService.AmountIncomingCallRequest objRequest = new PostIFIService.AmountIncomingCallRequest
            {
                Name = ConfigurationManager.AppSettings("SuspReconInt"),
                audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession)
            };

            Claro.Web.Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, "Iniciando LoadMsgAndHiddens");

            objResponseAmount =
                    Claro.Web.Logging.ExecuteMethod<PostIFIService.AmountIncomingCallResponse>(
                    () => { return oServicePostPaid.GetAmountIncomingCall(objRequest); });


            double ImportePagar = objResponseAmount.ListAmountIncomingCall.SingleOrDefault(x => x.Description == "MontoCobroReactivacionServicio").ValorN; //Monto
            double DiasMaxConRetencion = objResponseAmount.ListAmountIncomingCall.SingleOrDefault(x => x.Description == "DiasMaxConRetencion").ValorN; //120 dias
            double DiasMaxSinRetencion = objResponseAmount.ListAmountIncomingCall.SingleOrDefault(x => x.Description == "DiasMaxSinRetencion").ValorN; //62 dias
            double DiasMinSinRetencion = objResponseAmount.ListAmountIncomingCall.SingleOrDefault(x => x.Description == "DiasMinSinRetencion").ValorN; //12 dias
            Claro.Web.Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, "Iniciando LoadMsgAndHiddens xxxxx" + Claro.Utils.GetValueFromConfigFileIFI("gConstMsgEstaSegGT", vArchivoHFCPOSTConfigMsg));
            var dictionaryPageLoad = new Dictionary<string, object>
            {
                { "FlagContingenciaHP", Claro.Utils.CheckStr(ConfigurationManager.AppSettings("strFlagContingenciaHP"))},
                { "flagRestringirAccesoTemporalSRASC", Claro.Utils.GetValueFromConfigFileIFI("flagRestringirAccesoTemporalSRASC", vArchivoHFCPOSTConfig)},
                { "ConstMsgOpcionTemporalmenteInhabilitada", Claro.Utils.GetValueFromConfigFileIFI("gConstMsgOpcionTemporalmenteInhabilitada",vArchivoHFCPOSTConfigMsg)},
                { "ConsLineaDesaActiva", Claro.Utils.CheckStr(ConfigurationManager.AppSettings("strConsLineaDesaActiva"))},
                { "ConstMsgLineaStatSuspe", Claro.Utils.GetValueFromConfigFileIFI("gConstMsgLineaStatSuspe", vArchivoHFCPOSTConfigMsg)},                
                { "DiasMinSuspension", Claro.Utils.GetValueFromConfigFileIFI("intDiasMinSuspensionLTE", vArchivoHFCPOSTConfig)},                
                { "MinDiasRetSuspension", Claro.Utils.GetValueFromConfigFileIFI("intDiasMinRetencionSuspensionLTE", vArchivoHFCPOSTConfig)},                                
                
                { "MontoRet", Constants.ZeroNumber},                
                { "ImpPagar", ImportePagar},
                { 
                    "LoadMsgHiddens", 
                    new Dictionary<string, string> 
                    { 
                        {"hdnTituloPagina",Claro.Utils.GetValueFromConfigFileIFI("gConstMsgTituloSusTemp", vArchivoHFCPOSTConfigMsg)}, 
                        {"hdnMensaje1",Claro.Utils.GetValueFromConfigFileIFI("gConstMsgEstaSegGT", vArchivoHFCPOSTConfigMsg)}, 
                        {"hdnMensaje2",Claro.Utils.GetValueFromConfigFileIFI("gConstMsgPIFechSus", vArchivoHFCPOSTConfigMsg)},
                        {"hdnMensaje3",Claro.Utils.GetValueFromConfigFileIFI("gConstMsgPIFechRea", vArchivoHFCPOSTConfigMsg)},
                        {"hdnMensaje4",Claro.Utils.GetValueFromConfigFileIFI("strMsgCampNotasMaxC", vArchivoHFCPOSTConfigMsg)}
                    } 
                },
                { 
                    "EstadoContrato", 
                    new Dictionary<string, string>
                    {
                        {"EstadoContratoInactivo",Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoInactivo", vArchivoHFCPOSTConfig)}, 
                        {"EstadoContratoSuspendido",Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoSuspendido", vArchivoHFCPOSTConfig)}, 
                        {"EstadoContratoReservado",Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoReservado", vArchivoHFCPOSTConfig)},
                        {"MsgValidacionContratoInactivo",Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoInactivo", vArchivoHFCPOSTConfigMsg)},
                        {"MsgValidacionContratoReservado",Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoReservado", vArchivoHFCPOSTConfigMsg)}
                    }
                },
                {"DiasMaxConRetencion", DiasMaxConRetencion},
                {"DiasMaxSinRetencion", DiasMaxSinRetencion},
                {"DiasMinSinRetencion", DiasMinSinRetencion}
            };

            return dictionaryPageLoad;
        }

        ///<summary>Metodo que carga la tipificación de suspensión/reconexión </summary>
        ///<param name="idSession"></param>
        ///<param name="typeProduct"></param>
        ///<param name="lblMensaje"></param>
        ///<returns>CommonService.Typification<returns>
        ///<remarks>LoadTypificationSusRecon</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public CommonService.Typification LoadTypificationSusRecon(string idSession,
                                                            string typeProduct,
                                                            ref string lblMensaje)
        {
            CommonService.Typification oTypification = null;
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);

            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypification");


            try
            {
                CommonService.TypificationRequest objTypificationRequest = new CommonIFIService.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = typeProduct == "DeleteIFI" ? ConfigurationManager.AppSettings("TransaccionDesistimientoRetSusp") : ConfigurationManager.AppSettings("TransaccionSuspensionReactivacionINT");
                objTypificationRequest.audit = audit;

                CommonService.TypificationResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<CommonService.TypificationResponse>(
                    () => { return oCommonService.GetTypification(objTypificationRequest); });



                oTypification = objResponse.ListTypification.First();


                if (oTypification == null)
                {

                    lblMensaje = Claro.Utils.GetValueFromConfigFileIFI("strAjusteNoRecon", vArchivoHFCPOSTConfigMsg);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", vArchivoHFCPOSTConfigMsg);
            }

            return oTypification;
        }

        ///<summary>Metodo que revisa el estado de las  tareas programadas</summary>
        ///<param name="idSession"></param>
        ///<param name="contractId"></param>
        ///<param name="currentUser"></param>
        ///<returns>Dictionary<returns>
        ///<remarks>CheckStatusScheduledTransactions</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public Dictionary<string, object> CheckStatusScheduledTransactions(string idSession,
                                                                           string contractId,
                                                                           string currentUser)
        {
            Dictionary<string, object> dictionaryCheckStatusScheduledTransactions = null;

            bool result = true;
            string msg = string.Empty;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(idSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo CheckStatusScheduledTransactions");


            FixedIFIService.ListScheduledTransactionsRequest objTransactionScheduledRequest = new FixedIFIService.ListScheduledTransactionsRequest
            {
                audit = audit,
                UserApp = currentUser,
                ApplicationName = ConfigurationManager.AppSettings("strConsCodAplicSuReTemp"),
                Advisor = string.Empty,
                Account = string.Empty,
                NameCACDAC = string.Empty,
                StrStartDate = string.Empty,
                StrEndDate = string.Empty,
                ServiceState = string.Empty,
                CodeInteraction = string.Empty,
                TransactionType = string.Empty,
                ServiCoId = contractId
            };




            try
            {
                FixedIFIService.ListScheduledTransactionsResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<FixedIFIService.ListScheduledTransactionsResponse>(
                () => { return oFixedService.GetListScheduledTransactions(objTransactionScheduledRequest); }
                );

                result = objResponse.LstTransactions.Where(x => x.SERVC_STATE == Claro.Constants.NumberZeroString ||
                                                         x.SERVC_STATE == Claro.Constants.NumberOneString ||
                                                         x.SERVC_STATE == Claro.Constants.NumberTwoString).ToList().Count > 0 ? false : true;

                if (!result)
                {
                    msg = Claro.Utils.GetValueFromConfigFileIFI("gConstMsgLineaPOTP", vArchivoHFCPOSTConfigMsg);
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                result = false;
            }

            return dictionaryCheckStatusScheduledTransactions = new Dictionary<string, object>(){
                { "Result", result},
                { "Msg", msg}
            };
        }


        ///<summary>Metodo que revisa el estado de la deuda</summary>
        ///<param name="Session"></param>
        ///<param name="strCustomerId"></param>
        ///<returns>bool<returns>
        ///<remarks>GetStatedebt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public bool GetStatedebt(string Session, string strCustomerId)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.PostIFIService.AuditRequest>(Session);
            PostIFIService.GetStatedebtIFIRequest objRequest = new PostIFIService.GetStatedebtIFIRequest()
            {
                audit = audit,
                strCustomerCode = strCustomerId
            };
            PostIFIService.GetStatedebtIFIResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oServicePostPaid.GetStatedebt(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, ex.Message);
                return false;
            }
            return objResponse.blResult;
        }


        ///<summary>Metodo que obtiene el número de MSISDN por id de contrato</summary>
        ///<param name="contractId"></param>
        ///<param name="idSession"></param>
        ///<returns>string<returns>
        ///<remarks>GetMSISDNByContractId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public string GetMSISDNByContractId(string contractId,
                                            string idSession)
        {
            FixedIFIService.ConsultationServiceByContractResponse objConsultationServiceByContractResponse = new FixedIFIService.ConsultationServiceByContractResponse();
            FixedIFIService.AuditRequest objAuditRequest = new FixedIFIService.AuditRequest();

            objAuditRequest = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AuditRequest>(idSession);

            try
            {
                Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "Metodo GetMSISDNByContractId");



                var ConsultationServiceByContractRequest = new FixedIFIService.ConsultationServiceByContractRequest()
                {
                    audit = objAuditRequest,
                    strCodContrato = contractId,
                    typeProduct = Claro.Utils.CheckStr(ConfigurationManager.AppSettings("gConstTipoINT"))
                };

                objConsultationServiceByContractResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oFixedService.GetCustomerLineNumber(ConsultationServiceByContractRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }



            return objConsultationServiceByContractResponse.msisdn;
        }

        #endregion

        #region Guardar Interaccion

        ///<summary>Metodo que guarda la interacción</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strPathFileHPxtream"></param>
        ///<returns>bool<returns>
        ///<remarks>SaveInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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


        ///<summary>Metodo que inserta la interacción de datos y plantilla en la BD</summary>
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
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public CommonIFIService.InsertGeneralResponse InsertInterac(CommonIFIService.Iteraction objInteractionModel, InsertTemplateInteraction oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
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


        ///<summary>Metodo que guarda los datos de la interacción</summary>
        ///<param name="objModel"></param>
        ///<returns>CommonIFIService.Iteraction<returns>
        ///<remarks>DataInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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


        ///<summary>Metodo que guarda los datos de la plantilla de la interacción</summary>
        ///<param name="objModel"></param>
        ///<returns>CommonIFIService.InsertTemplateInteraction<returns>
        ///<remarks>DataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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
                responseModel._X_EMAIL_CONFIRMATION = objModel.chkEmail ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString;
                responseModel._X_BASKET = objModel.contractId;
                responseModel._X_ADDRESS5 = objModel.fullName;
                responseModel._X_INTER_17 = objModel.fullName;
                responseModel._X_NAME_LEGAL_REP = objModel.legalAgent;

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
                if (!string.IsNullOrEmpty(objModel.email))
                    responseModel._X_EMAIL = objModel.email;



            }
            catch (Exception ex)
            {
                Logging.Error(objModel.idSession, objModel.idSession, ex.Message);
            }

            return responseModel;
        }
        #endregion


        ///<summary>Metodo que permite editar las tareas programadas</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strCodId"></param>
        ///<param name="dtFechaProgramacion"></param>
        ///<param name="currentUser"></param>
        ///<returns>bool<returns>
        ///<remarks>EditTaskProgram</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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


        ///<summary>Metodo que permite reactivar los servicios</summary>
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
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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


        ///<summary>Metodo que permite suspender los servicios</summary>
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
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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
                    desCoSer = Claro.Utils.GetValueFromConfigFileIFI("gConstDesTicklerNormal", vArchivoHFCPOSTConfigMsg),
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


        ///<summary>Metodo que permite validar el guardado de la transacción</summary>
        ///<param name="objModel"></param>
        ///<param name="outMsg"></param>
        ///<param name="lblMsgView"></param>
        ///<param name="strPathFileHPxtream"></param>
        ///<returns>bool<returns>
        ///<remarks>ValidateSaveTransaccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public bool ValidateSaveTransaccion(SuspensionReconnectionModel objModel,
                                            ref string outMsg,
                                            ref bool lblMsgView,
                                            ref string strPathFileHPxtream)
        {
            lblMsgView = false;

            string strSusxReten = Claro.Utils.GetValueFromConfigFileIFI("strMensajeSusxReten", vArchivoHFCPOSTConfigMsg);
            string strMinSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMinSus", vArchivoINTPOSTConfigMsg), objModel.diasMinSinRetencion);
            string strMaxSus = string.Format(Claro.Utils.GetValueFromConfigFileIFI("strMensajeMaxSus", vArchivoINTPOSTConfigMsg), objModel.diasMaxSinRetencion);
            string strMenFin = Claro.Utils.GetValueFromConfigFileIFI("strMensajeFinal", vArchivoHFCPOSTConfigMsg);
            int intDias = 0;
            bool resfinal = false;
            bool blnRetorno = false;
            string strInteraccionId = string.Empty;
            strPathFileHPxtream = string.Empty;


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

                if (DateAndTime.DateDiff(DateInterval.Day, objModel.dFechaSuspension, objModel.dFechaReactivacion) < Claro.Utils.CheckInt(objModel.diasMinSinRetencion))
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


            if (objModel.dFechaSuspension > Claro.Utils.TruncDateTime(DateTime.Now))
            {

                bool result1 = false;
                bool result2 = false;

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

                if (result1)
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
                    if (result2)
                    {
                        resfinal = true;
                    }
                    else
                    {
                        Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, "Error al Reactivar Servicio");
                        Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, "Inicio - Ejecutar Rollback de Suspensión");
                        bool blnEditarSusp = false;
                        blnEditarSusp = EditTaskProgram(objModel.idSession,
                                                        objModel.contractId,
                                                        objModel.dFechaSuspension,
                                                        objModel.currentUser);

                        if (blnEditarSusp)
                        {
                            Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, "Final - Se ejecutó el rollback de Suspensión correctamente");
                        }
                        else
                        {
                            Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, "Error al ejecutar el rollback de Suspensión");
                        }
                    }
                }
                else
                {
                    Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, "Error al SuspenderServicio");
                }
            }
            else
            {
                resfinal = SuspendService(objModel.customerId,
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
            }

            if (resfinal)
            {
                lblMsgView = true;
                outMsg = strMenFin;
                string strMsg = "Monto: " + objModel.impPagar + Claro.SIACU.Constants.gstrContrato + objModel.contractId + Claro.SIACU.Constants.gstrFechaHora + DateTime.Now;
                InsertAudit(objModel, strMsg);

                if (objModel.chkEmail)
                {

                    if (GetSendEmail2(objModel, strPathFileHPxtream) == Claro.SIACU.Constants.OK)
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
            }
            else
            {
                if (strInteraccionId.Length > 1)
                {
                    var UpdateInter29Request = new FixedIFIService.UpdateInter29Request
                    {
                        audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(objModel.idSession),
                        p_objid = strInteraccionId,
                        p_texto = Claro.Utils.GetValueFromConfigFileIFI("strMensajeErrorEnTranActNota", vArchivoHFCPOSTConfigMsg)
                    };

                    var objResponseGetUpdateInter29 = oFixedService.GetUpdateInter29(UpdateInter29Request);
                    Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, "Respuesta Metodo objResponseGetUpdateInter29");
                    Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, objResponseGetUpdateInter29.rResult.ToString());
                    Claro.Web.Logging.Info(objModel.idSession, objModel.idSession, objResponseGetUpdateInter29.rMsgText);

                }
                outMsg = Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", vArchivoHFCPOSTConfigMsg);
            }


            return true;
        }



        ///<summary>Metodo que permite validar el identificador del cliente</summary>
        ///<param name="objModel"></param>
        ///<returns>bool<returns>
        ///<remarks>GetValidateCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
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


        ///<summary>Metodo que permite generar la constancia en formato PDF</summary>
        ///<param name="objConstancy"></param>
        ///<param name="objModel"></param>
        ///<returns>CommonIFIService.GenerateConstancyResponseCommon<returns>
        ///<remarks>GenerateConstancy</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><ActualizadoPor>Everis</ActualizadoPor></item>
        /// <item><FecAct>30/09/2018.</FecAct></item></list>
        public CommonIFIService.GenerateConstancyResponseCommon GenerateConstancy(CommonIFIService.ParametersGeneratePDF objConstancy,
                                                                                      SuspensionReconnectionModel objModel)
        {

            CommonIFIService.GenerateConstancyResponseCommon objGeneratePdf = null;

            try
            {
                objConstancy.StrContenidoComercial = Claro.Utils.GetValueFromConfigFileIFI("IncomingCallDetailContentCommercial", vArchivoHFCPOSTConfigMsg);
                objConstancy.StrContenidoComercial2 = Claro.Utils.GetValueFromConfigFileIFI("IncomingCallDetailContentCommercial2", vArchivoHFCPOSTConfigMsg);
                objConstancy.StrCarpetaTransaccion = string.Format(@"{0}\{1}\", "INT", "PRUEBA");
                objConstancy.StrNombreArchivoTransaccion = GetTypeTransaction(objModel, string.Empty, ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + objModel.customerId);

                CommonServicesController obj = new CommonServicesController();
                objGeneratePdf = Claro.Web.Logging.ExecuteMethod<CommonIFIService.GenerateConstancyResponseCommon>(() =>
                {
                    return obj.GenerateContancyPDF(objModel.idSession, objConstancy);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.idSession, objModel.idSession, "Error al Generar el HPEXTREAM ENTRANTES : " + ex.Message);
            }

            return objGeneratePdf;
        }


        ///<summary>Metodo para obtener la tipo de tipificacion de la transacción</summary>
        ///<param name="objModel"></param>
        ///<param name="strRetention"></param>
        ///<param name="strIdentificador"></param>
        ///<returns>string<returns>
        ///<remarks>GetTypeTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public string GetTypeTransaction(SuspensionReconnectionModel objModel,
                                         string strRetention,
                                         string strIdentificador)
        {
            PostIFIService.TypeTransactionBRMSResponse objResponse = new PostIFIService.TypeTransactionBRMSResponse();
            try
            {
                var strTransaccion = ConfigurationManager.AppSettings("strTransaccion");
                PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(objModel.idSession);

                PostIFIService.TypeTransactionBRMSRequest objRequest = new PostIFIService.TypeTransactionBRMSRequest()
                {
                    audit = audit,
                    StrIdentifier = strIdentificador,
                    StrOperationCodSubClass = objModel.subClaseDes,
                    StrRetention = strRetention,
                    StrTransactionM = strTransaccion
                };

                objResponse = Claro.Web.Logging.ExecuteMethod<PostIFIService.TypeTransactionBRMSResponse>(() =>
                {
                    return oServicePostPaid.GetTypeTransactionBRMS(objRequest);
                });
            }
            catch (Exception e)
            {
                Claro.Web.Logging.Error(objModel.idSession, objModel.idTransaccion, e.Message);
            }
            return objResponse.StrResult;
        }




        #region enviar correo

        ///<summary>Metodo para enviar el correo electronico al generar la constancia</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraccionId"></param>
        ///<param name="strRutaArchivo"></param>
        ///<returns>string<returns>
        ///<remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public string GetSendEmail(SuspensionReconnectionModel objModel,
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
                    strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionChanges", vArchivoINTPOSTConfigMsg),
                    strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailSuspensionReconexionChanges", vArchivoINTPOSTConfigMsg),
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


        #region enviar correo

        /// <summary>Método que permite el envio de la constancia por email</summary>
        /// <param name="objModel"></param>  
        /// <param name="strRutaArchivo"></param>  
        /// <returns>string</returns>
        /// <remarks>GetSendEmail2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>23/03/2019</FecCrea></item></list>
        public string GetSendEmail2(Model.SuspensionReconnectionModel objModel, string strRutaArchivo)
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

                    if (DisplayFileFromServerSharedFile(objModel.idSession, objAudit.transaction, strRutaArchivo, out attachFile))
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

                            strSubject = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailSuspensionReconexionChanges", vArchivoINTPOSTConfigMsg);
                            strMessage = Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailSuspensionReconexionChanges", vArchivoINTPOSTConfigMsg);

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
        #endregion

        #region CONSTANCY PDF

        ///<summary>Metodo para generar la constancia en formato PDF</summary>
        ///<param name="objModel"></param>
        ///<param name="strInteraction"></param>
        ///<returns>string<returns>
        ///<remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public string GetConstancyPDF(string strIdSession, MODELS.SuspensionReconnectionModel objModel, string strInteraction)
        {
            string NAME_PDF = string.Empty;


            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "Inicia metodo GetConstancyPDF");
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

                    StrFechaSuspension = objModel.dateSuspension,
                    StrFechaActivacion = objModel.dateReactivation,
                    StrCostoReactivacion = objModel.impPagar,
                    strEnvioCorreo = objModel.chkEmail ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not,
                    strCorreoCliente = objModel.chkEmail ? objModel.email : string.Empty,
                    StrEmail = objModel.chkEmail ? objModel.email : string.Empty,
                    //made13
                    strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital"),
                    StrRepresLegal = objModel.legalAgent,
                    strContrato = objModel.msisdn,
                    StrCodUsuario = objModel.currentUser,
                    StrNombreAgenteUsuario = objModel.fullNameUser,
                    StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaSuspReconINT"),


                    //onbase
                    strTipoOperacion = objModel.subClaseCode,
                    StrPuntoAtencion = objModel.cboCACDAC,
                    strNroInter = strInteraction,
                    strNombreCliente = objModel.fullName,
                    strTipoCliente = objModel.tipoCliente,
                    strCuentaCliente = objModel.account,
                    strTipoDocTitular = objModel.documentType,
                    strNroDocTitutar = objModel.documentNumber,
                    strRepresentanteLegal = objModel.fullNameUser,
                    StrNroServicio = objModel.msisdn,
                    strCodeTCRM = objModel.subClaseCode
                };

                CommonIFIService.GenerateConstancyResponseCommon response = GenerateContancyPDF(objAuditRequest.Session, oParameter, GetLstKeywordRequest);

                NAME_PDF = response.FullPathPDF;

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, " NAME_PDF:   " + NAME_PDF);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return NAME_PDF;
        }


        ///<summary>Metodo para guarda los datos del objeto E_KeywordRequest </summary>
        ///<param name="objGenerateConstancyRequest"></param>
        ///<returns>List<returns>
        ///<remarks>GetLstKeywordRequest</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public List<E_KeywordRequest> GetLstKeywordRequest(GenerateConstancyRequestCommon objGenerateConstancyRequest)
        {
            List<E_KeywordRequest> lstKeywordRequest = new List<E_KeywordRequest>();
            string Cadena = ConfigurationManager.AppSettings("strKeywordRequestSusp").ToString();
            string[] KeywordRequest = Cadena.Split(',');

            string[] Keyword = { objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strTipoOperacion,
                                   objGenerateConstancyRequest.ParametersGeneratePDFGeneric.StrPuntoAtencion,
                                   objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strNroInter,
                                   objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strNombreCliente,
                                   objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strTipoCliente,
                                   objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strCuentaCliente,
                                   objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strTipoDocTitular,
                                    objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strNroDocTitutar,
                                    objGenerateConstancyRequest.ParametersGeneratePDFGeneric.strRepresentanteLegal,
                                     objGenerateConstancyRequest.ParametersGeneratePDFGeneric.StrNroServicio
                               };

            for (int i = 0; i < KeywordRequest.Length; i++)
            {
                E_KeywordRequest objKeywordRequest = new E_KeywordRequest();
                objKeywordRequest.Campo = KeywordRequest[i].ToString() == null ? string.Empty : KeywordRequest[i].ToString();
                objKeywordRequest.Valor = Keyword[i] == null ? string.Empty : Keyword[i];
                objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString(); ;
                objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString(); ;
                lstKeywordRequest.Add(objKeywordRequest);
            }
            return lstKeywordRequest;
        }
        #endregion

        #region Auditoria

        ///<summary>Metodo para guardar los datos para registrar la auditoría</summary>
        ///<param name="objModel"></param>
        ///<param name="strText"></param>
        ///<returns>void<returns>
        ///<remarks>InsertAudit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        private void InsertAudit(SuspensionReconnectionModel objModel, string strText)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(objModel.idSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "SuspensionReconnectionCustomerBaseController", "InsertAudit", "strWebServiceSeguridad");
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Message :" + msg);
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

                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Iniciando SAVE AUDIT");
                CommonIFIService.SaveAuditResponseCommon SaveAudit = SaveResponse(objRequest);
                result = SaveAudit.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

        }


        ///<summary>Metodo para registrar la auditoría</summary>
        ///<param name="objRequest"></param>
        ///<returns>CommonIFIService.SaveAuditResponseCommon<returns>
        ///<remarks>SaveResponse</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public CommonIFIService.SaveAuditResponseCommon SaveResponse(CommonIFIService.SaveAuditRequestCommon objRequest)
        {
            Claro.Web.Logging.Configure();
            Claro.Web.Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, "INICIANDO SaveResponse");
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
        #endregion

        ///<summary>Metodo para cargar los datos de la lista de tareas programadas a editar</summary>
        ///<param name="idSession"></param>
        ///<param name="contractId"></param>
        ///<param name="currentUser"></param>
        ///<param name="vServType"></param>
        ///<returns>Dictionary<returns>
        ///<remarks>LoadScheduledTransactionDataEdit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>30/09/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Cliente Claro</Mot></item></list>
        public Dictionary<string, object> LoadScheduledTransactionDataEdit(string idSession,
                                                                       string contractId,
                                                                       string currentUser,
                                                                       string vServType)
        {

            Dictionary<string, object> objResult = new Dictionary<string, object>();

            FixedIFIService.ScheduledTransaction oSuspensionReactivation = null;

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(idSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadScheduledTransactionDataEdit");


            FixedIFIService.ListScheduledTransactionsRequest objTransactionScheduledRequest = new FixedIFIService.ListScheduledTransactionsRequest
            {
                audit = audit,
                UserApp = currentUser,
                ApplicationName = ConfigurationManager.AppSettings("strConsCodAplicSuReTemp"),
                Advisor = string.Empty,
                Account = string.Empty,
                NameCACDAC = string.Empty,
                StrStartDate = string.Empty,
                StrEndDate = string.Empty,
                ServiceState = string.Empty,
                CodeInteraction = string.Empty,
                TransactionType = string.Empty,
                ServiCoId = contractId
            };

            try
            {
                FixedIFIService.ListScheduledTransactionsResponse objResponse =
                Claro.Web.Logging.ExecuteMethod<FixedIFIService.ListScheduledTransactionsResponse>(
                () => { return oFixedService.GetListScheduledTransactions(objTransactionScheduledRequest); }
                );


                if (objResponse.LstTransactions.Count > 0)
                {
                    oSuspensionReactivation = objResponse.LstTransactions.Where(x => x.SERVI_COD.Equals(vServType))
                                              .OrderByDescending(x => x.SERVC_CODE_INTERACTION)
                                              .FirstOrDefault();

                    objResult["SuspensionReactivationList"] = oSuspensionReactivation;

                    if (!string.IsNullOrEmpty(oSuspensionReactivation.SERVC_CODE_INTERACTION))
                    {
                        objResult["TemplateInteraction"] = GetInfoInteractionTemplate(idSession, oSuspensionReactivation.SERVC_CODE_INTERACTION);
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);

            }

            return objResult;
        }

    }


}
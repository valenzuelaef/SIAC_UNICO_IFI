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
using System.Collections.Generic;
using Claro.Web;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class RetentionCancelServicesController : CommonServicesController
    {


        private readonly Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient _oServiceCommon = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient _oServiceFixed = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.WirelessIFIService.WirelessIFIServiceClient _oServiceWireless = new Claro.SIACU.Web.WebApplication.IFI.WirelessIFIService.WirelessIFIServiceClient();


        #region LOAD TRANSACTION


        ///<summary>Metodo que retorna la vista del controlador para Retención/Cancelación</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>RetentionCancelServices</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public ActionResult RetentionCancelServices()
        {

            return View("~/Areas/IFITransactions/Views/RetentionCancelServices.cshtml");
        }


        ///<summary>Metodo que se ejecuta al cargar la página</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strContratoID"></param>
        ///<param name="strListNumImportar"></param>
        ///<param name="strNroTelefono"></param>
        ///<param name="CadenaOpciones"></param>
        ///<param name="CodeTipification"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>IniLoadPage</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult IniLoadPage(string strIdSession, string strContratoID, string strListNumImportar, string strNroTelefono, string CadenaOpciones, string CodeTipification)
        {
            bool resultado = false;
            Model.InteractionModel objInteractionTemp = new Model.InteractionModel();
            string hidTipoServ;
            var audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(strIdSession);
            try
            {
                string strTempTrans = ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Substring(ConfigurationManager.AppSettings("strCodigoTransRetCanServ").Length - 3, 3);
                string gConstTipoHFC = ConfigurationManager.AppSettings("gConstTipoHFC");
                string strNombreTipoTelef = string.Empty;
                string gstrTipoServDTH = Claro.SIACU.Constants.gstrTipoServDTH;
                string strFechaResultado = string.Empty;
                string strMenParam = string.Empty;
                string strFecMinimaCancel = string.Empty;
                int intNroDias = 0;
                string strNumDiasHabiles = ConfigurationManager.AppSettings("strRetentionCancelServicesNumDiasHabilesINT");
                string strFechaProgRes = string.Empty;
                bool FlatReintegro = false;
                string strFechaIni = string.Empty;
                double dlbCodNuevoPlan = 0;
                bool habilitaFecha = false;
                string Message = string.Empty;
                var dblPenalidad = Claro.SIACU.Constants.NumeracionCERODECIMAL2;
                CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServINT");
                Model.InteractionModel oTipificacion = new Model.InteractionModel();

                Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AddDayWorkResponse oDayLabour = new FixedIFIService.AddDayWorkResponse();

                if (strListNumImportar == null || strListNumImportar == string.Empty)
                {
                    if (strTempTrans == gConstTipoHFC)
                    {

                        strNombreTipoTelef = ValidarPermiso(strIdSession, strContratoID, strListNumImportar);

                    }
                    else
                    {

                        strNombreTipoTelef = gstrTipoServDTH;

                    }
                    hidTipoServ = strNombreTipoTelef;

                    oTipificacion = CargarTipificacion(strIdSession, CodeTipification);
                    objInteractionTemp = oTipificacion;

                }// --------  Tipificacion


                if (oTipificacion.FlagCase == Claro.SIACU.Constants.OK)
                {

                    #region DIAS LABORABLES

                    intNroDias = string.IsNullOrEmpty(strNumDiasHabiles) ? Claro.Constants.NumberOne : System.Convert.ToInt16(strNumDiasHabiles);

                    strFechaIni = DateTime.Now.ToShortDateString();
                    oDayLabour = GetAddDayWork(strIdSession, strFechaIni, intNroDias);


                    if (oDayLabour.FechaResultado == string.Empty)
                    {


                        oDayLabour.FechaResultado = CalculaDiasHabiles(GetParameterData(strIdSession, ConfigurationManager.AppSettings("gConstDiasHabiles")).Parameter.Value_C, audit);

                    }

                    #endregion


                    #region PENALIDAD

                    dblPenalidad = Claro.SIACU.Constants.NumeracionCERODECIMAL2;
                    WirelessIFIService.ApadeceResponse response = _oServiceWireless.GetApadece(new WirelessIFIService.ApadeceRequest()
                                {

                                    audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(strIdSession),
                                    strCoId = strContratoID,
                                    strMsisdn = strNroTelefono

                                });

                    if (response != null && !string.IsNullOrEmpty(response.monto))
                    {
                        dblPenalidad = response.monto;
                    }

                    #endregion
                    strFecMinimaCancel = CalculaDiasHabiles(GetParameterData(strIdSession, ConfigurationManager.AppSettings("gConstCodParametroDiasMinimo")).Parameter.Value_C, audit);



                    if (CadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfRetCanPenalidadINT")) > -1)
                    {
                        FlatReintegro = false;
                    }
                    else
                    {
                        FlatReintegro = true;
                    }


                    if (!(CadenaOpciones.IndexOf(ConfigurationManager.AppSettings("strKeyPerfActualizaFechaProgramacionINT")) > -1))
                    {
                        habilitaFecha = true;
                    }
                    else
                    {
                        habilitaFecha = false;
                    }

                    resultado = true;
                }
                else
                {

                    resultado = false;
                    Message = oTipificacion.Result;
                }


                return Json(new { data = resultado, Message, oDayLabour.FechaResultado, FlatReintegro, dblPenalidad, habilitaFecha });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message.ToString());
                return Json(new { data = ex.Message.ToString() });
            }
        }


        ///<summary>Metodo que obtiene los mensajes indicados</summary>
        ///<param name="strIdSession"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetMessage</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgLineaPOTP", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgErrRecData", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strValueTipoTrabajoBajaTOTAL", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstFlagRetensionCancelacion"));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strValueMotivoSOTDefecto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strValueTipoTrabajoDefecto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strFlagInhabTipTraMotSot", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstFlagRetensionCancelacionEstado"));
            lstMessage.Add(ConfigurationManager.AppSettings("gConstPerfHayCaso"));
            lstMessage.Add(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00")); //9
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgSelTr", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgSelMot", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgSelSubMot", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgSelAc", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgSelsinoCaso", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgErrRecData", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgErrCampNumeri", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgSelCacDac", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMensajeEsperaLoader", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("CambNumSinCosto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("CostoCambioNumeroConsumer", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("SusTempSinCostoReconexion", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("MontoCobroReactivacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strEstadoContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("flagRestringirAccesoTemporalCR", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgOpcionTemporalmenteInhabilitada", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("strMsgDebeCargLinea", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(Claro.Utils.GetValueFromConfigFileIFI("gConstMsgLineaStatSuspe", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
            lstMessage.Add(ConfigurationManager.AppSettings("strConsLineaDesaActiva"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMessageETAValidation"));
            lstMessage.Add(ConfigurationManager.AppSettings("strMsgTranGrabSatis"));
            lstMessage.Add(ConfigurationManager.AppSettings("strCodigoTransRetCanServINT"));
            //lstMessage.Add(ConfigurationManager.AppSettings("strConsLineaDesaActiva"));//Se confundieron-al pasar
            lstMessage.Add(ConfigurationManager.AppSettings("strMensajeRetenidoCancelacionDevolucionEquipoIFI"));//PROY-140709_cambio_de_servicio- Evalenzs
           // lstMessage.Add(ConfigurationManager.AppSettings("strMensajeNoRetenidoDevolucionEquipoIFI"));//PROY-140709_cambio_de_servicio- Evalenzs
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }


        ///<summary>Metodo que obtiene las transacciones de las tareas programadas</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strContratoID"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetTransactionScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetTransactionScheduled(string strIdSession, string strContratoID)
        {
            bool Resultado = true;

            var audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(strIdSession);

            var objResponse = new WirelessIFIService.TransactionScheduledResponse();
            var objRequest = new WirelessIFIService.TransactionScheduledRequest()
            {
                audit = audit,
                vstrCoId = strContratoID,
                vstrCuenta = string.Empty,
                vstrFDesde = string.Empty,
                vstrFHasta = string.Empty,
                vstrEstado = string.Empty,
                vstrAsesor = string.Empty,
                vstrTipoTran = string.Empty,
                vstrCodInter = string.Empty,
                vstrCacDac = string.Empty
            };


            try
            {

                objResponse =
                    Claro.Web.Logging.ExecuteMethod<WirelessIFIService.TransactionScheduledResponse>(
                    () => { return _oServiceWireless.GetListScheduledTransactions(objRequest); });

                if (objResponse != null && objResponse.ListTransactionScheduled != null && objResponse.ListTransactionScheduled.Length > 0)
                {

                    foreach (var item in objResponse.ListTransactionScheduled)
                    {

                        if (item.SERVC_ESTADO.Equals(Claro.Constants.NumberZeroString))
                        { Resultado = false; break; }
                        else if (item.SERVC_ESTADO.Equals(Claro.Constants.NumberOneString))
                        { Resultado = false; break; }
                        else if (item.SERVC_ESTADO.Equals(Claro.Constants.NumberTwoString))
                        { Resultado = false; break; }
                    }

                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);
                Resultado = false;
            }



            return Json(new { data = Resultado });
        }


        ///<summary>Metodo que obtiene la penalidad</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strTempTrans"></param>
        ///<param name="gConstTipoHFC"></param>
        ///<param name="strNroTelefono"></param>
        ///<param name="dlbCodNuevoPlan"></param>
        ///<param name="strContratoID"></param>
        ///<returns>FixedIFIService.RetentionCancelServicesResponse<returns>
        ///<remarks>ObtainPenalty</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.RetentionCancelServicesResponse ObtainPenalty(string strIdSession, string strTempTrans, string gConstTipoHFC, string strNroTelefono, double dlbCodNuevoPlan, string strContratoID)
        {
            string strPagoApadece = string.Empty;
            DateTime FechaPenalidad = DateTime.Now;
            double VariableCero = Claro.Constants.NumberZero;
            double VariableCeroDouble = Claro.Constants.NumberZeroDouble;

            FixedIFIService.RetentionCancelServicesResponse oApadeceCancel = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.RetentionCancelServicesResponse oPenalidadExt = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.RetentionCancelServicesResponse oResultado = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            try
            {
                if (strTempTrans != gConstTipoHFC)//HFC != 
                {
                    #region GetDataBSCSExt
                    oApadeceCancel = new FixedIFIService.RetentionCancelServicesResponse();

                    oApadeceCancel = GetDataBSCSExt(strIdSession, strNroTelefono, dlbCodNuevoPlan);

                    #endregion

                    if (oApadeceCancel.Resultado.ToString() == Claro.SIACU.Constants.TrueMin)
                    {
                        #region GetPenalidadExt
                        oPenalidadExt = new RetentionCancelServicesResponse();
                        oPenalidadExt = GetPenalidadExt(strIdSession, strNroTelefono, FechaPenalidad, oApadeceCancel.NroFacturas, oApadeceCancel.CargoFijoActual,
                                                    oApadeceCancel.CargoFijoNuevoPlan, 30, oApadeceCancel.CargoFijoNuevoPlan);


                        #endregion
                    }

                    Claro.Web.Logging.Info(strIdSession, audit.transaction, oApadeceCancel.ValorApadece.ToString());
                    if (oPenalidadExt.PenalidaAPADECE == VariableCero || oPenalidadExt.PenalidaAPADECE == VariableCeroDouble || double.IsNaN(oPenalidadExt.PenalidaAPADECE))
                    {
                        #region GetApadeceCancelRet
                        oApadeceCancel = new RetentionCancelServicesResponse();
                        oApadeceCancel = GetApadeceCancelRet(strIdSession, Convert.ToInt(strNroTelefono), Convert.ToInt(strContratoID));
                        oApadeceCancel.PenalidaAPADECE = oPenalidadExt.PenalidaAPADECE;
                        #endregion

                        if (oApadeceCancel.ValorApadece == VariableCeroDouble || double.IsNaN(oApadeceCancel.ValorApadece))
                            strPagoApadece = Claro.Constants.NumberZeroString;
                        else
                            strPagoApadece = Claro.Constants.NumberOneString;
                    }

                }
                else
                {
                    #region GetApadeceCancelRet
                    oApadeceCancel = GetApadeceCancelRet(strIdSession, Convert.ToInt(strNroTelefono), Convert.ToInt(strContratoID));
                    #endregion
                    oApadeceCancel.PenalidadPCS = Claro.Constants.NumberZero;
                }

                if (oApadeceCancel.CodMessage != Claro.SIACU.Constants.OK)
                {
                    oApadeceCancel.PenalidadPCS = Claro.Constants.NumberZero;
                    oApadeceCancel.PenalidaAPADECE = Claro.Constants.NumberZero;
                    oApadeceCancel.CargoFijoNuevoPlan = Claro.Constants.NumberZero;
                }

                oResultado = new RetentionCancelServicesResponse();
                oResultado.PenalidadPCS = Math.Round(oApadeceCancel.PenalidadPCS, 2); //hdnPenalidadPCS
                oResultado.PenalidaAPADECE = Math.Round(oApadeceCancel.ValorApadece, 2);//  txtReintegro -PO_MONTO_APADECE
                oResultado.CargoFijoNuevoPlan = Math.Round(oApadeceCancel.PenalidaAPADECE + oApadeceCancel.PenalidadPCS, 2); //hdnTotalPenalidad

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, ex.Message);
            }


            return oResultado;

        }


        ///<summary>Metodo que añade días de trabajo</summary>
        ///<param name="strIdSession"></param>
        ///<param name="strFechaIni"></param>
        ///<param name="intNroDias"></param>
        ///<returns>FixedIFIService.AddDayWorkResponse<returns>
        ///<remarks>GetAddDayWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.AddDayWorkResponse GetAddDayWork(string strIdSession, string strFechaIni, int intNroDias)
        {
            FixedIFIService.AddDayWorkResponse objkResponse = new FixedIFIService.AddDayWorkResponse();
            FixedIFIService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            FixedIFIService.AddDayWorkRequest objRequest = new FixedIFIService.AddDayWorkRequest();
            objRequest.audit = audit;
            objRequest.FechaInicio = strFechaIni;
            objRequest.NumeroDias = intNroDias;


            try
            {

                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedIFIService.AddDayWorkResponse>(() =>
                    {
                        return _oServiceFixed.GetAddDayWork(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, ex.Message);
            }


            return objkResponse;
        }


        ///<summary>Metodo que obtiene los datos indicados de la BD BSCS</summary>
        ///<param name="strIdSession"></param>
        ///<param name="NroTelefono"></param>
        ///<param name="CodNuevoPlan"></param>
        ///<returns>FixedIFIService.RetentionCancelServicesResponse<returns>
        ///<remarks>GetDataBSCSExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.RetentionCancelServicesResponse GetDataBSCSExt(string strIdSession, string NroTelefono, double CodNuevoPlan)
        {
            FixedIFIService.RetentionCancelServicesResponse objkResponse = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            FixedIFIService.RetentionCancelServicesRequest objRequest = new FixedIFIService.RetentionCancelServicesRequest();
            objRequest.audit = audit;
            objRequest.NroTelefono = NroTelefono;
            objRequest.CodNuevoPlan = CodNuevoPlan;



            try
            {
                objkResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedIFIService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.ObtenerDatosBSCSExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);

            }



            return objkResponse;
        }


        ///<summary>Metodo que obtiene la penalidad</summary>
        ///<param name="strIdSession"></param>
        ///<param name="NroTelefono"></param>
        ///<param name="FechaPenalidad"></param>
        ///<param name="NroFacturas"></param>
        ///<param name="CargoFijoActual"></param>
        ///<param name="CargoFijoNuevoPlan"></param>
        ///<param name="DiasxMes"></param>
        ///<param name="CodNuevoPlan"></param>
        ///<returns>FixedIFIService.RetentionCancelServicesResponse<returns>
        ///<remarks>GetPenalidadExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.RetentionCancelServicesResponse GetPenalidadExt(string strIdSession, string NroTelefono, DateTime FechaPenalidad, double NroFacturas, double CargoFijoActual,
                                          double CargoFijoNuevoPlan, double DiasxMes, double CodNuevoPlan)
        {
            FixedIFIService.RetentionCancelServicesResponse objResponse = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);



            FixedIFIService.RetentionCancelServicesRequest objRequest = new FixedIFIService.RetentionCancelServicesRequest();
            objRequest.audit = audit;
            objRequest.NroTelefono = NroTelefono;
            objRequest.FechaPenalidad = FechaPenalidad;
            objRequest.NroFacturas = NroFacturas;
            objRequest.CargoFijoActual = CargoFijoActual;
            objRequest.CargoFijoNuevoPlan = CargoFijoNuevoPlan;
            objRequest.DiasxMes = DiasxMes;
            objRequest.CodNuevoPlan = CodNuevoPlan;


            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedIFIService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetObtainPenalidadExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);

            }



            return objResponse;
        }


        ///<summary>Metodo que obtiene el APADECE para cancelación/retención</summary>
        ///<param name="strIdSession"></param>
        ///<param name="NroTelefono"></param>
        ///<param name="codId"></param>
        ///<returns>FixedIFIService.RetentionCancelServicesResponse<returns>
        ///<remarks>GetApadeceCancelRet</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.RetentionCancelServicesResponse GetApadeceCancelRet(string strIdSession, int NroTelefono, int codId)
        {
            FixedIFIService.RetentionCancelServicesResponse objResponse = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            FixedIFIService.RetentionCancelServicesRequest objRequest =
                new FixedIFIService.RetentionCancelServicesRequest();

            objRequest.audit = audit;
            objRequest.Phone = NroTelefono;
            objRequest.CodId = codId;


            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedIFIService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetApadeceCancelRet(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);

            }


            return objResponse;
        }
        #endregion

        #region LOAD TIPIFICATION

        ///<summary>Metodo que permite cargar la tipificación</summary>
        ///<param name="strIdSession"></param>
        ///<param name="CodeTipification"></param>
        ///<returns>Model.InteractionModel<returns>
        ///<remarks>CargarTipificacion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public Model.InteractionModel CargarTipificacion(string IdSession, string CodeTipification)
        {
            FixedIFIService.AuditRequest audit =
               App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(IdSession);
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
                Claro.Web.Logging.Info(IdSession, audit.transaction, ex.Message);

            }

            return objInteraction;
        }

        #endregion

        #region INSERT CASE

        ///<summary>Metodo que permite insert el caso</summary>
        ///<param name="oCaso"></param>
        ///<param name="oPlantillaCaso"></param>
        ///<param name="oModel"></param>
        ///<param name="strEstadoForm"></param>
        ///<returns>FixedIFIService.Interaction<returns>
        ///<remarks>InsertCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.Interaction InsertCaso(FixedIFIService.Interaction oCaso, FixedIFIService.CaseTemplate oPlantillaCaso, Model.RetentionCancelServicesModel oModel,
                                             string strEstadoForm)
        {

            FixedIFIService.Interaction oResponseCase = new FixedIFIService.Interaction();
            FixedIFIService.CaseTemplate oPlantillaResponse = new FixedIFIService.CaseTemplate();
            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            bool resultado = false;
            string IdCaso = string.Empty;

            string strFlgRegistrado = Claro.Constants.NumberOneString;

            try
            {
                if (oCaso.OBJID_CONTACTO == null || oCaso.OBJID_CONTACTO == "0" || oCaso.OBJID_CONTACTO == "")
                {
                    var phone = oModel.Telephone;
                    CustomerResponse objCustomerResponse;
                    FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);
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
                        return GetCustomerData(objGetCustomerRequest, oModel.IdSession);
                    });
                    if (objCustomerResponse != null)
                    {

                        oCaso.OBJID_CONTACTO = objCustomerResponse.contactobjid;
                        oCaso.OBJID_SITE = objCustomerResponse.Customer.SiteCode;

                    }

                }
                if (oCaso.OBJID_SITE == null || oCaso.OBJID_SITE == Claro.Constants.NumberZeroString || oCaso.OBJID_SITE == String.Empty)
                {
                    var phone = oModel.Telephone;
                    CustomerResponse objCustomerResponse;
                    AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);
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
                        return GetCustomerData(objGetCustomerRequest, oModel.IdSession);
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
                    if (!string.IsNullOrEmpty(IdCaso))
                    {
                        if (oPlantillaCaso != null)
                        {
                            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);
                            oPlantillaCaso.ID_CASO = IdCaso;
                            oPlantillaResponse = GuardarPlantillaCaso(oPlantillaCaso, strEstadoForm, audit);
                            resultado = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(oModel.IdSession, oModel.Transaction, ex.Message);
            }
            oResponseCase.CASO_ID = oPlantillaResponse.ID_CASO;
            oResponseCase.FLAG_INSERCION_CASO = oPlantillaResponse.FLAG_INSERCION;
            oResponseCase.MESSAGE_CASO = oPlantillaResponse.MESSAGE;
            return oResponseCase;
        }


        ///<summary>Metodo que permite guardar la plantilla del caso</summary>
        ///<param name="oPlantilla"></param>
        ///<param name="vEstadoForm"></param>
        ///<param name="audit"></param>
        ///<returns>FixedIFIService.CaseTemplate<returns>
        ///<remarks>GuardarPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.CaseTemplate GuardarPlantillaCaso(FixedIFIService.CaseTemplate oPlantilla, string vEstadoForm, FixedIFIService.AuditRequest audit)
        {
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
                Claro.Web.Logging.Info(audit.Session, audit.transaction, ex.Message);
            }

            return oResponse;
        }


        ///<summary>Metodo que permite guardar la plantilla del caso</summary>
        ///<param name="oModel"></param>
        ///<returns>FixedIFIService.CaseTemplate<returns>
        ///<remarks>DatosPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.CaseTemplate DatosPlantillaCaso(Model.RetentionCancelServicesModel oModel)
        {
            FixedIFIService.CaseTemplate oPlantillaCampoData = new FixedIFIService.CaseTemplate();


            oPlantillaCampoData.X_CAS_8 = (oModel.Reintegro == String.Empty ? Claro.Constants.NumberZero : Convert.ToDouble(oModel.Reintegro));
            oPlantillaCampoData.X_CAS_9 = Convert.ToDouble(Claro.Constants.NumberZero);
            oPlantillaCampoData.MONTO_RECARGA = oPlantillaCampoData.X_CAS_8;
            oPlantillaCampoData.X_OPERATOR_PROBLEM = oModel.vMotiveSot;// validar

            oPlantillaCampoData.X_CAS_3 = (oModel.Accion == Claro.Constants.LetterR ? "Retenido" : "Cancelado");
            oPlantillaCampoData.X_CAS_7 = (oModel.hidSupJef == Claro.Constants.LetterS ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString);
            oPlantillaCampoData.X_SUSPENSION_DATE = Convert.ToDate(oModel.FechaCompromiso);
            oPlantillaCampoData.X_FIXED_NUMBER = String.Empty;
            oPlantillaCampoData.NRO_TELEFONO = oModel.NroCelular;
            oPlantillaCampoData.X_CAS_16 = oModel.DesSubMotivo;
            oPlantillaCampoData.X_CAS_5 = oModel.DesSubMotivo;
            oPlantillaCampoData.X_CAS_15 = oModel.DescCacDac;
            oPlantillaCampoData.X_CAS_30 = oModel.Note;
            oPlantillaCampoData.X_CAS_4 = (oModel.TotalInversion == String.Empty ? Claro.Constants.NumberZeroString : oModel.TotalInversion);
            oPlantillaCampoData.X_CAS_6 = oModel.DesAccion;

            if (oPlantillaCampoData.X_CAS_3.Equals("Cancelado"))
            {
                oPlantillaCampoData.X_FLAG_OTHER_PROBLEMS = oModel.PagoAPADECE;
                oPlantillaCampoData.X_MODEL = String.Empty;
            }

            oPlantillaCampoData.X_ADDRESS = oModel.AdressDespatch;
            oPlantillaCampoData.X_CAS_1 = oModel.Reference;
            oPlantillaCampoData.X_CAS_2 = oModel.Departament_Fact;
            oPlantillaCampoData.X_CAS_17 = oModel.District;
            oPlantillaCampoData.X_CAS_18 = oModel.Pais_Fac;
            oPlantillaCampoData.X_CAS_19 = oModel.Provincia;
            oPlantillaCampoData.X_CAS_20 = Claro.Constants.NumberZeroString;
            oPlantillaCampoData.X_CAS_21 = oModel.PlaneCodeBilling;
            oPlantillaCampoData.X_CAS_30 = oModel.Note;
            if (oModel.Flag_Email)
            {
                oPlantillaCampoData.X_FLAG_GPRS = "1";
                oPlantillaCampoData.X_CAS_29 = oModel.Destinatarios;


            }
            else
            {

                oPlantillaCampoData.X_FLAG_GPRS = "0";
                oPlantillaCampoData.X_CAS_29 = string.Empty;

            }


            return oPlantillaCampoData;
        }


        ///<summary>Metodo que obtiene los datos del caso</summary>
        ///<param name="model"></param>
        ///<returns>FixedIFIService.Interaction<returns>
        ///<remarks>DatosCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public FixedIFIService.Interaction DatosCaso(Model.RetentionCancelServicesModel model)
        {
            FixedIFIService.Interaction objInteractionModel = new FixedIFIService.Interaction();

            //tipificacion
            var objInteraction = new Model.InteractionModel();
            objInteraction = CargarTipificacion(model.IdSession, model.CodeTipification);

            if (objInteraction.FlagCase == Claro.SIACU.Constants.OK)
            {
                string strFlgRegistrado = Claro.Constants.NumberOneString;
                //ObtenerCliente
                var strNroTelephone = model.Telephone;


                objInteractionModel.OBJID_CONTACTO = GetCustomer(strNroTelephone, model.IdSession);  //Get Customer = strObjId
                objInteractionModel.TELEFONO = model.Telephone;
                objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
                objInteractionModel.TIPIFICACION = objInteraction.Type;
                objInteractionModel.OBJID_SITE = model.OBJID_SITE;
                objInteractionModel.CLASE = objInteraction.Class;
                objInteractionModel.SUBCLASE = objInteraction.SubClass;
                objInteractionModel.CONTRATO = model.ContractId;
                objInteractionModel.PLANO = model.Plan;
                objInteractionModel.COLA = Claro.Utils.GetValueFromConfigFileIFI("PColaRetenCanceServ", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfig"));
                objInteractionModel.PRIORIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                objInteractionModel.SEVERIDAD = ConfigurationManager.AppSettings("NoPrecisado");
                objInteractionModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                objInteractionModel.TIPO_INTERACCION = ConfigurationManager.AppSettings("AtencionDefault");
                objInteractionModel.NOTAS = model.Note;
                objInteractionModel.FLAG_INTERACCION = Claro.Constants.NumberOneString;
                objInteractionModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                objInteractionModel.USUARIO_ID = model.CurrentUser;


            }
            else
            {
                objInteractionModel.RESULTADO = objInteraction.Result;
                objInteractionModel.FLAG_INSERCION = objInteraction.FlagCase;

            }

            return objInteractionModel;
        }


        #endregion

        #region INSERT INTERACTION


        ///<summary>Metodo que permite guardar la interacción</summary>
        ///<param name="oModel"></param>
        ///<returns>List<returns>
        ///<remarks>GrabaInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public List<string> GrabaInteraccion(Model.RetentionCancelServicesModel oModel)
        {

            var strUserSession = string.Empty;
            var strUserAplication = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            var strPassUser = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            var strNroTelephone = oModel.Telephone;
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var lstaDatTemplate = new List<string>();
            Model.InteractionModel oInteraccion = new Model.InteractionModel();


            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);

            try
            {

                oPlantillaDat = GetDataTemplateInteraction(oModel);

                oInteraccion = DatosInteraccion(oModel);
                var resultInteraction = InsertInteraction(oInteraccion, oPlantillaDat, strNroTelephone, strUserSession, strUserAplication, strPassUser, true, oModel.IdSession, oModel.CustomerId);

                foreach (KeyValuePair<string, object> par in resultInteraction)
                {
                    lstaDatTemplate.Add(par.Value.ToString());
                }

                if (lstaDatTemplate[0] != Claro.SIACU.Constants.OK && lstaDatTemplate[3] == string.Empty)
                {

                    Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));
                    throw new Exception(Claro.Utils.GetValueFromConfigFileIFI("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")));

                }

            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

            }

            return lstaDatTemplate;
        }


        ///<summary>Metodo que permite insertar los datos de la interacción</summary>
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
        ///<remarks>InsertInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public new Dictionary<string, object> InsertInteraction(Model.InteractionModel objInteractionModel,
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

            var strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = Claro.Constants.NumberOneString;
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
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
                    TELEFONO = objInteractionModel.Telephone,
                    FECHA_CREACION = objInteractionModel.DateCreaction,
                    OBJID_CONTACTO = objInteractionModel.ObjidContacto


                },
                InteractionTemplate = new CommonIFIService.InsertTemplateInteraction()
                {
                    _X_INTER_8 = oPlantillaDat.X_INTER_8,
                    _X_INTER_9 = oPlantillaDat.X_INTER_9,
                    _X_CHARGE_AMOUNT = oPlantillaDat.X_CHARGE_AMOUNT,
                    _X_OPERATION_TYPE = oPlantillaDat.X_OPERATION_TYPE,
                    _X_REGISTRATION_REASON = oPlantillaDat.X_REGISTRATION_REASON,
                    _X_FLAG_OTHER = oPlantillaDat.X_FLAG_OTHER,
                    _X_EXPIRE_DATE = oPlantillaDat.X_EXPIRE_DATE,
                    _X_FIXED_NUMBER = oPlantillaDat.X_FIXED_NUMBER,
                    _X_CLARO_NUMBER = oPlantillaDat.X_CLARO_NUMBER,
                    _X_REASON = oPlantillaDat.X_REASON,
                    _X_INTER_16 = oPlantillaDat.X_INTER_16,
                    _X_INTER_15 = oPlantillaDat.X_INTER_15,
                    _X_ADJUSTMENT_AMOUNT = oPlantillaDat.X_ADJUSTMENT_AMOUNT,
                    _X_ADJUSTMENT_REASON = oPlantillaDat.X_ADJUSTMENT_REASON,
                    _X_FLAG_REGISTERED = oPlantillaDat.X_FLAG_REGISTERED,
                    _X_MODEL = oPlantillaDat.X_MODEL,
                    _X_ZIPCODE = oPlantillaDat.X_ZIPCODE,
                    _X_INTER_18 = oPlantillaDat.X_INTER_18,
                    _X_NAME_LEGAL_REP = oPlantillaDat.X_NAME_LEGAL_REP,
                    _X_OLD_LAST_NAME = oPlantillaDat.X_OLD_LAST_NAME,
                    _X_LASTNAME_REP = oPlantillaDat.X_LASTNAME_REP,
                    _X_PHONE_LEGAL_REP = oPlantillaDat.X_PHONE_LEGAL_REP,
                    _X_FLAG_LEGAL_REP = oPlantillaDat.X_FLAG_LEGAL_REP,
                    _X_ADDRESS = oPlantillaDat.X_ADDRESS,
                    _X_INTER_1 = oPlantillaDat.X_INTER_1,
                    _X_DEPARTMENT = oPlantillaDat.X_DEPARTMENT,
                    _X_DISTRICT = oPlantillaDat.X_DISTRICT,
                    _X_INTER_2 = oPlantillaDat.X_INTER_2,
                    _X_INTER_3 = oPlantillaDat.X_INTER_3,
                    _X_INTER_20 = oPlantillaDat.X_INTER_20,
                    _X_CLARO_LDN1 = oPlantillaDat.X_CLARO_LDN1,
                    _X_INTER_29 = oPlantillaDat.X_INTER_29,
                }

            };

            var objCustomerResponse = Claro.Web.Logging.ExecuteMethod<CustomerResponse>(() => { return _oServiceFixed.GetCustomer(objGetCustomerRequest); });

            //Validacion de Contingencia
            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }



            CommonIFIService.InsertGeneralResponse objInsertGeneralResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.InsertGeneralResponse>(() => { return _oServiceCommon.GetinsertInteractionGeneral(objInsertGeneralRequest); });

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


        ///<summary>Metodo que permite insertar los datos de la plantilla de la interacción</summary>
        ///<param name="oModel"></param>
        ///<returns>Model.TemplateInteractionModel<returns>
        ///<remarks>GetDataTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.RetentionCancelServicesModel oModel)
        {
            var oPlantCampDat = new Model.TemplateInteractionModel();
            CaseInsertRequest oresponse = new CaseInsertRequest();
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);

            try
            {
              
                oPlantCampDat.X_INTER_9 = (oModel.Reintegro == string.Empty ? Claro.Constants.NumberZero : Convert.ToDouble(oModel.Reintegro));
                oPlantCampDat.X_CHARGE_AMOUNT = oPlantCampDat.X_INTER_8;
                oPlantCampDat.X_OPERATION_TYPE = oModel.DesMotivos;
                oPlantCampDat.X_REGISTRATION_REASON = oModel.DesAccion;
                oPlantCampDat.X_ADJUSTMENT_REASON = oModel.Accion.ToUpper().Equals("R") ? "Retenido" : "Cancelado";
                oPlantCampDat.X_FLAG_OTHER = (oModel.hidSupJef == Claro.Constants.LetterS ? Claro.Constants.NumberOneString : Claro.Constants.NumberZero.ToString());
                oPlantCampDat.X_EXPIRE_DATE = string.IsNullOrEmpty(oModel.DateProgrammingSot) ? DateTime.UtcNow : Convert.ToDate(oModel.DateProgrammingSot);
                oPlantCampDat.X_FIXED_NUMBER = string.Empty;
                oPlantCampDat.X_CLARO_NUMBER = oModel.Telephone;
                oPlantCampDat.X_REASON = oModel.Accion;
                oPlantCampDat.X_INTER_16 = oModel.DesSubMotivo;
                oPlantCampDat.X_INTER_15 = oModel.DescCacDac;
                oPlantCampDat.X_ADJUSTMENT_AMOUNT = (oModel.TotalInversion == string.Empty ? Claro.Constants.NumberZero : Convert.ToDouble(oModel.TotalInversion));
                if (!string.IsNullOrEmpty(oModel.Email))
                    oPlantCampDat.X_EMAIL = oModel.Email;


                if (oPlantCampDat.X_REASON == Claro.SIACU.Constants.gstrNoRetenido)
                {
                    oPlantCampDat.X_FLAG_REGISTERED = oModel.PagoAPADECE;
                    oPlantCampDat.X_MODEL = string.Empty;

                }

                oPlantCampDat.X_ZIPCODE = oModel.Telephone;
                oPlantCampDat.X_INTER_18 = string.Empty;


                if (oModel.TypeClient.ToUpper().Equals("CONSUMER"))
                {

                    oPlantCampDat.X_NAME_LEGAL_REP = string.Empty;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.NroDoc;

                }
                else
                {

                    oPlantCampDat.X_NAME_LEGAL_REP = oModel.RepresentLegal;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.DNI_RUC;

                }

                oPlantCampDat.X_LASTNAME_REP = oModel.TypeDoc.ToUpper();
                oPlantCampDat.X_PHONE_LEGAL_REP = oModel.TelefonoReferencia;
                oPlantCampDat.X_FLAG_LEGAL_REP = string.Empty; 
                oPlantCampDat.X_ADDRESS = oModel.AdressDespatch;
                oPlantCampDat.X_INTER_1 = oModel.Reference;
                oPlantCampDat.X_DEPARTMENT = oModel.Departament_Fact;
                oPlantCampDat.X_DISTRICT = oModel.District_Fac;
                oPlantCampDat.X_INTER_2 = oModel.Pais_Fac;
                oPlantCampDat.X_INTER_3 = oModel.Provincia_Fac;
                oPlantCampDat.X_INTER_20 = Claro.Constants.NumberZeroString;
                
                oPlantCampDat.X_CLARO_LDN1 = oModel.Flag_Email == true ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString; //Validar

                if (oPlantCampDat.X_CLARO_LDN1 == Claro.Constants.NumberOneString)
                {
                    oPlantCampDat.X_INTER_29 = oModel.Email;
                }
                else
                {
                    oPlantCampDat.X_INTER_29 = string.Empty;
                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, ex.Message);
            }


            return oPlantCampDat;

        }


        ///<summary>Metodo que obtiene los datos de la interacción</summary>
        ///<param name="oModel"></param>
        ///<returns>Model.InteractionModel<returns>
        ///<remarks>DatosInteraccion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public Model.InteractionModel DatosInteraccion(Model.RetentionCancelServicesModel oModel)
        {

            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest();


            try
            {
                // Get Datos de la Tipificacion
                objInteraction = CargarTipificacion(oModel.IdSession, oModel.CodeTipification);
                var strNroTelephone = oModel.Telephone;
                oInteraccion.ObjidContacto = GetCustomer(strNroTelephone, oModel.IdSession);
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = oModel.Telephone;
                oInteraccion.Type = objInteraction.Type;
                oInteraccion.Class = objInteraction.Class;
                oInteraccion.SubClass = objInteraction.SubClass;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.Constants.NumberZeroString;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Plan;
                oInteraccion.FlagCase = Claro.Constants.NumberZeroString;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }



            return oInteraccion;

        }

        #endregion

        #region SAVE TRANSACTIONS


        ///<summary>Metodo que permite guardar la transacción de retención</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>SaveTransactionRetention</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult SaveTransactionRetention(Model.RetentionCancelServicesModel oModel)
        {
            string vInteractionId = string.Empty;
            string vFlagInteraction = string.Empty;
            string vDesInteraction = string.Empty;
            bool ResultadoAudit = false;
            string strRutaArchivo = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            oModel.fechaActual = DateTime.Now.ToShortDateString();

            #region GrabaInteraccion
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);

            oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServINT");
            List<string> strInteractionId = GrabaInteraccion(oModel);
            vDesInteraction = strInteractionId[0].ToString();
            vFlagInteraction = strInteractionId[2].ToString();
            vInteractionId = strInteractionId[3].ToString();
            #endregion

            if (vDesInteraction == Claro.SIACU.Constants.OK)
            {
                #region CONSTANCY PDF
                vDesInteraction = ConfigurationManager.AppSettings("strMsgTranGrabSatis");

                Dictionary<string, object> oConstancyPDF = new Dictionary<string, object>();

                oConstancyPDF = GetConstancyPDF(oModel.IdSession, vInteractionId, oModel);
                strRutaArchivo = oModel.strFullPathPDF;
                #endregion

                if (oModel.Flag_Email)
                {
                    #region SEND EMAIL
                    byte[] attachFile = null;
                    string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                    if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                    {
                        List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                        if (attachFile != null)
                        {
                            AttachedFile objFile = new AttachedFile();

                            objFile.file = attachFile;
                            objFile.Name = strAdjunto;
                            objFile.HeadBoard = strAdjunto;
                            lstAdjuntos.Add(objFile);

                            if (GetSendEmail2(oModel, lstAdjuntos) == Claro.SIACU.Constants.OK)
                            {
                                MensajeEmail = "Se enviará un correo de notificación.";
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

                    }
                    else
                    {
                        MensajeEmail = "No se envió un correo de notificación.";
                    }
                    #endregion
                }

            }
            else
            {
                vDesInteraction = ConfigurationManager.AppSettings("strMensajeDeError");

            }

            #region AUDIT

            ResultadoAudit = Auditoria(oModel);

            #endregion

            return Json(new { vDesInteraction, vFlagInteraction, vInteractionId, strRutaArchivo, MensajeEmail });
        }


        ///<summary>Metodo que permite guardar la transacción de cancelación</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>SaveTransactionNoRetention</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult SaveTransactionNoRetention(Model.RetentionCancelServicesModel oModel)
        {
            bool resultado = false;
            string mesj = "";
            string Message = string.Empty;
            string GeneroCaso = string.Empty;
            bool ResultadoAudit = false;
            string vInteractionId = string.Empty;
            string vFlagInteraction = string.Empty;
            string strEstadoForm = Claro.Constants.LetterN;
            string strRutaArchivo = string.Empty;
            string strNombreArchivo = string.Empty;
            string mensajeEmail = string.Empty;

            oModel.fechaActual = DateTime.Now.ToShortDateString();

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);
            try
            {

                #region GetDesactivatedContract_INT

                oModel.CodeTipification = ConfigurationManager.AppSettings("strCodigoTransRetCanServINT");

                if (ConfigurationManager.AppSettings("strFlagCancelacionServicioINT") == Claro.Constants.NumberZeroString)
                    resultado = Claro.Web.Logging.ExecuteMethod<bool>(() => { return GetDesactivatedContract_INT(oModel); });
                else
                    resultado = Claro.Web.Logging.ExecuteMethod<bool>(() => { return GetDesactivatedContract_LTE(oModel,ref mesj); });

                #endregion

                if (resultado)
                {
                    if (oModel.Aplica.Trim().ToUpper() == Claro.SIACU.Constants.Yes)
                    {
                        #region SAVE CASE

                        var objInteractionModel = new FixedIFIService.Interaction();
                        objInteractionModel = DatosCaso(oModel);
                        objInteractionModel.audit = audit;

                        var oPlantillaCaso = new FixedIFIService.CaseTemplate();
                        oPlantillaCaso = DatosPlantillaCaso(oModel);
                        oPlantillaCaso.audit = audit;

                        var objInsertCaso = InsertCaso(objInteractionModel, oPlantillaCaso, oModel, strEstadoForm);
                        GeneroCaso = Claro.Constants.NumberOneString;

                        Claro.ItemGeneric oItemIteraccion = GetInteractIDforCaseID(oModel.IdSession, objInsertCaso.CASO_ID);
                        vInteractionId = oItemIteraccion.Code;

                        Message = oItemIteraccion.Code2;

                        if (Message == Claro.SIACU.Constants.OK)
                        { vFlagInteraction = Claro.SIACU.Constants.True; }
                        else
                        { vFlagInteraction = Claro.SIACU.Constants.False; }
                        #endregion

                        #region SAVE AUDIT
                        ResultadoAudit = Auditoria(oModel);
                        #endregion
                    }
                    else
                    {
                        #region SAVE INTERACTION
                        List<string> strInteractionId = GrabaInteraccion(oModel);
                        Message = strInteractionId[0].ToString();
                        vFlagInteraction = strInteractionId[2].ToString();
                        vInteractionId = strInteractionId[3].ToString();
                        GeneroCaso = Claro.Constants.NumberZeroString;
                        #endregion

                        #region SAVE AUDIT
                        ResultadoAudit = Auditoria(oModel);
                        #endregion
                    }
                    oModel.GeneroCaso = Claro.Constants.NumberOneString;
                    if (Message != Claro.SIACU.Constants.NO_OK)
                    {
                        try
                        {
                            #region CONSTANCY PDF
                            Dictionary<string, object> oConstancyPDF = new Dictionary<string, object>();
                            oConstancyPDF = GetConstancyPDF(oModel.IdSession, vInteractionId, oModel);
                            strRutaArchivo = oModel.strFullPathPDF;
                            #endregion
                            if (oModel.Flag_Email)
                            {
                                #region SEND EMAIL
                                byte[] attachFile = null;

                                string strAdjunto = string.IsNullOrEmpty(strRutaArchivo) ? string.Empty : strRutaArchivo.Substring(strRutaArchivo.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                                if (DisplayFileFromServerSharedFile(oModel.IdSession, audit.transaction, strRutaArchivo, out attachFile))
                                {
                                    List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                                    if (attachFile != null)
                                    {
                                        AttachedFile objFile = new AttachedFile();

                                        objFile.file = attachFile;
                                        objFile.Name = strAdjunto;
                                        objFile.HeadBoard = strAdjunto;
                                        lstAdjuntos.Add(objFile);

                                        if (GetSendEmail2(oModel, lstAdjuntos) == Claro.SIACU.Constants.OK)
                                        {
                                            mensajeEmail = "Se enviará un correo de notificación.";
                                        }
                                        else
                                        {
                                            mensajeEmail = "Pero no se pudo procesar el envío del correo de notificación.";
                                        }
                                    }
                                    else
                                    {
                                        mensajeEmail = "Pero no se pudo procesar el envío del correo de notificación.";
                                    }

                                }
                                else
                                {
                                    mensajeEmail = "No se envió un correo de notificación.";
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

                        }
                        resultado = true;
                    }
                }
                else
                {
                    resultado = false;
                    Message = mesj;
                    Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, Claro.SIACU.Constants.gConstMsgNoSePProCanLi);
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Message = Claro.SIACU.Constants.gstrSaveTransaccionNoRetenido;
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }


            HELPERS.CommonServices.GenericItem itemGenMessag = new HELPERS.CommonServices.GenericItem();
            itemGenMessag.Code = resultado.ToString().ToUpper();
            itemGenMessag.Code2 = vFlagInteraction;
            itemGenMessag.Code3 = vInteractionId;
            itemGenMessag.Description = mensajeEmail;
            itemGenMessag.Condition = GeneroCaso;
            itemGenMessag.Description2 = strRutaArchivo;

            return Json(new { data = itemGenMessag, mssj = Message });
        }


        ///<summary>Metodo que permite desactivar el contrato</summary>
        ///<param name="oModel"></param>
        ///<returns>bool<returns>
        ///<remarks>GetDesactivatedContract_LTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool GetDesactivatedContract_LTE(Model.RetentionCancelServicesModel oModel ,ref string msj)
        {
            string mesje = "";
            string strReasonIFI = !(oModel.DesMotivos.ToUpper().Equals("ANULACIÓN")) ? ConfigurationManager.AppSettings("gConstkeyReasonRCSINT") : ConfigurationManager.AppSettings("gConstkeyReasonAnulacion");
            bool resultado = false;
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.IdSession);
            FixedIFIService.Customer objContratoReq = new FixedIFIService.Customer();
            try
            {

                objContratoReq.audit = audit;
                objContratoReq.audit.Session = oModel.IdSession;

                objContratoReq.ApplicationName = KEY.AppSettings("ApplicationNameIFI");
                objContratoReq.UserApplication = oModel.CurrentUser;

                objContratoReq.AreaPCs = oModel.AreaPCs;
                objContratoReq.Des_CAC = oModel.DescCacDac;
                objContratoReq.BillingCycle = oModel.BillingCycle;
                objContratoReq.CustomerID = oModel.CustomerId;
                objContratoReq.ContractID = oModel.ContractId;
                objContratoReq.Account = oModel.Account;
                objContratoReq.CodigoInteraction = string.Empty;
                objContratoReq.Cod_Motive = oModel.CodMotiveSot;
                objContratoReq.CodigoService = ConfigurationManager.AppSettings("gConstkeyCodSerRCSINT");
                objContratoReq.Date_Present = Convert.ToDate(oModel.fechaActual); //validar


                objContratoReq.FechaProgramacion = oModel.FechaProgramacion.Substring(6, 4) + "-" + oModel.FechaProgramacion.Substring(3, 2) + "-" + oModel.FechaProgramacion.Substring(0, 2);
                objContratoReq.DateProgrammingSot = oModel.DateProgrammingSot.Substring(6, 4) + "-" + oModel.DateProgrammingSot.Substring(3, 2) + "-" + oModel.DateProgrammingSot.Substring(0, 2);

                objContratoReq.FlagNdPcs = oModel.flagNdPcs;

                objContratoReq.FlagOccApadece = Convert.ToDouble(oModel.TotalInversion) > 0
                    ? Claro.Constants.NumberOneString
                    : Claro.Constants.NumberZeroString;

                objContratoReq.MailUserAplication = oModel.Email;
                objContratoReq.MontoFidelizacion = oModel.MontoFidelizacion;
                objContratoReq.MontoPCs = oModel.MontoPCs;
                objContratoReq.AmountPenalty = oModel.Reintegro;
                objContratoReq.MotivePCS = oModel.MotivePCS;
                objContratoReq.Msisdn = oModel.Msisdn;
                objContratoReq.DocumentNumber = oModel.DocumentNumber;
                objContratoReq.Observation = oModel.Observation;
                objContratoReq.Reason = !(oModel.DesMotivos.ToUpper().Equals("ANULACIÓN")) ? ConfigurationManager.AppSettings("gConstkeyReasonRCSINT") : ConfigurationManager.AppSettings("gConstkeyReasonAnulacion");
                objContratoReq.SubMotivePCS = oModel.SubMotivePCS;
                objContratoReq.CustomerType = oModel.TypeClient;
                objContratoReq.TypeServices = ConfigurationManager.AppSettings("gConstTipoIFI");

                objContratoReq.Trace = oModel.Trace;
                objContratoReq.Assessor = oModel.CurrentUser;

                objContratoReq.FringeHorary = oModel.vSchedule;

                resultado = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceFixed.GetDesactivatedContract_LTE(objContratoReq,ref mesje);
                });
                msj = mesje;
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, objContratoReq.audit.transaction, ex.InnerException.ToString());

                }
                msj = mesje;
            }

            return resultado;
        }


        ///<summary>Metodo que permite desactivar el contrato</summary>
        ///<param name="oModel"></param>
        ///<returns>bool<returns>
        ///<remarks>GetDesactivatedContract_INT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool GetDesactivatedContract_INT(Model.RetentionCancelServicesModel oModel)
        {

            bool resultado = false;
            WirelessIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<WirelessIFIService.AuditRequest>(oModel.IdSession);
            WirelessIFIService.Customer objContratoReq = new WirelessIFIService.Customer();
            try
            {


                objContratoReq.audit = audit;
                objContratoReq.audit.Session = oModel.IdSession;

                objContratoReq.ApplicationName = ConfigurationManager.AppSettings("gConstTipoINT");
                objContratoReq.UserApplication = oModel.CurrentUser;

                objContratoReq.AreaPCs = oModel.AreaPCs;
                objContratoReq.Des_CAC = oModel.DescCacDac;
                objContratoReq.BillingCycle = oModel.BillingCycle;
                objContratoReq.CustomerID = oModel.CustomerId;
                objContratoReq.ContractID = oModel.ContractId;
                objContratoReq.Account = oModel.Account;
                objContratoReq.CodigoInteraction = string.Empty;
                objContratoReq.Cod_Motive = oModel.CodMotiveSot;
                objContratoReq.CodigoService = ConfigurationManager.AppSettings("gConstkeyCodSerRCSINT");
                objContratoReq.Date_Present = Convert.ToDate(oModel.fechaActual);


                objContratoReq.FechaProgramacion = oModel.FechaProgramacion.Substring(6, 4) + "-" + oModel.FechaProgramacion.Substring(3, 2) + "-" + oModel.FechaProgramacion.Substring(0, 2);
                objContratoReq.DateProgrammingSot = oModel.DateProgrammingSot.Substring(6, 4) + "-" + oModel.DateProgrammingSot.Substring(3, 2) + "-" + oModel.DateProgrammingSot.Substring(0, 2);

                objContratoReq.FlagNdPcs = oModel.flagNdPcs;

                objContratoReq.FlagOccApadece = Convert.ToDouble(oModel.TotalInversion) > 0
                    ? Claro.Constants.NumberOneString
                    : Claro.Constants.NumberZeroString;

                objContratoReq.MailUserAplication = oModel.Email;
                objContratoReq.MontoFidelizacion = oModel.MontoFidelizacion;
                objContratoReq.MontoPCs = oModel.MontoPCs;
                objContratoReq.AmountPenalty = oModel.Reintegro;
                objContratoReq.MotivePCS = oModel.MotivePCS;
                objContratoReq.Msisdn = oModel.Msisdn;
                objContratoReq.DocumentNumber = oModel.DocumentNumber;
                objContratoReq.Observation = oModel.Observation;
                objContratoReq.Reason = ConfigurationManager.AppSettings("gConstkeyReasonRCSINT");
                objContratoReq.SubMotivePCS = oModel.SubMotivePCS;
                objContratoReq.CustomerType = oModel.TypeClient;
                objContratoReq.TypeServices = ConfigurationManager.AppSettings("gConstTipoServicioINT"); //gConstTipoHFCLTE

                objContratoReq.Trace = oModel.Trace;
                objContratoReq.Assessor = oModel.CurrentUser;

                objContratoReq.FringeHorary = oModel.vSchedule;

                resultado = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceWireless.GetDesactivatedContract_INT(objContratoReq);
                });
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {
                    Claro.Web.Logging.Error(oModel.IdSession, objContratoReq.audit.transaction, ex.InnerException.ToString());
                }
            }

            return resultado;
        }

        #endregion

        #region LOAD CONTROLS

        ///<summary>Metodo que permite listar las acciones de cancelación/retención</summary>
        ///<param name="strIdSession"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetListarAccionesRC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetListarAccionesRC(string strIdSession)
        {
            FixedIFIService.RetentionCancelServicesResponse objlistaAccionesResponse = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            FixedIFIService.RetentionCancelServicesRequest objlistaAccionesRequest =
                new FixedIFIService.RetentionCancelServicesRequest()
                {
                    audit = audit,
                    vNivel = Convert.ToInt(ConfigurationManager.AppSettings("gConstPerfil_AsesorCAC"))
                };


            try
            {
                objlistaAccionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedIFIService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetListarAccionesRC(objlistaAccionesRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objlistaAccionesRequest.audit.transaction, ex.Message);

            }


            Models.CommonServices objCommonServices = new Models.CommonServices();

            if (objlistaAccionesResponse != null && objlistaAccionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.GenericItem item in objlistaAccionesResponse.AccionTypes)
                {
                    Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM oCacDacTypeVM =
                        new Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM();

                    if (item.Cod_tipo_servicio == Claro.Constants.NumberThree)
                    {
                        oCacDacTypeVM.Code = item.Codigo;
                        oCacDacTypeVM.Description = item.Descripcion;
                        listCacDacTypes.Add(oCacDacTypeVM);
                    }
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }


            return Json(new { data = objCommonServices.CacDacTypes });
        }


        ///<summary>Metodo que permite listar los motivos de cancelación</summary>
        ///<param name="strIdSession"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetMotCancelacion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetMotCancelacion(string strIdSession)
        {
            int strTipoLista = Convert.ToInt(ConfigurationManager.AppSettings("strTipoListaMotCancelacionINT"));
            FixedIFIService.RetentionCancelServicesResponse objMotCancelacionesResponse = null;
            FixedIFIService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);

            FixedIFIService.RetentionCancelServicesRequest objRequest = new FixedIFIService.RetentionCancelServicesRequest();
            objRequest.vEstado = Claro.Constants.NumberOne;
            objRequest.vTipoLista = strTipoLista;
            objRequest.audit = audit;


            try
            {
                objMotCancelacionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedIFIService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetMotCancelacion(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, ex.Message);

            }


            Models.CommonServices objCommonServices = new Models.CommonServices();

            if (objMotCancelacionesResponse != null && objMotCancelacionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.GenericItem item in objMotCancelacionesResponse.AccionTypes)
                {
                    listCacDacTypes.Add(new Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion,
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }


            return Json(new { data = objCommonServices.CacDacTypes });
        }


        ///<summary>Metodo que permite listar los submotivos de cancelación</summary>
        ///<param name="strIdSession"></param>
        ///<param name="IdMotive"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetSubMotiveCancel</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetSubMotiveCancel(string strIdSession, int IdMotive)
        {
            FixedIFIService.RetentionCancelServicesResponse objSubMotiveResponse = new FixedIFIService.RetentionCancelServicesResponse();
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            FixedIFIService.RetentionCancelServicesRequest objSubMotiveRequest = new FixedIFIService.RetentionCancelServicesRequest()
            {
                audit = audit,
                vIdMotive = IdMotive
            };



            try
            {
                objSubMotiveResponse = Claro.Web.Logging.ExecuteMethod<FixedIFIService.RetentionCancelServicesResponse>(() =>
                {
                    return _oServiceFixed.GetSubMotiveCancel(objSubMotiveRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objSubMotiveRequest.audit.transaction, ex.Message);

            }


            Models.CommonServices objCommonServices = new Models.CommonServices();

            if (objSubMotiveResponse != null && objSubMotiveResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.GenericItem item in objSubMotiveResponse.AccionTypes)
                {
                    listCacDacTypes.Add(new Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion,
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }


            return Json(new { data = objCommonServices.CacDacTypes });
        }


        ///<summary>Metodo que obtiene los motivos</summary>
        ///<param name="strIdSession"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetMotive_SOT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetMotive_SOT(string strIdSession)
        {

            CommonIFIService.MotiveSotResponseCommon objMotiveSotResponseCommon;
            CommonIFIService.AuditRequest audit =
                           App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);



            CommonIFIService.MotiveSotRequestCommon objMotiveSotRequestCommon =
            new CommonIFIService.MotiveSotRequestCommon()
            {
                audit = audit
            };

            try
            {
                objMotiveSotResponseCommon =
                    Claro.Web.Logging.ExecuteMethod<CommonIFIService.MotiveSotResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetMotiveSot(objMotiveSotRequestCommon);

                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objMotiveSotRequestCommon.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            Models.CommonServices objCommonServices = new Models.CommonServices();

            if (objMotiveSotResponseCommon != null && objMotiveSotResponseCommon.getMotiveSot != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM> listMotiveSot =
                    new List<Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (CommonIFIService.ListItem item in objMotiveSotResponseCommon.getMotiveSot)
                {
                    listMotiveSot.Add(new Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description,
                    });
                }
                objCommonServices.CacDacTypes = listMotiveSot;
            }

            return Json(new { data = objCommonServices.CacDacTypes });



        }


        ///<summary>Metodo que obtiene los motivos por tipo de trabajo</summary>
        ///<param name="strIdSession"></param>
        ///<param name="IdTipoTrabajo"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetMotiveSOTByTypeJob</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetMotiveSOTByTypeJob(string strIdSession, int IdTipoTrabajo)
        {
            var audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            var objResponse = new FixedIFIService.MotiveSOTByTypeJobResponse();
            var objRequest = new FixedIFIService.MotiveSOTByTypeJobRequest()
            {
                audit = audit,
                tipTra = IdTipoTrabajo
            };


            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedIFIService.MotiveSOTByTypeJobResponse>(() =>
                {
                    return _oServiceFixed.GetMotiveSOTByTypeJob(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.audit.transaction, ex.Message);
            }


            return Json(new { data = objResponse.List });
        }

        #endregion

        #region VALIDATE ETA


        ///<summary>Metodo que permite validar ETA</summary>
        ///<param name="strIdSession"></param>
        ///<param name="vJobTypes"></param>
        ///<param name="vPlansId"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetValidateETA</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetValidateETA(string strIdSession, string vJobTypes, string vPlansId)
        {
            string strTipoOrden = string.Empty;

            string strorigen = string.Empty;
            string strIdplano = string.Empty;
            string strUbigeo = string.Empty;
            string strTipoServ = string.Empty;




            strorigen = KEY.AppSettings("gConstHFCOrigen");
            strIdplano = vPlansId;
            strTipoServ = KEY.AppSettings("gConstHFCTipoServicio");


            ETAFlowRequestHfc objETAFlowRequestHfc = new ETAFlowRequestHfc();
            ETAFlowResponseHfc objETAFlowResponseHfc;
            objETAFlowRequestHfc.audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            objETAFlowRequestHfc.an_tipsrv = strTipoServ;
            objETAFlowRequestHfc.an_tiptra = Convert.ToInt(vJobTypes);
            objETAFlowRequestHfc.as_origen = strorigen;
            objETAFlowRequestHfc.av_idplano = strIdplano;
            objETAFlowRequestHfc.av_ubigeo = strUbigeo;
            OrderTypesResponseHfc objOrderTypesResponse = new OrderTypesResponseHfc();
            OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc();
            objOrderTypesRequest.audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.GenericItem objGenericItem = new Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.GenericItem();


            try
            {

                #region "Valida ETA"

                if (vJobTypes != null)
                {

                    if (vJobTypes.IndexOf(".|") == Convert.ToInt(Claro.Constants.NumberOneNegativeString))
                    {
                        objOrderTypesRequest.vIdtiptra = vJobTypes;
                    }
                    else
                    {
                        objOrderTypesRequest.vIdtiptra = vJobTypes.Substring(0, vJobTypes.Length - 2);
                    }

                    objOrderTypesResponse = Claro.Web.Logging.ExecuteMethod<OrderTypesResponseHfc>(() =>
                    {
                        return new FixedIFIServiceClient().GetOrderType(objOrderTypesRequest);
                    });

                }

                if (objOrderTypesResponse.ordertypes == null)
                {
                    strTipoOrden = Claro.Constants.NumberOneNegativeString;
                }
                else
                {
                    if (objOrderTypesResponse.ordertypes.Count() == 0)
                    {
                        strTipoOrden = Claro.Constants.NumberOneNegativeString;
                    }
                    else
                    {
                        strTipoOrden = objOrderTypesResponse.ordertypes[0].VALOR;
                    }
                }





                objETAFlowResponseHfc = Claro.Web.Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
                {
                    return new FixedIFIServiceClient().ETAFlowValidate(objETAFlowRequestHfc);
                });




                objGenericItem.Descripcion = string.Empty;
                objGenericItem.Codigo = objETAFlowResponseHfc.ETAFlow.an_indica.ToString();
                objGenericItem.Codigo2 = objETAFlowResponseHfc.ETAFlow.as_codzona + "|" + strIdplano + "|" + strTipoOrden;


                switch (objETAFlowResponseHfc.ETAFlow.an_indica)
                {
                    case -1:
                        objGenericItem.Descripcion = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoExistePlano", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        break;
                    case -2:
                        objGenericItem.Descripcion = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoExisteUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        break;
                    case -3:
                        objGenericItem.Descripcion = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoExistePlanoUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        break;
                    case -4:
                        objGenericItem.Descripcion = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoExisteTipoTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        break;
                    case -5:
                        objGenericItem.Descripcion = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoExisteTipoServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        break;
                    case 1:
                        objGenericItem.Descripcion = Claro.SIACU.Constants.OK;
                        break;
                    case 0:
                        objGenericItem.Descripcion = Claro.SIACU.Constants.NO_OK;
                        break;
                }
                #endregion


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, objOrderTypesRequest.audit.transaction, ex.Message);
            }


            return Json(new { data = objGenericItem });

        }


        ///<summary>Metodo que permite obtener la hora de la agenda ETA</summary>
        ///<param name="strIdSession"></param>
        ///<param name="cboHorario"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>ObtenerHoraAgendaETA</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        private string ObtenerHoraAgendaETA(string strIdSession, string cboHorario)
        {
            ArrayList lstGenericItem = new ArrayList();
            string strHora = string.Empty;
            var audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            strHora = Claro.Utils.GetValueFromConfigFileIFI("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCPOSTConfig"));
            try
            {
                lstGenericItem = App_Code.Common.GetXMLList("ListaFranjasHorariasETA");

                foreach (Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.GenericItem item in lstGenericItem)
                {
                    string[] Aux = cboHorario.Split('+');
                    if (Aux[0] == "")
                    {
                        strHora = item.Codigo2;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, ex.Message);
            }

            return strHora;
        }


        #region CONSTANCY PDF - SEND EMAIL


        ///<summary>Metodo que permite generar constancia en formato PDF</summary>summary>
        ///<param name="strIdSession"></param>
        ///<param name="strIdInteraction"></param>
        ///<param name="oModel"></param>
        ///<returns>Dictionary<returns>
        ///<remarks>GetConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.RetentionCancelServicesModel oModel)
        {

            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            string strTerminacionPdf = ConfigurationManager.AppSettings("strTerminacionPDF").ToString();
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;
            string submotivo = oModel.DesSubMotivo;
            InteractionServiceRequestHfc objInteractionServiceRequest = new InteractionServiceRequestHfc();
            ParametersGeneratePDF parameters = new ParametersGeneratePDF();

            try
            {
                strTexto = Claro.Utils.GetValueFromConfigFileIFI("strMsgRetencionCancelConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

                if (oModel.Accion.ToUpper() == Claro.Constants.LetterR)
                {

                    parameters = new ParametersGeneratePDF();
                    parameters.StrCentroAtencionArea = oModel.DescCacDac;
                    parameters.StrNroServicio = oModel.NroCelular;
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrIdInteraccion = strInteraccionId;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.StrSegmento = oModel.Segmento;
                    parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                    parameters.strtransaccion = "Solicitud de Cancelación";
                    parameters.StrResultado = oModel.Accion.ToUpper().Equals("R") ? "Retenido" : "Cancelado";
                    parameters.strMotivo = oModel.DesMotivos;
                    if (submotivo == "Seleccionar" || submotivo == String.Empty)
                    {
                        parameters.StrSubMotivoCancel = String.Empty;
                    }
                    else
                    {
                        parameters.StrSubMotivoCancel = submotivo;
                    }                   
                    parameters.StrAccionIFI = oModel.DesAccion;
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoRetenidocancelacionINT");
                    parameters.strMontoReintegro = oModel.Reintegro;
                    parameters.strEnvioCorreo = oModel.Flag_Email ? "SÍ" : "NO";
                    if (parameters.strEnvioCorreo == "SÍ")
                    {
                        parameters.strCorreoCliente = oModel.Email;
                    }                   
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrNombreAgenteUsuario = oModel.Agent;
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoINT");

                }
                else
                {

                    parameters = new ParametersGeneratePDF();

                    //made13
                    parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("strNombreArchivoNoRetenidocancelacionINT"); 
                    parameters.StrCentroAtencionArea = oModel.DescCacDac;
                    parameters.StrNroServicio = oModel.NroCelular;
                    parameters.StrTitularCliente = oModel.NameComplet;
                    parameters.StrIdInteraccion = strInteraccionId;
                    parameters.StrRepresLegal = oModel.RepresentLegal;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.StrSegmento = oModel.Segmento;
                    parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                    parameters.strtransaccion = "Solicitud de Cancelación";
                    parameters.StrResultado = oModel.Accion.ToUpper().Equals("R") ? "Retenido" : "Cancelado";
                    parameters.strMotivo = oModel.DesMotivos;
                    if (submotivo == "Seleccionar" || submotivo == String.Empty)
                    {
                        parameters.StrSubMotivoCancel = String.Empty;
                    }
                    else
                    {
                        parameters.StrSubMotivoCancel = submotivo;
                    }  
                    parameters.StrAccionIFI = oModel.DesAccion;
                    parameters.StrFechaCancel = oModel.DateProgrammingSot;
                    parameters.strMontoReintegro = oModel.Reintegro;
                    parameters.strEnvioCorreo = oModel.Flag_Email ? "SÍ" : "NO";
                    if (parameters.strEnvioCorreo == "SÍ")
                    {
                        parameters.strCorreoCliente = oModel.Email;
                    }  
                    parameters.StrCodUsuario = oModel.CurrentUser;
                    parameters.StrNombreAgenteUsuario = oModel.Agent;
                    parameters.strFirmaDigital = ConfigurationManager.AppSettings("strIsFirmaDigital");
                    parameters.StrCasoInter = strInteraccionId;
                    parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionNoRetenidoINT");
                    //made13


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
                Claro.Web.Logging.Error(oModel.IdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }
            return listResponse;
        }


        ///<summary>Metodo que permite enviar correo electrónico adjuntando archivos</summary>
        ///<param name="strInteraccionId"></param>
        ///<param name="strAdjunto"></param>
        ///<param name="model"></param>
        ///<param name="strNombreArchivoPDF"></param>
        ///<param name="attachFile"></param>
        ///<returns>string<returns>
        ///<remarks>GetSendEmail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public string GetSendEmail(string strInteraccionId, string strAdjunto, Model.RetentionCancelServicesModel model, string strNombreArchivoPDF, byte[] attachFile)
        {
            string strResul = string.Empty;
            CommonIFIService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.IdSession);
            CommonIFIService.SendEmailRequestCommon objGetSendEmailRequest;
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.Destinatarios;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailRetencionCancelacion",
                    ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                string OpcionRetenido = string.Empty;

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
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailRetencionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                }
                else
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailCancelacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
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

                CommonIFIService.SendEmailResponseCommon objGetSendEmailResponse = new CommonIFIService.SendEmailResponseCommon();
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
                objGetSendEmailResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailResponseCommon>(() => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); });

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
                Claro.Web.Logging.Info(model.IdSession, AuditRequest.transaction, ex.Message);

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
        public string GetSendEmail2(Model.RetentionCancelServicesModel model, List<CommonIFIService.AttachedFile> lstFileAdjunto)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.IdSession);

            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));

            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.Destinatarios;
                string strAsunto = Claro.Utils.GetValueFromConfigFileIFI("strAsuntoEmailRetencionCancelacion",
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
                if (model.Accion == Claro.Constants.LetterR)
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailRetencionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
                }
                else
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + Claro.Utils.GetValueFromConfigFileIFI("strMsgEmailCancelacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg")) + "</td></tr>";
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
                    return _oServiceCommon.SendEmailWithBase64(objSendEmailWithBase64Request);
                });
                if (objSendEmailWithBase64Response != null && objSendEmailWithBase64Response.ResponseCode == Claro.Constants.NumberZeroString)
                {
                    strResul = Claro.SIACU.Constants.OK;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.IdSession, objAudit.transaction, ex.Message);
            }
            return strResul;



        }

        #endregion

        #region REGISTER LOGS -TOOLS


        ///<summary>Metodo que permite calcular los días hábiles</summary>
        ///<param name="NroDias"></param>
        ///<param name="PostIFIService"></param>
        ///<param name="audit"></param>
        ///<returns>string<returns>
        ///<remarks>CalculaDiasHabiles</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public string CalculaDiasHabiles(string NroDias, PostIFIService.AuditRequest audit)
        {
            int intDiasHabiles = Convert.ToInt(NroDias);
            DateTime dtFechaSum = DateTime.Now;
            string ResultadoFecha = string.Empty;
            string strListaNoHabiles = ConfigurationManager.AppSettings("gListaDiasNoHabiles");
            int intCantDias = Claro.Constants.NumberTwenty;
            int intCont = 0;



            try
            {

                for (int i = 0; i < intCantDias; i++)
                {
                    if (strListaNoHabiles.IndexOf(DateTime.Now.AddDays(i).DayOfWeek.ToString().ToUpper().Trim()) == -1)
                    {
                        dtFechaSum = DateTime.Now.AddDays(i);
                        intCont += 1;

                    }
                    if (intCont == intDiasHabiles)
                    {
                        break;
                    }
                }

                ResultadoFecha = string.Format(dtFechaSum.ToString("dd/MM/yyyy"));
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(audit.Session, audit.transaction, ex.Message);
            }


            return ResultadoFecha;



        }


        ///<summary>Metodo que guardar la auditoría</summary>
        ///<param name="oModel"></param>
        ///<returns>string<returns>
        ///<remarks>Auditoria</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool Auditoria(Model.RetentionCancelServicesModel oModel)
        {
            bool FlatResultado = false;
            string strCodigoAuditoria = KEY.AppSettings("strCodTranRetCancTempINT");
            string strusuarioAutoriza = oModel.CurrentUser;  // validar
            string strAmmount = Claro.Constants.NumberZeroString;
            string[,] strDetails = new string[8, 3];
            string strService = KEY.AppSettings("gConstEvtServicio");


            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.IdSession);


            string strIpCliente = Claro.Utils.CheckStr(HttpContext.Request.UserHostAddress);
            string strIPServidor = App_Code.Common.GetApplicationIp();
            string strNombreServidor = App_Code.Common.GetApplicationName();
            string strCuentaUsuario = oModel.CurrentUser;
            string strPhone = oModel.Telephone;
            string strNameClient = oModel.NameComplet;

            strDetails[0, 0] = "Número Claro";
            strDetails[0, 1] = oModel.NroCelular;
            strDetails[0, 2] = "Número Claro";

            strDetails[1, 0] = "Código Contrato";
            strDetails[1, 1] = oModel.ContractId;
            strDetails[1, 2] = "Número Claro";

            strDetails[2, 0] = "Transacción Realizada";
            strDetails[2, 1] = (oModel.Accion == "R" ? "Retenido" : "Cancelado");
            strDetails[2, 2] = "Transacción Realizada";

            strDetails[3, 0] = "Motivo Cancelación";
            strDetails[3, 1] = oModel.DesMotivos;
            strDetails[3, 2] = "Motivo Cancelación";

            strDetails[4, 0] = "Acción";
            strDetails[4, 1] = oModel.DesAccion;
            strDetails[4, 2] = "Acción";

            strDetails[5, 0] = "Autoriza Acción Segundo Nivel";
            strDetails[5, 1] = (oModel.hidSupJef == "S" ? strusuarioAutoriza : string.Empty);
            strDetails[5, 2] = "Autoriza Acción Segundo Nivel";

            strDetails[6, 0] = "Fecha Programada para la Cancelación";
            strDetails[6, 1] = string.Empty;
            strDetails[6, 2] = "Fecha Programada para la Cancelación";

            strDetails[7, 0] = "CAC/DAC";
            strDetails[7, 1] = oModel.DescCacDac;
            strDetails[7, 2] = "CAC/DAC";

            var sbTexto = new System.Text.StringBuilder();

            int count = ((strDetails.Length / 4) - 1);
            var sbText = new System.Text.StringBuilder();
            for (int i = 0; i < count; i++)
            {
                if (strDetails.GetValue(i, 1) != null && strDetails.GetValue(i, 2) != null)
                {
                    sbText.Append(" " + strDetails.GetValue(i, 1) + " : ");
                    sbText.Append(strDetails.GetValue(i, 2));
                }
            }

            try
            {
                CommonIFIService.SaveAuditResponseCommon objResponse = null;
                CommonIFIService.SaveAuditRequestCommon objRequest = new CommonIFIService.SaveAuditRequestCommon()
                {
                    vCuentaUsuario = strCuentaUsuario,
                    vIpCliente = strIpCliente,
                    vIpServidor = strIPServidor,
                    vMonto = strAmmount,
                    vNombreCliente = strNameClient,
                    vNombreServidor = strNombreServidor,
                    vServicio = strService,
                    vTelefono = strPhone,
                    vTexto = sbText.ToString(),
                    vTransaccion = strCodigoAuditoria,
                    audit = audit
                };


                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SaveAuditResponseCommon>(() =>
                {
                    return _oServiceCommon.SaveAudit(objRequest);
                });
                FlatResultado = objResponse.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }


            return FlatResultado;
        }

        #endregion
        #endregion
    }
}
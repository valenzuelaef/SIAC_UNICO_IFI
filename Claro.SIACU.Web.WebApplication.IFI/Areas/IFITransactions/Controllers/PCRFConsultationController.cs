using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using System.Collections;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class PCRFConsultationController : Controller
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient ServiceFixed = new FixedIFIService.FixedIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient oServicePostpaid = new PostIFIService.PostIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient oServiceCommon = new CommonIFIService.CommonIFIServiceClient();


        ///<summary>Metodo que retorna la vista del controlador de consultas PCRF</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>PCRFConsultation</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public ActionResult PCRFConsultation()
        {
            return View();
        }


        ///<summary>Metodo que se ejecuta al cargar la página</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult PageLoad(PCRFConsultationModel oModel)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            PCRFConnectorRequest objRequest = new PCRFConnectorRequest()
            {
                audit = audit,
                strAccountId = oModel.strAccountIdSession
            };
            PCRFConnectorResponse objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
            {
                return ServiceFixed.ObtenerTelefonosClienteLTE(objRequest);
            });
            oModel.strTelephone = objResponse.strTelefonoLTE;
            if(oModel.strIMSISession == null)
            {
                DataLineResponsePostPaid oDataLinea = new DataLineResponsePostPaid();

                DataLineRequestPostPaid objRequestDataLine = new DataLineRequestPostPaid()
                {
                    ContractID = oModel.strAccountIdSession,
                    audit = new PostIFIService.AuditRequest()
                    {
                        applicationName = audit.applicationName,
                        Session = audit.Session,
                        ipAddress = audit.ipAddress,
                        transaction = audit.transaction,
                        userName = audit.userName
                    }
                };

                oDataLinea = oServicePostpaid.GetDataLine(objRequestDataLine);
                oModel.strIMSISession = oDataLinea.DataLine.NumIMSI;
            }
            BuscarPCRF(oModel);
            return Json(new { data = oModel });
        }


        ///<summary>Metodo que se ejecuta al cargar la página</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public void BuscarPCRF( PCRFConsultationModel oModel)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            string strMsjErrorConsultarPCRF = KEY.AppSettings("strMsjErrorConsultarPCRF");
            oModel.blnMensaje = false;
            oModel.blnAudit = false;
            try
            {
                
                ConsultarPCRFQuotaIniCosum(oModel);
                ConsultarPerfilSuscriptor(oModel);
                ConsultarPCRFPaquetesAdic(oModel);//INI - RF-04 - Evalenzs

                if(oModel.blnQuotaIniCosum)
                {
                    decimal decTotalTraficoDatosGiga = (Convert.ToDecimal(oModel.strTotalVolumeUsage)/1024)/1024;
                    if (Convert.ToDecimal(oModel.strTotalVolumeUsage) < Convert.ToDecimal(oModel.strTotalVolumeLimit))
                    {
                        oModel.strVelocidadDegradada = KEY.AppSettings("strIndicadorNO_PCRF");
                        oModel.blnVelocidadDegradada_Color = false;
                    }
                    else if (Convert.ToDecimal(oModel.strTotalVolumeUsage) >= Convert.ToDecimal(oModel.strTotalVolumeLimit))
                    {
                        oModel.strVelocidadDegradada = KEY.AppSettings("strIndicadorSI_PCRF");
                        oModel.blnVelocidadDegradada_Color = true;

                        if (oModel.bBono)
                        {
                            oModel.strVelocidadDegradada = KEY.AppSettings("strIndicadorNO_PCRF");
                            oModel.blnVelocidadDegradada_Color = false;
                        }
                    }
                    oModel.strDatosConsumidos = Math.Round(decTotalTraficoDatosGiga, 2) + "" + KEY.AppSettings("strIndicadorValorExpresado_PCRF");
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Velocidad Degradada: " + oModel.strVelocidadDegradada);
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Datos Consumidos: " + oModel.strDatosConsumidos);
                 
                    if(oModel.blnPerfilSuscriptor)
                    {
                         oModel.blnAudit = InsertarAuditoria(oModel);
                    }
                    else 
                    {
                        oModel.strMensaje = strMsjErrorConsultarPCRF;
                        oModel.blnMensaje = true;
               
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
                oModel.strMensaje = strMsjErrorConsultarPCRF;
                oModel.blnMensaje = true;
                oModel.blnAudit = InsertarAuditoria(oModel);
            }
         
        }


        ///<summary>Metodo que permite consultar la cuota del suscriptor</summary>
        ///<param name="oModel"></param>
        ///<remarks>ConsultarPCRFQuotaIniCosum</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public void ConsultarPCRFQuotaIniCosum(PCRFConsultationModel oModel)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : ConsultarPCRFQuotaIniCosum");
           
            try
            {
                PCRFConnectorRequest objRequest = new PCRFConnectorRequest()
                {
                    audit = audit,
                    strLinea = oModel.strTelephone
                };

                PCRFConnectorResponse objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return ServiceFixed.ConsultarPCRFSuscriber_Quota(objRequest);
                });



                oModel.bBono = objResponse.bBono;
                if(objResponse != null && objResponse.listSuscriberQuota != null)
                {
                    oModel.strTotalVolumeUsage = objResponse.listSuscriberQuota.QTACONSUMPTION;
                    oModel.strTotalVolumeLimit = objResponse.listSuscriberQuota.QTAVALUE;
                    oModel.strPerfilPCRF = KEY.AppSettings("str_RTDD");
                    oModel.blnQuotaIniCosum = true;
                  
                }
                else
                {
                    oModel.strMensaje = KEY.AppSettings("strMsjErrorConsultarPCRF");
                    oModel.blnMensaje = true;
                    oModel.blnQuotaIniCosum = false;
                }

            }
            catch(Exception ex)
            {
                oModel.blnQuotaIniCosum = false;
                oModel.strMensaje = KEY.AppSettings("strMsjErrorConsultarSubscriberQuota_Pcrf");
                oModel.blnMensaje = true;
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
                Claro.Web.Logging.Error(audit.Session, audit.transaction, oModel.strMensaje);
            }
           
        }


        ///<summary>Metodo que permite consultar el perfil del suscriptor</summary>
        ///<param name="oModel"></param>
        ///<remarks>ConsultarPerfilSuscriptor</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public void ConsultarPerfilSuscriptor(PCRFConsultationModel oModel)
        {
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : ConsultarPerfilSuscriptor");
            ArrayList arrErrores = new ArrayList();
            try
            {
                PCRFConnectorRequest objRequest = new PCRFConnectorRequest()
                {
                    audit = audit,
                    strLinea = oModel.strTelephone
                };
                PCRFConnectorResponse objResponse = Claro.Web.Logging.ExecuteMethod(audit.Session, audit.transaction, () =>
                {
                    return ServiceFixed.ConsultarPCRFSuscriber(objRequest);
                });
                if (objResponse != null && objResponse.listSuscriber != null)
                {
                    oModel.strIMSI = objResponse.listSuscriber.USRIMSI;
                    oModel.strLineaAsociada = oModel.strTelephone;
                    string strCYCLEDATE;
                    string strCorrecto = KEY.AppSettings("strIndicadorCorrecto_PCRF");
                    string strIncorrecto = KEY.AppSettings("strIndicadorIncorrecto_PCRF");
                    int USRBILLCYCLEDATE;
                    USRBILLCYCLEDATE = Int32.Parse(objResponse.listSuscriber.USRBILLCYCLEDATE.ToString());
                    if (USRBILLCYCLEDATE < 10)
                    {
                        strCYCLEDATE = String.Concat(Constants.ZeroNumber, objResponse.listSuscriber.USRBILLCYCLEDATE);
                    }
                    else
                    {
                        strCYCLEDATE = objResponse.listSuscriber.USRBILLCYCLEDATE;
                    }

                    oModel.strCicloFacturacionPCRF = strCYCLEDATE;
                    oModel.strTipoServicio = objResponse.listSuscriber.USRPAIDTYPE;
                    oModel.strAccountId = objResponse.listSuscriber.USRMSISDN;
                    oModel.blnPerfilSuscriptor = true;
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "**INICIO: VALIDACIONES CONSULTA PCRF***");
                    oModel.strLineaAsociada_Validacion = strCorrecto;
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de Línea Asociada: " + oModel.strLineaAsociada_Validacion);
                      
                    if(oModel.strIMSI == oModel.strIMSISession)
                    {
                        oModel.strIMSI_Validacion = strCorrecto;
                        oModel.blnIMSI_Validacion_Color = false;
                    }
                    else
                    {
                        oModel.strIMSI_Validacion = strIncorrecto;
                        oModel.blnIMSI_Validacion_Color = true;
                        arrErrores.Add(KEY.AppSettings("strIndicadorLabelIMSI"));
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de IMSI: " + oModel.strIMSI_Validacion);
                    if (!String.IsNullOrEmpty(oModel.strPerfilPCRF))
                    {
                        oModel.strPerfilPCRF_Validacion = strCorrecto;
                        oModel.blnPerfilPCRF_Validacion_Color = false;
                    }
                    else
                    {
                        oModel.strPerfilPCRF_Validacion = strIncorrecto;
                        oModel.blnPerfilPCRF_Validacion_Color = true;
                        arrErrores.Add(KEY.AppSettings("strIndicadorLabelPerfilPCRF"));
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de Perfil PCRF: " + oModel.strPerfilPCRF_Validacion);
                    if(oModel.strCicloFacturacionPCRF == oModel.strCicloFacturacionSession)
                    {
                        oModel.strCicloFacturacionPCRF_Validacion = strCorrecto;
                        oModel.blnCicloFacturacionPCRF_Validacion_Color = false;
                    }
                    else
                    {
                        oModel.strCicloFacturacionPCRF_Validacion = strIncorrecto;
                        oModel.blnCicloFacturacionPCRF_Validacion_Color = true;
                        arrErrores.Add(KEY.AppSettings("strIndicadorLabelCicloFacturacion"));
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de Ciclo de Facturación: " + oModel.strCicloFacturacionPCRF_Validacion);
                    if (oModel.strTipoServicio == KEY.AppSettings("strIndicadorPostpaid_PCRF"))
                    {
                        oModel.strTipoServicio_Validacion = strCorrecto;
                        oModel.blnTipoServicio_Validacion_Color = false;
                    }
                    else
                    {
                        oModel.strTipoServicio_Validacion = strIncorrecto;
                        oModel.blnTipoServicio_Validacion_Color = true;
                        arrErrores.Add(KEY.AppSettings("strIndicadorLabelTipoServicio"));
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de Tipo de Servicio: " + oModel.strTipoServicio_Validacion);
                    if(oModel.strIMSI_Validacion == strCorrecto && oModel.strLineaAsociada_Validacion == strCorrecto)
                    {
                        oModel.strSPC_Validacion = strCorrecto;
                        oModel.blnSPC_Validacion_Color = false;
                    }
                    else
                    {
                        oModel.strSPC_Validacion = strIncorrecto;
                        oModel.blnSPC_Validacion_Color = true;
                        arrErrores.Add(KEY.AppSettings("strIndicadorLabelSPC"));
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de SPC (Subscriber Profile Configuration): " + oModel.strSPC_Validacion);
                    if ((oModel.strPerfilPCRF_Validacion == strCorrecto && oModel.strCicloFacturacionPCRF_Validacion == strCorrecto) || !String.IsNullOrEmpty(oModel.strAccountId))
                    {
                        oModel.strSI_Validacion = strCorrecto;
                        oModel.blnSI_Validacion_Color = false;
                    }
                    else
                    {
                        oModel.strSI_Validacion = strIncorrecto;
                        oModel.blnSI_Validacion_Color = true;
                        arrErrores.Add(KEY.AppSettings("strIndicadorLabelSI"));
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Validación de SI (Subscriber Information): " + oModel.strSI_Validacion);
                    if(arrErrores.Count > 0)
                    {
                       oModel.strObservacion = String.Format(KEY.AppSettings("strMsjObservacion_PCRF"), String.Join(",",arrErrores.ToArray()).ToString());
                    }
                    else
                    {
                        oModel.strObservacion = String.Empty;
                    }

                }
                else
                {
                    oModel.strMensaje = KEY.AppSettings("strMsjErrorConsultarPCRF");
                    oModel.blnMensaje = true;
                    oModel.blnPerfilSuscriptor = false;
                }

            }
            catch(Exception ex)
            {
                oModel.blnPerfilSuscriptor = false;
                oModel.strMensaje = KEY.AppSettings("strMsjErrorConsultarSubscriber_Pcrf");
                oModel.blnMensaje = true;
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
                Claro.Web.Logging.Error(audit.Session, audit.transaction, oModel.strMensaje);
            }
       
        }

        //INI - RF-04 - Evalenzs
        ///<summary>Metodo que permite consultar PAquetes Adicioneles</summary>
        ///<param name="oModel"></param>
        ///<remarks>ConsultarPCRFPaquetesAdic</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor></CreadoPor></item>
        /// <item><FecCrea>25/07/2020.</FecCrea></item></list>
        public void ConsultarPCRFPaquetesAdic(PCRFConsultationModel oModel)
        {
            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "Inicio Método : ConsultarPCRFPaquetesAdic");
            ArrayList arrErrores = new ArrayList();
            try
            {
                FixedIFIService.HeaderRequest oHeaderRequest = new FixedIFIService.HeaderRequest
                {
                    consumer = ConfigurationManager.AppSettings("consumer"),
                    country = ConfigurationManager.AppSettings("country"),
                    dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                    language = ConfigurationManager.AppSettings("language"),
                    modulo = ConfigurationManager.AppSettings("modulo"),
                    msgType = ConfigurationManager.AppSettings("msgType"),
                    operation = ConfigurationManager.AppSettings("strOperatioPCRFPaqAdicionales"),//Validar
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = ConfigurationManager.AppSettings("system"),
                    timestamp = DateTime.Now.ToString("o"),
                    userId = App_Code.Common.CurrentUser,
                    wsIp = App_Code.Common.GetApplicationIp()
                };

                PCRFPaquetesAdicConsultarRequest objconsultarRequest = new PCRFPaquetesAdicConsultarRequest
                {
                    msisdn = oModel.strTelephone,
                    flagHistorico="0",
                    cantDias = "",
                };

                PCRFPaquetesAdicBodyRequest oBodyRequest = new PCRFPaquetesAdicBodyRequest
                {
                    consultarRequest = objconsultarRequest
                 };

                PCRFPaquetesAdicHeaderRequest oPCRFPaquetesAdicHeader = new PCRFPaquetesAdicHeaderRequest
                {
                    HeaderRequest = oHeaderRequest
                };

                 PCRFPaquetesAdicMessageRequest oMessageRequest = new PCRFPaquetesAdicMessageRequest
                {
                    Header = oPCRFPaquetesAdicHeader,
                    Body = oBodyRequest
                };
                PCRFPaquetesAdicRequest objRequest = new PCRFPaquetesAdicRequest()
                {
                       MessageRequest = oMessageRequest,
                    audit = oAuditRequest
                };


                PCRFPaquetesAdicBodyResponse objResponse = Claro.Web.Logging.ExecuteMethod(oAuditRequest.Session, oAuditRequest.transaction, () =>
                {
                    return ServiceFixed.ConsultarPCRFPaquetesAdic(objRequest);
                });   

                if (objResponse.consultarPaquetesResponseType.responseStatus.codigoRespuesta == "0")
                {
                    oModel.strUltimoPaqCompradoAdic = objResponse.consultarPaquetesResponseType.responseData.dataPCRF.paqueteComprado;
                    oModel.strMotDegradacionAdic = objResponse.consultarPaquetesResponseType.responseData.dataPCRF.motivoDegradacion;
                    oModel.strVelDegradadaAdic = objResponse.consultarPaquetesResponseType.responseData.dataPCRF.velocidadDegradada;
                    oModel.strDatosConsumidosAdic = objResponse.consultarPaquetesResponseType.responseData.dataPCRF.datosConsumidos;
                    oModel.bpcrfcConsulta = true;

                      if (objResponse.consultarPaquetesResponseType.responseData.dataPCRF == null) oModel.bpcrfcConsulta = false;
                  
                }
                else
                {
                    oModel.bpcrfcConsulta = false;
                    oModel.strUltimoPaqCompradoAdic = "-";
                    oModel.strMotDegradacionAdic = "-";
                    oModel.strVelDegradadaAdic =  "-";
                    oModel.strDatosConsumidosAdic =  "-";
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oAuditRequest.Session, oAuditRequest.transaction, ex.Message.ToString());
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "Inicio Fin : ConsultarPCRFPaquetesAdic");

        }
        //FIN - RF-04 - Evalenzs

        ///<summary>Metodo que permite insertar la auditoría</summary>
        ///<param name="oModel"></param>
        ///<returns>bool</returns>
        ///<remarks>InsertarAuditoria</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public bool InsertarAuditoria(PCRFConsultationModel oModel)
        {
            bool FlatResultado = false;
            string strCodigoAuditoria = KEY.AppSettings("strCodTransaccionConsultaPCRF");
            string strusuarioAutoriza = oModel.strCurrentUser;  
            string strAmmount = Claro.Constants.NumberZeroString;
            string strTexto = oModel.strMensaje;
            string strService = KEY.AppSettings("gConstEvtServicio");
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : InsertarAuditoria");
           
            string strIpCliente = Claro.Utils.CheckStr(HttpContext.Request.UserHostAddress);
            string strIPServidor = App_Code.Common.GetApplicationIp();
            string strNombreServidor = App_Code.Common.GetApplicationName();
            string strCuentaUsuario = oModel.strCurrentUser;
            string strPhone = oModel.strTelephone;
            string strNameClient = oModel.strNombreClienteSession;

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
                    vTexto = strTexto,
                    vTransaccion = strCodigoAuditoria,
                    audit = audit
                };


                objResponse = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SaveAuditResponseCommon>(() =>
                {
                    return oServiceCommon.SaveAudit(objRequest);
                });
                FlatResultado = objResponse.respuesta;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }


            return FlatResultado;
        }

	}
}
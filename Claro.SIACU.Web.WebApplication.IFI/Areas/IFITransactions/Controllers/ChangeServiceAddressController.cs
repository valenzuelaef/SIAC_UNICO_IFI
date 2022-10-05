using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using Common = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class ChangeServiceAddressController : Controller
    {
        //
        // GET: /IFITransactions/ChangeServiceAddress/
        public ActionResult Index()
        {
            string strError = string.Empty;
            string strCosto = string.Empty;
            string strRegistro = string.Empty;
            string strVer = string.Empty;
            string strTotal = string.Empty;
            string strCobertura = string.Empty;
            Model.NotificacionDireccionModel oChangeServiceAddress = new Model.NotificacionDireccionModel();
            alertaNotificacionIFI("", ref strCosto, ref strError, ref strRegistro, ref strVer, ref strTotal, ref strCobertura);
            oChangeServiceAddress.strCosto = strCosto;
            oChangeServiceAddress.strError = strError;
            oChangeServiceAddress.strRegistro = strRegistro;
            oChangeServiceAddress.strVer = strVer;
            oChangeServiceAddress.strTotal = strTotal;
            oChangeServiceAddress.strCobertura = strCobertura;
            return View("~/Areas/IFITransactions/Views/ChangeServiceAddress/ChangeServiceAddress.cshtml", oChangeServiceAddress);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeServiceGeoDir()
        {
            return PartialView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public JsonResult PageLoad(string strIdSession)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);

            Common.Typification oTipificacion = null;
            string strMensaje = "";
            try
            {
                oTipificacion = cargaTipificacion(audit, ref strMensaje);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message.ToString());
            }
            return Json(new { data = oTipificacion });

        }
        /// <summary>
        /// Permite obtener la tipificación de acuerdo a la transacción de Cambio de Dirección IFI.
        /// </summary>
        /// <param name="oAudit"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        public Common.Typification cargaTipificacion(CommonIFIService.AuditRequest oAudit, ref string strMensaje)
        {
            CommonIFIService.CommonIFIServiceClient oCommonService;
            Common.Typification oTypification = null;
            try
            {
                Common.TypificationRequest objTypificationRequest = new Common.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = KEY.AppSettings("strTransaccionChangeServiceAddress");
                objTypificationRequest.audit = oAudit;
                oCommonService = new CommonIFIService.CommonIFIServiceClient();
                Common.TypificationResponse objResponse = Claro.Web.Logging.ExecuteMethod<Common.TypificationResponse>(() => { return oCommonService.GetTypification(objTypificationRequest); });
                if (objResponse == null)
                {
                    strMensaje = "No se cargo las tipificaciones";
                }
                else { oTypification = objResponse.ListTypification.First(); }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oAudit.Session, oAudit.transaction, ex.Message);
                strMensaje = "No se cargo las tipificaciones";
            }
            return oTypification;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaccion"></param>
        /// <returns></returns>
        public void alertaNotificacionIFI(string strIdSession, ref string strNotCosto, ref string strNotError, ref string strNotRegistro, ref string strNotVer, ref string strNotTotal, ref string strNotCober)
        {
            PostIFIService.PostIFIServiceClient oServicio = null;
            List<GenericItem> olisGenericItem = null;
            string strTransaccion = string.Empty;
            string strEscenario = string.Empty;
            try
            {
                strTransaccion = KEY.AppSettings("strNotifacionChangeServiceAddress");
                oServicio = new PostIFIService.PostIFIServiceClient();
                olisGenericItem = oServicio.obtenerParametrosPorTipo(strIdSession, strTransaccion);
                foreach (GenericItem oItem in olisGenericItem)
                {
                    strEscenario = oItem.Descripcion2.Substring(0, 1);
                    switch (strEscenario)
                    {
                        case "C": //Costo
                            strNotCosto = oItem.Descripcion;
                            strNotTotal = oItem.Codigo2;
                            break;
                        case "E": //Error
                            strNotError = oItem.Descripcion;
                            break;
                        case "R": //Registro
                            strNotRegistro = oItem.Descripcion.Replace(KEY.AppSettings("strTiempoChangeServiceAddress"), oItem.Codigo2);
                            break;
                        case "V": //Visualizar
                            strNotVer = oItem.Descripcion;
                            break;
                        case "M": //Cobertura
                            strNotCober = oItem.Descripcion;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);
            }
        }

        #region "Obtener Flag General - Cobertura"
        /// <summary>
        /// Permite obtener el flag de cobertura: 1: Obtener dirección de GeoDir 0: Sin Cambios
        /// </summary>
        /// <returns></returns>
        public JsonResult obtenerFlagCobertura(string strIdTransaccion)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdTransaccion);
            CommonIFIService.CommonIFIServiceClient oServicio = null;
            List<ObtenerTipoTecnologia> olisObtenerTipoTecnologia = null;
            string strTipoTecnologia = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : obtenerFlagCobertura");
            try 
            {
                oServicio = new CommonIFIServiceClient();
                olisObtenerTipoTecnologia = oServicio.obtenerTipoTecnologia(audit.Session, audit.transaction, KEY.AppSettings("strObtenerTipoTecnologiaCobertura"));
                var lisObtenerTecnologia = from olis in olisObtenerTipoTecnologia where olis.PARAV_VALOR1.Trim().Equals(KEY.AppSettings("strObtenerFlagCobertura")) select olis;
                foreach (var item in olisObtenerTipoTecnologia)
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fila : " + item.PARAV_VALOR1);
                }

                if (lisObtenerTecnologia.Count() > 0)
                {
                    strTipoTecnologia = lisObtenerTecnologia.ElementAt(0).PARAV_VALOR;
                }
                else
                {
                    strTipoTecnologia = "";
                }
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Llave strObtenerFlagCobertura: " + KEY.AppSettings("strObtenerFlagCobertura"));
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "FlagCobertura: " + strTipoTecnologia);
            }catch(Exception ex)
            {
                Claro.Web.Logging.Error(audit.transaction, audit.Session, ex.Message);
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : obtenerFlagCobertura");
            return Json(new { data = strTipoTecnologia });
        }
        #endregion

        #region "Registro de Cambio de Dirección de Servicio IFI"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public JsonResult SaveTransactionChangeServiceAddress(Model.ChangeServiceAddressModel oModel)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.idTransaccion);
            string strInteraccionId = string.Empty;
            string strNotificacion = string.Empty;
            string strCobertura = string.Empty;
            string strFideliza = string.Empty;
            string strMontoFidelizado = string.Empty;
            bool bregistro = false;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : SaveTransactionChangeServiceAddress");
            try
            {
                if (oModel.cobertura == "1")
                {
                    strCobertura = "";
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Cobertura: Si: " + oModel.cobertura);
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Flag Cobro: flagCobro: " + oModel.flagCobro);
                    if (oModel.flagCobro =="0")
                    {
                        strFideliza = "Cliente Fidelizado.";
                        strMontoFidelizado = string.Format(KEY.AppSettings("strMontoFidelizado"), oModel.monto);
                    }
                    else
                    {
                        strFideliza = "Cliente NO Fidelizado.";
                    }
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Modalidad: strFideliza: " + strFideliza);
                bregistro = saveChangeServiceAddress(oModel);
                if (bregistro)
                {
                    bregistro = false;
                        bregistro = saveInteraccionChangeServiceAddress(oModel, strFideliza, strMontoFidelizado, ref strInteraccionId);
                    if (bregistro)
                    {
                        strNotificacion = "OK";
                    }
                        else
                        {
                            strNotificacion = "NO OK";
                        }
                    }
                    else
                    {
                        strNotificacion = "NO OK";
                    }
                }
                else
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Cobertura: No: " + oModel.cobertura);
                    strCobertura = KEY.AppSettings("strNoCoberturaChangeServiceAddress");
                    bregistro = saveInteraccionChangeServiceAddress(oModel, strCobertura, strMontoFidelizado, ref strInteraccionId);
                    if (bregistro)
                    {
                        strNotificacion = "OK";
                    }
                    else
                    {
                    strNotificacion = "NO OK";
                        strCobertura = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.idTransaccion, oModel.idTransaccion, ex.Message);
            }
            return Json(new { data = strNotificacion, msj = strCobertura });
        }
        /// <summary>
        /// Permite recuperar la información ingresada para luego procesarla.
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public bool saveChangeServiceAddress(Model.ChangeServiceAddressModel oModel)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.idTransaccion);
            PostIFIService.PostIFIServiceClient oServicio = null;
            registrarDireccionResponse oregistrarDireccionResponse = null;
            registrarDireccionRequest oregDireccionRequest = null;
            string strDefault = string.Empty;
            bool bregistro = false;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : saveChangeServiceAddress");
            try
            {
                oServicio = new PostIFIService.PostIFIServiceClient();
                oregDireccionRequest = new registrarDireccionRequest();
                oregDireccionRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss");  //oModel.idTransaccion;
                oregDireccionRequest.tipoCliente = oModel.tipoCliente == null ? strDefault : oModel.tipoCliente;
                oregDireccionRequest.linea = oModel.linea;
                oregDireccionRequest.tipoDireccion = (oModel.tipoDireccion == "Seleccionar" || oModel.tipoDireccion == null) ? strDefault : oModel.tipoDireccion;
                oregDireccionRequest.nombreDireccion = oModel.nombreDireccion == null ? strDefault : oModel.nombreDireccion;
                oregDireccionRequest.numeroDireccion = oModel.numeroDireccion == null ? strDefault : oModel.numeroDireccion;
                oregDireccionRequest.subDireccion = (oModel.subDireccion == "Seleccionar" || oModel.subDireccion == null) ? strDefault : oModel.subDireccion;
                oregDireccionRequest.numeroSubDireccion = oModel.numeroSubDireccion == null ? strDefault : oModel.numeroSubDireccion;
                oregDireccionRequest.lote = oModel.lote == null ? strDefault : oModel.lote;
                oregDireccionRequest.tipoSubDireccion = (oModel.tipoSubDireccion == "Seleccionar" || oModel.tipoSubDireccion == null) ? strDefault : oModel.tipoSubDireccion;
                oregDireccionRequest.tipoUrbanizacion = (oModel.tipoUrbanizacion == "Seleccionar" || oModel.tipoUrbanizacion == null) ? strDefault : oModel.tipoUrbanizacion;
                oregDireccionRequest.nombreUrbanizacion = oModel.nombreUrbanizacion == null? strDefault :oModel.nombreUrbanizacion;
                oregDireccionRequest.tipoDomicilio = (oModel.tipoDomicilio == "Seleccionar" || oModel.tipoDomicilio == null) ? strDefault : oModel.tipoDomicilio;
                oregDireccionRequest.zonaEtapa = (oModel.zonaEtapa == "Seleccionar" || oModel.zonaEtapa == null) ? strDefault : oModel.zonaEtapa;
                oregDireccionRequest.nombreZonaEtapa = oModel.nombreZonaEtapa == null ? strDefault : oModel.nombreZonaEtapa;
                oregDireccionRequest.referencia = oModel.referencia == null ? strDefault : oModel.referencia;
                oregDireccionRequest.departamento = oModel.departamento == null ? strDefault : oModel.departamento;
                oregDireccionRequest.provincia = oModel.provincia == null ? strDefault : oModel.provincia;
                oregDireccionRequest.distrito = oModel.distrito == null ? strDefault : oModel.distrito;
                oregDireccionRequest.codigoPostal = oModel.codigoPostal == null ? strDefault : oModel.codigoPostal;
                oregDireccionRequest.ubigeo = oModel.ubigeo == null ? strDefault : oModel.ubigeo;
                oregDireccionRequest.telefonoReferencia = oModel.telefonoReferencia == null ? strDefault : oModel.telefonoReferencia;
                oregDireccionRequest.flagCobro = oModel.flagCobro == null ? strDefault : oModel.flagCobro;
                oregDireccionRequest.accion = "2";
                oregDireccionRequest.currentUser = ConfigurationManager.AppSettings("USRProcesoSU"); //oModel.currentUser == null ? strDefault : oModel.currentUser;
                oregDireccionRequest.estado = "0";
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(oregDireccionRequest);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Cambio de Dirección --> Parametros de entrada actualizacion: " + json_object);
                oregistrarDireccionResponse = oServicio.registrarDireccion(oregDireccionRequest);
                if (oregistrarDireccionResponse.responseStatus.codigoRespuesta.Equals(Constants.ZeroNumber))
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Código Respuesta : " + oregistrarDireccionResponse.responseStatus.codigoRespuesta);
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Mensaje Respuesta : " + oregistrarDireccionResponse.responseStatus.descripcionRespuesta);
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "IdTransaccion : " + oregistrarDireccionResponse.responseStatus.idTransaccion);
                    bregistro = true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : "+ ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : saveChangeServiceAddress");
            return bregistro;
        }
        #endregion

        #region "Validar Cobertura de Cambio de Dirección de Servicio IFI"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oServicio"></param>
        /// <param name="oCliente">Información del cliente</param>
        /// <param name="oDireccion">Información de la dirección actual del cliente</param>
        /// <param name="oSolicitud">Información adicional</param>
        /// <returns></returns>
        public JsonResult consultarCoberturaDireccion(Model.ChangeServiceAddressModel oChangeServiceAdd, Model.ValidarCoberturaModel oServicio, Model.ClienteModel oCliente, Model.DireccionModel oDireccion, Model.SolicitudModel oSolicitud)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oServicio.idTransaccion);
            string strCobertura = string.Empty;
            Model.WebResponse oRespuesta = new Model.WebResponse();
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : consultarCoberturaDireccion");
            try
            {
                oRespuesta = validarCoberturaDireccion(oChangeServiceAdd.strTmCode, oServicio, oCliente, oDireccion, oSolicitud);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oServicio.idTransaccion, oServicio.idTransaccion, ex.Message);
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : consultarCoberturaDireccion");
            return Json(new { data = oRespuesta });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oValidar"></param>
        /// <returns></returns>
        public Model.WebResponse validarCoberturaDireccion(string strTMCode, Model.ValidarCoberturaModel oValidar, Model.ClienteModel oCliente, Model.DireccionModel oDireccion, Model.SolicitudModel oSolicitud)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oValidar.idTransaccion);
            PostIFIService.PostIFIServiceClient oServicio = null;
            Model.WebResponse objResponse = new Model.WebResponse();
            validarCoberturaMessageResponse ovalidarCoberturaMessageResponse = null;
            validarCoberturaMessageRequest ovalidarCoberturaMessageRequest = null;
            objResponse.Error = false;
            objResponse.exiteCobertura = false;
            bool existeCobertura = false;
            string strTipoTecnologia = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : validarCoberturaDireccion");
            try
            {
                oServicio = new PostIFIService.PostIFIServiceClient();
                Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("Id tipo tecnologia: {0}", KEY.AppSettings("strObtenerTipoTecnologiaCobertura")));
                Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("Plan Actual: {0}", strTMCode));

                strTipoTecnologia = consultarTipoTecnologias(audit.Session, strTMCode);

                Claro.Web.Logging.Info(audit.Session, audit.transaction, string.Format("Codigo tipo tecnologia: {0}", strTipoTecnologia));
                ovalidarCoberturaMessageRequest = new validarCoberturaMessageRequest();
                ovalidarCoberturaMessageRequest.audit = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.AuditRequest();
                ovalidarCoberturaMessageRequest.audit = audit;
                ovalidarCoberturaMessageRequest.MessageRequest = new validarMessageRequest()
                {
                    Header = new validarHeaderRequest()
                   {
                       HeaderRequest = new HeaderRequest1()
                       {
                           consumer = KEY.AppSettings("consConsumer"),
                           country = KEY.AppSettings("consCountry"),
                           dispositivo = KEY.AppSettings("consDispositivo"),
                           language = KEY.AppSettings("consLanguage"),
                           modulo = KEY.AppSettings("consModulo"),
                           msgType = KEY.AppSettings("consMsgType"),
                           operation = KEY.AppSettings("consOperationVCD"),
                           pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                           system = KEY.AppSettings("consSystem"),
                           timestamp = DateTime.Now.ToString("o"),
                           userId = App_Code.Common.CurrentUser,//KEY.AppSettings("consUserId"),//
                           //wsIp = App_Code.Common.GetApplicationIp() //"192.168.163.11",
                           wsIp = "172.19.71.83"
                       }
                   },
                    Body = new BodyRequestValidarCobertura()
                    {
                        validarCoberturaRequest = new ValidarCoberturaItemRequest()
                        {
                            idTransaccion = oValidar.idTransaccion,
                            codAplicacion = oValidar.codAplicacion,
                            latitud = oValidar.latitud,
                            longitud = oValidar.longitud,
                            tipoTecnologia = strTipoTecnologia,
                            motivo = oValidar.motivo,
                            cliente = new PostIFIService.cliente()
                            {
                                tipoDoc = oCliente.tipoDoc,
                                numeroDoc = oCliente.numeroDoc,
                                nombres = oCliente.nombres,
                                apellidos = oCliente.apellidos,
                                correo = oCliente.correo,
                                telefonoContacto = oCliente.telefonoContacto
                            },
                            direccion = new direccion()
                            {
                                departamento = oDireccion.departamento,
                                provincia = oDireccion.provincia,
                                distrito = oDireccion.distrito,
                                direccionMember = null
                            },
                            solicitud = null
                        }
                    }
                };

                ovalidarCoberturaMessageResponse = oServicio.consultarCoberturaDireccion(ovalidarCoberturaMessageRequest);
                if (ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.codigoRespuesta.Equals(Constants.ZeroNumber))
                {
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Código Respuesta : " + ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.codigoRespuesta);
                    Claro.Web.Logging.Info(audit.Session, audit.transaction, "Mensaje Respuesta : " + ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.descripcionRespuesta);
                    existeCobertura = ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseData.exiteCobertura.Equals(Constants.Yes) ? true : false;
                    if (existeCobertura)
                    {
                        objResponse.exiteCobertura = true;
                        objResponse.Mensaje = string.Empty;
                    }
                    else
                    {
                        objResponse.exiteCobertura = false;
                        int intCodigoRpta = Convert.ToInt(ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.codigoRespuesta);
                        if (intCodigoRpta < 0)
                        {
                            objResponse.Mensaje = ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.descripcionRespuesta;
                        }
                        else
                        {
                            objResponse.Mensaje = string.Empty;
                        }
                    }
                }
                else
                {
                    objResponse.exiteCobertura = false;
                    int intCodigoRpta = Convert.ToInt(ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.codigoRespuesta);
                    if (intCodigoRpta < 0)
                    {
                        objResponse.Mensaje = ovalidarCoberturaMessageResponse.MessageResponse.Body.validarCoberturaResponse.responseStatus.descripcionRespuesta;
                    }
                    else
                    {
                        objResponse.Mensaje = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : validarCoberturaDireccion");

            return objResponse;
        }
        /// <summary>
        /// Permite obtener el ID de Recupera Velocidad de acuerdo al plan.
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="strTMCode"></param>
        /// <returns></returns>
        public string consultarTipoTecnologias(string strIdSession, string strTMCode)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            CommonIFIService.CommonIFIServiceClient oServicio = null;
            List<ObtenerTipoTecnologia> olisObtenerTipoTecnologia = null;
            string strTipoTecnologia = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "INICIO consultarTipoTecnologias");
            try
            {
                oServicio = new CommonIFIService.CommonIFIServiceClient();
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "KEY: " + KEY.AppSettings("strObtenerTipoTecnologiaCobertura"));
                olisObtenerTipoTecnologia = oServicio.obtenerTipoTecnologia(audit.Session, audit.transaction, KEY.AppSettings("strObtenerTipoTecnologiaCobertura"));
                var lisObtenerTecnologia = from olis in olisObtenerTipoTecnologia where olis.PARAV_VALOR1.Trim().Equals(strTMCode) select olis;
                if (lisObtenerTecnologia.Count() > 0)
                {
                    strTipoTecnologia = lisObtenerTecnologia.ElementAt(0).PARAV_VALOR;
                }
                else
                {
                    strTipoTecnologia = KEY.AppSettings("strObtenerTipoRecuperaVelocidadDEF");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "FIN  consultarTipoTecnologias");
            return strTipoTecnologia;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strLlave"></param>
        /// <returns></returns>
        public JsonResult obtenerMensajeGeoDir(string strIdSession, string strLlave)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            CommonIFIService.CommonIFIServiceClient oServicio = null;
            List<ObtenerTipoTecnologia> olisObtenerMensajes = null;
            string strObtenerMensaje = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "INICIO obtenerMensajeGeoDir");
            try
            {
                oServicio = new CommonIFIService.CommonIFIServiceClient();
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "KEY: " + KEY.AppSettings("strObtenerTipoTecnologiaCobertura"));
                olisObtenerMensajes = oServicio.obtenerTipoTecnologia(audit.Session, audit.transaction, KEY.AppSettings("strObtenerTipoTecnologiaCobertura"));
                var lisObtenerMensajes = from olis in olisObtenerMensajes where olis.PARAV_VALOR1.Trim().Equals(strLlave) select olis;
                if (lisObtenerMensajes.Count() > 0)
                {
                    strObtenerMensaje = lisObtenerMensajes.ElementAt(0).PARAV_VALOR;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "FIN  obtenerMensajeGeoDir");
            return Json(new { data = strObtenerMensaje });
        }
        #endregion

        #region "Registro de Interaccion"
        /// <summary>
        /// Método que registra la interacción al cambiar la direccion del servicio IFI.
        /// </summary>
        /// <param name="oModel"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strPathFileHPxtream"></param>
        /// <returns></returns>
        public bool saveInteraccionChangeServiceAddress(Model.ChangeServiceAddressModel oModel, string strNotas, string strMontoFidelizado, ref string strInteraccionId)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.idTransaccion);
            string strUSUARIO_SISTEMA = string.Empty;
            string strUSUARIO_APLICACION = string.Empty;
            string strPASSWORD_USUARIO = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : saveInteraccionChangeServiceAddress");
            try
            {
                strUSUARIO_SISTEMA = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                strUSUARIO_APLICACION = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                strPASSWORD_USUARIO = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");
                var oInteraction = interactionChangeServiceAddress(oModel, strNotas, strMontoFidelizado);
                if (oInteraction == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(oModel.claseDes))
                {
                    return false;
                }
                var oTemplateInteraction = templateInteractionChangeServiceAddress(oModel);
                var resultInteraction = insertInteracChangeServiceAddress(oInteraction, oTemplateInteraction,
                                                                          oModel.linea, strUSUARIO_SISTEMA, 
                                                                          strUSUARIO_APLICACION, strPASSWORD_USUARIO, 
                                                                          true, oModel.idTransaccion);
                strInteraccionId = resultInteraction.rInteraccionId.ToString();
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Id Interacción : strInteraccionId: " + strInteraccionId);
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
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
                return false;
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : saveInteraccionChangeServiceAddress");
            return true;
        }

        /// <summary>
        /// Método que obtiene los datos de la interacción.
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public CommonIFIService.Iteraction interactionChangeServiceAddress(Model.ChangeServiceAddressModel oModel, string strNotas, string strMontoFidelizado)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.idTransaccion);
            var responseModel = new CommonIFIService.Iteraction();
            string strConcepto = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : interactionChangeServiceAddress");
            try
            {
                var tipo = ConfigurationManager.AppSettings("gConstTipoINT");
                responseModel.OBJID_CONTACTO = oModel.strObjidContacto;
                responseModel.START_DATE = DateTime.UtcNow.ToString("dd/MM/yyyy");
                responseModel.TELEFONO = oModel.linea;
                responseModel.TIPO = oModel.tipo;
                responseModel.CLASE = oModel.claseDes;
                responseModel.SUBCLASE = oModel.subClaseDes;
                responseModel.TIPO_CODIGO = oModel.tipoCode;
                responseModel.CLASE_CODIGO = oModel.claseCode;
                responseModel.SUBCLASE_CODIGO = oModel.subClaseCode;
                responseModel.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                responseModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                responseModel.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                responseModel.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                if ((strMontoFidelizado != null) || (strMontoFidelizado != ""))
                {
                    responseModel.NOTAS = oModel.strNote + (char)13 + strMontoFidelizado + " " + strNotas;
                }
                else {
                responseModel.NOTAS = oModel.strNote + " " + strNotas;
                }
                responseModel.FLAG_CASO = Claro.Constants.NumberZeroString;
                responseModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                responseModel.AGENTE = oModel.currentUser;
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Interacción --> Parametros de entrada tipificación: " + json_object);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : interactionChangeServiceAddress");
            return responseModel;
        }
        /// <summary>
        /// Método que obtiene los datos de la plantilla interacción.
        /// </summary>
        /// <param name="oModel"></param>
        /// <returns></returns>
        public CommonIFIService.InsertTemplateInteraction templateInteractionChangeServiceAddress(Model.ChangeServiceAddressModel oModel)
        {
            PostIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostIFIService.AuditRequest>(oModel.idTransaccion);
            var responseModel = new CommonIFIService.InsertTemplateInteraction();
            var strImporte = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : templateInteractionChangeServiceAddress");
            try
            {
                responseModel._NOMBRE_TRANSACCION = KEY.AppSettings("strTransaccionChangeServiceAddress");
                responseModel._X_CLARO_NUMBER = oModel.linea;
                responseModel._X_DOCUMENT_NUMBER = oModel.DNI_RUC;
                responseModel._X_FIRST_NAME = oModel.strNombres;
                responseModel._X_LAST_NAME = oModel.strApellidos;
                if (oModel.strDireccion.Length < 95) { responseModel._X_ADDRESS = oModel.strDireccion; } else { responseModel._X_ADDRESS = oModel.strDireccion.Substring(0, 95); }
                responseModel._X_REFERENCE_ADDRESS = oModel.referencia;
                responseModel._X_OTHER_FIRST_NAME = oModel.strContactoCliente;
                responseModel._X_DISTRICT = oModel.distrito;
                responseModel._X_CITY = oModel.provincia;
                responseModel._X_DEPARTMENT = oModel.departamento;
                responseModel._X_INTER_1 = oModel.pais;
                responseModel._X_ZIPCODE = oModel.codigoPostal;
                responseModel._X_NAME_LEGAL_REP = oModel.strfullNameUser;
                responseModel._X_EMAIL = "";
                responseModel._X_INTER_15 = oModel.strCacDac;
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Plantilla Cambio de Dirección --> Parametros de entrada actualizacion: " + json_object);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : templateInteractionChangeServiceAddress");
            return responseModel;
        }
        /// <summary>
        /// Método que inserta los datos enviados en la interacción.
        /// </summary>
        /// <param name="oInteractionModel"></param>
        /// <param name="oPlantillaDat"></param>
        /// <param name="strNroTelephone"></param>
        /// <param name="strUserSession"></param>
        /// <param name="strUserAplication"></param>
        /// <param name="strPassUser"></param>
        /// <param name="boolEjecutTransaction"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public CommonIFIService.InsertGeneralResponse insertInteracChangeServiceAddress(CommonIFIService.Iteraction oInteractionModel, Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.InsertTemplateInteraction oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession)
        {
            CommonIFIService.InsertGeneralRequest objRequest;
            CommonIFIService.InsertGeneralResponse objResult = null;
            Common.AuditRequest audit = App_Code.Common.CreateAuditRequest<Common.AuditRequest>(strIdSession);
            CommonIFIService.CommonIFIServiceClient oCommonService = null;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Inicio Método : insertInteracChangeServiceAddress");
            try
            {
                oCommonService = new CommonIFIService.CommonIFIServiceClient();
                objRequest = new Common.InsertGeneralRequest()
                {
                    Interaction = oInteractionModel,
                    InteractionTemplate = oPlantillaDat,
                    vNroTelefono = strNroTelephone,
                    vPASSWORD_USUARIO = strPassUser,
                    vUSUARIO_APLICACION = strUserSession,
                    vUSUARIO_SISTEMA = strUserAplication,
                    vEjecutarTransaccion = boolEjecutTransaction,
                    audit = audit,

                };
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Registro de Interacción --> Parametros de entrada actualizacion: " + json_object);
           
                objResult = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oCommonService.GetinsertInteractionGeneral(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.transaction, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Fin Método : insertInteracChangeServiceAddress");
            return objResult;
        }
        #endregion 
    }
}
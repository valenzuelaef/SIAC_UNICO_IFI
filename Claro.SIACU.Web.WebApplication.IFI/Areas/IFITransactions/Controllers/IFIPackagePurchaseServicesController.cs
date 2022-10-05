using Claro.Entity;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.Controllers;
using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using Claro.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.AuditRequest;
using Common = Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
//using Common = Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Constants;
using Model = Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Models;
using Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers;
using System.Text;
using AutoMapper; 

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class IFIPackagePurchaseServicesController : CommonServicesController
    {
        public readonly CommonIFIService.CommonIFIServiceClient oCommonIFIService = new CommonIFIService.CommonIFIServiceClient();
        public readonly FixedIFIService.FixedIFIServiceClient oFixedIFIService = new FixedIFIService.FixedIFIServiceClient();


        public const string TIPO_TRANSACCION = ".Tipo transaccion";
        public const string NRO_CONTRATO = ".Nro de Contrato";
        public const string NRO_LINEA = ".Nro Linea";
        public const string CODIGO_OPERACION = ".Codigo De Operacion";
        public const string TIPO_OPERACION = ".Tipo de Operacion";
        public const string TIPO_PRODUCTO = ".Tipo de Producto";
        public const string CANAL_ATENCION = ".Canal de Atencion";
        public const string FECHA_ALTA = ".Fecha Alta";
        public const string FECHA_BAJA = ".Fecha Baja";
        public const string ESTADO_SERVICIO = ".Estado Servicio";
        public const string USUARIO = ".Username";
        public const string EXTENSION = ".Extension";
        public const string CODIGO_CLIENTE = ".Codigo de Cliente";



        public ActionResult IFIPackagePurchaseServices()
        {      
            ViewBag.strTipoVentaReinicia = ConfigurationManager.AppSettings("strTipoVentaReinicia");
            ViewBag.strMotivoDegradacion = ConfigurationManager.AppSettings("strMotivoDegradacion");
            ViewBag.strFacturada = ConfigurationManager.AppSettings("strFacturada");
            ViewBag.strClaroPuntos = ConfigurationManager.AppSettings("strClaroPuntos");
            ViewBag.strMsjClaroPuntos = ConfigurationManager.AppSettings("strMsjClaroPuntos");
            ViewBag.strCodTransPackagePurchaseFixedServIFI = ConfigurationManager.AppSettings("strCodTransPackagePurchaseFixedServIFI");
            ViewBag.strNoPresentaVelocidadDegradada = ConfigurationManager.AppSettings("strNoPresentaVelocidadDegradadaIFI");
            return View();
        }
        /// <summary>
        /// Permite obtener el ID de Recupera Velocidad de acuerdo al plan.
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="strTMCode"></param>
        /// <returns></returns>
        public string consultarRecuperaVelocidad(string strIdSession, string strTMCode)
        {
            CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession);
            CommonIFIService.CommonIFIServiceClient oServicio = null;
            List<ObtenerTipoTecnologia> olisObtenerTipoTecnologia = null;
            string strRecuperaVelocidad = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "INICIO obtenerIdRecuperaVelocidad");
            try
            {
                oServicio = new CommonIFIService.CommonIFIServiceClient();
                olisObtenerTipoTecnologia = oServicio.obtenerTipoTecnologia(audit.Session, audit.transaction, KEY.AppSettings("strObtenerRecuperaVelocidad"));
                var lisRecuperaVelocidad = from olis in olisObtenerTipoTecnologia where olis.PARAV_VALOR1.Trim().Equals(strTMCode) select olis;
                if (lisRecuperaVelocidad.Count() > 0)
                {
                    strRecuperaVelocidad = lisRecuperaVelocidad.ElementAt(0).PARAV_VALOR;
                }
                else
                {
                    strRecuperaVelocidad = KEY.AppSettings("strObtenerTipoRecuperaVelocidadDEF");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : " + strIdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "FIN  obtenerIdRecuperaVelocidad");
            return strRecuperaVelocidad;
        }

        public JsonResult ConsultarPCRFDegradacion(string IdSession, string strLinea, string strTMCode)
        {
            string DegradacionPlan = string.Empty;
            string DegradacionPaquetes = string.Empty;
            string Message = string.Empty;
            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ConsultarPCRFDegradacion");
            PCRFConnectorResponse objPCRFConnectorResponse = new PCRFConnectorResponse();
            string strRecupera = string.Empty;
            string[] strRecVel;
            string strServicio = string.Empty;
            try
            {

                PCRFConnectorRequest objPCRFConnectorRequest = new PCRFConnectorRequest()
                {
                    audit = oAuditRequest,
                    strLinea = strLinea
                };

                objPCRFConnectorResponse = Claro.Web.Logging.ExecuteMethod<PCRFConnectorResponse>(() =>
                {
                    return oFixedIFIService.ConsultarPCRFDegradacion(objPCRFConnectorRequest);
                });

                strRecupera = consultarRecuperaVelocidad(IdSession, strTMCode);
                strRecVel = strRecupera.Split('|');
                strServicio = strRecVel[1];
                foreach (var item in objPCRFConnectorResponse.listaSuscriberQuota)
                {
                    string strSRVNAME = item.SRVNAME.Replace(strRecVel[0], "");
                    int temp;
                    if (int.TryParse(strSRVNAME, out temp))
                    {
                        DegradacionPlan = item.QTABALANCE == "0" ? "SI" : "NO";
                    }
                    else
                    {
                        DegradacionPaquetes = item.QTABALANCE == "0" ? "SI" : "NO";
                    }
                }
            }

            catch (Exception ex)
            {
                objPCRFConnectorResponse.codRespuesta = "-1";
                Claro.Web.Logging.Error("IdSession : " + IdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ConsultarPCRFDegradacion");
            return Json(new { codRespuesta = objPCRFConnectorResponse.codRespuesta, DegradacionPlan = DegradacionPlan, DegradacionPaquetes = DegradacionPaquetes, Servicio = strServicio });
        }      
     
        public JsonResult ConsultarClaroPuntos(string IdSession, string tipoDocumento, string numeroDocumento)
        {
            string Message = string.Empty;
            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ConsultarClaroPuntos");
            ConsultarClaroPuntosResponse objConsultarClaroPuntosResponse = new ConsultarClaroPuntosResponse();
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
                    operation = ConfigurationManager.AppSettings("strOperatioConsultarClaroPuntos"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = ConfigurationManager.AppSettings("system"),
                    timestamp = DateTime.Now.ToString("o"),
                    userId = App_Code.Common.CurrentUser,
                    wsIp = App_Code.Common.GetApplicationIp()
                };

                ConsultarClaroPuntosBodyRequest oBodyRequest = new ConsultarClaroPuntosBodyRequest()
                {
                    tipoConsulta = ConfigurationManager.AppSettings("ClaroPuntoTipoConsultaIFI"),                  
                    tipoDocumento =tipoDocumento,
                    numeroDocumento = numeroDocumento,
                    bolsa = ConfigurationManager.AppSettings("ClaroPuntosBolsaIFI"),
                    tipoPuntos = ConfigurationManager.AppSettings("ClaroPuntosTipoPuntosIFI"),
                };

                ConsultarClaroPuntosHeaderRequest oConsultarClaroPuntosHeader = new ConsultarClaroPuntosHeaderRequest()
                {
                    HeaderRequest = oHeaderRequest
                };

                ConsultarClaroPuntosMessageRequest oMessageRequest = new ConsultarClaroPuntosMessageRequest
                {
                    Header = oConsultarClaroPuntosHeader,
                    Body = oBodyRequest,
                };

                ConsultarClaroPuntosRequest objConsultarClaroPuntosRequest = new ConsultarClaroPuntosRequest()
                {
                    MessageRequest = oMessageRequest,
                    audit = oAuditRequest
                };

                  objConsultarClaroPuntosResponse = Claro.Web.Logging.ExecuteMethod<ConsultarClaroPuntosResponse>(() =>
                {
                   return oFixedIFIService.ConsultarClaroPuntos(objConsultarClaroPuntosRequest); 
                });


            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : " + IdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ConsultarClaroPuntos");
            return Json(new { data = objConsultarClaroPuntosResponse });
        }

        public JsonResult ConsultarPaqDisponibles(string IdSession,   string idContrato,  string prepagoCode, string tmCode)
        {
            string Message = string.Empty;
            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(IdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ConsultarPaqDisponibles");
            ConsultarPaqDisponiblesResponse objConsultarPaqDisponiblesResponse = new ConsultarPaqDisponiblesResponse();

            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;

            try{
                FixedIFIService.HeaderRequest oHeaderRequest = new FixedIFIService.HeaderRequest
                {
                    consumer = ConfigurationManager.AppSettings("consumer"),
                    country = ConfigurationManager.AppSettings("country"),
                    dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                    language = ConfigurationManager.AppSettings("language"),
                    modulo = ConfigurationManager.AppSettings("modulo"),
                    msgType = ConfigurationManager.AppSettings("msgType"),
                    operation = ConfigurationManager.AppSettings("strOperatioObtenerProductosPlan"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    system = ConfigurationManager.AppSettings("system"),
                    timestamp = DateTime.Now.ToString("o"),
                    userId = App_Code.Common.CurrentUser,
                    //QUITAR
                    wsIp =  App_Code.Common.GetApplicationIp()//"172.19.84.171"//
                };

                ConsultarPaqDisponiblesBodyRequest oBodyRequest = new ConsultarPaqDisponiblesBodyRequest()
                {
                    idCategoria = ConfigurationManager.AppSettings("idCategoria"),
                    idContrato = idContrato,
                    codigoCategoria = ConfigurationManager.AppSettings("codigoCategoria"),
                    prepagoCode=prepagoCode,
                    tmCode=tmCode

                };

                ConsultarPaqDisponiblesHeaderRequest oConsultarClaroPuntosHeader = new ConsultarPaqDisponiblesHeaderRequest()
                {
                    HeaderRequest = oHeaderRequest
                };

                ConsultarPaqDisponiblesMessageRequest oMessageRequest = new ConsultarPaqDisponiblesMessageRequest
                {
                    Header = oConsultarClaroPuntosHeader,
                    Body = oBodyRequest,
                };

                ConsultarPaqDisponiblesRequest objConsultarPaqDisponiblesRequest = new ConsultarPaqDisponiblesRequest()
                {
                    MessageRequest = oMessageRequest,
                    audit = oAuditRequest
                };

                objConsultarPaqDisponiblesResponse = Claro.Web.Logging.ExecuteMethod<ConsultarPaqDisponiblesResponse>(() =>
                {
                    return oFixedIFIService.ConsultarPaqDisponibles(objConsultarPaqDisponiblesRequest);
                });

                day = DateTime.Now.Day.ToString();
                month = DateTime.Now.Month.ToString();
                year = DateTime.Now.Year.ToString();
            }
            catch (Exception ex)
            {
               Claro.Web.Logging.Error("IdSession : " + IdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ConsultarPaqDisponibles");



            return Json(new { data = objConsultarPaqDisponiblesResponse, day = day, month = month, year=year });
        }

        public JsonResult SaveTransaccionIFIPackagePurchase(Model.IFIPackagePurchaseServicesModel oModel)
        {
             string rInteractionId = string.Empty;
            string vFlagInteraction = string.Empty;
            string vDesInteraction = string.Empty;
            string vDescCAC = string.Empty;
            string vresultado = string.Empty;
            string Mensaje = string.Empty;
            string MensajeEmail = string.Empty;
            string strNombreArchivo = string.Empty;
            string strNumeroIntentos = string.Empty;
            byte[] byteArchivoSamba = null;
            var errorMessage = "";
            oModel.fechaActual = DateTime.Now.ToShortDateString();
            var oPlantillaDat = new Model.TemplateInteractionModel();
            var oInteraccion = new Model.InteractionModel();
             Claro.Web.Logging.Info(" Método : " + "SaveTransaccionIFIPackagePurchase", "INICIO", oModel.strIdSession);
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            #region COMPRARPAQUETES

            ComprarPaquetesBodyResponse objComprarPaquetesResponse = new ComprarPaquetesBodyResponse();
            objComprarPaquetesResponse = ComprarPaquetes(oModel);
            Claro.Web.Logging.Info(" Método : " + "ComprarPaquetes", "codigoRespuesta: ", objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.codigoRespuesta);
            Claro.Web.Logging.Info(" Método : " + "ComprarPaquetes", "mensajeRespuesta: ", objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta);              
                 
            #endregion

            if (objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.codigoRespuesta=="0")
            {
                #region INTERACCION
                var result = new Dictionary<string, string>();
 
                string idInteraccion = string.Empty;
           
                string rFlagInsertion; 
                string rMsgText;
                Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction item;
                item = DatosInteraccionNegocio(oModel);

                InsertBusinessInteraction2(item, oModel.strIdSession, out  rInteractionId, out  rFlagInsertion, out  rMsgText);
                Claro.Web.Logging.Info(" Método : " + "InsertBusinessInteraction2", "rInteractionId: ", rInteractionId);
                Claro.Web.Logging.Info(" Método : " + "InsertBusinessInteraction2", "rFlagInsertion: ", rFlagInsertion);
                Claro.Web.Logging.Info(" Método : " + "InsertBusinessInteraction2", "rMsgText: ", rMsgText);
                oModel.strCasoInteraccion=rInteractionId;
                #endregion 

                 if (!string.IsNullOrEmpty(rInteractionId) )
                {
                    if (rInteractionId != "null")
                    {
                        try
                        {
                            #region GENERAR CONSTANCIA PDF

                            oModel.intNumeroIntentos = Convert.ToInt(ConfigurationManager.AppSettings("strNumeroIntentosReinicia"));
                            GetConstancyPDF(oModel.strIdSession, rInteractionId, oModel);

                            #endregion

                            if (oModel.bGeneratedPDF)
                            {
                               oModel.Document = string.IsNullOrEmpty(oModel.strFullPathPDF) ? string.Empty : oModel.strFullPathPDF.Substring(oModel.strFullPathPDF.LastIndexOf(@"\")).Replace(@"\", string.Empty);

                                #region OBTENER ARCHIVO SAMBA Y ENVIO DE CORREO
                                if (DisplayFileFromServerSharedFile(oModel.strIdSession, rInteractionId, oModel.strFullPathPDF, out byteArchivoSamba))
                                {
                              
                                //byteArchivoSamba =oModel.byteArchivoSamba;    
                              
                                List<CommonIFIService.AttachedFile> lstAdjuntos = new List<AttachedFile>();

                                if (byteArchivoSamba != null && oModel.ChkEmail)
                                {
                                    AttachedFile objFile = new AttachedFile();
                                    objFile.file = byteArchivoSamba;
                                    objFile.Name = oModel.Document;// + ".pdf"; ;
                                    objFile.HeadBoard = oModel.Document;
                                    lstAdjuntos.Add(objFile);
                                    MensajeEmail = sendCorreoSB(oModel, lstAdjuntos);
                                }
                                else
                                {
                                    MensajeEmail = "No se envio la constancia.";
                                }
                            }
                            else
                            {
                                MensajeEmail = "No se pudo obtener la Constancia.";
                            }
                                #endregion

                            }
                            else
                            {
                                errorMessage = "Error al momento de generar la Constancia";
                            }
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                            errorMessage = ex.Message;
                        }
                    }
                    else {
                        errorMessage = "Error al momento de generar Interaccion.";
                    }
              
                   
                }
                 else
                {
                    if (!string.IsNullOrEmpty(rMsgText))
                    {
                        errorMessage = rMsgText;
                    }
                    else
                    {
                        errorMessage = "Error al momento de generar la Comprar de Paquetes.";
                    }
                    
                    
                }

            }
            else
            {
                if (objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta != "")
                {
                    errorMessage = objComprarPaquetesResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta;
                }
                else {
                    errorMessage = "Error al momento de generar la Comprar de Paquetes."; 
                }
                      
            }
             oModel.strMensajeEmail = MensajeEmail;
             oModel.strErrorMessage = errorMessage;
             oModel.byteArchivoSamba = byteArchivoSamba; 
            Claro.Web.Logging.Info(" Método : " + "SaveTransaccionIFIPackagePurchase", "FIN", oModel.strIdSession);
            return Json(new { data = oModel });
        }

        public Dictionary<string, object> GetConstancyPDF(string strIdSession, string strIdInteraction, Model.IFIPackagePurchaseServicesModel oModel)
        {
            var listResponse = new Dictionary<string, object>();
             bool generado = false;
            string strInteraccionId = strIdInteraction;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;

            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            ParametersGeneratePDF parameters = null;

            try
            {
                Claro.Web.Logging.Info("IdSession: " + strIdSession, audit.transaction, "Método :  GetConstancyPDF - INICIO ");
                strNombreArchivo = ConfigurationManager.AppSettings("consPackgePurchaseServiceIFI"); //Llave para comprar paquetes IFI (no le veo la utilidad)

                parameters = new ParametersGeneratePDF();
                parameters.StrTitularCliente = oModel.strNombreCliente;
                parameters.StrTipoDocIdentidad = oModel.strTipoDocIdentidad;
                parameters.strRepLegNroDocumento = oModel.strNumeroDoc;
                parameters.StrTelfReferencia = oModel.strTelefono;
                parameters.StrMovil = oModel.strTelefono;
                parameters.strRepresentanteLegal = oModel.strRepresentanteLegal;
                parameters.StrNroDocIdentidad = oModel.strNumeroDoc;
                parameters.strCentroAtencion = oModel.strPuntoAtencion;
                parameters.StrFechaTransaccionProgram = (DateTime.Now).ToString("dd/MM/yyyy");
                parameters.StrTipoTransaccion = ConfigurationManager.AppSettings("strNombreArchivo_CompraPaqueteFijaIFI");
                parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionCompraPaquetesIFI");
                parameters.StrNombreArchivoTransaccion = ConfigurationManager.AppSettings("consNombreArchivoPackgePurchaseServiceIFI");
                parameters.StrCasoInter = strInteraccionId;
                parameters.strFechaHoraAtención = (DateTime.Now).ToString("dd/MM/yyyy");
                parameters.strTipoCliente = oModel.strTipoCliente;
                parameters.strPaqueteVelDegradada = oModel.PqtVelocidadDegradacion;
                parameters.strVigenciaPaquete = oModel.strVigencia;
                parameters.strPrecioPaquete = "S/ " + oModel.strPrecioPaquete;
                parameters.strMBIncluidos = oModel.strMBIncluidos;
                parameters.strEnvioCorreo = oModel.ChkEmail ? "SI" : "NO";
                parameters.strCorreoCliente = oModel.strEmailCliente;
                parameters.StrCodUsuario = oModel.strCodigoAsesor;
                parameters.StrNombreAgenteUsuario = oModel.strNombreAsesor;
                parameters.strCodeTCRM = ConfigurationManager.AppSettings("strCodeTCRM");
                parameters.strCuentaCliente = oModel.strNumeroCuenta;

                parameters.strNumeroServicio = oModel.strNumeroServicio;
                parameters.strTipoVenta = oModel.strTipoVenta;
                parameters.StrEmail = oModel.strEmailCliente;
                 


                string strCodigoOnBase = string.Empty;
                GenerateConstancyResponseCommon response = PackageGenerateContancyPDF(strIdSession, parameters, GetLstKeywordRequest(oModel), out strCodigoOnBase);

                oModel.byteArchivoSamba = response.bytesConstancy;

                generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                oModel.Document = response.Document;
                oModel.strCodOnBase = strCodigoOnBase;
                listResponse.Add("ruta", response.Document);
                listResponse.Add("respuesta", generado);
                listResponse.Add("nombreArchivo", strNombreArchivo);
            }
            catch (Exception ex)
            {
                oModel.bGeneratedPDF = false;
                Claro.Web.Logging.Error("IdSession: " + strIdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, audit.transaction, "Método :  GetConstancyPDF - FIN ");
            return listResponse;
        }

        public string sendCorreoSB(Model.IFIPackagePurchaseServicesModel model, List<CommonIFIService.AttachedFile> lstFileAdjunto)
        {
            CommonIFIService.SendEmailWithBase64Request objSendEmailWithBase64Request = new CommonIFIService.SendEmailWithBase64Request();
            CommonIFIService.SendEmailWithBase64Response objSendEmailWithBase64Response = null;
            CommonIFIService.AuditRequest objAudit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(model.strIdSession);
            string strResul = Claro.Utils.GetValueFromConfigFileIFI("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                
            try
            {
                string strMessage = string.Empty;
                string strAsunto = ConfigurationManager.AppSettings("strAsuntoIFIPackagePurchase");
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                string strDestinatarios = model.strEmailCliente;
                string cuerpo = ConfigurationManager.AppSettings("strCuerpoCorreoIFIPackagePurchase");
                string strMsgEmailConsultaLlameGratis = ConfigurationManager.AppSettings("strMsgEmailConsultaLlameGratis");
                Claro.Web.Logging.Info(objAudit.Session, objAudit.transaction, "Método : GetSendEmail strInteraccionId: " + objAudit);

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
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + cuerpo + "</td></tr>";
                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";

                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>Cordialmente,</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>Atención al Cliente</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>"+strMsgEmailConsultaLlameGratis+"</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion
                
                objSendEmailWithBase64Request.audit = objAudit;
                objSendEmailWithBase64Request.Sender = strRemitente;
                objSendEmailWithBase64Request.To = strDestinatarios;
                objSendEmailWithBase64Request.HtmlFlag = "1";
                objSendEmailWithBase64Request.Subject = strAsunto;
                objSendEmailWithBase64Request.Message = strMessage;

                if (lstFileAdjunto != null && lstFileAdjunto.Count > 0)
                {
                    objSendEmailWithBase64Request.ListAttachedFile = lstFileAdjunto;
            }

                for (int i = 0; i <= model.intNumeroIntentos; i++)
                {
                    objSendEmailWithBase64Response = Claro.Web.Logging.ExecuteMethod<CommonIFIService.SendEmailWithBase64Response>(() =>
                    {
                        return oCommonIFIService.SendEmailWithBase64(objSendEmailWithBase64Request);
                    });

                    if (objSendEmailWithBase64Response != null && objSendEmailWithBase64Response.ResponseCode == Claro.Constants.NumberZeroString)
                    {
                        
                        strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        Claro.Web.Logging.Info("idSeesion: " + model.strIdSession, "Codigo de Respuesta: " + objSendEmailWithBase64Response.ResponseCode, "Mensaje retornado : " + strResul);
                        break;
                    }
                    else
                    {
                        strResul = Claro.Utils.GetValueFromConfigFileIFI("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUIFIConfigMsg"));
                        Claro.Web.Logging.Info("idSeesion: " + model.strIdSession, "CorreoEnviado: No se pudo Enviar el correo", "Intento : " + i);
        }
                }
               
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.strIdSession, objAudit.transaction, ex.Message);
            }
            return strResul;
        }

        public Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction DatosInteraccionNegocio(Model.IFIPackagePurchaseServicesModel oModel)
        {

            var oInteraccion = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.Iteraction();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.strIdSession);
            Claro.Web.Logging.Info(oModel.strIdSession, audit.transaction, "Inicio Método : DatosInteraccion");
            try
            {
                oInteraccion.OBJID_CONTACTO = oModel.strObjidContacto;
                oInteraccion.START_DATE = Convert.ToString(DateTime.Now);
                oInteraccion.TELEFONO = oModel.strTelefono;
                oInteraccion.TIPO = oModel.TIPO;
                oInteraccion.TIPO_CODIGO = oModel.TIPO_CODE;
                oInteraccion.CLASE = oModel.CLASE;
                oInteraccion.CLASE_CODIGO = oModel.CLASE_CODE;
                oInteraccion.SUBCLASE = oModel.SUBCLASE;
                oInteraccion.SUBCLASE_CODIGO = oModel.SUBCLASE_CODE;
                oInteraccion.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
                oInteraccion.NOTAS = oModel.strNotas;
                oInteraccion.FLAG_CASO = "0";
                oInteraccion.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.AGENTE = oModel.CurrentUser;

            }
            catch (Exception ex)
            {
                Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }


            return oInteraccion;

        }

        public Model.TemplateInteractionModel GetDataTemplateInteraction(Model.IFIPackagePurchaseServicesModel oModel)
        {
            var oPlantillaCampoData = new Model.TemplateInteractionModel();
            FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            try
            {
                oPlantillaCampoData.ID_INTERACCION = oModel.strCasoInteraccion;
                oPlantillaCampoData.X_INTER_16 = oModel.strCodOnBase;
                oPlantillaCampoData.X_CLARO_NUMBER = oModel.strTelefono;
                oPlantillaCampoData.X_FIRST_NAME = oModel.strNombreCliente;
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            return oPlantillaCampoData;

        }

        public JsonResult GetInsertInteractionTemplate(Model.IFIPackagePurchaseServicesModel oModel)
        {
             CommonIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(oModel.strIdSession);
             TemplateInteractionModel   objInteractionTemplateModel = GetDataTemplateInteraction(oModel);
             InsertTemplateInteractionResponseCommon objInsertTemplInteractResponse = null;
            var serviceModelInteraction = Mapper.Map<CommonIFIService.InsertTemplateInteraction>(objInteractionTemplateModel);
            try
            {


                InsertTemplateInteractionRequestCommon objInsertTempInteractRequest = new InsertTemplateInteractionRequestCommon()
                {
                    audit = audit,
                    item = serviceModelInteraction,
                    IdInteraction = oModel.strCasoInteraccion
                };
              
                    objInsertTemplInteractResponse = Logging.ExecuteMethod<InsertTemplateInteractionResponseCommon>(() =>
                    {
                        return oCommonIFIService.GetInsertInteractionTemplate(objInsertTempInteractRequest); 
                    
                    });
                    Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "CodOnbase: ", oModel.strCodOnBase);
                    Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "rInteractionId: ", oModel.strCasoInteraccion);
                    Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "rFlagInsertion: ", objInsertTemplInteractResponse.FlagInsercion);
                    Claro.Web.Logging.Info(" Método : " + "GetInsertInteractionTemplate", "rMsgText: ", objInsertTemplInteractResponse.MsgText);
             
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.strIdSession, audit.transaction, ex.Message);
            }

            return Json(new { data = objInsertTemplInteractResponse });
        }

        public ComprarPaquetesBodyResponse ComprarPaquetes(Model.IFIPackagePurchaseServicesModel oModel)
        {
            string Message = string.Empty;
            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "INICIO ComprarPaquetes");
            ConsultarClaroPuntosResponse objConsultarClaroPuntosResponse = new ConsultarClaroPuntosResponse();
            ComprarPaquetesBodyResponse objComprarPaquetesBodyResponse = new ComprarPaquetesBodyResponse();

            string CadenaPaquetesDescripcionIFI = ConfigurationManager.AppSettings("PaquetesDescripcionIFI").ToString().ToUpper();
            string CadenaPaquetesSncodesIFI = ConfigurationManager.AppSettings("PaquetesSncodesIFI").ToString().ToUpper();

            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes Paquetes " + CadenaPaquetesDescripcionIFI);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes Sncodes " + CadenaPaquetesSncodesIFI);

            string[] PaquetesDescripcionIFI = CadenaPaquetesDescripcionIFI.Split(',');
            string[] PaquetesSncodesIFI = CadenaPaquetesSncodesIFI.Split(',');
            string paqueteSncode = string.Empty;

            for (int i = 0; i < PaquetesDescripcionIFI.Length; i++)
            {
                if (oModel.PqtVelocidadDegradacion.ToUpper() == PaquetesDescripcionIFI[i].ToString())
                {
                    paqueteSncode = PaquetesSncodesIFI[i].ToString();
                }
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes Sncode " + paqueteSncode);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes FlagActivacionCampo " + ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionCampo"));
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "ComprarPaquetes FlagActivacionValor " + ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionValor"));

            try
            {
                oModel.strCargoFijo = GetDatosporNroDocumentos(oModel);
                comprarPaquetesRequest ocomprarPaquetesRequest = new comprarPaquetesRequest() {
                    msisdn = oModel.strTelefono,
                    monto = oModel.strPrecioPaquete,
                    paquete = oModel.strPaquete,
                    customerId = oModel.strCustomerID,
                    planBase = oModel.strPlanBase, //"100",//oModel.strPlan, 
                    tipoProducto = ConfigurationManager.AppSettings("ComprarPaquetesTipoProductoIFI"),
                    tipoCliente = ConfigurationManager.AppSettings("ComprarPaquetesTipoClienteIFI"),
                    cicloFact = oModel.strCicloFacturacion,
                    fechaAct = oModel.fechaActual,
                    cargoFijo = oModel.strCargoFijo,
                    tipoPago = oModel.strTipoVenta,
                    departamento = oModel.strDepartamento,
                    provincia = oModel.strProvincia,
                    distrito = oModel.strDistrito,
                    listaOpcionalType = new List<ComprarPaquetesListaOpcionalType>()
                    {
                        new ComprarPaquetesListaOpcionalType()
                        {
                            campo = ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionCampo"),
                            valor = ConfigurationManager.AppSettings("ComprarPaquetesFlagActivacionValor")
                        }
                    }
                    
                };
                comprarPaquetesBodyRequest ocomprarPaquetesBodyRequest = new comprarPaquetesBodyRequest()
                {
                    
                    comprarPaquetesRequest = ocomprarPaquetesRequest 
                };

                ComprarPaquetesMessageRequest oComprarPaquetesMessageRequest = new ComprarPaquetesMessageRequest() {
                    audit = oAuditRequest,
                    comprarPaquetesBodyRequest = ocomprarPaquetesBodyRequest
                    
                };
              

                objComprarPaquetesBodyResponse = Claro.Web.Logging.ExecuteMethod<ComprarPaquetesBodyResponse>(() =>
                {
                    return oFixedIFIService.ComprarPaquetesRest(oComprarPaquetesMessageRequest);
                });

                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "codigoRespuesta: " + objComprarPaquetesBodyResponse.comprarPaqueteResponseType.responseStatus.codigoRespuesta);
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "mensajeRespuesta: " + objComprarPaquetesBodyResponse.comprarPaqueteResponseType.responseStatus.mensajeRespuesta);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : " + oModel.strIdSession, "Message Error : ", ex.Message);
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "FIN  ComprarPaquetes");
            return objComprarPaquetesBodyResponse;
        }

        public List<E_KeywordRequest> GetLstKeywordRequest(Model.IFIPackagePurchaseServicesModel oModel)
        {
            List<E_KeywordRequest> lstKeywordRequest = new List<E_KeywordRequest>();
            string Cadena = ConfigurationManager.AppSettings("strOnBaseKeyCompraPaqueteFijaNameIFI").ToString();
            string[] KeywordRequest = Cadena.Split(',');

            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);


            int day_now = DateTime.Now.Day;
            int year_now = DateTime.Now.Year;
            int day = (!string.IsNullOrEmpty(oModel.strCicloFacturacion) ? Convert.ToInt(oModel.strCicloFacturacion) : DateTime.Now.Day);

            string DateBaja = string.Empty;
            if (day_now > day)
            {
                int year_temp = DateTime.Now.AddMonths(1).Year;

                if (year_temp == year_now)
                {
                    DateBaja = day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.AddMonths(1).Month.ToString().PadLeft(2, '0') + "/" + year_now.ToString().PadLeft(4, '0');
                }
                else
                {
                    DateBaja = day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.AddMonths(1).Month.ToString().PadLeft(2, '0') + "/" + year_temp.ToString().PadLeft(4, '0');
                }


            }
            else
            {
                DateBaja = day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + year_now.ToString().PadLeft(4, '0');
            }

            string FORMATO_FECHA_ALTA = ConfigurationManager.AppSettings("FORMATO_FECHA_ALTA").ToString();
            string FORMATO_FECHA_BAJA = ConfigurationManager.AppSettings("FORMATO_FECHA_BAJA").ToString();

            try {
                string Campos = string.Empty;
                for (int i = 0; i < KeywordRequest.Length; i++)
                    {
                    Campos = string.Empty;
                    E_KeywordRequest objKeywordRequest = new E_KeywordRequest();

                    Campos = KeywordRequest[i].ToString() == null ? string.Empty : KeywordRequest[i].ToString();


                    if (TIPO_TRANSACCION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("strOnBaseKeyCompraPaqueteFijaNameLTE_TipoTransaccion").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);


                    }

                    if (NRO_CONTRATO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strContrato;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (NRO_LINEA == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strTelefono;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (CODIGO_OPERACION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oAuditRequest.transaction;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (TIPO_OPERACION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("strOnBaseKeyCompraPaqueteFijaNameLTE_TIPO_OPERACION").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }
                    if (TIPO_PRODUCTO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("ClaroPuntoTipoConsultaIFI").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (CANAL_ATENCION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strPuntoAtencion;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (FECHA_ALTA == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = (DateTime.Now).ToString(FORMATO_FECHA_ALTA); // DateTime.Now.ToString("mm/dd/yyyy");  //  DateTime.Now.Month.ToString().PadLeft(2, '0')+ "/" + DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString().PadLeft(4, '0');

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (FECHA_BAJA == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = Utils.CheckDate(DateBaja).ToString(FORMATO_FECHA_BAJA); ;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (ESTADO_SERVICIO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = "A";

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (USUARIO == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.CurrentUser;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (EXTENSION == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = ConfigurationManager.AppSettings("strExtensionOnBase").ToString();

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }

                    if (CODIGO_CLIENTE == Campos)
                    {
                        objKeywordRequest.Campo = Campos;
                        objKeywordRequest.Valor = oModel.strCustomerID;

                        objKeywordRequest.longitud = ConfigurationManager.AppSettings("strLongitud").ToString();
                        objKeywordRequest.codigoCampo = ConfigurationManager.AppSettings("strCodigoCampo").ToString();
                        lstKeywordRequest.Add(objKeywordRequest);

                    }


     
                    }
                }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession : " , "Message Error : ", ex.Message);
            }
            return lstKeywordRequest;
        }

        public GenerateConstancyResponseCommon PackageGenerateContancyPDF(string idSession, ParametersGeneratePDF parameters,   List<E_KeywordRequest> MethodKeywords, out string strCodigoOnBase)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            var strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");
            strCodigoOnBase = string.Empty; 

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Claro.SIACU.Web.WebApplication.IFI.App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(idSession)
            };

            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return oCommonIFIService.GetConstancyPDFWithOnbase(request);
            });

            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
            
                objResponse.bytesConstancy = objResponse.bytesConstancy; 
         
                if (MethodKeywords != null)
                {
                    UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse = new UploadDocumentOnBaseResponse();
                    UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest = new UploadDocumentOnBaseRequest()
                    {

                        listaDocumentos = new E_DocumentRequest()
                        {
                            ListaMetadatos = MethodKeywords,
                            Seq = KEY.AppSettings("strConstSEQ"),
                            CodigoOnBase = KEY.AppSettings("strCodigoOnBase"),
                            CodigoTCRM = request.ParametersGeneratePDFGeneric.strCodeTCRM,
                            strExtension = KEY.AppSettings("strExtensionOnBase"),
                            TipoDocumentoOnBase = KEY.AppSettings("TipoDocumentoOnBaseReiniciaIFI"),
                            abytArchivo = System.Convert.ToBase64String(objResponse.bytesConstancy,0, objResponse.bytesConstancy.Length)
                        },
                        audit = request.audit
                    };


                    objUploadDocumentOnBaseResponse = oCommonIFIService.GetUploadDocumentOnBase(objUploadDocumentOnBaseRequest);

                    if (objUploadDocumentOnBaseResponse != null)
                    {
                        if (objUploadDocumentOnBaseResponse.E_Document.Estado == "OK")
                        {
                            strCodigoOnBase = objUploadDocumentOnBaseResponse.E_Document.CodigoOnBase; //Código OnBase
                        }
                        else
                        {
                            objResponse.Generated = false;
                            objResponse.FullPathPDF = string.Empty;
                            objResponse.Path = string.Empty;
                            objResponse.Document = string.Empty;
                            strCodigoOnBase = string.Empty; //Código OnBase
                            Logging.Error(idSession, request.audit.transaction, "Error al registrar Onbase." + objUploadDocumentOnBaseResponse.E_Document.Estado);
                        }
                    }
                    else
                    {
                        objResponse.FullPathPDF = string.Empty;
                        objResponse.Path = string.Empty;
                        objResponse.Document = string.Empty;
                        strCodigoOnBase = string.Empty; //Código OnBase
                    }
                }

                Logging.Info(idSession, request.audit.transaction, " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
                strCodigoOnBase = string.Empty; //Código OnBase
            }


            return objResponse;
        }

        public string GetDatosporNroDocumentos(Model.IFIPackagePurchaseServicesModel oModel)
        {
            string strCargofijo = "0.00";
            Claro.Web.Logging.Info(oModel.strIdSession, "INI - GetDatosporNroDocumentos", "");

            FixedIFIService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

            string strTipoDocIdentidad = oModel.strTipoDocIdentidad == "DNI" ? "2" : "3";
            Claro.Web.Logging.Info(oModel.strIdSession, " strIdSession: ", oModel.strIdSession);
            Claro.Web.Logging.Info(oModel.strIdSession, " transaction: ", objAuditRequest.transaction);
            Claro.Web.Logging.Info(oModel.strIdSession, " strTipoDocIdentidad: ", strTipoDocIdentidad);
            Claro.Web.Logging.Info(oModel.strIdSession, " strNumeroDoc: ", oModel.strNumeroDoc);

            try
            {
                var objDatos = new List<FixedIFIService.Client>();
                objDatos = Claro.Web.Logging.ExecuteMethod<List<FixedIFIService.Client>>(() =>
                {
                    return oFixedIFIService.GetDatosporNroDocumentos(oModel.strIdSession,
                                                                                                    objAuditRequest.transaction,
                                                                                                          strTipoDocIdentidad,
                                                                                                          oModel.strNumeroDoc,
                                                                                                          "");
                });

                if (objDatos.Count > 0)
                    strCargofijo = objDatos[0].CARGO;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                strCargofijo = "0.00";

            }
            Claro.Web.Logging.Info(oModel.strIdSession, "INI - GetDatosporNroDocumentos", "");
            return strCargofijo;
        }




    }
}
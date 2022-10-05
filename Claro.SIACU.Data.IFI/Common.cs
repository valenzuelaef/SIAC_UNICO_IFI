using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Constants;
using Fun = Claro.Utils;
using Claro.SIACU.Entity.IFI;
using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Common;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;
using REGISTAUDIT = Claro.SIACU.ProxyService.IFI.RegistroAuditoriaWS;
using OPTIONAUDIT = Claro.SIACU.ProxyService.IFI.ConsultaOpcionesAuditoriaWS;
using AUDIT = Claro.SIACU.ProxyService.IFI.AuditoriaWS;
using SECURITY = Claro.SIACU.Entity.IFI.Common;
using EntitiesConsultaSeguridad = Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad;
using AUDIT_CREDENTIALS = Claro.SIACU.ProxyService.IFI.RedirectWS;
using System.Net;
using EntitiesOpcionesAuditoria = Claro.SIACU.ProxyService.IFI.ConsultaOpcionesAuditoriaWS;
using Claro.SIACU.Entity.IFI.Common.GetSaveAuditM;
using Claro.SIACU.ProxyService.IFI.ServiciosPostpagoWS;
using COMMON_IGV = Claro.SIACU.ProxyService.IFI.SIACU.ConsultIGV;
using COMMON_ENVIOCORREOSB = Claro.SIACU.ProxyService.IFI.SIACU.EnvioCorreoSB;
using GENERATEPDF = Claro.SIACU.ProxyService.IFI.WSGeneratePDF;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Claro.Web;
using Claro.SIACU.Data.IFI.Configuration;
using CONSULTA_SECURITY = Claro.SIACU.ProxyService.IFI.SIACUConsultaSeguridad;
using AUDIT_SECURITY = Claro.SIACU.ProxyService.IFI.SIACSecurity.Permissions;
using Claro.Transversal.ProxyService.ConsultaClaves;
using EntitiesCommon = Claro.SIACU.Entity.IFI.Common;
using ONBASELOAD = Claro.SIACU.ProxyService.IFI.SIACU.OnBaseLoadDocumentIFI;
using Claro.SIACU.Entity.IFI.Common.GetGenerateConstancy;
using Claro.SIACU.Entity.IFI.Postpaid;
using Service_Interaccion = Claro.SIACU.ProxyService.IFI.SIACU.IFI.TransaccionInteracciones;
using EntitiesEmail = Claro.SIACU.Entity.IFI.Common.GetSendEmailWithBase64;
using EntitiesDigitalSignature = Claro.SIACU.Entity.IFI.Common.GetDigitalSignature;
using COMMON_DIGITAL_SIGNATURE = Claro.SIACU.ProxyService.IFI.SIACU.DigitalSignature;

namespace Claro.SIACU.Data.IFI
{
    public class Common
    {
        /// <summary>
        /// Inserta evidencia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetInsertEvidence</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static SECURITY.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(SECURITY.GetInsertEvidence.InsertEvidenceRequest request)
        {
            SECURITY.GetInsertEvidence.InsertEvidenceResponse model = new SECURITY.GetInsertEvidence.InsertEvidenceResponse();

            DbParameter[] parameters = {
                new DbParameter("V_TRANSACTION_TYPE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TRANSACTION_CODE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_CUSTOMER_CODE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_PHONE_NUMBER", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TYPIFICATION_CODE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TYPIFICATION_DESC", DbType.String,200,ParameterDirection.Input),
                new DbParameter("V_COMMERCIAL_DESC", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_PRODUCT_TYPE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_SERVICE_CHANNEL", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TRANSACTION_DATE", DbType.Date,ParameterDirection.Input),
                new DbParameter("D_ACTIVATION_DATE", DbType.Date,ParameterDirection.Input),
                new DbParameter("D_SUSPENSION_DATE", DbType.Date,ParameterDirection.Input),												   
                new DbParameter("V_SERVICE_STATUS", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_USER_NAME", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_NAME",DbType.String,1000,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_TYPE",DbType.String,250,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_EXTENSION",DbType.String,250,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_PATH",DbType.String,250,ParameterDirection.Input),				
                new DbParameter("P_RESULTADO", DbType.Int32,ParameterDirection.Output),
                new DbParameter("P_MSG", DbType.String,255,ParameterDirection.Output)
            };

            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            if (request.Evidence.StrDocumentType != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrTransactionType);
            i++;
            if (request.Evidence.StrTransactionCode != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrTransactionCode);

            i++;
            if (request.Evidence.StrCustomerCode != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrCustomerCode);
            i++;
            if (request.Evidence.StrPhoneNumber != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrPhoneNumber);
            i++;

            if (request.Evidence.StrTypificationCode != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrTypificationCode);
            i++;

            if (request.Evidence.StrTypificationDesc != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrTypificationDesc);
            i++;

            if (request.Evidence.StrCommercialDesc != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrCommercialDesc);
            i++;

            if (request.Evidence.StrProductType != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrProductType);
            i++;

            if (request.Evidence.StrServiceChannel != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrServiceChannel);
            i++;

            if (request.Evidence.StrTransactionDate != null)
                parameters[i].Value = Claro.Utils.CheckDate(request.Evidence.StrTransactionDate);
            i++;

            if (request.Evidence.StrActivationDate != null)
                parameters[i].Value = Claro.Utils.CheckDate(request.Evidence.StrActivationDate);
            i++;

            if (request.Evidence.StrSuspensionDate != null)
                parameters[i].Value = Claro.Utils.CheckDate(request.Evidence.StrSuspensionDate);
            i++;

            if (request.Evidence.StrServiceStatus != null)
                parameters[i].Value = request.Evidence.StrServiceStatus;
            i++;

            if (request.Evidence.StrUserName != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrUserName);
            i++;

            if (request.Evidence.StrDocumentName != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrDocumentName);
            i++;

            if (request.Evidence.StrDocumentType != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrDocumentType);
            i++;

            if (request.Evidence.StrDocumentExtension != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrDocumentExtension);
            i++;

            if (request.Evidence.StrDocumentPath != null)
                parameters[i].Value = Claro.Utils.CheckStr(request.Evidence.StrDocumentPath);
            i++;

            try
            {
                Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(request.Audit.Session, request.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU,
                        DbCommandConfiguration.SIACU_POST_PVU_SP_INSERTAR_EVIDENCIA_A, parameters);


                });
                model.BoolResult = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                model.BoolResult = false;
            }


            var parSalida1 = string.Empty;
            var parSalida2 = string.Empty;

            parSalida1 = parameters[parameters.Length - 2].Value.ToString();
            parSalida2 = parameters[parameters.Length - 1].Value.ToString();

            model.StrFlagInsertion = Claro.Utils.CheckStr(parSalida1);
            model.StrMsgText = Claro.Utils.CheckStr(parSalida2);

            return model;
        }
        /// <summary>
        /// guarda auditoria
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vCuentaUsuario"></param>
        /// <param name="vIpCliente"></param>
        /// <param name="vIpServidor"></param>
        /// <param name="vMonto"></param>
        /// <param name="vNombreCliente"></param>
        /// <param name="vNombreServidor"></param>
        /// <param name="vServicio"></param>
        /// <param name="vTelefono"></param>
        /// <param name="vTexto"></param>
        /// <param name="vTransaccion"></param>
        /// <returns></returns>
        /// <remarks>SaveAudit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static SECURITY.GetSaveAudit.SaveAuditResponse SaveAudit(string strIdSession, string strTransaction, string vCuentaUsuario, string vIpCliente, string vIpServidor, string vMonto, string vNombreCliente, string vNombreServidor,
                                     string vServicio, string vTelefono, string vTexto, string vTransaccion)
        {

            SECURITY.GetSaveAudit.SaveAuditResponse objAuditResp = null;

            try
            {
                objAuditResp = new SECURITY.GetSaveAudit.SaveAuditResponse();
                AUDIT.EbsAuditoriaService objAuditoria = new AUDIT.EbsAuditoriaService();
                AUDIT.RegistroResponse objAudiResponse = new AUDIT.RegistroResponse();
                AUDIT.RegistroRequest objAudiRequest = new AUDIT.RegistroRequest();

                objAudiRequest.cuentaUsuario = vCuentaUsuario;
                objAudiRequest.monto = vMonto;
                objAudiRequest.servicio = vServicio;
                objAudiRequest.telefono = vTelefono;
                objAudiRequest.texto = vTexto;
                objAudiRequest.transaccion = vTransaccion;

                objAudiRequest.ipCliente = vIpCliente;
                objAudiRequest.nombreCliente = vNombreCliente;
                objAudiRequest.ipServidor = vIpServidor;
                objAudiRequest.nombreServidor = vNombreServidor;

                objAudiResponse = Claro.Web.Logging.ExecuteMethod<AUDIT.RegistroResponse>(strIdSession, strTransaction, () =>
                {
                    return Configuration.WebServiceConfiguration.GRABARAUDIT.registroAuditoria(objAudiRequest);

                });

                objAuditResp.vResultado = objAudiResponse.resultado;

                objAuditResp.vTransaccionResp = objAudiResponse.transaccion;
                objAuditResp.vestado = objAudiResponse.estado;

                if (objAudiResponse.estado == "0")
                {
                    objAuditResp.respuesta = false;
                }
                else
                {
                    objAuditResp.respuesta = true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "ERROR AUDITORIA : " + ex.Message);
            }


            return objAuditResp;
        }
        #region Generar Constancia
        /// <summary>
        /// genera constancia en pdf
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns></returns>
        /// <remarks>GenerateConstancyPDF</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GenerateConstancyPDF(string idSession, string transaction, ParametersGeneratePDF parameters, ref string strErroMsg)
        {
            bool blnResult;

            try
            {
                Claro.Web.Logging.Info(idSession, transaction, "IN GenerateConstancyPDF()");
                string xml = BuildXML_Constancy(parameters);
                Claro.Web.Logging.Info(idSession, transaction, string.Format("ConstruyeXML: {0}", xml.ToString()));
                Claro.Web.Logging.Info(idSession, transaction, "Before StrNombreArchivoTransaccion:  " + parameters.StrNombreArchivoTransaccion);
                string strDateTransaction = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strPathPDF = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
              

                string strNamePDF = string.Format("{0}_{1}_{2}.pdf", parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"));

                if (String.IsNullOrEmpty(parameters.StrCasoInter))
                {
                    parameters.StrCasoInter = "X";
                }
                Claro.Web.Logging.Info(idSession, transaction, "After strNamePDF:  " + strNamePDF);
                string pubFile = "claro-postventa.pub", strDriver = "Driver";
                GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();
                GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();

                GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();
                objGenerarPDFRequestOutput.directory = strPathPDF;
                objGenerarPDFRequestOutput.fileName = strNamePDF;

                GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;

                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF");
                objGenerarPDF.Credentials = CredentialCache.DefaultCredentials;

                GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;
                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;
                objGenerarPDFRequest.pubFile = pubFile;

                string strInputTrama = Fun.CreateXML(objGenerarPDFRequest);

                objGenerarPDFResponse = Claro.Web.Logging.ExecuteMethod<GENERATEPDF.ewsComposeResponse>(() =>
                {
                    return objGenerarPDF.Compose(objGenerarPDFRequest);
                });

                string strOutputTrama = Fun.CreateXML(objGenerarPDFResponse);

                if (objGenerarPDFResponse.statusMessage.Contains(Claro.Constants.NumberTwelveString) || objGenerarPDFResponse.files == null)
                {
                    strErroMsg = "Ha ocurrido un problema en el servicio que genera PDF, por favor intentar en otro momento.";
                    blnResult = false;
                    Claro.Web.Logging.Error(idSession, transaction, strErroMsg);
                    Claro.Web.Logging.Error(idSession, transaction, objGenerarPDFResponse.statusMessage);
                }
                else
                {
                    strErroMsg = "Se generó correctamente el archivo: " + objGenerarPDFResponse.files[0].fileName;
                    Claro.Web.Logging.Info(idSession, transaction, strErroMsg);
                    blnResult = true;
                }



            }
            catch (Exception ex)
            {
                strErroMsg = Claro.Utils.GetExceptionMessage(ex);
                Claro.Web.Logging.Error(idSession, transaction, "strErroMsg: " + strErroMsg);
                blnResult = false;
            }
            Claro.Web.Logging.Info(idSession, transaction, "OUT GenerateConstancyPDF()");
            return blnResult;
        }

        /// <summary>
        /// genera constancia pdf con onbase
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters"></param>
        /// <param name="strErroMsg"></param>
        /// <returns></returns>
        /// <remarks>GetConstancyPDFWithOnbase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static GenerateConstancyResponse GetConstancyPDFWithOnbase(string idSession, string transaction, ParametersGeneratePDF parameters, ref string strErroMsg)
        {
            GenerateConstancyResponse objConstancyResponse = new GenerateConstancyResponse();
            objConstancyResponse.Generated = false;
            byte[] databytes;
            try
            {
                Claro.Web.Logging.Info(idSession, transaction, "IN GenerateConstancyPDF()");
                string xml = BuildXML_Constancy(parameters);
                Claro.Web.Logging.Info(idSession, transaction, string.Format("ConstruyeXML: {0}", xml.ToString()));
                Claro.Web.Logging.Info(idSession, transaction, "Before StrNombreArchivoTransaccion:  " + parameters.StrNombreArchivoTransaccion);
                string strDateTransaction = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strPathPDF = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                string strNamePDF = string.Format("{0}_{1}_{2}.pdf", parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"));
                if (String.IsNullOrEmpty(parameters.StrCasoInter))
                {
                    parameters.StrCasoInter = "X";
                }
                Claro.Web.Logging.Info(idSession, transaction, "After strNamePDF:  " + strNamePDF);
                string pubFile = "claro-postventa.pub", strDriver = "Driver";
                GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();
                GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();
                GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();
                objGenerarPDFRequestOutput.directory = strPathPDF;
                objGenerarPDFRequestOutput.fileName = strNamePDF;
                GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;
                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF");
                objGenerarPDF.Credentials = CredentialCache.DefaultCredentials;
                GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;
                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;
                objGenerarPDFRequest.pubFile = pubFile;
                string strInputTrama = Fun.CreateXML(objGenerarPDFRequest);
                objGenerarPDFResponse = Claro.Web.Logging.ExecuteMethod<GENERATEPDF.ewsComposeResponse>(() =>
                {
                    return objGenerarPDF.Compose(objGenerarPDFRequest);
                });
                string strOutputTrama = Fun.CreateXML(objGenerarPDFResponse);
                if (objGenerarPDFResponse.statusMessage.Contains(Claro.Constants.NumberTwelveString) || objGenerarPDFResponse.files == null)
                {
                    strErroMsg = "Ha ocurrido un problema en el servicio que genera PDF, por favor intentar en otro momento.";
                    objConstancyResponse.Generated = false;
                    Claro.Web.Logging.Error(idSession, transaction, strErroMsg);
                    Claro.Web.Logging.Error(idSession, transaction, objGenerarPDFResponse.statusMessage);
                }
                else
                {
                    strErroMsg = "Se generó correctamente el archivo: " + objGenerarPDFResponse.files[0].fileName;
                    Claro.Web.Logging.Info(idSession, transaction, strErroMsg);
                    string strServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
                    string strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");
                    string strNameResultPDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", strServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);
                    objConstancyResponse.Document = Convert.ToString(objGenerarPDFRequestDriver.driver);
                    objConstancyResponse.FullPathPDF = strNameResultPDF;
                    objConstancyResponse.Path = objGenerarPDFRequestOutput.directory;
                    objConstancyResponse.Generated = true;
                    objConstancyResponse.Drive = objGenerarPDFRequestDriver.driver;
                    bool dfsf = DisplayFileFromServerSharedFile(idSession, transaction, strNameResultPDF, out databytes);

                    objConstancyResponse.bytesConstancy = dfsf ? databytes : null;

                }
            }
            catch (Exception ex)
            {
                strErroMsg = Claro.Utils.GetExceptionMessage(ex);
                Claro.Web.Logging.Error(idSession, transaction, "strErroMsg: " + strErroMsg);
                objConstancyResponse.Generated = false;
            }
            Claro.Web.Logging.Info(idSession, transaction, "OUT GenerateConstancyPDF()");
            return objConstancyResponse;
        }

        /// <summary>
        /// crea xml de cancelacion
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy_Cancelation</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Constancy_Cancelation(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_ATENCION>" + DateTime.Today.ToString("dd/MM/yyyy") + "</FECHA_ATENCION>";
            xml += "<NUMERO_SERVICIO>" + parameters.StrNroServicio + "</NUMERO_SERVICIO>";
            xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
            xml += "<ID_INTERACCION>" + parameters.StrIdInteraccion + "</ID_INTERACCION>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<TIPO_DOC>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC>";
            xml += "<NRO_DOC>" + parameters.StrNroDocIdentidad + "</NRO_DOC>";
            xml += "<SEGMENTO_CLIENTE>" + parameters.StrSegmento + "</SEGMENTO_CLIENTE>";
            xml += "<TELEFONO_REFERENCIA>" + parameters.StrTelfReferencia + "</TELEFONO_REFERENCIA>";
            xml += "<TRANSACCION>" + parameters.strtransaccion + "</TRANSACCION>";
            xml += "<RESULTADO>" + parameters.StrResultado + "</RESULTADO>";
            xml += "<MOTIVO>" + parameters.strMotivo + "</MOTIVO>";
            xml += "<SUB_MOTIVO>" + parameters.StrSubMotivoCancel + "</SUB_MOTIVO>";
            xml += "<ACCION>" + parameters.StrAccionIFI + "</ACCION>";
            xml += "<FECHA_CANCELACION>" + parameters.StrFechaCancel + "</FECHA_CANCELACION>";
            xml += "<REINTEGRO>" + parameters.strMontoReintegro + "</REINTEGRO>";
            xml += "<ENVIO_CORREO>" + parameters.strEnvioCorreo + "</ENVIO_CORREO>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "<VALIDADOR_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</VALIDADOR_FIRMA_DIGITAL>";
            xml += "</PLANTILLA>";

            return xml;
        }


        /// <summary>
        /// crea xml de bloqueo de servicio
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy_LockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Constancy_LockService(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_SOLICITUD>" + DateTime.Today.ToString("dd/MM/yyyy") + "</FECHA_SOLICITUD>";
            xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<NRO_SERVICIO>" + parameters.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<COD_BLOQ>" + parameters.strCodigoBloqueo + "</COD_BLOQ>";
            xml += "<SOLICITANTE>" + parameters.strSolicitante + "</SOLICITANTE>";
            xml += "<MOTIVOS>" + parameters.strMotivo + "</MOTIVOS>";
            xml += "<NOMBRES_SOLICITANTE>" + parameters.strSolicitanteNombre + "</NOMBRES_SOLICITANTE>";
            xml += "<APELLIDOS_SOLICITANTE>" + parameters.strSolicitanteApellido + "</APELLIDOS_SOLICITANTE>";
            xml += "<TIPO_DOC_SOLICITANTE>" + parameters.strSolicitanteTipoDoc + "</TIPO_DOC_SOLICITANTE>";
            xml += "<NRO_DOC_SOLICITANTE>" + parameters.strSolicitanteNroDoc + "</NRO_DOC_SOLICITANTE>";
            xml += "<NUMERO_SOLICITANTE>" + parameters.strSolicitanteNro + "</NUMERO_SOLICITANTE>";
            xml += "<LINEA>" + parameters.strLinea + "</LINEA>";
            xml += "<IMEI>" + parameters.StrImei + "</IMEI>";
            xml += "<MARCA_MODELO>" + parameters.strMarcaModelo + "</MARCA_MODELO>";
            xml += "<ENVIAR_CORREO>" + parameters.strEnvioCorreo + "</ENVIAR_CORREO>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<FLAG_BLOQUEO>" + parameters.strFlagBloqueo + "</FLAG_BLOQUEO>";
            xml += "<FLAG_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</FLAG_FIRMA_DIGITAL>";
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";

            xml += "</PLANTILLA>";

            return xml;
        }

        /// <summary>
        /// crea xml de desbloqueo de servicio
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy_UnLockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Constancy_UnLockService(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>SOLICITUD_DESBLOQUEO_LINEA_EQUIPO</FORMATO_TRANSACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_SOLICITUD>" + DateTime.Today.ToString("dd/MM/yyyy") + "</FECHA_SOLICITUD>";
            xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<NRO_SERVICIO>" + parameters.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<COD_DESBLOQ>" + parameters.strCodigoBloqueo + "</COD_DESBLOQ>";
            xml += "<SOLICITANTE>" + parameters.strSolicitante + "</SOLICITANTE>";
            xml += "<MOTIVOS>" + parameters.strMotivo + "</MOTIVOS>";
            xml += "<NOMBRES_SOLICITANTE>" + parameters.strSolicitanteNombre + "</NOMBRES_SOLICITANTE>";
            xml += "<APELLIDOS_SOLICITANTE>" + parameters.strSolicitanteApellido + "</APELLIDOS_SOLICITANTE>";
            xml += "<TIPO_DOC_SOLICITANTE>" + parameters.strSolicitanteTipoDoc + "</TIPO_DOC_SOLICITANTE>";
            xml += "<NRO_DOC_SOLICITANTE>" + parameters.strSolicitanteNroDoc + "</NRO_DOC_SOLICITANTE>";
            xml += "<NUMERO_SOLICITANTE>" + parameters.strSolicitanteNro + "</NUMERO_SOLICITANTE>";
            xml += "<LINEA>" + parameters.strLinea + "</LINEA>";
            xml += "<IMEI>" + parameters.StrImei + "</IMEI>";
            xml += "<MARCA_MODELO>" + parameters.strMarcaModelo + "</MARCA_MODELO>";
            xml += "<ENVIAR_CORREO>" + parameters.strEnvioCorreo + "</ENVIAR_CORREO>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<FLAG_DESBLOQUEO>" + parameters.strFlagBloqueo + "</FLAG_DESBLOQUEO>";
            xml += "<FLAG_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</FLAG_FIRMA_DIGITAL>";
            //huella digital ini

            xml += "<FLAG_FIRMA_DIGITAL>" + parameters.strFirmaDigitalHuella + "</FLAG_FIRMA_DIGITAL>";
            xml += "<HUELLA_MINUCIA_CIENTE>" + parameters.strHuellaMinucia + "</HUELLA_MINUCIA_CIENTE>";
            xml += "<HUELLA_ENCODE>" + parameters.strHuellaEncode + "</HUELLA_ENCODE>";
            xml += "<FECHA_HORA>" + parameters.strHuellaFechaHora + "</FECHA_HORA>";
            xml += "<NRO_DOCUMENTO>" + parameters.strHuellaNroDoc + "</NRO_DOCUMENTO>";
            xml += "<TIPO_DOCUMENTO>" + parameters.strHuellaTipDoc + "</TIPO_DOCUMENTO>";
            xml += "<NOMBRES_APELLIDOS_CLIENTE>" + parameters.strHuellaNomApeCli + "</NOMBRES_APELLIDOS_CLIENTE>";

            //huella digital fin
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";

            xml += "</PLANTILLA>";

            return xml;
        }

        /// <summary>
        /// crea xml de retencion
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy_Retencion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Constancy_Retencion(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_ATENCION>" + DateTime.Today.ToString("dd/MM/yyyy") + "</FECHA_ATENCION>";
            xml += "<NUMERO_SERVICIO>" + parameters.StrNroServicio + "</NUMERO_SERVICIO>";
            xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
            xml += "<ID_INTERACCION>" + parameters.StrIdInteraccion + "</ID_INTERACCION>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<TIPO_DOC>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC>";
            xml += "<NRO_DOC>" + parameters.StrNroDocIdentidad + "</NRO_DOC>";
            xml += "<SEGMENTO_CLIENTE>" + parameters.StrSegmento + "</SEGMENTO_CLIENTE>";
            xml += "<TELEFONO_REFERENCIA>" + parameters.StrTelfReferencia + "</TELEFONO_REFERENCIA>";
            xml += "<TRANSACCION>" + parameters.strtransaccion + "</TRANSACCION>";
            xml += "<RESULTADO>" + parameters.StrResultado + "</RESULTADO>";
            xml += "<MOTIVO>" + parameters.strMotivo + "</MOTIVO>";
            if (parameters.StrSubMotivoCancel != "Seleccionar" || parameters.StrSubMotivoCancel != String.Empty)
            {
                xml += "<SUB_MOTIVO>" + parameters.StrSubMotivoCancel + "</SUB_MOTIVO>";
            }
            else
            {
                xml += "<SUB_MOTIVO>" + " " + "</SUB_MOTIVO>";
            }
            xml += "<ACCION>" + parameters.StrAccionIFI + "</ACCION>";
            xml += "<REINTEGRO>" + parameters.strMontoReintegro + "</REINTEGRO>";
            xml += "<ENVIO_CORREO>" + parameters.strEnvioCorreo + "</ENVIO_CORREO>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "<VALIDADOR_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</VALIDADOR_FIRMA_DIGITAL>";
            xml += "</PLANTILLA>";

            return xml;
        }



        /// <summary>Permite construir el xml para la plantilla de afiliacion a recibo por correo</summary>
        /// <param name="parameters"></param>   
        /// <returns>string</returns>
        /// <remarks>BuildXML_Mail_Receipt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>27/12/2018.</FecCrea></item></list>
        ///  <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Mail_Receipt(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_ATENCION>" + DateTime.Today.ToString("dd/MM/yyyy") + "</FECHA_ATENCION>";
            xml += "<NRO_SERVICIO>" + parameters.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            //xml += "<SEGMENTO>" + parameters.StrSegmento + "</SEGMENTO>";
            xml += "<SEGMENTO_CLIENTE>" + parameters.StrSegmento + "</SEGMENTO_CLIENTE>";
            xml += "<TELEFONO_REFERENCIA>" + parameters.StrTelfReferencia + "</TELEFONO_REFERENCIA>";
            xml += "<TRANSACCION>" + parameters.strtransaccion + "</TRANSACCION>";
            xml += "<ACCION>" + parameters.StrAccionIFI + "</ACCION>";
            xml += "<TELEFONO_CONFIRMACION>" + parameters.StrTelfConfirmacion + "</TELEFONO_CONFIRMACION>";
            xml += "<FECHA_AFILIACION>" + parameters.StrFecAfiliacion + "</FECHA_AFILIACION>";

            xml += "<DIRECCION>" + parameters.StrDireccion + "</DIRECCION>";
            xml += "<DISTRITO>" + parameters.StrDistrito + "</DISTRITO>";
            xml += "<PROVINCIA>" + parameters.StrProvincia + "</PROVINCIA>";
            xml += "<DEPARTAMENTO>" + parameters.StrDepartamento + "</DEPARTAMENTO>";
            xml += "<REFERENCIA>" + parameters.StrReferenciaActual + "</REFERENCIA>";

            xml += "<EMAIL_AFILIADO>" + parameters.strCorreoCliente + "</EMAIL_AFILIADO>";
            xml += "<ENVIAR_CORREO>" + parameters.strEnvioCorreo + "</ENVIAR_CORREO>";
            xml += "<CORREO>" + parameters.StrEmail + "</CORREO>";
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "<FLAG_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</FLAG_FIRMA_DIGITAL>";

            xml += "<TIPO_TRANSACCION>" + parameters.StrTipoTransaccion + "</TIPO_TRANSACCION>";

            xml += "</PLANTILLA>";

            return xml;
        }

        /// <summary>
        ///  Método que construye XML general
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Constancy(ParametersGeneratePDF parameters)
        {           
            if (parameters.StrNombreArchivoTransaccion.Equals("SOLICITUD_DE_CANCELACION_DE_SERVICIO"))
            {
                return BuildXML_Constancy_Cancelation(parameters);
            }
            if (parameters.StrNombreArchivoTransaccion.Equals("SOLICITUD_CONSTANCIA_RETENCION"))
            {
                return BuildXML_Constancy_Retencion(parameters);
            }
            if (parameters.StrNombreArchivoTransaccion.Equals("SOLICITUD_BLOQUEO_LINEA_EQUIPO"))
            {
                return BuildXML_Constancy_LockService(parameters);
            }
            if (parameters.StrNombreArchivoTransaccion.Equals("DESBLOQUEO_SERVICIO"))
            {
                return BuildXML_Constancy_UnLockService(parameters);
            }
            
            if (parameters.StrNombreArchivoTransaccion.Equals("DESISTIMIENTO_DE_CAMBIO_DE_PLAN_IFI"))
            {
                return BuildXML_Constancy_Desist_PlanMigration(parameters);
            }
            if (parameters.StrNombreArchivoTransaccion.Equals("CONSTANCIA_ADQUISICION_REINICIA_VELOCIDAD"))
            {
                return BuildXML_Constancy_RestarSpeed(parameters);
            }
            
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<NRO_SERVICIO>" + parameters.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TITULAR_CLIENTE>" + parameters.StrTitularCliente + "</TITULAR_CLIENTE>";
            xml += "<CONTACTO_CLIENTE>" + parameters.StrContactoCliente + "</CONTACTO_CLIENTE>";
            xml += "<PLAN_ACTUAL>" + parameters.StrPlanActual + "</PLAN_ACTUAL>";
            xml += "<CENTRO_ATENCION_AREA>" + parameters.StrCentroAtencionArea + "</CENTRO_ATENCION_AREA>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<TELEFONO_CONFIRMACION>" + parameters.StrTelfConfirmacion + "</TELEFONO_CONFIRMACION>";
            xml += "<FECHA_AFILIACION>" + parameters.StrFecAfiliacion + "</FECHA_AFILIACION>";

            xml += "<SEGMENTO_CLIENTE>" + parameters.StrSegmento + "</SEGMENTO_CLIENTE>";
            xml += "<TELEFONO_REFERENCIA>" + parameters.StrTelfReferencia + "</TELEFONO_REFERENCIA>";
            xml += "<TRANSACCION>" + parameters.strtransaccion + "</TRANSACCION>";
            xml += "<MOTIVO>" + parameters.strMotivo + "</MOTIVO>";
            xml += "<SUB_MOTIVO>" + parameters.StrSubMotivoCancel + "</SUB_MOTIVO>";
            xml += "<FECHA_TRANSACCION_PROGRAM>" + parameters.StrFechaTransaccionProgram + "</FECHA_TRANSACCION_PROGRAM>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";
            xml += "<REPRES_LEGAL>" + parameters.StrRepresLegal + "</REPRES_LEGAL>";
            xml += "<NRO_DOC_REP_LEGAL>" + parameters.StrNroDocRepLegal + "</NRO_DOC_REP_LEGAL>";
            xml += "<CUSTOMER_ID>" + parameters.StrCustomerId + "</CUSTOMER_ID>";
            xml += "<CUENTA_POSTPAGO>" + parameters.StrCuentaPostpago + "</CUENTA_POSTPAGO>";
            xml += "<NOTAS>" + parameters.StrNotas + "</NOTAS>";
            xml += "<TELF_REFERENCIA>" + parameters.StrTelfReferencia + "</TELF_REFERENCIA>";
            xml += "<CICLO_FACTURACION>" + parameters.StrCicloFacturacion + "</CICLO_FACTURACION>";
            xml += "<FECHA_EJECUCION>" + parameters.StrFechaEjecucion + "</FECHA_EJECUCION>";
            xml += "<COD_USUARIO>" + parameters.StrCodUsuario + "</COD_USUARIO>";
            xml += "<NOMBRE_AGENTE_USUARIO>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_AGENTE_USUARIO>";
            xml += "<APLICA_EMAIL>" + parameters.StrAplicaEmail + "</APLICA_EMAIL>";
            xml += "<EMAIL>" + parameters.StrEmail + "</EMAIL>";
            xml += "<APLICA_OTRO_CONTACTO>" + parameters.StrAplicaOtroContacto + "</APLICA_OTRO_CONTACTO>";
            xml += "<CONTACTO_OTRO>" + parameters.StrContactoOtro + "</CONTACTO_OTRO>";
            xml += "<NRO_DOC_CONTACTO_OTRO>" + parameters.StrNroDocContactoOtro + "</NRO_DOC_CONTACTO_OTRO>";
            xml += "<MOTIVO_PARIENTE>" + parameters.StrMotivoPariente + "</MOTIVO_PARIENTE>";
            xml += "<TELF_OTRO_CONTACTO>" + parameters.StrTelfOtroContacto + "</TELF_OTRO_CONTACTO>";
            xml += "<CANAL_ATENCION>" + parameters.StrCanalAtencion + "</CANAL_ATENCION>";
            xml += "<FLAG_PLANTILLA_PLAZO>" + parameters.StrFlagPlantillaPlazo + "</FLAG_PLANTILLA_PLAZO>";
            xml += "<ESCENARIO_SERVICIO_COM>" + parameters.StrEscenarioServicioCom + "</ESCENARIO_SERVICIO_COM>";
            xml += "<APLICA_PROGRAMACION>" + parameters.StrAplicaProgramacion + "</APLICA_PROGRAMACION>";
            xml += "<CF_SERVICIO_COM>" + parameters.StrCfServicioCom + "</CF_SERVICIO_COM>";
            xml += "<FECHA_PLAZO>" + parameters.StrFechaPlazo + "</FECHA_PLAZO>";
            xml += "<PLAZO>" + parameters.StrPlazo + "</PLAZO>";
            xml += "<CANAL_ATENCION>" + parameters.StrCanalAtencion + "</CANAL_ATENCION>";

            xml += "<BOLSA_SOLES_ADICIONALES>" + parameters.StrBolsaSolesAdicionales + "</BOLSA_SOLES_ADICIONALES>";
            xml += "<IMEI>" + parameters.StrImei + "</IMEI>";
            xml += "<MARCA_EQUIPO>" + parameters.StrMarcaEquipo + "</MARCA_EQUIPO>";
            xml += "<MODELO_EQUIPO>" + parameters.StrModeloEquipo + "</MODELO_EQUIPO>";
            xml += "<TRANSACCION_BLOQUEO>" + parameters.StrTransaccionBloqueo + "</TRANSACCION_BLOQUEO>";
            xml += "<MOTIVO_TIPO_BLOQUEO>" + parameters.StrMotivoTipoBloqueo + "</MOTIVO_TIPO_BLOQUEO>";
            xml += "<TOPE_CONSUMO>" + parameters.StrTopeConsumo + "</TOPE_CONSUMO>";
            xml += "<FECHA_EJEC_TOPE_CONS>" + parameters.StrFechaEjecTopeCons + "</FECHA_EJEC_TOPE_CONS>";
            xml += "<FLAG_GRILA_ATP>" + parameters.StrFlagGrilaAtp + "</FLAG_GRILA_ATP>";
            xml += "<ESCENARIO_MIGRACION>" + parameters.StrEscenarioMigracion + "</ESCENARIO_MIGRACION>";
            xml += "<NUEVO_PLAN>" + parameters.StrNuevoPlan + "</NUEVO_PLAN>";
            xml += "<CF_TOTAL_NUEVO>" + parameters.StrCfTotalNuevo + "</CF_TOTAL_NUEVO>";
            xml += "<MONTO_APADECE>" + parameters.StrMontoApadece + "</MONTO_APADECE>";
            xml += "<MONTO_PCS>" + parameters.StrMontoPcs + "</MONTO_PCS>";
            xml += "<MOTIVO_CANCELACION>" + parameters.StrMotivoCancelacion + "</MOTIVO_CANCELACION>";
            xml += "<ESCENARIO_RETENCION>" + parameters.StrEscenarioRetencion + "</ESCENARIO_RETENCION>";
            xml += "<ACCION_RETENCION>" + parameters.StrAccionRetencion + "</ACCION_RETENCION>";
            xml += "<MODALIDAD>" + parameters.StrModalidad + "</MODALIDAD>";
            xml += "<PRODUCTOS>" + parameters.StrProductos + "</PRODUCTOS>";

            xml += "<PUNTOS_CC_ANTES_TRANS>" + parameters.StrPuntosCcAntesTrans + "</PUNTOS_CC_ANTES_TRANS>";
            xml += "<CANTIDAD_TOTAL_CANJE_DEV>" + parameters.StrCantidadTotalCanjeDev + "</CANTIDAD_TOTAL_CANJE_DEV>";
            xml += "<TIPO_DOC_FACT>" + parameters.StrTipoDocFact + "</TIPO_DOC_FACT>";
            xml += "<NRO_DOC_FACT>" + parameters.StrNroDocFact + "</NRO_DOC_FACT>";
            xml += "<DIRECCION_POSTAL>" + parameters.StrDireccionPostal + "</DIRECCION_POSTAL>";
            xml += "<DISTRITO_POSTAL>" + parameters.StrDistritoPostal + "</DISTRITO_POSTAL>";
            xml += "<PROVINCIA_POSTAL>" + parameters.StrProvinciaPostal + "</PROVINCIA_POSTAL>";
            xml += "<DEPARTAMENTO_POSTAL>" + parameters.StrDepartamentoPostal + "</DEPARTAMENTO_POSTAL>";
            xml += "<FECHA_EMISION_DOC_FACT>" + parameters.StrFechaEmisionDocFact + "</FECHA_EMISION_DOC_FACT>";
            xml += "<FECHA_VENC_DOC_FACT>" + parameters.StrFechaVencDocFact + "</FECHA_VENC_DOC_FACT>";

            xml += "<IMPORTE_CONCEPTO_AJUSTE_SIN_IGV>" + parameters.StrImporteConceptoAjusteSinIgv + "</IMPORTE_CONCEPTO_AJUSTE_SIN_IGV>";
            xml += "<SUBTOTAL_AJUSTE_SIN_IGV>" + parameters.StrSubtotalAjusteSinIgv + "</SUBTOTAL_AJUSTE_SIN_IGV>";
            xml += "<IGV_TAX>" + parameters.StrIgvTax + "</IGV_TAX>";
            xml += "<TOTAL_CON_IGV>" + parameters.StrTotalConIgv + "</TOTAL_CON_IGV>";
            xml += "<TOTAL_AJUSTE>" + parameters.StrTotalAjuste + "</TOTAL_AJUSTE>";
            xml += "<MOTIVO_CAMBIO_SIM>" + parameters.StrMotivoCambioSim + "</MOTIVO_CAMBIO_SIM>";
            xml += "<NUEVO_SIM>" + parameters.StrNuevoSim + "</NUEVO_SIM>";
            xml += "<COSTO_TRANSACCION>" + parameters.StrCostoTransaccion + "</COSTO_TRANSACCION>";
            xml += "<FLAG_4G>" + parameters.StrFlag4G + "</FLAG_4G>";
            xml += "<SIM_4G_LTE>" + parameters.StrSim4GLte + "</SIM_4G_LTE>";
            xml += "<ESTADO_SERVICIO_4G>" + parameters.StrEstadoServicio4G + "</ESTADO_SERVICIO_4G>";
            xml += "<NRO_DOC_REF>" + parameters.StrNroDocIdentidadRef + "</NRO_DOC_REF>";
            xml += "<FECHA_EMISION_DOC_REF>" + parameters.StrFechaEmisionDocRef + "</FECHA_EMISION_DOC_REF>";
            xml += "<COD_DESBLOQ>" + parameters.StrCodDesbloqueo + "</COD_DESBLOQ>";
            xml += "<PAIS_POSTAL>" + parameters.StrPaisA + "</PAIS_POSTAL>";
            xml += "<CODIGO_POSTAL>" + parameters.StrCodigoLocalA + "</CODIGO_POSTAL>";
            xml += "<CENTRO_POBLADO_ACTUAL>" + parameters.StrCentroPobladoActual + "</CENTRO_POBLADO_ACTUAL>";
            xml += "<REFERENCIA>" + parameters.StrReferenciaActual + "</REFERENCIA>";
            xml += "<DIRECCION_DESTINO>" + parameters.StrDireccionPostalC + "</DIRECCION_DESTINO>";
            xml += "<DESCRIP_TRANSACCION>" + parameters.StrDescripTransaccion + "</DESCRIP_TRANSACCION>";
            xml += "<REFERENCIA_DESTINO>" + parameters.StrReferenciaDestino + "</REFERENCIA_DESTINO>";
            xml += "<DEPARTAMENTO_DESTINO>" + parameters.StrDepartamentoLocalB + "</DEPARTAMENTO_DESTINO>";
            xml += "<DISTRITO_DESTINO>" + parameters.StrDistrtitoLocalB + "</DISTRITO_DESTINO>";
            xml += "<CODIGO_POSTAL_DESTINO>" + parameters.StrCodigoLocalB + "</CODIGO_POSTAL_DESTINO>";
            xml += "<PAIS_DESTINO>" + parameters.StrPaisB + "</PAIS_DESTINO>";
            xml += "<PROVINCIA_DESTINO>" + parameters.StrProvinciaLocalB + "</PROVINCIA_DESTINO>";
            xml += "<CENTRO_POBLADO_DESTINO>" + parameters.StrCentroPobladoDestino + "</CENTRO_POBLADO_DESTINO>";
            xml += "<CORREO_SOLICITUD>" + parameters.StrEmail + "</CORREO_SOLICITUD>";
            xml += "<APLICA_CAMBIO_DIREC>" + parameters.StrAplicaCambioDireccion + "</APLICA_CAMBIO_DIREC>";
            xml += "<APLICA_CAMBIO_NOMBRE>" + parameters.StrAplicaCambioNombre + "</APLICA_CAMBIO_NOMBRE>";
            xml += "<FECHA_SUSP>" + parameters.StrFechaSuspension + "</FECHA_SUSP>";
            xml += "<FECHA_ACTIVACION>" + parameters.StrFechaActivacion + "</FECHA_ACTIVACION>";
            xml += "<COSTO_REACTIVACION>" + parameters.StrCostoReactivacion + "</COSTO_REACTIVACION>";
            xml += "<FLAG_TRASLADO>" + parameters.StrFlagExterInter + "</FLAG_TRASLADO>";
            xml += "<FECHA_AUTORIZACION>" + parameters.StrFechaTransaccionProgram + "</FECHA_AUTORIZACION>";

            xml += "<ACCION_EJECUTAR>" + parameters.strAccionEjecutar + "</ACCION_EJECUTAR>";
            xml += "<NRO_ANTERIOR>" + parameters.strNroAnterior + "</NRO_ANTERIOR>";
            xml += "<NRO_NUEVO>" + parameters.strNroNuevo + "</NRO_NUEVO>";
            xml += "<LOCUCION>" + parameters.strLocucion + "</LOCUCION>";
            xml += "<COSTO_LOCUCION>" + parameters.strCostoLocucion + "</COSTO_LOCUCION>";
            xml += "<DURACION_LOCUCION>" + parameters.strDuracionLocucion + "</DURACION_LOCUCION>";
            xml += "<ENVIO_CORREO>" + parameters.strEnvioCorreo + "</ENVIO_CORREO>";
            //made13
            xml += "<CONTRATO>" + parameters.strContrato + "</CONTRATO>";
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "<VALIDADOR_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</VALIDADOR_FIRMA_DIGITAL>";
            //made13

            if (!String.IsNullOrEmpty(parameters.strPuntoDeAtencion))
                xml += "<PUNTO_DE_ATENCION>" + parameters.strPuntoDeAtencion + "</PUNTO_DE_ATENCION>";

            xml += "<NRO_DOC>" + parameters.strNroDoc + "</NRO_DOC>";
            xml += "<FECHA_TRANSACCION>" + parameters.strFechaTransaccion + "</FECHA_TRANSACCION>";
            xml += "<CASO_INTERACCION>" + parameters.strCasoInteraccion + "</CASO_INTERACCION>";
            xml += "<TRANSACCION_DESCRIPCION>" + parameters.strTransaccionDescripcion + "</TRANSACCION_DESCRIPCION>";
            xml += "<COSTO_TRANSACCIÓN>" + parameters.strCostoTransaccion + "</COSTO_TRANSACCIÓN>";
            xml += "<DIRECCION_CLIENTE_ACTUAL>" + parameters.strDireccionClienteActual + "</DIRECCION_CLIENTE_ACTUAL>";
            xml += "<REFERENCIA_TRANSACCION_ACTUAL>" + parameters.strRefTransaccionActual + "</REFERENCIA_TRANSACCION_ACTUAL>";
            xml += "<DISTRITO_CLIENTE_ACTUAL>" + parameters.strDistritoClienteActual + "</DISTRITO_CLIENTE_ACTUAL>";
            xml += "<CODIGO_POSTAL_ACTUAL>" + parameters.strCodigoPostalActual + "</CODIGO_POSTAL_ACTUAL>";
            xml += "<PAIS_CLIENTE_ACTUAL>" + parameters.strPaisClienteActual + "</PAIS_CLIENTE_ACTUAL>";
            xml += "<PROVINCIA_CLIENTE_ACTUAL>" + parameters.strProvClienteActual + "</PROVINCIA_CLIENTE_ACTUAL>";
            xml += "<DIRECCION_CLIENTE_DESTINO>" + parameters.strDirClienteDestino + "</DIRECCION_CLIENTE_DESTINO>";
            xml += "<REFERENCIA_TRANSACCION_DESTINO>" + parameters.strRefTransaccionDestino + "</REFERENCIA_TRANSACCION_DESTINO>";
            xml += "<DEPARTAMENTO_CLIENTE_DESTINO>" + parameters.strDepClienteDestino + "</DEPARTAMENTO_CLIENTE_DESTINO>";
            xml += "<DISTRITO_CLIENTE_DESTINO>" + parameters.strDistClienteDestino + "</DISTRITO_CLIENTE_DESTINO>";
            xml += "<APLICA_CAMBIO_DIR_FACT>" + parameters.strAplicaCambioDirFact + "</APLICA_CAMBIO_DIR_FACT>";
            xml += "<CODIGO_POSTALL_DESTINO>" + parameters.strCodigoPostallDestino + "</CODIGO_POSTALL_DESTINO>";
            xml += "<PAIS_CLIENTE_DESTINO>" + parameters.strPaisClienteDestino + "</PAIS_CLIENTE_DESTINO>";
            xml += "<PROVINCIA_CLIENTE_DESTINO>" + parameters.strProvClienteDestino + "</PROVINCIA_CLIENTE_DESTINO>";
            xml += "<CODIGO_PLANO_DESTINO>" + parameters.strCodigoPlanoDestino + "</CODIGO_PLANO_DESTINO>";
            xml += "<ENVIO_MAIL>" + parameters.strEnviomail + "</ENVIO_MAIL>";
            xml += "<CORREO_CLIENTE>" + parameters.strCorreoCliente + "</CORREO_CLIENTE>";
            xml += "<FLAG_TIPO_TRASLADO>" + parameters.strflagTipoTraslado + "</FLAG_TIPO_TRASLADO>";
            xml += "<CENTRO_ATENCIÓN>" + parameters.strCentroAtencion + "</CENTRO_ATENCIÓN>";
            xml += "<CONTRATO_CLIENTE>" + parameters.strContratoCliente + "</CONTRATO_CLIENTE>";
            xml += "<DEPARTAMENTO_CLIENTE_ACTUAL>" + parameters.strDepClienteActual + "</DEPARTAMENTO_CLIENTE_ACTUAL>";

            xml += "<FEC_INICIAL_REPORTE>" + parameters.StrFecInicialReporte + "</FEC_INICIAL_REPORTE>";
            xml += "<FEC_FINAL_REPORTE>" + parameters.StrFecFinalReporte + "</FEC_FINAL_REPORTE>";
            xml += "<MONTO_OCC>" + parameters.StrMontoOCC + "</MONTO_OCC>";
            xml += "<CONTENIDO_COMERCIAL>" + parameters.StrContenidoComercial + "</CONTENIDO_COMERCIAL>";
            xml += "<CONTENIDO_COMERCIAL2>" + parameters.StrContenidoComercial2 + "</CONTENIDO_COMERCIAL2>";

            xml += "<TITULO_INSTALACION>" + parameters.StrTituloInstalacion + "</TITULO_INSTALACION>";
            xml += "<TITULO_DESINSTALACION>" + parameters.StrTituloDesinstalacion + "</TITULO_DESINSTALACION>";
            xml += "<NRO_CONTRATO>" + parameters.StrNumeroContrato + "</NRO_CONTRATO>";
            xml += "<FLAG_TIPO_DECO>" + parameters.StrFlagTipoDeco + "</FLAG_TIPO_DECO>";
            xml += "<DIRECCION>" + parameters.StrDireccion + "</DIRECCION>";
            xml += "<NOTAS_DIRECCION>" + parameters.StrNotasDireccion + "</NOTAS_DIRECCION>";
            xml += "<DEPARTAMENTO>" + parameters.StrDepartamento + "</DEPARTAMENTO>";
            xml += "<DISTRITO>" + parameters.StrDistrito + "</DISTRITO>";
            xml += "<PAIS>" + parameters.StrPais + "</PAIS>";
            xml += "<PROVINCIA>" + parameters.StrProvincia + "</PROVINCIA>";
            xml += "<CODIGO_PLANO>" + parameters.StrCodigoPlano + "</CODIGO_PLANO>";
            xml += "<FECHA_COMPROMISO>" + parameters.StrFechaCompromiso + "</FECHA_COMPROMISO>";
            xml += "<X_ETIQUETA_2>" + parameters.StrXEtiqueta2 + "</X_ETIQUETA_2>";
            xml += "<NOMBRE_SERVICIO>" + parameters.StrNombreServicio + "</NOMBRE_SERVICIO>";

            if (parameters.ListDecoder != null)
                foreach (var item in parameters.ListDecoder)
                {
                    xml += "<NOMBRE_EQUIPO>" + item.StrNombreEquipo + "</NOMBRE_EQUIPO>";
                    xml += "<TIPO_SERVICIO>" + item.StrTipoServicio + "</TIPO_SERVICIO>";
                    xml += "<CARGO_FIJO_SIN_IGV>" + item.StrCargoFijoSinIGV + "</CARGO_FIJO_SIN_IGV>";
                }

            xml += "<TIPO_EQUIPO>" + parameters.StrTipoEquipo + "</TIPO_EQUIPO>";
            xml += "<GRUPO_SERVICIO>" + parameters.StrGrupoServicio + "</GRUPO_SERVICIO>";
            xml += "<CARGO_FIJO>" + parameters.StrCargoFijo + "</CARGO_FIJO>";
            xml += "<CANTIDAD_INSTALAR>" + parameters.StrCantidadInstalar + "</CANTIDAD_INSTALAR>";
            xml += "<CANTIDAD_DESINSTALAR>" + parameters.StrCantidadDesinstalar + "</CANTIDAD_DESINSTALAR>";
            xml += "<CARGO_FIJO_CON_IGV>" + parameters.StrCargoFijoConIGV + "</CARGO_FIJO_CON_IGV>";
            xml += "<FIDELIZAR>" + parameters.StrFidelizar + "</FIDELIZAR>";
            xml += "<COSTO_INSTALACION>" + parameters.StrCostoInstalacion + "</COSTO_INSTALACION>";
            xml += "<COSTO_DESINSTALACION>" + parameters.StrCostoDesinstalacion + "</COSTO_DESINSTALACION>";
            xml += "<ENVIAR_EMAIL>" + parameters.StrEnviarEmail + "</ENVIAR_EMAIL>";
            xml += "<TEXTO>" + parameters.StrTexto + "</TEXTO>";
            xml += "<SUB_MOTIVO_CANCELACION>" + parameters.StrSubMotivoCancel + "</SUB_MOTIVO_CANCELACION>";
            xml += "<ACCIONES_OFRECIDAS>" + parameters.StrAccion + "</ACCIONES_OFRECIDAS>";
            xml += "<SEGMENTO>" + parameters.StrSegmento + "</SEGMENTO>";
            xml += "<DIRECCION_INSTALACION>" + parameters.strDireccionInstalac + "</DIRECCION_INSTALACION>";
            xml += "<CANTIDAD_PUNTOS>" + parameters.StrCantidadCc + "</CANTIDAD_PUNTOS>";
            xml += "<PAIS_CLIENTE_ACTUAL>" + parameters.strPaisClienteActual + "</PAIS_CLIENTE_ACTUAL>";
            xml += "<TIPO_TRANSACCION>" + parameters.StrTipoTransaccion + "</TIPO_TRANSACCION>";
            xml += "<ID_CU_ID>" + parameters.StrCustomerId + "</ID_CU_ID>";
            xml += "<CONTRATO_CANCELAR>" + parameters.strContrato + "</CONTRATO_CANCELAR>";
            xml += "<FECHA_CANCELACION>" + parameters.StrFechaCancel + "</FECHA_CANCELACION>";
            xml += "<FECHA_HORA_DE_ATENCION>" + parameters.strFechaHoraAtención + "</FECHA_HORA_DE_ATENCION>";
            xml += "<REINTEGRO>" + parameters.strMontoReintegro + "</REINTEGRO>";
            xml += "<MONTO_REINTEGRO>" + parameters.strMontoReintegro + "</MONTO_REINTEGRO>";
            xml += "<SERV_COMERCIAL>" + parameters.strServComercial + "</SERV_COMERCIAL>";
            xml += "<CARGO_FIJO_PROMOCION>" + parameters.strCargoFijoPromocion + "</CARGO_FIJO_PROMOCION>";
            xml += "<ACCION_EJECUTADA>" + parameters.strAccionEjecutiva + "</ACCION_EJECUTADA>";
            xml += "<PROGRAMADO>" + parameters.strProgramado + "</PROGRAMADO>";
            xml += "<NUM_PER_PROM>" + parameters.strNumPerProm + "</NUM_PER_PROM>";
            xml += "<REP_LEG_NRO_DOCUMENTO>" + parameters.strRepLegNroDocumento + "</REP_LEG_NRO_DOCUMENTO>";
            xml += "<DIRECCION_INSTALACION>" + parameters.strDireccionInstalcion + "</DIRECCION_INSTALACION>";
            xml += "<NRO_SOT>" + parameters.strNroSot + "</NRO_SOT>";

            //INICIO - VARIABLES NUEVA PARA HPXTREAM SUSPENSION Y RECONEXION : JOH
            xml += "<CENTRO_ATENCION>" + parameters.StrCentroAtencionArea + "</CENTRO_ATENCION>";
            xml += "<NRO_DOCUMENTO>" + parameters.StrNroDocIdentidad + "</NRO_DOCUMENTO>";
            xml += "<CUENTA>" + parameters.StrCuentaPostpago + "</CUENTA>";
            xml += "<TIPO_CLIENTE>" + parameters.strTipoCliente + "</TIPO_CLIENTE>";
            xml += "<CORREO_AUTORIZADO>" + parameters.strCorreoCliente + "</CORREO_AUTORIZADO>";
            //FIN - VARIABLES NUEVA PARA HPXTREAM SUSPENSION Y RECONEXION : JOH
            xml += "<FECHA_DESISTIMIENTO>" + parameters.StrFecDesistimiento + "</FECHA_DESISTIMIENTO>";
            xml += "<ACCION>" + parameters.StrAccionIFI + "</ACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_ATENCION>" + DateTime.Today.ToString("dd/MM/yyyy") + "</FECHA_ATENCION>";
            xml += "<NUMERO_SERVICIO>" + parameters.StrNroServicio + "</NUMERO_SERVICIO>";
            xml += "<RESULTADO>" + parameters.StrResultado + "</RESULTADO>";
            xml += "<EMAIL_AFILIADO>" + parameters.StrEmail + "</EMAIL_AFILIADO>";

            xml += "<ID_INTERACCION>" + parameters.StrIdInteraccion + "</ID_INTERACCION>";
            xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<TIPO_DOC>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<COD_DESBLOQUEO>" + parameters.StrCodDesbloqueo + "</COD_DESBLOQUEO>";
            xml += "<NRO_CASO>" + parameters.StrCasoInter + "</NRO_CASO>";
            xml += "<NOMBRE_COMERCIAL>" + parameters.StrContenidoComercial + "</NOMBRE_COMERCIAL>";
            xml += "<ESTADO_CIVIL>" + parameters.StrEstadoCivil + "</ESTADO_CIVIL>";
            xml += "<TELEFONO>" + parameters.StrTelfReferencia + "</TELEFONO>";
            xml += "<NRO_CELULAR>" + parameters.StrTelfOtroContacto + "</NRO_CELULAR>";
            xml += "<DIRECCION_CORREO>" + parameters.strEmailEnviar + "</DIRECCION_CORREO>";
            xml += "<FECHA_ACTUALIZACION>" + parameters.strFechaHoraAtención + "</FECHA_ACTUALIZACION>";
            xml += "<FECHA_NAC>" + parameters.StrFechaNac + "</FECHA_NAC>";
            xml += "<NACIONALIDAD>" + parameters.StrNacionalidad + "</NACIONALIDAD>";
            xml += "<GENERO>" + parameters.StrSexo + "</GENERO>";
            xml += "<CARGO>" + parameters.StrCargo + "</CARGO>";
            xml += "<NRO_FAX>" + parameters.StrNumeroFax + "</NRO_FAX>";
            xml += "<NRO_CELULAR_2>" + parameters.StrPhone1 + "</NRO_CELULAR_2>";
            xml += "<NRO_CELULAR_3>" + parameters.StrPhone2 + "</NRO_CELULAR_3>";
            xml += "<CORREO_2>" + parameters.StrEmail1 + "</CORREO_2>";
            xml += "<CORREO_3>" + parameters.StrEmail2 + "</CORREO_3>";
            xml += "<ENVIAR_CORREO>" + parameters.strEnvioCorreo + "</ENVIAR_CORREO>"; 
            xml += "<FECHA_SOLICITUD>" + parameters.strFechaTransaccion + "</FECHA_SOLICITUD>";
            xml += "<ID_INTERACCION>" + parameters.StrCasoInter + "</ID_INTERACCION>";
            xml += "<TIPO_DOC_TIT>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_TIT>";
            xml += "<TITULAR_TIT>" + parameters.StrTitularCliente + "</TITULAR_TIT>";
            xml += "<REPRESENTANTE_LEGAL_TIT>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL_TIT>";
            xml += "<NRO_DOC_TIT>" + parameters.StrNroDocIdentidad + "</NRO_DOC_TIT>";
            xml += "<TRANSACCION_IFI>" + parameters.strtransaccion + "</TRANSACCION_IFI>";
            xml += "<DIRECCION_AFILIADA>" + parameters.StrDireccion + "</DIRECCION_AFILIADA>";
            xml += "<DIRECCION_AFILIADO>" + parameters.StrDireccion + "</DIRECCION_AFILIADO>";
            xml += "<DIR_DISTRITO>" + parameters.StrDistrito + "</DIR_DISTRITO>";
            xml += "<DIR_PROVINCIA>" + parameters.StrProvincia + "</DIR_PROVINCIA>";
            xml += "<DIR_DEPARTAMENTO>" + parameters.StrDepartamento + "</DIR_DEPARTAMENTO>";
            xml += "<DIS_REFERENCIA>" + parameters.StrReferenciaActual + "</DIS_REFERENCIA>";

            if (!string.IsNullOrEmpty(parameters.StrFechaEmisionDocFact))
            {
                var separadas2 = parameters.StrFechaEmisionDocFact.Split('|');

                for (int i = 0; i < separadas2.Length; i++)
                {
                    parameters.StrFechaEmisionDocFact = separadas2[i].ToString() + System.Environment.NewLine;
                    xml += "<SOLICITUD_SERVICIOS>" + parameters.StrFechaEmisionDocFact + "\n" + "</SOLICITUD_SERVICIOS>";
                }

            }

            xml += "<METODO_ENVIO>" + parameters.StrMedio + "</METODO_ENVIO>";
            xml += "<FLAG_FIRMA_DIGITAL>" + parameters.strFirmaDigital + "</FLAG_FIRMA_DIGITAL>";
            xml += "<CORREO_AUTORIZADO>" + parameters.StrEmail + "</CORREO_AUTORIZADO>";
            xml += "</PLANTILLA>";

            return xml;
        }
        /// <summary>
        /// obtiene el motivo sot
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <returns></returns>
        /// <remarks>GetMotiveSot</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetMotiveSot(string strIdSession, string strTransaction)
        {


            DbParameter[] parameters = 
            {
                new DbParameter("srv_cur", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_POST_SGA_P_CONSULTA_MOTIVO, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["CODMOTOT"]),
                        Description = Convert.ToString(reader["MOTIVO"])
                    });
                }
            });

            return listItem;
        }
        /// <summary>
        /// valida calendario
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objScheduleRequest"></param>
        /// <returns></returns>
        /// <remarks>ValidateSchedule</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool ValidateSchedule(string strIdSession, string strTransaction, SECURITY.GetSchedule.ScheduleRequest objScheduleRequest)
        {

            string an_disponible = string.Empty;
            string av_mensaje = string.Empty;
            try
            {

                DbParameter[] parameters = {
                       new DbParameter("an_tiptra", DbType.Int64,255, ParameterDirection.Input,Convert.ToInt( objScheduleRequest.vJobTypes)),
					   new DbParameter("av_ubigeo", DbType.String,255,ParameterDirection.Input,objScheduleRequest.vUbigeo),
					   new DbParameter("ad_fecha_prog", DbType.Date,255,ParameterDirection.Input, Convert.ToDate(objScheduleRequest.vCommitmentDate)),
                       new DbParameter("av_franja", DbType.String,255,ParameterDirection.Input, objScheduleRequest.vTimeZona),
                       new DbParameter("an_disponible", DbType.Int64,255, ParameterDirection.Output),
                       new DbParameter("an_codcon", DbType.Int64,255, ParameterDirection.Output),
                       new DbParameter("av_codcuadrilla", DbType.String,255, ParameterDirection.Output),
                       new DbParameter("an_resultado", DbType.Int64,255, ParameterDirection.Output),
					   new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
			   };

                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_COD_UBIGEO, parameters);
                an_disponible = parameters[4].Value.ToString();
                av_mensaje = parameters[8].Value.ToString();

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            if (an_disponible.Equals(Claro.Constants.NumberZeroString))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// inserta traza de log
        /// </summary>
        /// <param name="strAplicacion"></param>
        /// <param name="strTransaccion"></param>
        /// <param name="strOpcion"></param>
        /// <param name="strAccion"></param>
        /// <param name="strPhone"></param>
        /// <param name="strIdInteraction"></param>
        /// <param name="strIdTypification"></param>
        /// <param name="strUser"></param>
        /// <param name="strIPPCClient"></param>
        /// <param name="strPCClient"></param>
        /// <param name="strIPServer"></param>
        /// <param name="strNameServer"></param>
        /// <param name="strInputParameters"></param>
        /// <param name="strOutpuParameters"></param>
        /// <param name="flagInserction"></param>
        /// <returns></returns>
        /// <remarks>InsertLogTrx</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertLogTrx(string strAplicacion, string strTransaccion, string strOpcion, string strAccion, string strPhone,
            string strIdInteraction, string strIdTypification, string strUser,
            string strIPPCClient, string strPCClient, string strIPServer, string strNameServer,
            string strInputParameters, string strOutpuParameters, ref string flagInserction)
        {
            bool response = false;
            DbParameter[] parameters = {
									new DbParameter("P_APLICACION", DbType.String,10,ParameterDirection.Input,strAplicacion),
									new DbParameter("P_TRANSACCION", DbType.String,50,ParameterDirection.Input,strTransaccion),
									new DbParameter("P_OPCION", DbType.String,20,ParameterDirection.Input,strOpcion),
									new DbParameter("P_ACCION", DbType.String,20,ParameterDirection.Input,strAccion),
									new DbParameter("P_TELEFONO", DbType.String,20,ParameterDirection.Input,strPhone),
									new DbParameter("P_INTERACCION_ID", DbType.String,20,ParameterDirection.Input,strIdInteraction),
                                    new DbParameter("P_TIPIFICACION_ID", DbType.String,10,ParameterDirection.Input,strIdTypification),
									new DbParameter("P_USUARIO_LOGIN", DbType.String,30,ParameterDirection.Input,strUser),
									new DbParameter("P_IP_CLIENTE", DbType.String,15,ParameterDirection.Input,strIPPCClient),
                                    new DbParameter("P_NOMBRE_CLIENTE", DbType.String,30,ParameterDirection.Input,strPCClient),
									new DbParameter("P_IP_SERVIDOR", DbType.String,15,ParameterDirection.Input,strIPServer),
									new DbParameter("P_NOMBRE_SERVIDOR", DbType.String,30,ParameterDirection.Input,strNameServer),
                                    new DbParameter("P_PARAMS_IN", DbType.String,400,ParameterDirection.Input,strInputParameters),
									new DbParameter("P_PARAMS_OUT", DbType.String,60,ParameterDirection.Input,strOutpuParameters),
									new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
								};

            DbFactory.ExecuteNonQuery("S", strTransaccion, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_COMMON_SP_INSERTAR_LOG_TRX, parameters);

            flagInserction = Claro.Utils.CheckStr(parameters[parameters.Length - 1].Value);
            if (flagInserction.Equals("OK"))
            {
                response = true;
            }

            return response;
        }
        /// <summary>
        /// obtiene parametros 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>GetParameterData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ParameterData GetParameterData(string name, ref string message)
        {
            return GetParameterData("s", "t", name, ref message);
        }
        /// <summary>
        /// obtiene parametros
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="transaction"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>GetParameterData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ParameterData GetParameterData(string sesion, string transaction, string name, ref string message)
        {
            ParameterData entity = null;
            DbParameter[] parameters = {
                new DbParameter("P_NOMBRE", DbType.String, 255,ParameterDirection.Input, name),
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(sesion, transaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_COMMON_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
            {
                if (reader.Read())
                {
                    entity = new ParameterData()
                    {
                        Description = Claro.Utils.CheckStr(reader["descripcion"]),
                        Value_C = Claro.Utils.CheckStr(reader["valor_C"]),
                        Value_N = Claro.Utils.CheckDbl(reader["valor_N"])
                    };
                }
            });

            message = parameters[1].Value.ToString();
            return entity;
        }
        /// <summary>
        /// Método que devuele el tipo de establecimiento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="code">Código</param>
        /// <returns>Devuelve listado de tipos de establecimiento.</returns>
        /// <remarks>GetCacDacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetCacDacType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
                new DbParameter("P_OBJID", DbType.Int64, ParameterDirection.Input, code),
                new DbParameter("P_FLAG_CONSULTA", DbType.String,225, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,225, ParameterDirection.Output),
                new DbParameter("P_LIST", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["NOMBRE"])
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// Retorna lista de Items BSCS para e listado del estado civil
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>ArrayList</returns>
        /// <remarks>GetEstCivType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetEstCivType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
               
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT_EC, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["MAS_ID"]),
                        Description = Convert.ToString(reader["MAS_DES"]).ToUpper()
                    });
                }
            });

            return listItem;
        }


        /// <summary>
        /// Retorna lista de fechas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="vCuentaCliente"></param>
        /// <param name="code"></param>      
        /// <returns>ArrayList</returns>
        /// <remarks>GetdgFechasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.Receipt> GetdgFechasType(string vCantidadRegistros, string vCuentaCliente, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
                new DbParameter("K_CODIGOCLIENTE", DbType.String,24,ParameterDirection.Input),
				new DbParameter("K_ERRMSG", DbType.String,ParameterDirection.Output),
				new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
          
            };

            List<Entity.IFI.Common.Receipt> listItem = null;
            DbFactory.ExecuteReader(vCantidadRegistros, vCuentaCliente, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_SP_SHOW_LIST_OBTENER_FECHAS_RECIBOS, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.Receipt>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.Receipt
                    {
                        INVOICENUMBER = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("InvoiceNumber"))),						
						CCName = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("CCName"))),
						ContactClient = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("ContactClient"))),
						CCAddr1 = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("CCAddr1"))),
						CCAddr2 = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("CCAddr2"))),
                        Distrito = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("Distrito"))),
                        Provincia = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("Provincia"))),
                        Departamento = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("Departamento"))),
                        NroDoc = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("NroDoc"))),
                        FechaInicio = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("FechaInicio"))),
						FechaFin = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("FechaFin"))),
                        FECHA_EMISION = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("FechaEmision"))),
						FECHA_VENCIMIENTO = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("FechaVencimiento"))),
						CodCliente = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("CodCliente"))),
						NroCiclo = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("NroCiclo"))),
                        PERIODO = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("Periodo"))),
						TotalPrevCharges = Claro.Utils.CheckStr(reader.GetDecimal(reader.GetOrdinal("TotalPrevCharges"))),
                        TotalPaymentsRcvd = Claro.Utils.CheckStr(reader.GetDecimal(reader.GetOrdinal("TotalPaymentsRcvd"))),
                        TotalPrevBalance = Claro.Utils.CheckStr(reader.GetDecimal(reader.GetOrdinal("TotalPrevBalance"))),
                        TotalCurrentCharges = Claro.Utils.CheckStr(reader.GetDecimal(reader.GetOrdinal("TotalCurrentCharges"))),
                        TotalTaxes = Claro.Utils.CheckStr(reader.GetDecimal(reader.GetOrdinal("TotalTaxes"))),
                        TotalAmountDue = Claro.Utils.CheckStr(reader.GetDecimal(reader.GetOrdinal("TotalAmountDue"))),
						Mes = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("Mes"))),
						Anho = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("Anho"))),
						version = Claro.Utils.CheckStr(reader.GetValue(reader.GetOrdinal("version"))),
                     

                    });
                }
            });

            return listItem;
        }

        /// <summary> Retorna lista de SIAC_POST_DB para eL listado de tipo de vias </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>List</returns>
        /// <remarks>GetViasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetViasType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
               
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTA_TIPO_VIA, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["ID_TIPO_VIA"]),
                        Description = Convert.ToString(reader["TIPO_VIA_DESC"]).ToUpper()
                    });
                }
            });

            return listItem;
        }

        /// <summary> Retorna lista de SIAC_POST_DB para eL listado de tipo de manzanas </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>List</returns>
        /// <remarks>GetManzanasType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>XXXX</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetManzanasType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
               
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTA_TIPO_MZ, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["ID_TIPO_MANZANA"]),
                        Description = Convert.ToString(reader["TIPO_MANZANA_DESC"]).ToUpper()
                    });
                }
            });

            return listItem;
        }

        /// <summary> Retorna lista de SIAC_POST_DB para eL listado de tipo de interiores </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>List</returns>
        /// <remarks>GetInterioresType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetInterioresType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
               
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTA_TIPO_INTERIOR, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["ID_TIPO_INTERIOR"]),
                        Description = Convert.ToString(reader["DESC_TIPO_INTERIOR"]).ToUpper()
                    });
                }
            });

            return listItem;
        }

        /// <summary> Retorna lista de SIAC_POST_DB para eL listado de tipo de urbanizaciones </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>List</returns>
        /// <remarks>GetUrbsType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetUrbsType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
               
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTA_TIPO_URBANIZACION, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["ID_TIPO_URBANIZACION"]),
                        Description = Convert.ToString(reader["DESC_TIPO_URBANIZACION"]).ToUpper()
                    });
                }
            });

            return listItem;
        }

        /// <summary> Retorna lista de SIAC_POST_DB para eL listado de tipo de zonas </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>List</returns>
        /// <remarks>GetZonesType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>05/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetZonesType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
               
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTA_TIPO_ZONA, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["ID_TIPO_ZONA"]),
                        Description = Convert.ToString(reader["DESC_TIPO_ZONA"]).ToUpper()
                    });
                }
            });

            return listItem;
        }

        /// <summary> Retorna lista de SIAC_POST_BSCS para eL listado de ubigeo </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>List</returns>
        /// <remarks>GetZonesType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>09/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetUbigeosType(string strIdSession, string strTransaction, int code,int dep,int prov)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
                new DbParameter("p_departamento", DbType.Int32, ParameterDirection.Input,dep),
                new DbParameter("p_provincia", DbType.Int32, ParameterDirection.Input,prov),
                new DbParameter("p_distrito", DbType.Int32, ParameterDirection.Input,0),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTA_UBIGEO, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["DESCRIPCION"]).ToUpper()
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// Retorna lista de Items BSCS para e listado de las nacionalidades
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <returns>ArrayList</returns>
        /// <remarks>GetNacType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetNacType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
                
                new DbParameter("RESULT", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT_NC, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["NACIONALIDAD"]).ToUpper()
                    });
                }
            });

            return listItem;
        }
        /// <summary>
        /// Método que obtiene las tipificaciones.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTransactionName">Transacción de la tipificacion</param>
        /// <returns>Devuelve las tipificaciones.</returns>
        /// <remarks>GetTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.Typification> GetTypification(string strIdSession, string strTransaction, string strTransactionName)
        {
            List<Claro.SIACU.Entity.IFI.Common.Typification> lstTypification = null;
            DbParameter[] parameters = {
                new DbParameter("P_TRANSACCION", DbType.String, ParameterDirection.Input,strTransactionName),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                lstTypification = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.IFI.Common.Typification>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_TIPIFICACIONES, parameters);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, "Error GetTypification" + ex.Message);
            }

            return lstTypification;
        }

        #region Interacciones-


        /// <summary>
        /// Método que devuelve el código de la lista.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name=">straccount"</param>cuenta
        /// <param name=">strtelephone"</param>telefono
        /// <param name=">intcontactobjid1"</param>id de contacto
        /// <returns>Devuelve código de lista.</returns>
        /// <remarks>GetTypification</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Iteraction> GetIteractionClient(string strIdSession, string strTransaction, string strAccount, string strTelephone, long intContactobjid1, long intSiteobjid1, string strTipification, int intNrorecordshow, out  string strFlagcreation, out string strMessage)
        {

            List<Iteraction> list = new List<Iteraction>();
            DbParameter[] parameters = new DbParameter[]
            {
            new DbParameter ("P_PHONE", DbType.String,255,ParameterDirection.Input, strTelephone),
		    new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input,strAccount),
		    new DbParameter("P_SITEOBJID_1", DbType.String,255,ParameterDirection.Input,intSiteobjid1),
		    new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,intContactobjid1),
		    new DbParameter("P_TIPIFICACION", DbType.String,255,ParameterDirection.Input,strTipification),
		    new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
			new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output),
		    new DbParameter("INTERACT", DbType.Object,ParameterDirection.Output)
            };

            //List<IteractionClient> result = null;
            // DbFactory.ExecuteReader<List<IteractionClient>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_INTERACT,parameters,));

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_INTERACT, parameters, (IDataReader reader) =>
                {

                    //result= new List<IteractionClient>();
                    //      Iteraction entity = null;
                    int contador = 0;
                    if (intNrorecordshow < 0)
                        intNrorecordshow = 0;

                    while (reader.Read())
                    {
                        if (intNrorecordshow <= contador)
                        {
                            break;
                        }
                        contador++;

                        list.Add(new Iteraction()
                        {

                            ID_INTERACCION = Claro.Utils.CheckStr(reader["ID_INTERACCION"]),
                            FECHA_CREACION = Claro.Utils.CheckStr(reader["FECHA_CREACION"]),
                            START_DATE = Claro.Utils.CheckStr(reader["START_DATE"]),
                            TELEFONO = Claro.Utils.CheckStr(reader["TELEFONO"]),
                            TIPO = Claro.Utils.CheckStr(reader["TIPO"]),
                            CLASE = Claro.Utils.CheckStr(reader["CLASE"]),
                            SUBCLASE = Claro.Utils.CheckStr(reader["SUBCLASE"]),
                            TIPIFICACION = Claro.Utils.CheckStr(reader["TIPIFICACION"]),
                            TIPO_CODIGO = Claro.Utils.CheckStr(reader["TIPO_CODIGO"]),
                            CLASE_CODIGO = Claro.Utils.CheckStr(reader["CLASE_CODIGO"]),
                            SUBCLASE_CODIGO = Claro.Utils.CheckStr(reader["SUBCLASE_CODIGO"]),
                            INSERTADO_POR = Claro.Utils.CheckStr(reader["INSERTADO_POR"]),
                            TIPO_INTER = Claro.Utils.CheckStr(reader["TIPO_INTER"]),
                            METODO = Claro.Utils.CheckStr(reader["METODO"]),
                            RESULTADO = Claro.Utils.CheckStr(reader["RESULTADO"]),
                            HECHO_EN_UNO = Claro.Utils.CheckStr(reader["HECHO_EN_UNO"]),
                            AGENTE = Claro.Utils.CheckStr(reader["AGENTE"]),
                            NOMBRE_AGENTE = Claro.Utils.CheckStr(reader["NOMBRE_AGENTE"]),
                            APELLIDO_AGENTE = Claro.Utils.CheckStr(reader["APELLIDO_AGENTE"]),
                            ID_CASO = Claro.Utils.CheckStr(reader["ID_CASO"]),
                            SERVICIO = Claro.Utils.CheckStr(reader["SERVICIO"]),
                            INCONVENIENTE = Claro.Utils.CheckStr(reader["INCONVENIENTE"])
                        });

                    }

                });
            strFlagcreation = Claro.Utils.CheckStr(parameters[5].Value.ToString()).ToString();
            strMessage = Claro.Utils.CheckStr(parameters[6].Value.ToString()).ToString();

            return list;

        }
        /// <summary>
        /// Registra plantilla interaccion
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objitem"></param>
        /// <param name="strInteraccionId"></param>
        /// <param name="strFlagInsercion"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>RegistrationInsertTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool RegistrationInsertTemplateInteraction(string strSesion, string strTransaction, InsertTemplateInteraction objitem, string strInteraccionId, out string strFlagInsercion, out string strMessage)
        {
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: entra a RegistrationInsertTemplateInteraction");

            DbParameter[] parameters = {
            new DbParameter("P_NRO_INTERACCION",DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_INTER_1",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_2",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_3",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_4",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_5",DbType.String,1500,ParameterDirection.Input),
				new DbParameter("P_INTER_6",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_7",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_8",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_9",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_10",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_11",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_12",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_13",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_14",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_15",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_16",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_17",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_18",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_19",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_20",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_21",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_22",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_23",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_24",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_25",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_26",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_27",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_28",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_29",DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_INTER_30",DbType.String,32000,ParameterDirection.Input),
				new DbParameter("P_PLUS_INTER2INTERACT",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_ADJUSTMENT_AMOUNT",DbType.Double,ParameterDirection.Input),
				new DbParameter("P_ADJUSTMENT_REASON",DbType.String,	20,ParameterDirection.Input),
				new DbParameter("P_ADDRESS",DbType.String,100,ParameterDirection.Input),
				new DbParameter("P_AMOUNT_UNIT",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_BIRTHDAY",DbType.Date,ParameterDirection.Input),
				new DbParameter("P_CLARIFY_INTERACTION",DbType.String,15,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CONTACT_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_DNI_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_EMAIL",DbType.String,100,ParameterDirection.Input),
				new DbParameter("P_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_FLAG_CHANGE_USER",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_LEGAL_REP",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_OTHER",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_TITULAR",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_IMEI",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_LAST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_LASTNAME_REP",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_NAME_LEGAL_REP",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OLD_FIXED_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_LAST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OLD_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OPERATION_TYPE",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_OTHER_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OTHER_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OTHER_LAST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OTHER_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_PHONE_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_REFERENCE_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_REASON",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_LOT_CODE",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_FLAG_REGISTERED",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_REGISTRATION_REASON",DbType.String,80,ParameterDirection.Input),
				new DbParameter("P_CLARO_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_MONTH",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input),
			    new DbParameter("P_BASKET",DbType.String,50,ParameterDirection.Input),
			    new DbParameter("P_EXPIRE_DATE",DbType.Date,ParameterDirection.Input),
			    new DbParameter("P_ADDRESS5",DbType.String,200,ParameterDirection.Input),
			    new DbParameter("P_CHARGE_AMOUNT",DbType.Decimal,ParameterDirection.Input),
			    new DbParameter("P_CITY",DbType.String,30,ParameterDirection.Input),
			    new DbParameter("P_CONTACT_SEX",DbType.String,1,ParameterDirection.Input),
			    new DbParameter("P_DEPARTMENT",DbType.String,40,ParameterDirection.Input),
			    new DbParameter("P_DISTRICT",DbType.String,200,ParameterDirection.Input),
			    new DbParameter("P_EMAIL_CONFIRMATION",DbType.String,1,ParameterDirection.Input),
			    new DbParameter("P_FAX",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_FLAG_CHARGE",DbType.String,1,ParameterDirection.Input),
			    new DbParameter("P_MARITAL_STATUS",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_OCCUPATION",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_POSITION",DbType.String,30,ParameterDirection.Input),
			    new DbParameter("P_REFERENCE_ADDRESS",DbType.String,50,ParameterDirection.Input),
			    new DbParameter("P_TYPE_DOCUMENT",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_ZIPCODE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_ICCID",DbType.String,20,ParameterDirection.Input),
				new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
				new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
				new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)	
        
        };
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }

            int i = 0;
            DateTime dateBegin = new DateTime(1, 1, 1);


            if (strInteraccionId != null)
            {
                parameters[i].Value = strInteraccionId;
                objitem._X_PLUS_INTER2INTERACT = Claro.Utils.CheckDbl(strInteraccionId);
            }

            i++;
            if (objitem._X_INTER_1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_1);

            i++;
            if (objitem._X_INTER_2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_2);

            i++;
            if (objitem._X_INTER_3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_3);

            i++;
            if (objitem._X_INTER_4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_4);

            i++;
            if (objitem._X_INTER_5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_5);

            i++;
            if (objitem._X_INTER_6 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_6);

            i++;
            if (objitem._X_INTER_7 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_7);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_8);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_9);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_10);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objitem._X_INTER_11);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_12);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_13);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_14);

            i++;
            if (objitem._X_INTER_15 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_15);

            i++;
            if (objitem._X_INTER_16 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_16);

            i++;
            if (objitem._X_INTER_17 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_17);

            i++;
            if (objitem._X_INTER_18 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_18);

            i++;
            if (objitem._X_INTER_19 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_19);

            i++;
            if (objitem._X_INTER_20 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_20);

            i++;
            if (objitem._X_INTER_21 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_21);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objitem._X_INTER_22);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objitem._X_INTER_23);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objitem._X_INTER_24);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_25);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_26);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_27);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_INTER_28);

            i++;
            if (objitem._X_INTER_29 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_29);

            i++;
            if (objitem._X_INTER_30 != null)
            {
                string strValor = Claro.Utils.CheckStr(objitem._X_INTER_30);
                if (strValor == "")
                    strValor = ".";
                else
                    parameters[i].Value = Claro.Utils.CheckStr(objitem._X_INTER_30);
            }
            else
            {
                parameters[i].Value = " ";
            }
            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_PLUS_INTER2INTERACT);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objitem._X_ADJUSTMENT_AMOUNT);

            i++;
            if (objitem._X_ADJUSTMENT_REASON != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_ADJUSTMENT_REASON);

            i++;
            if (objitem._X_ADDRESS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_ADDRESS);

            i++;
            if (objitem._X_AMOUNT_UNIT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_AMOUNT_UNIT);

            i++;
            if (objitem._X_BIRTHDAY != dateBegin)
                parameters[i].Value = Claro.Utils.CheckDate(objitem._X_BIRTHDAY);

            i++;
            if (objitem._X_CLARIFY_INTERACTION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLARIFY_INTERACTION);

            i++;
            if (objitem._X_CLARO_LDN1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLARO_LDN1);

            i++;
            if (objitem._X_CLARO_LDN2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLARO_LDN2);

            i++;
            if (objitem._X_CLARO_LDN3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLARO_LDN3);

            i++;
            if (objitem._X_CLARO_LDN4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLARO_LDN4);

            i++;
            if (objitem._X_CLAROLOCAL1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLAROLOCAL1);

            i++;
            if (objitem._X_CLAROLOCAL2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLAROLOCAL2);

            i++;
            if (objitem._X_CLAROLOCAL3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLAROLOCAL3);

            i++;
            if (objitem._X_CLAROLOCAL4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLAROLOCAL4);

            i++;
            if (objitem._X_CLAROLOCAL5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLAROLOCAL5);

            i++;
            if (objitem._X_CLAROLOCAL6 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLAROLOCAL6);

            i++;
            if (objitem._X_CONTACT_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CONTACT_PHONE);

            i++;
            if (objitem._X_DNI_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_DNI_LEGAL_REP);

            i++;
            if (objitem._X_DOCUMENT_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_DOCUMENT_NUMBER);

            i++;
            if (objitem._X_EMAIL != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_EMAIL);

            i++;
            if (objitem._X_FIRST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FIRST_NAME);

            i++;
            if (objitem._X_FIXED_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FIXED_NUMBER);

            i++;
            if (objitem._X_FLAG_CHANGE_USER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FLAG_CHANGE_USER);

            i++;
            if (objitem._X_FLAG_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FLAG_LEGAL_REP);

            i++;
            if (objitem._X_FLAG_OTHER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FLAG_OTHER);

            i++;
            if (objitem._X_FLAG_TITULAR != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FLAG_TITULAR);

            i++;
            if (objitem._X_IMEI != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_IMEI);

            i++;
            if (objitem._X_LAST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_LAST_NAME);

            i++;
            if (objitem._X_LASTNAME_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_LASTNAME_REP);

            i++;
            if (objitem._X_LDI_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_LDI_NUMBER);

            i++;
            if (objitem._X_NAME_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_NAME_LEGAL_REP);

            i++;
            if (objitem._X_OLD_CLARO_LDN1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLARO_LDN1);

            i++;
            if (objitem._X_OLD_CLARO_LDN2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLARO_LDN2);

            i++;
            if (objitem._X_OLD_CLARO_LDN3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLARO_LDN3);

            i++;
            if (objitem._X_OLD_CLARO_LDN4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLARO_LDN4);

            i++;
            if (objitem._X_OLD_CLAROLOCAL1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLAROLOCAL1);

            i++;
            if (objitem._X_OLD_CLAROLOCAL2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLAROLOCAL2);

            i++;
            if (objitem._X_OLD_CLAROLOCAL3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLAROLOCAL3);

            i++;
            if (objitem._X_OLD_CLAROLOCAL4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLAROLOCAL4);

            i++;
            if (objitem._X_OLD_CLAROLOCAL5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLAROLOCAL5);

            i++;
            if (objitem._X_OLD_CLAROLOCAL6 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_CLAROLOCAL6);

            i++;
            if (objitem._X_OLD_DOC_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_DOC_NUMBER);

            i++;
            if (objitem._X_OLD_FIRST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_FIRST_NAME);

            i++;
            if (objitem._X_OLD_FIXED_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_FIXED_PHONE);

            i++;
            if (objitem._X_OLD_LAST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_LAST_NAME);

            i++;
            if (objitem._X_OLD_LDI_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_LDI_NUMBER);

            i++;
            if (objitem._X_OLD_FIXED_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OLD_FIXED_NUMBER);

            i++;
            if (objitem._X_OPERATION_TYPE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OPERATION_TYPE);

            i++;
            if (objitem._X_OTHER_DOC_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OTHER_DOC_NUMBER);

            i++;
            if (objitem._X_OTHER_FIRST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OTHER_FIRST_NAME);

            i++;
            if (objitem._X_OTHER_LAST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OTHER_LAST_NAME);

            i++;
            if (objitem._X_OTHER_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OTHER_PHONE);

            i++;
            if (objitem._X_PHONE_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_PHONE_LEGAL_REP);

            i++;
            if (objitem._X_REFERENCE_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_REFERENCE_PHONE);

            i++;
            if (objitem._X_REASON != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_REASON);

            i++;
            if (objitem._X_MODEL != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_MODEL);

            i++;
            if (objitem._X_LOT_CODE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_LOT_CODE);

            i++;
            if (objitem._X_FLAG_REGISTERED != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FLAG_REGISTERED);

            i++;
            if (objitem._X_REGISTRATION_REASON != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_REGISTRATION_REASON);

            i++;
            if (objitem._X_CLARO_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CLARO_NUMBER);

            i++;
            if (objitem._X_MONTH != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_MONTH);

            i++;
            if (objitem._X_OST_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OST_NUMBER);

            i++;
            if (objitem._X_BASKET != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_BASKET);

            i++;
            if (objitem._X_EXPIRE_DATE != dateBegin)
                parameters[i].Value = Claro.Utils.CheckDate(objitem._X_EXPIRE_DATE);

            i++;
            if (objitem._X_ADDRESS5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_ADDRESS5);
            i++;
            parameters[i].Value = Claro.Utils.CheckDbl(objitem._X_CHARGE_AMOUNT);
            i++;
            if (objitem._X_CITY != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CITY);
            i++;
            if (objitem._X_CONTACT_SEX != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_CONTACT_SEX);
            i++;
            if (objitem._X_DEPARTMENT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_DEPARTMENT);
            i++;
            if (objitem._X_DISTRICT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_DISTRICT);
            i++;
            if (objitem._X_EMAIL_CONFIRMATION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_EMAIL_CONFIRMATION);
            i++;
            if (objitem._X_FAX != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FAX);
            i++;
            if (objitem._X_FLAG_CHARGE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_FLAG_CHARGE);
            i++;
            if (objitem._X_MARITAL_STATUS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_MARITAL_STATUS);
            i++;
            if (objitem._X_OCCUPATION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_OCCUPATION);
            i++;
            if (objitem._X_POSITION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_POSITION);
            i++;
            if (objitem._X_REFERENCE_ADDRESS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_REFERENCE_ADDRESS);
            i++;
            if (objitem._X_TYPE_DOCUMENT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_TYPE_DOCUMENT);
            i++;
            if (objitem._X_ZIPCODE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_ZIPCODE);

            i++;
            if (objitem._X_ICCID != null)
                parameters[i].Value = Claro.Utils.CheckStr(objitem._X_ICCID);


            int result = 0;
            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER, parameters);

            strFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
            strMessage = parameters[parameters.Length - 1].Value.ToString();
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: sale de RegistrationInsertTemplateInteractionstr FlagInsercion: " + strFlagInsercion + "strMessage: " + strMessage);

            Logging.Info(strSesion, strTransaction, "BACK_INTERACTION" + strFlagInsercion);
            Logging.Info(strSesion, strTransaction, "BACK_INTERACTION" + strMessage);

            return (strFlagInsercion.Equals(KEY.AppSettings("InteractionMessage")));
            //return (result>0);

        }
        /// <summary>
        /// inserta plantilla interaccion
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objItem"></param>
        /// <param name="strInteractionid"></param>
        /// <param name="flagInsercion"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>InsTemplateInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsTemplateInteraction(string strSesion, string strTransaction, InsertTemplateInteraction objItem, string strInteractionid, out string flagInsercion, out string strMessage)
        {

            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a GetInserInteractionTemplatInsTemplateInteractioneresponse");

            DbParameter[] parameters = new DbParameter[] { 
            
                                                   new DbParameter("PN_SECUENCIAL",DbType.Double,ParameterDirection.Input),
												   new DbParameter("PV_NRO_INTERACCION",DbType.String,255,ParameterDirection.Input),
												   new DbParameter("PV_INTER_1",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_2",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_3",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_4",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_5",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_6",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_7",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PN_INTER_8",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_9",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_10",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_11",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_12",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_13",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_14",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PV_INTER_15",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_16",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_17",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_18",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_19",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_20",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_21",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PN_INTER_22",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_23",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_24",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_25",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_26",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_27",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_28",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PV_INTER_29",DbType.String,255,ParameterDirection.Input),
												   new DbParameter("PC_INTER_30",DbType.String,ParameterDirection.Input),
												   new DbParameter("PN_PLUS_INTER2INTERACT",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_ADJUSTMENT_AMOUNT",DbType.Double,ParameterDirection.Input),
												   new DbParameter("PV_ADJUSTMENT_REASON",DbType.String,	20,ParameterDirection.Input),
												   new DbParameter("PV_ADDRESS",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("PV_AMOUNT_UNIT",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PD_BIRTHDAY",DbType.Date,ParameterDirection.Input),
												   new DbParameter("PV_CLARIFY_INTERACTION",DbType.String,15,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CONTACT_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_DNI_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_EMAIL",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_FIRST_NAME",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_CHANGE_USER",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_LEGAL_REP",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_OTHER",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_TITULAR",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_IMEI",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_LAST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_LASTNAME_REP",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_NAME_LEGAL_REP",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OLD_FIXED_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_LAST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OLD_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OPERATION_TYPE",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_LAST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_PHONE_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_REFERENCE_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_REASON",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_MODEL",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_LOT_CODE",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_REGISTERED",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_REGISTRATION_REASON",DbType.String,80,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_MONTH",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OST_NUMBER",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_BASKET",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PD_EXPIRE_DATE",DbType.Date,ParameterDirection.Input),
												   new DbParameter("PV_ADDRESS5",DbType.String,200,ParameterDirection.Input),
												   new DbParameter("PN_CHARGE_AMOUNT",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PV_CITY",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_CONTACT_SEX",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_DEPARTMENT",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_DISTRICT",DbType.String,200,ParameterDirection.Input),
												   new DbParameter("PV_EMAIL_CONFIRMATION",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FAX",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_CHARGE",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_MARITAL_STATUS",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OCCUPATION",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_POSITION",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_REFERENCE_ADDRESS",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_TYPE_DOCUMENT",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_ZIPCODE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_ICCID",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
												   new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
												   new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)										
            };

            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
            int i = 0;
            DateTime dateStart = new DateTime(1, 1, 1);
            if (strInteractionid != null)
            {
                parameters[i].Value = Claro.Utils.CheckDbl(strInteractionid);
            }

            i++;
            if (strInteractionid != null)
            {
                parameters[i].Value = strInteractionid;
                objItem._X_PLUS_INTER2INTERACT = Claro.Utils.CheckDbl(strInteractionid);
            }

            i++;
            if (objItem._X_INTER_1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_1);

            i++;
            if (objItem._X_INTER_2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_2);

            i++;
            if (objItem._X_INTER_3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_3);

            i++;
            if (objItem._X_INTER_4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_4);

            i++;
            if (objItem._X_INTER_5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_5);

            i++;
            if (objItem._X_INTER_6 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_6);

            i++;
            if (objItem._X_INTER_7 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_7);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_8);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_9);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_10);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objItem._X_INTER_11);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_12);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_13);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_14);

            i++;
            if (objItem._X_INTER_15 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_15);

            i++;
            if (objItem._X_INTER_16 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_16);

            i++;
            if (objItem._X_INTER_17 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_17);

            i++;
            if (objItem._X_INTER_18 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_18);

            i++;
            if (objItem._X_INTER_19 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_19);

            i++;
            if (objItem._X_INTER_20 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_20);

            i++;
            if (objItem._X_INTER_21 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_21);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objItem._X_INTER_22);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objItem._X_INTER_23);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objItem._X_INTER_24);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_25);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_26);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_27);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_INTER_28);

            i++;
            if (objItem._X_INTER_29 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_29);

            i++;
            if (objItem._X_INTER_30 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_INTER_30);

            i++;

            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_PLUS_INTER2INTERACT);

            i++;

            parameters[i].Value = Claro.Utils.CheckDblDB(objItem._X_ADJUSTMENT_AMOUNT);

            i++;
            if (objItem._X_ADJUSTMENT_REASON != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_ADJUSTMENT_REASON);

            i++;
            if (objItem._X_ADDRESS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_ADDRESS);

            i++;
            if (objItem._X_AMOUNT_UNIT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_AMOUNT_UNIT);

            i++;
            if (objItem._X_BIRTHDAY != dateStart)
                parameters[i].Value = Claro.Utils.CheckDate(objItem._X_BIRTHDAY);

            i++;
            if (objItem._X_CLARIFY_INTERACTION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLARIFY_INTERACTION);

            i++;
            if (objItem._X_CLARO_LDN1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLARO_LDN1);

            i++;
            if (objItem._X_CLARO_LDN2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLARO_LDN2);

            i++;
            if (objItem._X_CLARO_LDN3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLARO_LDN3);

            i++;
            if (objItem._X_CLARO_LDN4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLARO_LDN4);

            i++;
            if (objItem._X_CLAROLOCAL1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLAROLOCAL1);

            i++;
            if (objItem._X_CLAROLOCAL2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLAROLOCAL2);

            i++;
            if (objItem._X_CLAROLOCAL3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLAROLOCAL3);

            i++;
            if (objItem._X_CLAROLOCAL4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLAROLOCAL4);

            i++;
            if (objItem._X_CLAROLOCAL5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLAROLOCAL5);

            i++;
            if (objItem._X_CLAROLOCAL6 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLAROLOCAL6);

            i++;
            if (objItem._X_CONTACT_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CONTACT_PHONE);

            i++;
            if (objItem._X_DNI_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_DNI_LEGAL_REP);

            i++;
            if (objItem._X_DOCUMENT_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_DOCUMENT_NUMBER);

            i++;
            if (objItem._X_EMAIL != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_EMAIL);

            i++;
            if (objItem._X_FIRST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FIRST_NAME);

            i++;
            if (objItem._X_FIXED_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FIXED_NUMBER);

            i++;
            if (objItem._X_FLAG_CHANGE_USER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FLAG_CHANGE_USER);

            i++;
            if (objItem._X_FLAG_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FLAG_LEGAL_REP);

            i++;
            if (objItem._X_FLAG_OTHER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FLAG_OTHER);

            i++;
            if (objItem._X_FLAG_TITULAR != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FLAG_TITULAR);

            i++;
            if (objItem._X_IMEI != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_IMEI);

            i++;
            if (objItem._X_LAST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_LAST_NAME);

            i++;
            if (objItem._X_LASTNAME_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_LASTNAME_REP);

            i++;
            if (objItem._X_LDI_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_LDI_NUMBER);

            i++;
            if (objItem._X_NAME_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_NAME_LEGAL_REP);

            i++;
            if (objItem._X_OLD_CLARO_LDN1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLARO_LDN1);

            i++;
            if (objItem._X_OLD_CLARO_LDN2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLARO_LDN2);

            i++;
            if (objItem._X_OLD_CLARO_LDN3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLARO_LDN3);

            i++;
            if (objItem._X_OLD_CLARO_LDN4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLARO_LDN4);

            i++;
            if (objItem._X_OLD_CLAROLOCAL1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLAROLOCAL1);

            i++;
            if (objItem._X_OLD_CLAROLOCAL2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLAROLOCAL2);

            i++;
            if (objItem._X_OLD_CLAROLOCAL3 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLAROLOCAL3);

            i++;
            if (objItem._X_OLD_CLAROLOCAL4 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLAROLOCAL4);

            i++;
            if (objItem._X_OLD_CLAROLOCAL5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLAROLOCAL5);

            i++;
            if (objItem._X_OLD_CLAROLOCAL6 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_CLAROLOCAL6);

            i++;
            if (objItem._X_OLD_DOC_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_DOC_NUMBER);

            i++;
            if (objItem._X_OLD_FIRST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_FIRST_NAME);

            i++;
            if (objItem._X_OLD_FIXED_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_FIXED_PHONE);

            i++;
            if (objItem._X_OLD_LAST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_LAST_NAME);

            i++;
            if (objItem._X_OLD_LDI_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_LDI_NUMBER);

            i++;
            if (objItem._X_OLD_FIXED_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OLD_FIXED_NUMBER);

            i++;
            if (objItem._X_OPERATION_TYPE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OPERATION_TYPE);

            i++;
            if (objItem._X_OTHER_DOC_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OTHER_DOC_NUMBER);

            i++;
            if (objItem._X_OTHER_FIRST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OTHER_FIRST_NAME);

            i++;
            if (objItem._X_OTHER_LAST_NAME != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OTHER_LAST_NAME);

            i++;
            if (objItem._X_OTHER_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OTHER_PHONE);

            i++;
            if (objItem._X_PHONE_LEGAL_REP != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_PHONE_LEGAL_REP);

            i++;
            if (objItem._X_REFERENCE_PHONE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_REFERENCE_PHONE);

            i++;
            if (objItem._X_REASON != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_REASON);

            i++;
            if (objItem._X_MODEL != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_MODEL);

            i++;
            if (objItem._X_LOT_CODE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_LOT_CODE);

            i++;
            if (objItem._X_FLAG_REGISTERED != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FLAG_REGISTERED);

            i++;
            if (objItem._X_REGISTRATION_REASON != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_REGISTRATION_REASON);

            i++;
            if (objItem._X_CLARO_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CLARO_NUMBER);

            i++;
            if (objItem._X_MONTH != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_MONTH);

            i++;
            if (objItem._X_OST_NUMBER != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OST_NUMBER);

            i++;
            if (objItem._X_BASKET != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_BASKET);

            i++;
            if (objItem._X_EXPIRE_DATE != dateStart)
                parameters[i].Value = Claro.Utils.CheckDate(objItem._X_EXPIRE_DATE);

            i++;
            if (objItem._X_ADDRESS5 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_ADDRESS5);
            i++;
            parameters[i].Value = Claro.Utils.CheckDbl(objItem._X_CHARGE_AMOUNT);
            i++;
            if (objItem._X_CITY != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CITY);
            i++;
            if (objItem._X_CONTACT_SEX != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_CONTACT_SEX);
            i++;
            if (objItem._X_DEPARTMENT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_DEPARTMENT);
            i++;
            if (objItem._X_DISTRICT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_DISTRICT);
            i++;
            if (objItem._X_EMAIL_CONFIRMATION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_EMAIL_CONFIRMATION);
            i++;
            if (objItem._X_FAX != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FAX);
            i++;
            if (objItem._X_FLAG_CHARGE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_FLAG_CHARGE);
            i++;
            if (objItem._X_MARITAL_STATUS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_MARITAL_STATUS);
            i++;
            if (objItem._X_OCCUPATION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_OCCUPATION);
            i++;
            if (objItem._X_POSITION != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_POSITION);
            i++;
            if (objItem._X_REFERENCE_ADDRESS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_REFERENCE_ADDRESS);
            i++;
            if (objItem._X_TYPE_DOCUMENT != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_TYPE_DOCUMENT);
            i++;
            if (objItem._X_ZIPCODE != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_ZIPCODE);

            i++;
            if (objItem._X_ICCID != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem._X_ICCID);




            int result = 0;
            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER, parameters);

            flagInsercion = parameters[parameters.Length - 2].Value.ToString();
            strMessage = parameters[parameters.Length - 1].Value.ToString();

            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: sale de GetInserInteractionTemplatInsTemplateInteractioneresponse flagInsercion: " + flagInsercion + "strMessage: " + strMessage);

            return (flagInsercion.Equals(KEY.AppSettings("InteractionMessage")));
        }
        /// <summary>
        /// inserata interaccion
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objItem"></param>
        /// <param name="strInteractionid"></param>
        /// <param name="strFlaginsercion"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>InsertInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertInteraction(string strSesion, string strTransaction, Iteraction objItem, out string strInteractionid, out string strFlaginsercion, out string strMessage)
        {
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a InsertInteraction");

            DbParameter[] parameters = new DbParameter[]
                         {
                                  new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
							      new DbParameter("PN_SITEOBJID_1", DbType.Int64,ParameterDirection.Input),
								  new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
							      new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_AGENTE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PN_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input),
								  new DbParameter("PV_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_RESULTADO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
								  new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
								  new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)			
                      };
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }
            int i = 0;
            if (objItem.OBJID_CONTACTO != null)
                parameters[0].Value = 0;

            i++;
            if (objItem.OBJID_SITE != null)
                parameters[1].Value = Claro.Utils.CheckInt64(objItem.OBJID_SITE);

            i++;
            if (objItem.CUENTA != null)
                parameters[2].Value = objItem.CUENTA.ToString();

            i++;
            if (objItem.TELEFONO != null)
                parameters[3].Value = objItem.TELEFONO.ToString();
            i++;
            if (objItem.TIPO != null)
                parameters[4].Value = objItem.TIPO.ToString();

            i++;
            if (objItem.CLASE != null)
                parameters[5].Value = objItem.CLASE.ToString();

            i++;
            if (objItem.SUBCLASE != null)
                parameters[6].Value = objItem.SUBCLASE.ToString();

            i++;
            if (objItem.METODO != null)
                parameters[7].Value = objItem.METODO.ToString();

            i++;
            if (objItem.TIPO_INTER != null)
                parameters[8].Value = objItem.TIPO_INTER.ToString();

            i++;
            if (objItem.AGENTE != null)
                parameters[9].Value = objItem.AGENTE.ToString();

            i++;
            if (objItem.USUARIO_PROCESO != null)
                parameters[10].Value = objItem.USUARIO_PROCESO.ToString();

            i++;
            if (objItem.HECHO_EN_UNO != null)
                parameters[11].Value = objItem.HECHO_EN_UNO.ToString();

            i++;
            if (objItem.NOTAS != null)
                parameters[12].Value = objItem.NOTAS.ToString();

            i++;
            if (objItem.FLAG_CASO != null)
                parameters[13].Value = objItem.FLAG_CASO.ToString();

            i++;
            if (objItem.RESULTADO != null)
                parameters[14].Value = objItem.RESULTADO.ToString();

            int result = 0;
            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_INSERTAR_INTERACT, parameters);

            strInteractionid = Claro.Utils.CheckStr(parameters[15].Value.ToString());
            strFlaginsercion = Claro.Utils.CheckStr(parameters[16].Value.ToString());
            strMessage = Claro.Utils.CheckStr(parameters[17].Value.ToString());

            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: sale de InsertInteraction strInteractionid: " + strInteractionid + "strFlaginsercion: " + strFlaginsercion + "strMessage: " + strMessage);

            return (strFlaginsercion.Equals(KEY.AppSettings("InteractionMessage")));
        }

        /// <summary>
        /// inserta interaccion
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objItem"></param>
        /// <param name="intTeractionid"></param>
        /// <param name="strFlaginsercion"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>Insert</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool Insert(string strSesion, string strTransaction, Iteraction objItem, out string intTeractionid, out string strFlaginsercion, out string strMessage)
        {
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a Insert");

            DbParameter[] parameters = new DbParameter[] 
            {
            new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
				new DbParameter("P_SITEOBJID_1", DbType.String,ParameterDirection.Input),
				new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
				new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
				new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
				new DbParameter("MSG_TEXT", DbType.String,500,ParameterDirection.Output)			
            
            };



            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }
            if (objItem.OBJID_CONTACTO != null)
                parameters[0].Value = Claro.Utils.CheckInt64(objItem.OBJID_CONTACTO);


            if (objItem.OBJID_SITE != null)
                parameters[1].Value = Claro.Utils.CheckInt64(objItem.OBJID_SITE);


            if (objItem.CUENTA != null)
                parameters[2].Value = objItem.CUENTA.ToString();


            if (objItem.TELEFONO != null)
                parameters[3].Value = objItem.TELEFONO.ToString();

            if (objItem.TIPO != null)
                parameters[4].Value = objItem.TIPO.ToString();


            if (objItem.CLASE != null)
                parameters[5].Value = objItem.CLASE.ToString();


            if (objItem.SUBCLASE != null)
                parameters[6].Value = objItem.SUBCLASE.ToString();


            if (objItem.METODO != null)
                parameters[7].Value = objItem.METODO.ToString();


            if (objItem.TIPO_INTER != null)
                parameters[8].Value = objItem.TIPO_INTER.ToString();


            if (objItem.AGENTE != null)
                parameters[9].Value = objItem.AGENTE.ToString();


            if (objItem.USUARIO_PROCESO != null)
                parameters[10].Value = objItem.USUARIO_PROCESO.ToString();


            if (objItem.HECHO_EN_UNO != null)
                parameters[11].Value = objItem.HECHO_EN_UNO.ToString();


            if (objItem.NOTAS != null)
                parameters[12].Value = objItem.NOTAS.ToString();


            if (objItem.FLAG_CASO != null)
                parameters[13].Value = objItem.FLAG_CASO.ToString();


            if (objItem.RESULTADO != null)
                parameters[14].Value = objItem.RESULTADO.ToString();

            int result = 0;

            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);
            intTeractionid = Claro.Utils.CheckStr(parameters[15].Value);
            if (intTeractionid.ToUpper().Equals("NULL")) intTeractionid = "";
            strFlaginsercion = Claro.Utils.CheckStr(parameters[16].Value);
            strMessage = Claro.Utils.CheckStr(parameters[17].Value);

            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a Insert intTeractionid: " + intTeractionid + "strFlaginsercion: " + strFlaginsercion + "strMessage: " + strMessage);



            return (strFlaginsercion.Equals(KEY.AppSettings("InteractionMessage")));
        }
        /// <summary>
        /// obtiene datos cliente
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strPhone"></param>
        /// <param name="strAccount"></param>
        /// <param name="strContactobjid"></param>
        /// <param name="strflagreg"></param>
        /// <param name="strflagquery"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>GetClient</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static Client GetClient(string strSesion, string strTransaction, string strPhone, string strAccount, string strContactobjid, string strflagreg, out  string strflagquery, out string strMessage)
        {
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a GetClient strphone" + strPhone + "//straccount : " + strAccount + "// strContactobjid : " + strContactobjid + "//strflagreg : " + strflagreg);

            if (strContactobjid == "")
                strContactobjid = null;
            DbParameter[] parameters = new DbParameter[]
            {
            new DbParameter("P_PHONE", DbType.String,20,ParameterDirection.Input,strPhone),
			new DbParameter("P_ACCOUNT", DbType.String,80,ParameterDirection.Input,strAccount),
			new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,Convert.ToInt64(strContactobjid)),
		    new DbParameter("P_FLAG_REG", DbType.String,20,ParameterDirection.Input,strflagreg),												
			new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output),
			new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
			new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            
            };


            Client entity = null;
            DbFactory.ExecuteReader(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY, parameters, (IDataReader reader) =>
            {

                if (reader.Read())
                {

                    entity = new Client();
                    entity.OBJID_CONTACTO = Claro.Utils.CheckStr(reader["OBJID_CONTACTO"]);
                    entity.OBJID_SITE = Claro.Utils.CheckStr(reader["OBJID_SITE"]);
                    entity.TELEFONO = Claro.Utils.CheckStr(reader["TELEFONO"]);
                    entity.CUENTA = Claro.Utils.CheckStr(reader["CUENTA"]);
                    entity.MODALIDAD = Claro.Utils.CheckStr(reader["MODALIDAD"]);
                    entity.SEGMENTO = Claro.Utils.CheckStr(reader["SEGMENTO"]);
                    entity.ROL_CONTACTO = Claro.Utils.CheckStr(reader["ROL_CONTACTO"]);
                    entity.ESTADO_CONTACTO = Claro.Utils.CheckStr(reader["ESTADO_CONTACTO"]);
                    entity.ESTADO_CONTRATO = Claro.Utils.CheckStr(reader["ESTADO_CONTRATO"]);
                    entity.ESTADO_SITE = Claro.Utils.CheckStr(reader["ESTADO_SITE"]);
                    entity.S_NOMBRES = Claro.Utils.CheckStr(reader["S_NOMBRES"]);
                    entity.S_APELLIDOS = Claro.Utils.CheckStr(reader["S_APELLIDOS"]);
                    entity.NOMBRES = Claro.Utils.CheckStr(reader["NOMBRES"]);
                    entity.APELLIDOS = Claro.Utils.CheckStr(reader["APELLIDOS"]);
                    entity.DOMICILIO = Claro.Utils.CheckStr(reader["DOMICILIO"]);
                    entity.URBANIZACION = Claro.Utils.CheckStr(reader["URBANIZACION"]);
                    entity.REFERENCIA = Claro.Utils.CheckStr(reader["REFERENCIA"]);
                    entity.CIUDAD = Claro.Utils.CheckStr(reader["CIUDAD"]);
                    entity.DISTRITO = Claro.Utils.CheckStr(reader["DISTRITO"]);
                    entity.DEPARTAMENTO = Claro.Utils.CheckStr(reader["DEPARTAMENTO"]);
                    entity.ZIPCODE = Claro.Utils.CheckStr(reader["ZIPCODE"]);
                    entity.EMAIL = Claro.Utils.CheckStr(reader["EMAIL"]);
                    entity.TELEF_REFERENCIA = Claro.Utils.CheckStr(reader["TELEF_REFERENCIA"]);
                    entity.FAX = Claro.Utils.CheckStr(reader["FAX"]);
                    DateTime fecha = new DateTime(1, 1, 1);
                    if (reader["FECHA_NAC"] != DBNull.Value)
                        if (Claro.Utils.CheckDate(reader["FECHA_NAC"]) != fecha)
                            entity.FECHA_NAC = Claro.Utils.CheckDate(reader["FECHA_NAC"]);
                    entity.SEXO = Claro.Utils.CheckStr(reader["SEXO"]);
                    entity.ESTADO_CIVIL = Claro.Utils.CheckStr(reader["ESTADO_CIVIL"]);
                    entity.TIPO_DOC = Claro.Utils.CheckStr(reader["TIPO_DOC"]);
                    entity.NRO_DOC = Claro.Utils.CheckStr(reader["NRO_DOC"]);
                    entity.FECHA_ACT = Claro.Utils.CheckDate(reader["FECHA_ACT"]);
                    entity.PUNTO_VENTA = Claro.Utils.CheckStr(reader["PUNTO_VENTA"]);
                    entity.FLAG_REGISTRADO = Claro.Utils.CheckInt(reader["FLAG_REGISTRADO"]);
                    entity.OCUPACION = Claro.Utils.CheckStr(reader["OCUPACION"]);
                    entity.CANT_REG = Claro.Utils.CheckStr(reader["CANT_REG"]);
                    entity.FLAG_EMAIL = Claro.Utils.CheckStr(reader["FLAG_EMAIL"]);
                    entity.MOTIVO_REGISTRO = Claro.Utils.CheckStr(reader["MOTIVO_REGISTRO"]);
                    entity.FUNCION = Claro.Utils.CheckStr(reader["FUNCION"]);
                    entity.CARGO = Claro.Utils.CheckStr(reader["CARGO"]);
                    entity.LUGAR_NACIMIENTO_DES = Claro.Utils.CheckStr(reader["LUGAR_NAC"]);

                }
            });
            strflagquery = Claro.Utils.CheckStr(parameters[4].Value.ToString());
            strMessage = Claro.Utils.CheckStr(parameters[5].Value.ToString());

            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: sale de GetObtClient Flagquery" + strflagquery + "//MsgText : " + strMessage);


            return entity;
        }


        /// <summary>
        /// insertar negocio interaccion
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objItem"></param>
        /// <param name="strInteractionId"></param>
        /// <param name="strFlagInsertion"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>InsertBusinessInteraction2</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertBusinessInteraction2(string strSesion, string strTransaction, Iteraction objItem, out string strInteractionId, out string strFlagInsertion, out string strMessage)
        {
            DbParameter[] parameters = new DbParameter[] {
                                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input), 
								new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input), 
                                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),												   
								new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),													   
								new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_HECHO_EN_UNO", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)};
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
            parameters[0].Value = objItem.OBJID_CONTACTO;
            parameters[1].Value = objItem.OBJID_SITE;
            parameters[2].Value = objItem.CUENTA;
            parameters[3].Value = objItem.TELEFONO;
            parameters[4].Value = objItem.TIPO;
            parameters[5].Value = objItem.CLASE;
            parameters[6].Value = objItem.SUBCLASE;
            parameters[7].Value = objItem.METODO;
            parameters[8].Value = objItem.TIPO_INTER;
            parameters[9].Value = objItem.AGENTE;
            parameters[10].Value = objItem.USUARIO_PROCESO;
            parameters[11].Value = objItem.HECHO_EN_UNO;
            parameters[12].Value = objItem.NOTAS;
            parameters[13].Value = objItem.FLAG_CASO;
            parameters[14].Value = objItem.RESULTADO;

            int result =
            DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);

            strInteractionId = Claro.Utils.CheckStr(parameters[parameters.Length - 3].Value);
            strFlagInsertion = Claro.Utils.CheckStr(parameters[parameters.Length - 2].Value);
            strMessage = Claro.Utils.CheckStr(parameters[parameters.Length - 1].Value);

            if (String.IsNullOrEmpty(strMessage)) strMessage = KEY.AppSettings("InteractionMessage").ToString();

            return (result > 0);
        }





        #endregion


        /// <summary>
        /// obtiene validacion del cliente
        /// </summary>
        /// <param name="oGetCustomerRequest"></param>
        /// <returns></returns>
        /// <remarks>GetValidateCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.GetCustomer.CustomerResponse GetValidateCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_phone", DbType.String,30, ParameterDirection.Input, oGetCustomerRequest.vPhone),
                new DbParameter("p_contactobjid", DbType.Int32,225, ParameterDirection.Output),
                new DbParameter("p_flag_insert", DbType.String,225, ParameterDirection.Output),
                new DbParameter("p_msg_text", DbType.String,225, ParameterDirection.Output)
            };
            EntitiesFixed.GetCustomer.CustomerResponse oCustomerResponse = new EntitiesFixed.GetCustomer.CustomerResponse();
            try
            {
                Claro.Web.Logging.ExecuteMethod(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SA_SP_SEARCH_CONTACT_USERLDI, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, ex.Message);

                Web.Logging.Info(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, "");
            }
            finally
            {
                oCustomerResponse.contactobjid = parameters[parameters.Length - 3].Value.ToString();
                oCustomerResponse.vFlagInsert = parameters[parameters.Length - 2].Value.ToString();
                oCustomerResponse.rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }
            return oCustomerResponse;
        }

        #region FDQ


        /// <summary>
        /// guardar auditoria
        /// </summary>
        /// <param name="objRegistAuditoria"></param>
        /// <returns></returns>
        /// <remarks>SaveAuditM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static SECURITY.GetSaveAuditM.SaveAuditMResponse SaveAuditM(Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMRequest objRegistAuditoria)
        {

            SECURITY.GetSaveAuditM.SaveAuditMResponse oRegAuditoria = new SECURITY.GetSaveAuditM.SaveAuditMResponse();

            REGISTAUDIT.RegistroRequest oRegistroRequest = new REGISTAUDIT.RegistroRequest();
            REGISTAUDIT.registroAuditoriaResponse oRegistroAuditoriaResponse = new REGISTAUDIT.registroAuditoriaResponse();
            REGISTAUDIT.registroAuditoria oRegistroAuditoria = new REGISTAUDIT.registroAuditoria();

            REGISTAUDIT.RequestOpcionalComplexType[] listOptional = new REGISTAUDIT.RequestOpcionalComplexType[0];

            REGISTAUDIT.AuditRequest oAuditRequest = new REGISTAUDIT.AuditRequest();
            oAuditRequest.idTransaccion = objRegistAuditoria.Audit.Transaction;
            oAuditRequest.ipAplicacion = objRegistAuditoria.Audit.IPAddress;
            oAuditRequest.aplicacion = objRegistAuditoria.Audit.ApplicationName;
            oAuditRequest.usrAplicacion = objRegistAuditoria.Audit.UserName;

            oRegistroAuditoria.Audit = oAuditRequest;
            //oRegistroAuditoria.ListaOpcionalRequest = listOptional;
            oRegistroRequest.transaccion = objRegistAuditoria.vTransaccion;
            oRegistroRequest.servicio = objRegistAuditoria.vServicio;
            oRegistroRequest.ipCliente = objRegistAuditoria.vIpCliente;
            oRegistroRequest.nombreCliente = objRegistAuditoria.vNombreCliente;
            oRegistroRequest.ipServidor = objRegistAuditoria.vIpServidor;
            oRegistroRequest.nombreServidor = objRegistAuditoria.vNombreServidor;
            oRegistroRequest.cuentaUsuario = objRegistAuditoria.vCuentaUsuario;
            oRegistroRequest.telefono = objRegistAuditoria.vTelefono;
            oRegistroRequest.monto = objRegistAuditoria.vMonto;
            oRegistroRequest.texto = objRegistAuditoria.vTexto;
            oRegistroAuditoria.RegistroRequest = oRegistroRequest;


            oRegistroAuditoriaResponse = Claro.Web.Logging.ExecuteMethod(objRegistAuditoria.Audit.Session, objRegistAuditoria.Audit.Transaction, WebServiceConfiguration.REGISTRARAUDIT, () =>
            {
                return WebServiceConfiguration.REGISTRARAUDIT.registroAuditoria(oRegistroAuditoria);

            });

            oRegAuditoria.vResultado = oRegistroAuditoriaResponse.AuditResponse.mensajeRespuesta;
            oRegAuditoria.vTransaccionResp = oRegistroAuditoriaResponse.AuditResponse.idTransaccion;

            if (oRegistroAuditoriaResponse.AuditResponse.codigoRespuesta == "0")
            {
                oRegAuditoria.respuesta = false;
            }
            else
            {
                oRegAuditoria.respuesta = true;
            }

            return oRegAuditoria;
        }
        #endregion
        /// <summary>
        /// inserta interaccion para hfc
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="item"></param>
        /// <param name="rInteraccionId"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        /// <remarks>GetInsertInteractHFC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetInsertInteractHFC(string strIdSession, string strTransaction, EntitiesFixed.Interaction item,
            ref string rInteraccionId,
            ref string rFlagInsercion,
            ref string rMsgText)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SERVAFECT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INCONVEN", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SERVAFECT_CODE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INCONVEN_CODE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_CO_ID", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_COD_PLANO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_VALOR1", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_VALOR2", DbType.String,255,ParameterDirection.Input),
                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)			
            };

            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            if (item.OBJID_CONTACTO != null)
                parameters[i].Value = Convert.ToInt64(item.OBJID_CONTACTO);

            i++;
            if (item.OBJID_SITE != null)
                parameters[i].Value = Convert.ToInt64(item.OBJID_SITE);

            i++;
            if (item.CUENTA != null)
                parameters[i].Value = item.CUENTA;

            i++;
            if (item.TELEFONO != null)
                parameters[i].Value = item.TELEFONO;
            i++;
            if (item.TIPO != null)
                parameters[i].Value = item.TIPO;

            i++;
            if (item.CLASE != null)
                parameters[i].Value = item.CLASE;

            i++;
            if (item.SUBCLASE != null)
                parameters[i].Value = item.SUBCLASE;

            i++;
            if (item.METODO != null)
                parameters[i].Value = item.METODO;

            i++;
            if (item.TIPO_INTER != null)
                parameters[i].Value = item.TIPO_INTER;

            i++;
            if (item.AGENTE != null)
                parameters[i].Value = item.AGENTE;

            i++;
            if (item.USUARIO_PROCESO != null)
                parameters[i].Value = item.USUARIO_PROCESO;

            i++;
            if (item.HECHO_EN_UNO != null)
                parameters[i].Value = item.HECHO_EN_UNO;

            i++;
            if (item.NOTAS != null)
                parameters[i].Value = item.NOTAS;

            i++;
            if (item.FLAG_CASO != null)
                parameters[i].Value = item.FLAG_CASO;

            i++;
            if (item.RESULTADO != null)
                parameters[i].Value = item.RESULTADO;

            i++;
            if (item.SERVICIO != null)
                parameters[i].Value = item.SERVICIO;

            i++;
            if (item.INCONVENIENTE != null)
                parameters[i].Value = item.INCONVENIENTE;

            i++;
            if (item.SERVICIO_CODE != null)
                parameters[i].Value = item.SERVICIO_CODE;

            i++;
            if (item.INCONVENIENTE_CODE != null)
                parameters[i].Value = item.INCONVENIENTE_CODE;

            i++;
            if (item.CONTRATO != null)
                parameters[i].Value = item.CONTRATO;

            i++;
            if (item.PLANO != null)
                parameters[i].Value = item.PLANO;

            i++;
            if (item.VALOR_1 != null)
                parameters[i].Value = item.VALOR_1;

            i++;
            if (item.VALOR_2 != null)
                parameters[i].Value = item.VALOR_2;



            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rInteraccionId = parameters[parameters.Length - 3].Value.ToString();
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

                rInteraccionId = Claro.Utils.CheckStr(rInteraccionId);
                rFlagInsercion = Claro.Utils.CheckStr(rFlagInsercion);
                rMsgText = Claro.Utils.CheckStr(rMsgText);

                Logging.Info(strIdSession, strTransaction, "INTERACCIÓN FRONT END: " + rInteraccionId);
                Logging.Info(strIdSession, strTransaction, "INTERACCIÓN FRONT END: " + rFlagInsercion);
                Logging.Info(strIdSession, strTransaction, "INTERACCIÓN FRONT END: " + rMsgText);
            }

            return true;
        }
        /// <summary>
        /// obtiene plantilla interaccion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vInteraccionID"></param>
        /// <param name="vFLAG_CONSULTA"></param>
        /// <param name="vMSG_TEXT"></param>
        /// <returns></returns>
        /// <remarks>GetDatTempInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static SECURITY.InteractionTemplate GetDatTempInteraction(string strIdSession, string strTransaction, string vInteraccionID, ref string vFLAG_CONSULTA, ref string vMSG_TEXT)
        {
            DbParameter[] parameters = {
                new DbParameter("P_NRO_INTERACCION", DbType.String,255,ParameterDirection.Input, vInteraccionID),
                new DbParameter("FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output, vFLAG_CONSULTA),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,vMSG_TEXT),
                new DbParameter("OUT_CURSOR", DbType.Object,ParameterDirection.Output)
            };

            var item = new SECURITY.InteractionTemplate();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                item.TIENE_DATOS = "S";
                                item.ID_INTERACCION = reader["NRO_INTERACCION"].ToString();
                                item.X_INTER_1 = reader["X_INTER_1"].ToString();
                                item.X_INTER_2 = reader["X_INTER_2"].ToString();
                                item.X_INTER_3 = reader["X_INTER_3"].ToString();
                                item.X_INTER_4 = reader["X_INTER_4"].ToString();
                                item.X_INTER_5 = reader["X_INTER_5"].ToString();
                                item.X_INTER_6 = reader["X_INTER_6"].ToString();
                                item.X_INTER_7 = reader["X_INTER_7"].ToString();
                                item.X_INTER_8 = Convert.ToDouble(reader["X_INTER_8"]);
                                item.X_INTER_9 = Convert.ToDouble(reader["X_INTER_9"]);
                                item.X_INTER_10 = Convert.ToDouble(reader["X_INTER_10"]);
                                item.X_INTER_11 = Convert.ToDouble(reader["X_INTER_11"]);
                                item.X_INTER_12 = Convert.ToDouble(reader["X_INTER_12"]);
                                item.X_INTER_13 = Convert.ToDouble(reader["X_INTER_13"]);
                                item.X_INTER_14 = Convert.ToDouble(reader["X_INTER_14"]);
                                item.X_INTER_15 = reader["X_INTER_15"].ToString();
                                item.X_INTER_16 = reader["X_INTER_16"].ToString();
                                item.X_INTER_17 = reader["X_INTER_17"].ToString();
                                item.X_INTER_18 = reader["X_INTER_18"].ToString();
                                item.X_INTER_19 = reader["X_INTER_19"].ToString();
                                item.X_INTER_20 = reader["X_INTER_20"].ToString();
                                item.X_INTER_21 = reader["X_INTER_21"].ToString();
                                item.X_INTER_22 = Convert.ToDouble(reader["X_INTER_22"]);
                                item.X_INTER_23 = Convert.ToDouble(reader["X_INTER_23"]);
                                item.X_INTER_24 = Convert.ToDouble(reader["X_INTER_24"]);
                                item.X_INTER_25 = Convert.ToDouble(reader["X_INTER_25"]);
                                item.X_INTER_26 = Convert.ToDouble(reader["X_INTER_26"]);
                                item.X_INTER_27 = Convert.ToDouble(reader["X_INTER_27"]);
                                item.X_INTER_28 = Convert.ToDouble(reader["X_INTER_28"]);
                                item.X_INTER_29 = reader["X_INTER_29"].ToString();
                                item.X_INTER_30 = reader["X_INTER_30"].ToString();
                                item.X_PLUS_INTER2INTERACT = Convert.ToDouble(reader["X_PLUS_INTER2INTERACT"]);
                                item.X_ADJUSTMENT_AMOUNT = Convert.ToDouble(reader["X_ADJUSTMENT_AMOUNT"]);
                                item.X_ADJUSTMENT_REASON = reader["X_ADJUSTMENT_REASON"].ToString();
                                item.X_ADDRESS = reader["X_ADDRESS"].ToString();
                                item.X_AMOUNT_UNIT = reader["X_AMOUNT_UNIT"].ToString();

                                if (reader["X_BIRTHDAY"] != null && reader["X_BIRTHDAY"] != DBNull.Value)
                                    item.X_BIRTHDAY = Convert.ToDate(reader["X_BIRTHDAY"]);

                                item.X_CLARIFY_INTERACTION = reader["X_CLARIFY_INTERACTION"].ToString();
                                item.X_CLARO_LDN1 = reader["X_CLARO_LDN1"].ToString();
                                item.X_CLARO_LDN2 = reader["X_CLARO_LDN2"].ToString();
                                item.X_CLARO_LDN3 = reader["X_CLARO_LDN3"].ToString();
                                item.X_CLARO_LDN4 = reader["X_CLARO_LDN4"].ToString();
                                item.X_CLAROLOCAL1 = reader["X_CLAROLOCAL1"].ToString();
                                item.X_CLAROLOCAL2 = reader["X_CLAROLOCAL2"].ToString();
                                item.X_CLAROLOCAL3 = reader["X_CLAROLOCAL3"].ToString();
                                item.X_CLAROLOCAL4 = reader["X_CLAROLOCAL4"].ToString();
                                item.X_CLAROLOCAL5 = reader["X_CLAROLOCAL5"].ToString();
                                item.X_CLAROLOCAL6 = reader["X_CLAROLOCAL6"].ToString();
                                item.X_CONTACT_PHONE = reader["X_CONTACT_PHONE"].ToString();
                                item.X_DNI_LEGAL_REP = reader["X_DNI_LEGAL_REP"].ToString();
                                item.X_DOCUMENT_NUMBER = reader["X_DOCUMENT_NUMBER"].ToString();
                                item.X_EMAIL = reader["X_EMAIL"].ToString();
                                item.X_FIRST_NAME = reader["X_FIRST_NAME"].ToString();
                                item.X_FIXED_NUMBER = reader["X_FIXED_NUMBER"].ToString();
                                item.X_FLAG_CHANGE_USER = reader["X_FLAG_CHANGE_USER"].ToString();
                                item.X_FLAG_LEGAL_REP = reader["X_FLAG_LEGAL_REP"].ToString();
                                item.X_FLAG_OTHER = reader["X_FLAG_OTHER"].ToString();
                                item.X_FLAG_TITULAR = reader["X_FLAG_TITULAR"].ToString();
                                item.X_IMEI = reader["X_IMEI"].ToString();
                                item.X_LAST_NAME = reader["X_LAST_NAME"].ToString();
                                item.X_LASTNAME_REP = reader["X_LASTNAME_REP"].ToString();
                                item.X_LDI_NUMBER = reader["X_LDI_NUMBER"].ToString();
                                item.X_NAME_LEGAL_REP = reader["X_NAME_LEGAL_REP"].ToString();
                                item.X_OLD_CLARO_LDN1 = reader["X_OLD_CLARO_LDN1"].ToString();
                                item.X_OLD_CLARO_LDN2 = reader["X_OLD_CLARO_LDN2"].ToString();
                                item.X_OLD_CLARO_LDN3 = reader["X_OLD_CLARO_LDN3"].ToString();
                                item.X_OLD_CLARO_LDN4 = reader["X_OLD_CLARO_LDN4"].ToString();
                                item.X_OLD_CLAROLOCAL1 = reader["X_OLD_CLAROLOCAL1"].ToString();
                                item.X_OLD_CLAROLOCAL2 = reader["X_OLD_CLAROLOCAL2"].ToString();
                                item.X_OLD_CLAROLOCAL3 = reader["X_OLD_CLAROLOCAL3"].ToString();
                                item.X_OLD_CLAROLOCAL4 = reader["X_OLD_CLAROLOCAL4"].ToString();
                                item.X_OLD_CLAROLOCAL5 = reader["X_OLD_CLAROLOCAL5"].ToString();
                                item.X_OLD_CLAROLOCAL6 = reader["X_OLD_CLAROLOCAL6"].ToString();
                                item.X_OLD_DOC_NUMBER = reader["X_OLD_DOC_NUMBER"].ToString();
                                item.X_OLD_FIRST_NAME = reader["X_OLD_FIRST_NAME"].ToString();
                                item.X_OLD_FIXED_PHONE = reader["X_OLD_FIXED_PHONE"].ToString();
                                item.X_OLD_LAST_NAME = reader["X_OLD_LAST_NAME"].ToString();
                                item.X_OLD_LDI_NUMBER = reader["X_OLD_LDI_NUMBER"].ToString();
                                item.X_OLD_FIXED_NUMBER = reader["X_OLD_FIXED_NUMBER"].ToString();
                                item.X_OPERATION_TYPE = reader["X_OPERATION_TYPE"].ToString();
                                item.X_OTHER_DOC_NUMBER = reader["X_OTHER_DOC_NUMBER"].ToString();
                                item.X_OTHER_FIRST_NAME = reader["X_OTHER_FIRST_NAME"].ToString();
                                item.X_OTHER_LAST_NAME = reader["X_OTHER_LAST_NAME"].ToString();
                                item.X_OTHER_PHONE = reader["X_OTHER_PHONE"].ToString();
                                item.X_PHONE_LEGAL_REP = reader["X_PHONE_LEGAL_REP"].ToString();
                                item.X_REFERENCE_PHONE = reader["X_REFERENCE_PHONE"].ToString();
                                item.X_REASON = reader["X_REASON"].ToString();
                                item.X_MODEL = reader["X_MODEL"].ToString();
                                item.X_LOT_CODE = reader["X_LOT_CODE"].ToString();
                                item.X_FLAG_REGISTERED = reader["X_FLAG_REGISTERED"].ToString();
                                item.X_REGISTRATION_REASON = reader["X_REGISTRATION_REASON"].ToString();
                                item.X_CLARO_NUMBER = reader["X_CLARO_NUMBER"].ToString();
                                item.X_MONTH = reader["X_MONTH"].ToString();
                                item.X_OST_NUMBER = reader["X_OST_NUMBER"].ToString();
                                item.X_BASKET = reader["X_BASKET"].ToString();
                                if (reader["X_EXPIRE_DATE"] != null && reader["X_EXPIRE_DATE"] != DBNull.Value)
                                    item.X_EXPIRE_DATE = Convert.ToDate(reader["X_EXPIRE_DATE"]);
                                item.X_ADDRESS5 = reader["X_ADDRESS5"].ToString();
                                item.X_CHARGE_AMOUNT = Convert.ToDouble(reader["X_CHARGE_AMOUNT"]);
                                item.X_CITY = reader["X_CITY"].ToString();
                                item.X_CONTACT_SEX = reader["X_CONTACT_SEX"].ToString();
                                item.X_DEPARTMENT = reader["X_DEPARTMENT"].ToString();
                                item.X_DISTRICT = reader["X_DISTRICT"].ToString();
                                item.X_EMAIL_CONFIRMATION = reader["X_EMAIL_CONFIRMATION"].ToString();
                                item.X_FAX = reader["X_FAX"].ToString();
                                item.X_FLAG_CHARGE = reader["X_FLAG_CHARGE"].ToString();
                                item.X_MARITAL_STATUS = reader["X_MARITAL_STATUS"].ToString();
                                item.X_OCCUPATION = reader["X_OCCUPATION"].ToString();
                                item.X_POSITION = reader["X_POSITION"].ToString();
                                item.X_REFERENCE_ADDRESS = reader["X_REFERENCE_ADDRESS"].ToString();
                                item.X_TYPE_DOCUMENT = reader["X_TYPE_DOCUMENT"].ToString();
                                item.X_ZIPCODE = reader["X_ZIPCODE"].ToString();
                                item.X_ICCID = reader["X_ICCID"].ToString();
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                vFLAG_CONSULTA = parameters[parameters.Length - 2].Value.ToString();
                vMSG_TEXT = parameters[parameters.Length - 1].ToString();
            }

            return item;
        }

        #region Redirect
        /// <summary>
        /// valida redireccion
        /// </summary>
        /// <param name="audit"></param>
        /// <param name="sequence"></param>
        /// <param name="errorMsg"></param>
        /// <param name="ipServer"></param>
        /// <param name="urlDest"></param>
        /// <param name="availability"></param>
        /// <param name="jsonParameters"></param>
        /// <returns></returns>
        /// <remarks>ValidateRedirectCommunication</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static Boolean ValidateRedirectCommunication(Claro.Entity.AuditRequest audit, string sequence, out string errorMsg, string ipServer, out string urlDest, out string availability, out string jsonParameters)
        {
            Claro.Web.Logging.Info(audit.Session, audit.Transaction, "sequence1 " + sequence + "ipServer1" + ipServer);
            errorMsg = "";
            urlDest = "";
            availability = "";
            jsonParameters = "";

            try
            {
                AUDIT_CREDENTIALS.ListaResponseOpcional objResponseOpc;

                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse = null;

                Claro.Web.Logging.Info(audit.Session, audit.Transaction, "sequence2 " + sequence + "ipServer2" + ipServer);

                objAuditResponse = Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.validarComunicacion(new AUDIT_CREDENTIALS.parametrosAuditRequest()
                {
                    idTransaccion = audit.Transaction,
                    ipAplicacion = audit.IPAddress,
                    nombreAplicacion = audit.ApplicationName,
                    usuarioAplicacion = audit.UserName,
                },
                sequence,
                ipServer,
                new AUDIT_CREDENTIALS.ListaRequestOpcional(),
                out urlDest,
                out availability,
                out jsonParameters,
                out objResponseOpc);



                string codError = objAuditResponse.codigoRespuesta;

                if (codError == Claro.Constants.NumberZeroString)
                {
                    if (jsonParameters != "" && urlDest != "")
                    {
                        //errorMsg = Claro.SIACU.Constants.ok;
                        errorMsg = "OK";
                    }
                    else
                    {
                        errorMsg = objAuditResponse.mensajeRespuesta;
                    }
                }
                else
                {
                    errorMsg = objAuditResponse.mensajeRespuesta;
                }
            }
            catch (TimeoutException ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, "TimeoutException_ex_: " + ex + "TimeoutException_ex.iner" + ex.InnerException);
                // errorMsg = Claro.SIACU.Transac.Service.Constants.MessageNotServicesLimitWait;
                errorMsg = "No se ha podido completar la operación debido a que el servicio web ha superado el tiempo límite de espera.";
                throw ex;
            }
            catch (WebException ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, "WebException_ex_: " + ex + "WebException_ex.iner" + ex.InnerException);
                //errorMsg = Claro.SIACU.Transac.Service.Constants.MessageNotComunicationServerRemote;
                errorMsg = "No se ha podido establecer comunicación con el servidor remoto.";
                throw ex;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, "Exception_ex_: " + ex + "Exception_ex.iner" + ex.InnerException);
                errorMsg = ex.Message;
                throw;
            }

            return true;
        }
        #endregion
        /// <summary>
        /// obtiene empleado por usuario
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="Transaction"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        /// <remarks>GetEmployeByUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.Employee> GetEmployeByUser(string IdSession, string Transaction, string UserName)
        {

            AUDIT_SECURITY.EbsAuditoriaClient objEbsAuditoria = Configuration.ServiceConfiguration.SECURITY_PERMISSIONS;

            AUDIT_SECURITY.DatosEmpleadoRequest objRequest = new AUDIT_SECURITY.DatosEmpleadoRequest()
            {
                login = UserName
            };

            AUDIT_SECURITY.EmpleadoResponse objEmpleadoResponse = null;
            Claro.Web.Logging.Info(IdSession, Transaction,
            string.Format("El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerDatosEmpleado, Parametros de Entrada: login={0}", UserName));
            objEmpleadoResponse = objEbsAuditoria.leerDatosEmpleado(objRequest);
            List<Entity.IFI.Common.Employee> lstEmploye = null;
            if (objEmpleadoResponse != null && (objEmpleadoResponse.empleados != null && objEmpleadoResponse.empleados.item != null && objEmpleadoResponse.empleados.item.Length > 0))
            {
                AUDIT_SECURITY.EmpleadoType[] objEmpleadoType = objEmpleadoResponse.empleados.item;
                lstEmploye = new List<Entity.IFI.Common.Employee>();
                foreach (AUDIT_SECURITY.EmpleadoType item in objEmpleadoType)
                {
                    lstEmploye.Add(new Employee()
                    {
                        Login = item.login,
                        UserID = int.Parse(item.codigo),
                        FullName = string.Format("{0} {1} {2}", item.nombres, item.paterno, item.materno),
                        FirstName = item.nombres,
                        LastName1 = item.paterno,
                        LastName2 = item.materno,
                        SearchUser = "0"
                    });
                }
            }
            else
            {
                Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerDatosEmpleado , No devuelve resultados");
            }

            return lstEmploye;
        }
        /// <summary>
        /// lee las opciones por usuario
        /// </summary>
        /// <param name="IdSession"></param>
        /// <param name="Transaction"></param>
        /// <param name="IdAplication"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        /// <remarks>ReadOptionsByUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.PaginaOption> ReadOptionsByUser(string IdSession, string Transaction, int IdAplication, int IdUser)
        {

            AUDIT_SECURITY.EbsAuditoriaClient objEbsAuditoria = Configuration.ServiceConfiguration.SECURITY_PERMISSIONS;

            AUDIT_SECURITY.PaginaOpcionesUsuarioRequest objRequest = new AUDIT_SECURITY.PaginaOpcionesUsuarioRequest();
            objRequest.aplicCod = IdAplication;
            objRequest.user = IdUser;

            AUDIT_SECURITY.PaginaOpcionesUsuarioResponse objleerOpcionesPorUsuarioResponse = null;
            Claro.Web.Logging.Info(IdSession, Transaction,
            string.Format("El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario, Parametros de Entrada: aplicCod={0} ,user={1}", IdAplication, IdUser));
            objleerOpcionesPorUsuarioResponse = objEbsAuditoria.leerPaginaOpcionesPorUsuario(objRequest);
            List<Entity.IFI.Common.PaginaOption> lstPaginaOption = null;
            if (objleerOpcionesPorUsuarioResponse != null && objleerOpcionesPorUsuarioResponse.listaOpciones.Length > 0)
            {
                lstPaginaOption = new List<Entity.IFI.Common.PaginaOption>();
                AUDIT_SECURITY.PaginaOpcionType[] arrPaginaOpcionType = objleerOpcionesPorUsuarioResponse.listaOpciones;
                foreach (AUDIT_SECURITY.PaginaOpcionType item in arrPaginaOpcionType)
                {
                    lstPaginaOption.Add(new Entity.IFI.Common.PaginaOption()
                    {
                        OptionCode = item.opcicCod,
                        OptionDescription = item.opcicDes,
                        Clave = item.clave
                    });
                }
            }
            else
            {
                Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario , No devuelve resultados");
            }

            return lstPaginaOption;
        }
        /// <summary>
        /// valida usuario
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="IdTransaction"></param>
        /// <param name="IpAplication"></param>
        /// <param name="Aplication"></param>
        /// <param name="user"></param>
        /// <param name="AppCode"></param>
        /// <returns></returns>
        /// <remarks>CheckingUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static Entity.IFI.Common.CheckingUser.CheckingUserResponse CheckingUser(string sessionId, string IdTransaction, string IpAplication, string Aplication, string user, long AppCode)
        {

            Entity.IFI.Common.CheckingUser.CheckingUserResponse objCheckingUserResponse = new Entity.IFI.Common.CheckingUser.CheckingUserResponse();
            List<Entity.IFI.Common.ConsultSecurity> list = new List<Entity.IFI.Common.ConsultSecurity>();
            CONSULTA_SECURITY.ConsultaSeguridad consulta = Configuration.ServiceConfiguration.SIACU_ConsultaSeguridad;
            CONSULTA_SECURITY.verificaUsuarioRequest request = new CONSULTA_SECURITY.verificaUsuarioRequest();
            CONSULTA_SECURITY.verificaUsuarioResponse response;

            request.idTransaccion = IdTransaction;
            request.ipAplicacion = IpAplication;
            request.aplicacion = Aplication;
            request.usuarioLogin = user;
            request.appCod = AppCode;

            CONSULTA_SECURITY.seguridadType[] seguridadType;

            response = consulta.verificaUsuario(request);
            seguridadType = response.cursorSeguridad;


            Claro.Web.Logging.Info("", IdTransaction,
            string.Format("El servicio ConsultaSeguridad(strWebServiceDBAUDIT) Metodo: verificaUsuario Parametros de Entrada: idTransaccion={0} ,IpAplication={1}, Aplication={2}, usuarioLogin={3} , AppCode={4}", IdTransaction, IpAplication, Aplication, user, AppCode));
            if (seguridadType != null && seguridadType.Length > 0)
            {
                int seguridadTypeCount = seguridadType.Length;
                for (int i = 0; i < seguridadTypeCount; i++)
                {
                    Entity.IFI.Common.ConsultSecurity item = new Entity.IFI.Common.ConsultSecurity();

                    item.Usuaccod = seguridadType[i].UsuacCod;
                    item.Perfccod = seguridadType[i].PerfcCod;
                    item.Usuaccodvensap = seguridadType[i].UsuacCodVenSap;
                    list.Add(item);
                }
            }
            else
            {
                Claro.Web.Logging.Error("", IdTransaction, "El servicio ConsultaSeguridad(strWebServiceDBAUDIT) Metodo: verificaUsuario , No devuelve resultados");
            }

            objCheckingUserResponse.consultasSeguridad = list;
            objCheckingUserResponse.IdTransaccion = response.idTransaccion;
            objCheckingUserResponse.CodeErr = response.errorCode;
            objCheckingUserResponse.MsgErr = response.errorMsg;

            return objCheckingUserResponse;
        }
        /// <summary>
        /// obtiene numero EAI
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vMsisdn"></param>
        /// <param name="vNumber"></param>
        /// <returns></returns>
        /// <remarks>GetNumberEAI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetNumberEAI(string strIdSession, string strTransaction, string vMsisdn, ref string vNumber)
        {
            //string vNumber = "";
            DbParameter[] parameters = {
                new  DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input),
                new  DbParameter("P_NUMERO", DbType.String,255, ParameterDirection.Output)
            };
            for (int i = 0; i < parameters.Length; i++)
                parameters[i].Value = DBNull.Value;

            parameters[0].Value = vMsisdn;
            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_NUMERO, parameters);
            vNumber = Claro.Utils.CheckStr(parameters[1].Value);
            return vNumber;
        }
        /// <summary>
        /// Obtiene numero GWP
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vMsisdn"></param>
        /// <param name="vNumber"></param>
        /// <returns></returns>
        /// <remarks>GetNumberGWP</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetNumberGWP(string strIdSession, string strTransaction, string vMsisdn, ref string vNumber)
        {
            //string vNumber = "";
            DbParameter[] parameters = {
                new  DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input),
                new  DbParameter("P_NUMERO", DbType.String,255, ParameterDirection.Output)
            };

            for (int i = 0; i < parameters.Length; i++)
                parameters[i].Value = DBNull.Value;

            parameters[0].Value = vMsisdn;

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_GWP, DbCommandConfiguration.SIACU_SP_OBTENER_NUMERO_PORT, parameters);
            vNumber = Claro.Utils.CheckStr(parameters[1].Value);
            return vNumber;
        }
        /// <summary>
        /// Evalua monto dcm
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vListaPerfil"></param>
        /// <param name="vMonto"></param>
        /// <param name="vUnidad"></param>
        /// <param name="vModalidad"></param>
        /// <param name="vTipoTelefono"></param>
        /// <returns></returns>
        /// <remarks>GetEvaluateAmount_DCM</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetEvaluateAmount_DCM(string strIdSession, string strTransaction, string vListaPerfil, double vMonto, string vUnidad, string vModalidad, string vTipoTelefono)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_LISTA_PERFIL", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("P_MONTO", DbType.Double, ParameterDirection.Input),
                new DbParameter("P_UNIDAD", DbType.String, 255, ParameterDirection.Input, vUnidad),
                new DbParameter("P_MODALIDAD", DbType.String, 255, ParameterDirection.Input, vModalidad),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, 255,ParameterDirection.Input, vTipoTelefono),
                new DbParameter("P_VALIDACION", DbType.String, 255, ParameterDirection.Output)
            };

            string[] arrLista = System.Text.RegularExpressions.Regex.Split(vListaPerfil, ",");
            var perfiles = string.Empty;
            var perfil = string.Empty;
            var resultado = false;

            for (int i = 0; i < arrLista.Length; i++)
            {
                perfil = "*" + arrLista[i] + "*";

                if (perfiles == "")
                    perfiles = perfil;
                else
                    perfiles += "," + perfil;
            }

            parameters[0].Value = perfiles;
            parameters[1].Value = vMonto;

            var objectTemp = DbFactory.ExecuteScalar(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_EVALUAR_MONTO_AUTORIZAR_DCM, parameters);
            var validacion = Claro.Utils.CheckStr(parameters[parameters.Length - 1].Value);

            if (validacion == "1")
                resultado = true;

            return resultado;
        }
        /// <summary>
        /// Evalua monto
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vListaPerfil"></param>
        /// <param name="vMonto"></param>
        /// <param name="vUnidad"></param>
        /// <param name="vModalidad"></param>
        /// <param name="vTipoTelefono"></param>
        /// <returns></returns>
        /// <remarks>GetEvaluateAmount</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetEvaluateAmount(string strIdSession, string strTransaction, string vListaPerfil, double vMonto, string vUnidad, string vModalidad, string vTipoTelefono)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_LISTA_PERFIL", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("P_MONTO", DbType.Double, ParameterDirection.Input),
                new DbParameter("P_UNIDAD", DbType.String, 255, ParameterDirection.Input, vUnidad),
                new DbParameter("P_MODALIDAD", DbType.String, 255, ParameterDirection.Input, vModalidad),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, 255,ParameterDirection.Input, vTipoTelefono),
                new DbParameter("P_VALIDACION", DbType.String, 255, ParameterDirection.Output)
            };

            string[] arrLista = System.Text.RegularExpressions.Regex.Split(vListaPerfil, ",");
            var perfiles = string.Empty;
            var perfil = string.Empty;
            var resultado = false;

            for (int i = 0; i < arrLista.Length; i++)
            {
                perfil = "*" + arrLista[i] + "*";

                if (perfiles == "")
                    perfiles = perfil;
                else
                    perfiles += "," + perfil;
            }

            parameters[0].Value = perfiles;
            parameters[1].Value = vMonto;

            var objectTemp = DbFactory.ExecuteScalar(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_EVALUAR_MONTO_AUTORIZAR, parameters);
            var validacion = Claro.Utils.CheckStr(parameters[parameters.Length - 1].Value);

            if (validacion == "1")
                resultado = true;

            return resultado;
        }
        /// <summary>
        /// actualiza campo inter 29
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="P_INTERACT_ID"></param>
        /// <param name="P_TEXTO"></param>
        /// <param name="P_ORDEN"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>UpdateXinter29</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string UpdateXinter29(string strIdSession, string strTransaction, string P_INTERACT_ID, string P_TEXTO, string P_ORDEN, out string message)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_INTERACT_ID", DbType.String,256, ParameterDirection.Input,P_INTERACT_ID),
                new DbParameter("P_TEXTO", DbType.String,256, ParameterDirection.Input,P_TEXTO),
                new DbParameter("P_ORDEN", DbType.String,256, ParameterDirection.Input,P_ORDEN),
                new DbParameter("P_FLAG_INSERCION", DbType.String,256, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,256, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_UPDATE_INTERACT_X_INTER29, parameters);

            string Flag = Convert.ToString(parameters[3].Value);
            message = Convert.ToString(parameters[4].Value);
            return Flag;
        }
        /// <summary>
        /// obtiene tipo de trabajo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>GetWorkType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<ListItem> GetWorkType(string strIdSession, string strTransaction, int type)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_tipo", DbType.Int64, ParameterDirection.Input, type),
                new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ListItem>();

                    while (reader.Read())
                    {
                        listItem.Add(new ListItem()
                        {
                            Code = Convert.ToString(reader["TIPTRA"]),
                            Description = Convert.ToString(reader["DESCRIPCION"]),
                            Code2 = Convert.ToString(reader["FLAG_FRANJA"])
                        });
                    }

                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
        }
        /// <summary>
        /// obtiene subtipos de trabajo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>GetWorkSubType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<ListItem> GetWorkSubType(string strIdSession, string strTransaction, string type)
        {
            // <add name="SALES_P_CONSULTA_SUBTIPTRA" procedureName="SALES.PKG_ETADIRECT.OBTIENE_TIPO_ORDEN_TIPTRA" /> - Antes
            //  Validar - GetOrderSubType - Plan Migration
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String, ParameterDirection.Input, type),
                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_SUBTIPORD, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ListItem>();

                    while (reader.Read())
                    {
                        listItem.Add(new ListItem()
                        {
                            Code = Convert.ToString(reader["VALOR"]),
                            Description = Convert.ToString(reader["DESCRIPCION"]),
                        });
                    }
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
        }
        /// <summary>
        /// obtiene usuario
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCodeUser"></param>
        /// <param name="strCodeRol"></param>
        /// <param name="strCodeCac"></param>
        /// <param name="strState"></param>
        /// <returns></returns>
        /// <remarks>GetUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static User GetUser(string strIdSession, string strTransaction, string strCodeUser, string strCodeRol, string strCodeCac, string strState)
        {


            Claro.Web.Logging.Info(strIdSession, strTransaction, "strCodeUser: " + strCodeUser + "strCodeRol: " + strCodeRol + "strCodeCac: " + strCodeCac + "strState: " + strState);


            User userModel = new User();
            try
            {
                DbParameter[] parameters = {
                new DbParameter("P_CODIGOUSUARIO", DbType.String, ParameterDirection.Input,strCodeUser), 
                new DbParameter("P_CODIGOROL", DbType.Int32,ParameterDirection.Input, strCodeRol), 
                new DbParameter("P_CODIGOCAC", DbType.String, ParameterDirection.Input,strCodeCac), 
                new DbParameter("P_ESTADO", DbType.Int32, ParameterDirection.Input,strState ), 
                new DbParameter("P_RESULTADO", DbType.Object,ParameterDirection.Output)
            };

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_ST_CONSULTAS_USUARIO, parameters,
                    (IDataReader dr) =>
                    {
                        while (dr.Read())
                        {
                            userModel = new User();
                            userModel.CodeUser = Claro.Utils.CheckStr(dr["ID_USUARIO"]);
                            userModel.Name = Claro.Utils.CheckStr(dr["USU_NOMBRES"]);
                            userModel.LatName = Claro.Utils.CheckStr(dr["USU_APELLIDOS"]);
                            userModel.Dni = Claro.Utils.CheckStr(dr["USU_DNI"]);
                            userModel.Password = Claro.Utils.CheckStr(dr["USU_PASS"]);
                            userModel.CodeRol = Claro.Utils.CheckInt(dr["USU_ROL"]);
                            userModel.Rol = Claro.Utils.CheckStr(dr["ROL"]);
                            userModel.CodeState = Claro.Utils.CheckInt(dr["USU_ESTADO"]);
                            userModel.State = Claro.Utils.CheckStr(dr["Estado"]);
                            userModel.CodeEmpress = Claro.Utils.CheckInt(dr["ID_EMPRESA"]);
                            userModel.Empress = Claro.Utils.CheckStr(dr["Empresa"]);
                            userModel.CodeCac = Claro.Utils.CheckStr(dr["ID_CAC"]);
                            userModel.Cac = Claro.Utils.CheckStr(dr["CAC"]);
                            userModel.CodeTypeCac = Claro.Utils.CheckInt(dr["ID_TIPOCAC"]);
                            userModel.TypeCac = Claro.Utils.CheckStr(dr["TIPOCAC"]);
                        }
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return userModel;
        }
        /// <summary>
        /// obtiene empleados por fecha
        /// </summary>
        /// <param name="objDatosEmpleadoRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEmployerDate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static SECURITY.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(SECURITY.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest)
        {


            SECURITY.GetEmployerDate.GetEmployerDateResponse objEmpleado = new SECURITY.GetEmployerDate.GetEmployerDateResponse();


            var opcionesUsuarioReques = new AUDIT.DatosEmpleadoRequest();
            var objResponseAuditoria = new AUDIT.EmpleadoResponse();

            objResponseAuditoria = Claro.Web.Logging.ExecuteMethod<AUDIT.EmpleadoResponse>(objDatosEmpleadoRequest.Audit.Session, objDatosEmpleadoRequest.Audit.Transaction, Configuration.WebServiceConfiguration.GRABARAUDIT, () =>
            {
                return Configuration.WebServiceConfiguration.GRABARAUDIT.leerDatosEmpleado(opcionesUsuarioReques);

            });

            if (objResponseAuditoria.resultado.estado.Equals("1"))
            {
                objEmpleado.strCode = objResponseAuditoria.empleados.item[0].codigo;
                objEmpleado.strCodeArea = objResponseAuditoria.empleados.item[0].codigoArea;
                objEmpleado.strCodeCargo = objResponseAuditoria.empleados.item[0].codigoCargo;
                objEmpleado.strDesAddress = objResponseAuditoria.empleados.item[0].codigoDireccion;
                objEmpleado.strDesBoss = objResponseAuditoria.empleados.item[0].codigoJefe;
                objEmpleado.strEmail = objResponseAuditoria.empleados.item[0].correo;
                objEmpleado.strDesArea = objResponseAuditoria.empleados.item[0].descripcionArea;
                objEmpleado.strDesCargo = objResponseAuditoria.empleados.item[0].descripcionCargo;
                objEmpleado.strDesAddress = objResponseAuditoria.empleados.item[0].descripcionDireccion;
                objEmpleado.strDesBoss = objResponseAuditoria.empleados.item[0].descripcionJefe;
                objEmpleado.strlogin = objResponseAuditoria.empleados.item[0].login;
                objEmpleado.strNomb = objResponseAuditoria.empleados.item[0].nombres;
                objEmpleado.strApPat = objResponseAuditoria.empleados.item[0].paterno;
                objEmpleado.strApMat = objResponseAuditoria.empleados.item[0].materno;
            }
            else
            {
                objEmpleado = null;
            }

            return objEmpleado;
        }
        /// <summary>
        /// insertar interaccion de negocio
        /// </summary>
        /// <param name="strSesion"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objItem"></param>
        /// <param name="strInteractionId"></param>
        /// <param name="strFlagInsertion"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>InsertBusinessInteractionFixed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertBusinessInteractionFixed(string strSesion, string strTransaction, Iteraction objItem, ref string strInteractionId, ref string strFlagInsertion, ref string strMessage)
        {
            DbParameter[] parameters = new DbParameter[] 
            {
                                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input), 
								new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input), 
                                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),												   
								new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),													   
								new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_HECHO_EN_UNO", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)
            };

            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
            parameters[0].Value = objItem.OBJID_CONTACTO;
            parameters[1].Value = objItem.OBJID_SITE;
            parameters[2].Value = objItem.CUENTA;
            parameters[3].Value = objItem.TELEFONO;
            parameters[4].Value = objItem.TIPO;
            parameters[5].Value = objItem.CLASE;
            parameters[6].Value = objItem.SUBCLASE;
            parameters[7].Value = objItem.METODO;
            parameters[8].Value = objItem.TIPO_INTER;
            parameters[9].Value = objItem.AGENTE;
            parameters[10].Value = objItem.USUARIO_PROCESO;
            parameters[11].Value = objItem.HECHO_EN_UNO;
            parameters[12].Value = objItem.NOTAS;
            parameters[13].Value = objItem.FLAG_CASO;
            parameters[14].Value = objItem.RESULTADO;

            int result =
            DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);

            strInteractionId = parameters[15].Value.ToString() == "null" ? string.Empty : parameters[15].Value.ToString();
            strFlagInsertion = parameters[16].Value.ToString();
            strMessage = parameters[17].Value.ToString();

            if (String.IsNullOrEmpty(strMessage)) strMessage = "OK";

            return (strFlagInsertion == CONSTANTS.Message_OK);
        }
        /// <summary>
        /// obtiene tipos de transacciones
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <returns></returns>
        /// <remarks>GetTypeTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<EntitiesFixed.GenericItem> GetTypeTransaction(string strIdSession, string strTransaction)
        {
            var salida = new List<EntitiesFixed.GenericItem>();
            var cantidadRegistros = 0;
            DbParameter[] parameters = 
            {
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)										   
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_TIMEAI,
                        DbCommandConfiguration.SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                cantidadRegistros++;
                                var item = new EntitiesFixed.GenericItem
                                {
                                    Codigo = Claro.Utils.CheckStr(reader["CODIGO"]),
                                    Descripcion = Claro.Utils.CheckStr(reader["DESCRIPCION"]),
                                };
                                salida.Add(item);
                            }
                        });
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                Web.Logging.Info(strIdSession, strTransaction, "Parametros de Salida-> Cantidad Registros: " + cantidadRegistros);
            }

            return salida;
        }
        /// <summary>
        /// valida flujo ETA
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="as_origen"></param>
        /// <param name="av_idplano"></param>
        /// <param name="av_ubigeo"></param>
        /// <param name="an_tiptra"></param>
        /// <param name="an_tipsrv"></param>
        /// <returns></returns>
        /// <remarks>ETAFlowValidate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ETAFlow ETAFlowValidate(string strIdSession, string strTransaction, string as_origen, string av_idplano, string av_ubigeo, int an_tiptra,
                             string an_tipsrv)
        {
            ETAFlow oEtaFlow;
            string as_codzona = "";
            int an_indica = 1;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("as_origen", DbType.String, ParameterDirection.Input,as_origen),
                new DbParameter("av_idplano", DbType.String,255, ParameterDirection.Input, av_idplano),
                new DbParameter("av_ubigeo", DbType.String,255, ParameterDirection.Input, av_ubigeo),
                new DbParameter("an_tiptra", DbType.Int32,20, ParameterDirection.Input, an_tiptra),
                new DbParameter("an_tipsrv", DbType.String,255, ParameterDirection.Input, an_tipsrv),
                new DbParameter("as_codzona", DbType.String,255, ParameterDirection.Output,as_codzona),
                new DbParameter("an_indica", DbType.Int32,20, ParameterDirection.Output,an_indica)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VALIDA_FLUJO_ZONA_ADC, parameters);

            return new ETAFlow
            {
                an_indica = Convert.ToInt(parameters[6].Value.ToString()),
                as_codzona = Convert.ToString(parameters[5].Value)
            };
        }
        
        #endregion
        /// <summary>
        /// Obtiene parametro terminal TPI
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="ParameterID"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        /// <remarks>GetParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<ParameterTerminalTPI> GetParameterTerminalTPI(string strIdSession, string strTransaction, int ParameterID, ref string Message)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetParameterTerminalTPI", ParameterID);
            Claro.Web.Logging.Info("Session: " + strIdSession, "GetParameterTerminalTPI ", "ParameterID" + ParameterID);
            List<ParameterTerminalTPI> listParameterTerminalTPI = new List<ParameterTerminalTPI>();


            try
            {

                DbParameter[] parameters = {
                new DbParameter("P_PARAMETRO_ID", DbType.Int32, ParameterDirection.Input, ParameterID),
                new DbParameter("P_MENSAJE", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };


                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_OBTENER_PARAMETRO, parameters, (IDataReader reader) =>
                {
                    ParameterTerminalTPI entity = null;
                    while (reader.Read())
                    {
                        entity = new ParameterTerminalTPI();
                        entity.ParameterID = Claro.Utils.CheckStr(reader["PARAMETRO_ID"]);
                        entity.Name = Claro.Utils.CheckStr(reader["NOMBRE"]);
                        entity.Description = Claro.Utils.CheckStr(reader["DESCRIPCION"]);
                        entity.Type = Claro.Utils.CheckStr(reader["TIPO"]);
                        entity.ValorC = Claro.Utils.CheckStr(reader["VALOR_C"]);
                        entity.ValorN = Claro.Utils.CheckDbl(reader["VALOR_N"]);
                        entity.ValorL = Claro.Utils.CheckStr(reader["VALOR_L"]);

                        listParameterTerminalTPI.Add(entity);

                    }

                });

                Message = parameters[1].Value.ToString();
                Claro.Web.Logging.Info("Session: " + strIdSession, "GetParameterTerminalTPI ", "Message" + Message);


            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction, "Error GetParameterTerminalTPI : " + ex.Message);
            }





            return listParameterTerminalTPI;

        }
        /// <summary>
        /// Obtiene codigo de lista
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="nameList"></param>
        /// <returns></returns>
        /// <remarks>GetCodeList</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static int GetCodeList(string strIdSession, string strTransaction, string nameList)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_TITLE", DbType.String,3, ParameterDirection.Input, nameList),
                new DbParameter("P_OBJID", DbType.String,225, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_CODIGO, parameters);

            return Convert.ToInt(parameters[1].Value.ToString());
        }
        /// <summary>
        /// obtiene reglas de negocio
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strSubClase"></param>
        /// <returns></returns>
        /// <remarks>GetBusinessRules</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.BusinessRules> GetBusinessRules(string strIdSession, string strTransaction, string strSubClase)
        {
            List<Claro.SIACU.Entity.IFI.Common.BusinessRules> lstBusinessRules = new List<Claro.SIACU.Entity.IFI.Common.BusinessRules>();

            try
            {
                DbParameter[] parameters = {
                new DbParameter("P_SUSCLASE", DbType.String, ParameterDirection.Input, strSubClase),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

                lstBusinessRules = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.IFI.Common.BusinessRules>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_CONSULTAR_REGLAS_ATENCION, parameters);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }

            return lstBusinessRules;
        }

        /// <summary>
        /// Método que retorna el tipo de estado de la solicitud.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="idList">Id de lista</param>
        /// <returns>Devuelve listado de tipos de estado de solicitud.</returns>
        /// <remarks>GetStateType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetStateType(string strIdSession, string strTransaction, string idList)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_LISTA", DbType.String, 100, ParameterDirection.Input, idList),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem()
                    {
                        Code = Convert.ToString(reader["VALOR"]),
                        Description = Convert.ToString(reader["DESCRIPCION"])
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// Método que retorna el tipo de transacción de la solicitud.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve listado de tipos de transacción.</returns>
        /// <remarks>GetTransactionType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.ListItem> GetTransactionType(string strIdSession, string strTransaction)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
                
            };

            List<Entity.IFI.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_TIMEAI, DbCommandConfiguration.SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.ListItem()
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["DESCRIPCION"])
                    });
                }
            });

            return listItem;
        }
        /// <summary>
        /// actualiza campo inter 30
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="p_objid"></param>
        /// <param name="p_texto"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        /// <remarks>UpdatexInter30</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdatexInter30(string strIdSession, string strTransaction, string p_objid, string p_texto, ref string rFlagInsercion, ref string rMsgText)
        {
            var salida = false;
            DbParameter[] parameters = {
                new DbParameter("P_INTERACT_ID", DbType. Int64,ParameterDirection.Input),
                new DbParameter("P_TEXTO", DbType.String, 1000,ParameterDirection.Input),
                new DbParameter("P_FLAG_INSERCION", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255,ParameterDirection.Output)
            };

            foreach (var t in parameters)
                t.Value = DBNull.Value;

            var i = 0;
            if (!string.IsNullOrEmpty(p_objid))
                parameters[i].Value = Convert.ToInt64(p_objid);

            if (!string.IsNullOrEmpty(p_texto))
                parameters[1].Value = p_texto;
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_UPDATE_X_INTER_30, parameters);
                    salida = true;
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }
            return salida;
        }
        /// <summary>
        /// valida email
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerCode"></param>
        /// <returns></returns>
        /// <remarks>GetValidateMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetValidateMail(string strIdSession, string strTransaction, string strCustomerCode)
        {
            string strServicio;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.String, ParameterDirection.Input, strCustomerCode),
                new DbParameter("P_STATUS", DbType.String,1, ParameterDirection.Output),
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_VALIDA_MAIL, parameters);
            strServicio = parameters[1].Value.ToString();

            return strServicio;
        }

        /// <summary>
        /// obtiene numero de sms
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerCode"></param>
        /// <returns></returns>
        /// <remarks>GetNumberSMS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetNumberSMS(string strIdSession, string strTransaction, string strCustomerCode)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input, strCustomerCode),
                new DbParameter("P_NUMERO", DbType.String,20, ParameterDirection.Output),
            };

            string numeroSMS = "";

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_NUMERO_SMS, parameters);

            numeroSMS = parameters[1].Value.ToString();

            return numeroSMS;
        }


        /// <summary> Retorna lista de ultimos recibos del cliente </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="code"></param>      
        /// <param name="strCustomerCode"></param>   
        /// <returns>List</returns>
        /// <remarks>GetLastInvoiceData</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>15/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.Receipt> GetLastInvoiceData(string strIdSession, string strTransaction, int code, string strCustomerCode)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
                new DbParameter("K_CODIGOCLIENTE", DbType.String,24, ParameterDirection.Input, strCustomerCode),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.Receipt> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.Receipt>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.Receipt
                    {
                        FECHA_VENCIMIENTO = ((DateTime)(reader["FechaVencimiento"])).ToString("dd/MM/yyyy"),
                        MTO_ULT_FACTURA = Convert.ToString(reader["TotalCurrentCharges"]),
                        PERIODO = Convert.ToString(reader["Periodo"]),
                        INVOICENUMBER =    Claro.Utils.CheckStr(reader["InvoiceNumber"])
                    });
                }
            });

            return listItem;
        }

        /// <summary>
        /// valida claves para datapower
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="idTransaccion"></param>
        /// <param name="ipAplicacion"></param>
        /// <param name="ipTransicion"></param>
        /// <param name="usrAplicacion"></param>
        /// <param name="idAplicacion"></param>
        /// <param name="User"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        ///  <remarks>IsOkGetKey</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool IsOkGetKey(string idSession, string idTransaccion, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, out string User, out string Pass)
        {

            try
            {
                const string TIMRootRegistry = @"SOFTWARE\TIM";
                string strKey = ConfigurationManager.AppSettings("strRestricTetheVelocWS");
                string cryptoUser = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("User", "").ToString();
                string cryptoPass = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("Password", "").ToString();
                desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                {
                    idTransaccion = idTransaccion,
                    ipAplicacion = ipAplicacion,
                    ipTransicion = ipTransicion,
                    usrAplicacion = usrAplicacion,
                    idAplicacion = idAplicacion,
                    codigoAplicacion = ConfigurationManager.AppSettings("strCodigoAplicacion"),
                    usuarioAplicacionEncriptado = cryptoUser,
                    claveEncriptado = cryptoPass,
                };
                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );
                User = objdesencriptarResponse.usuarioAplicacion;
                Pass = objdesencriptarResponse.clave;
                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    return true;

                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, idTransaccion, ex.Message);
                User = "";
                Pass = "";
                return false;
            }

            return false;

        }





        /// <summary>
        /// sube pdf por onbase
        /// </summary>
        /// <param name="UploadDocumentOnBaseRequest"></param>
        /// <returns></returns>
        ///   <remarks>GetUploadDocumentOnBase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesCommon.GetUploadDocumentOnBase.UploadDocumentOnBaseResponse GetUploadDocumentOnBase(EntitiesCommon.GetUploadDocumentOnBase.UploadDocumentOnBaseRequest UploadDocumentOnBaseRequest)
        {
            EntitiesCommon.GetUploadDocumentOnBase.UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse = new EntitiesCommon.GetUploadDocumentOnBase.UploadDocumentOnBaseResponse();
            StringBuilder xml = new StringBuilder();
            xml.Append("<ArrayOfE_Documento xmlns:xsd='" + "http://www.w3.org/2001/XMLSchema'" + " xmlns:xsi='" + "http://www.w3.org/2001/XMLSchema-instance'" + ">");
            xml.Append("<E_Documento>");
            xml.Append("<seq>" + UploadDocumentOnBaseRequest.listaDocumentos.Seq + "</seq>");
            xml.Append("<CodigoTCRM>" + UploadDocumentOnBaseRequest.listaDocumentos.CodigoTCRM + "</CodigoTCRM>");
            xml.Append("<TipoDocumentoOnBase>" + UploadDocumentOnBaseRequest.listaDocumentos.TipoDocumentoOnBase + "</TipoDocumentoOnBase>");
            xml.Append("<ListaMetadatos>");
            foreach (var itemMetadatos in UploadDocumentOnBaseRequest.listaDocumentos.ListaMetadatos)
            {
                xml.Append("<E_Keyword>");
                xml.Append("<codigoCampo>" + itemMetadatos.codigoCampo + "</codigoCampo>");
                xml.Append("<Campo>" + itemMetadatos.Campo + "</Campo>");
                xml.Append("<Valor>" + itemMetadatos.Valor + "</Valor>");
                xml.Append("<longitud>" + itemMetadatos.longitud + "</longitud>");
                xml.Append("</E_Keyword>");
            }
            xml.Append("</ListaMetadatos>");
            xml.Append("<abytArchivo>" + UploadDocumentOnBaseRequest.listaDocumentos.abytArchivo + "</abytArchivo>");
            xml.Append("<CodigoOnBase>" + UploadDocumentOnBaseRequest.listaDocumentos.CodigoOnBase + "</CodigoOnBase>");
            xml.Append("<strExtension>" + UploadDocumentOnBaseRequest.listaDocumentos.strExtension + "</strExtension>");
            xml.Append("</E_Documento>");
            xml.Append("</ArrayOfE_Documento>");

            ONBASELOAD.auditRequest oAuditRequest = new ONBASELOAD.auditRequest()
            {
                idTransaccion = UploadDocumentOnBaseRequest.Audit.Transaction,
                ipAplicacion = UploadDocumentOnBaseRequest.Audit.IPAddress,
                nombreAplicacion = UploadDocumentOnBaseRequest.Audit.ApplicationName,
                usuarioAplicacion = UploadDocumentOnBaseRequest.Audit.UserName,

            };

            ONBASELOAD.ArrayOfObjetoRequestOpcional listOptionalRequest = new ONBASELOAD.ArrayOfObjetoRequestOpcional();

            if (UploadDocumentOnBaseRequest.parametrosRequest != null)
            {
                foreach (var item in UploadDocumentOnBaseRequest.parametrosRequest.ListaRequestOpcional.objetoRequestOpcional)
                {
                    ONBASELOAD.objetoRequestOpcional OptionalObject = new ONBASELOAD.objetoRequestOpcional()
                    {
                        campo = item.campo,
                        valor = item.valor
                    };
                    listOptionalRequest.Add(OptionalObject);
                }
            }
            try
            {
                ONBASELOAD.CargarDocumentoOnBaseRequestBody objOnBaseRequestBody = new ONBASELOAD.CargarDocumentoOnBaseRequestBody()
                {

                    auditRequest = oAuditRequest,
                    idInterfazTCRM = UploadDocumentOnBaseRequest.idInterfazTCRM,
                    usuario = UploadDocumentOnBaseRequest.usuario,
                    fecha = UploadDocumentOnBaseRequest.fecha,
                    hora = UploadDocumentOnBaseRequest.hora,
                    listaDocumentos = xml.ToString(),
                    parametrosRequest = new ONBASELOAD.parametrosRequest()
                    {
                        ListaRequestOpcional = listOptionalRequest
                    }
                };
                var objHeaderResponse = Web.Logging.ExecuteMethod(UploadDocumentOnBaseRequest.Audit.Session, UploadDocumentOnBaseRequest.Audit.Transaction, () =>
                {
                    return Configuration.ServiceConfiguration.ONBASELOAD.CargarDocumentoOnBase(oAuditRequest, objOnBaseRequestBody.idInterfazTCRM, objOnBaseRequestBody.usuario, objOnBaseRequestBody.fecha, objOnBaseRequestBody.hora, objOnBaseRequestBody.listaDocumentos, objOnBaseRequestBody.parametrosRequest);
                });
                if (objHeaderResponse != null)
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(objHeaderResponse.listaDocumentos);
                    var objEd = xmlDoc.SelectSingleNode("ArrayOfE_Documento").SelectSingleNode("E_Documento");

                    objUploadDocumentOnBaseResponse.idTransaccion = objHeaderResponse.idTransaccion;
                    objUploadDocumentOnBaseResponse.idInterfazTCRM = objHeaderResponse.idInterfazTCRM;
                    objUploadDocumentOnBaseResponse.ipServerResponse = objHeaderResponse.ipServerResponse;
                    objUploadDocumentOnBaseResponse.fecha = objHeaderResponse.fecha;
                    objUploadDocumentOnBaseResponse.hora = objHeaderResponse.hora;
                    objUploadDocumentOnBaseResponse.E_Document = new EntitiesCommon.GetUploadDocumentOnBase.E_DocumentResponse()
                    {
                        CodigoTCRM = objEd.SelectSingleNode("CodigoTCRM").InnerText,
                        Estado = objEd.SelectSingleNode("Estado").InnerText,
                        Seq = objEd.SelectSingleNode("Seq").InnerText,
                        Fecha = objEd.SelectSingleNode("Fecha").InnerText,
                        CodigoOnBase = objEd.SelectSingleNode("CodigoOnBase").InnerText,
                        Hora = objEd.SelectSingleNode("Hora").InnerText,
                    };
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(UploadDocumentOnBaseRequest.Audit.Session, UploadDocumentOnBaseRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objUploadDocumentOnBaseResponse;
        }
        /// <summary>
        /// obtiene la secuencia 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="flagUnlock"></param>
        /// <returns></returns>
        ///  <remarks>GetSequenceCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetSequenceCode(string strIdSession, string strTransaction, bool flagUnlock)
        {
            string code = "";

            DbParameter[] parameters = 
            {
               
                new DbParameter("CUR_RPTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession,
                                    strTransaction,
                                    DbConnectionConfiguration.SIAC_POST_DB,
                                    (!flagUnlock) ? DbCommandConfiguration.SIACU_IFI_OBTIENE_CODIGO_BLOQUEO : DbCommandConfiguration.SIACU_IFI_OBTIENE_CODIGO_DESBLOQUEO,
                                    parameters,
                                    (IDataReader reader) =>
                                    {
                                        while (reader.Read())
                                        {
                                            code = (flagUnlock) ? Convert.ToString(reader["COD_DESBLOQUEO_L_Q"]) : Convert.ToString(reader["COD_BLOQUEO_L_Q"]);

                                            if (!string.IsNullOrEmpty(code))
                                            {
                                                code = string.Format("{0:D7}", int.Parse(code));
                                                break;
                                            }


                                        }
                                    }
                                   );

            return code;
        }


        /// <summary>
        /// Método para obtener las colas asociadas a la interacción
        /// </summary>
        /// <param name="strIdSession"> Id de la Sesión</param>
        /// <param name="strTransaction">Id de la transacción</param>
        /// <param name="strCodeSubClase">Codigo de Clase</param>
        /// <param name="strDesTitle">Titulo de la Cola</param>
        /// <param name="strFlagSearch">Flag 1</param>
        /// <param name="strUser">Usuario de la Aplicación</param>
        /// <param name="Desc_Error">Descripción del Error</param>
        /// <param name="Cod_Error">Código de Error</param>
        /// <returns>Cursor con la lista de colas</returns>
        /// <remarks>GetQueuesCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.QueuesCase> GetQueuesCase(string strIdSession, string strTransaction, string strCodeSubClase, string strDesTitle, string strFlagSearch, string strUser, ref string Desc_Error, ref string Cod_Error)
        {
            List<Claro.SIACU.Entity.IFI.Common.QueuesCase> ListQueues = new List<Claro.SIACU.Entity.IFI.Common.QueuesCase>();

            try
            {
                DbParameter[] parameters = {
                new DbParameter("v_subclase", DbType.String,40, ParameterDirection.Input, strCodeSubClase),
                new DbParameter("v_des_title", DbType.String,100, ParameterDirection.Input, strDesTitle),
                new DbParameter("p_flag_buscar", DbType.String,1, ParameterDirection.Input, strFlagSearch),
                new DbParameter("v_usuario", DbType.String,50, ParameterDirection.Input, strUser),
                new DbParameter("n_cod_error", DbType.String,255, ParameterDirection.Output),
                new DbParameter("v_desc_error", DbType.String,255, ParameterDirection.Output),
                new DbParameter("queues", DbType.Object, ParameterDirection.Output) 
            };

                ListQueues = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.IFI.Common.QueuesCase>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_IFI_QUEUES_CASE, parameters);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }

            return ListQueues;
        }


        /// <summary>
        /// Método para obtener las colas asociadas a la interacción
        /// </summary>
        /// <param name="strIdSession"> Id de la Sesión</param>
        /// <param name="strTransaction">Id de la transacción</param>
        /// <param name="strCodeSubClase">Codigo de Clase</param>
        /// <param name="strDesTitle">Titulo de la Cola</param>
        /// <param name="strFlagSearch">Flag 1</param>
        /// <param name="strUser">Usuario de la Aplicación</param>
        /// <param name="Desc_Error">Descripción del Error</param>
        /// <param name="Cod_Error">Código de Error</param>
        /// <returns>Cursor con la lista de colas</returns>
        /// <remarks>GetCaseLa</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetCaseLa(string strIdSession, string strTransaction, string vPhone, string vfechaini, string vfechafin, ref string vFlagReg, ref string MsgText)
        {

            var salida = false;

            DbParameter[] parameters = {
                new DbParameter("TELEPHONE", DbType.String,255, ParameterDirection.Input, vPhone),
                new DbParameter("P_FECHA_INI", DbType.Date,22,ParameterDirection.Input, Convert.ToDate(vfechaini)),
                new DbParameter("P_FECHA_FIN", DbType.Date,22, ParameterDirection.Input, Convert.ToDate(vfechafin)),
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,255, ParameterDirection.Output),
                new DbParameter("OUT_CURSOR", DbType.Object,ParameterDirection.Output)

            };

            try
            {

                var result2 = DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                     DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_CASE_LA, parameters);

                salida = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            finally
            {

                string parSalida1 = parameters[parameters.Length - 3].Value.ToString();
                string parSalida2 = parameters[parameters.Length - 2].Value.ToString();
                vFlagReg = parSalida1;
                MsgText = parSalida2;


            }

            return salida;
        }


        /// <summary>
        /// Método para insertar la interacción mediante el servicio  SIACU.IFI.TransaccionInteracciones
        /// </summary>
        /// <param name="strIdSession"> Id de la Sesión</param>
        /// <param name="strTransaction">Id de la transacción</param>
        /// <param name="strCodeSubClase">Codigo de Clase</param>
        /// <param name="strDesTitle">Titulo de la Cola</param>
        /// <param name="strFlagSearch">Flag 1</param>
        /// <param name="strUser">Usuario de la Aplicación</param>
        /// <param name="Desc_Error">Descripción del Error</param>
        /// <param name="Cod_Error">Código de Error</param>
        ///<returns>Bool<returns>
        ///<remarks>InsertService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>09/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertService(string strSesion, string strTransaction, Iteraction objItem, out string intTeractionid, out string strFlaginsercion, out string strMessage)
        {
            bool blnRetorno;
            string idInteraccion = string.Empty;
            string flagCreacion = string.Empty, MsgText = string.Empty;
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a Insert made13");

            try
            {
                Service_Interaccion.nuevaInteraccionRequest Req = new Service_Interaccion.nuevaInteraccionRequest();
                Service_Interaccion.nuevaInteraccionResponse Resp = new Service_Interaccion.nuevaInteraccionResponse();

                Service_Interaccion.InteraccionType Itype = new Service_Interaccion.InteraccionType();

                Service_Interaccion.AuditType AuditType = new Service_Interaccion.AuditType();


                Itype.objId = objItem.OBJID_CONTACTO != null ? objItem.OBJID_CONTACTO : string.Empty;

                Itype.siteObjId = objItem.OBJID_SITE != null ? objItem.OBJID_SITE : string.Empty;

                Itype.cuenta = objItem.CUENTA != null ? objItem.CUENTA : string.Empty;

                Itype.telefono = objItem.TELEFONO != null ? objItem.TELEFONO : string.Empty;

                Itype.tipo = objItem.TIPO != null ? objItem.TIPO : string.Empty;

                Itype.clase = objItem.CLASE != null ? objItem.CLASE : string.Empty;

                Itype.subClase = objItem.SUBCLASE != null ? objItem.SUBCLASE : string.Empty;

                Itype.metodoContacto = objItem.METODO != null ? objItem.METODO : string.Empty;

                Itype.tipoInteraccion = objItem.TIPO_INTER != null ? objItem.TIPO_INTER : string.Empty;

                Itype.codigoEmpleado = objItem.AGENTE != null ? objItem.AGENTE : string.Empty;

                Itype.codigoSistema = objItem.USUARIO_PROCESO != null ? objItem.USUARIO_PROCESO : string.Empty;

                Itype.hechoEnUno = objItem.HECHO_EN_UNO != null ? objItem.HECHO_EN_UNO : string.Empty;

                Itype.notas = objItem.NOTAS != null ? objItem.NOTAS : string.Empty;

                Itype.flagCaso = objItem.FLAG_CASO != null ? objItem.FLAG_CASO : string.Empty;

                Itype.textResultado = objItem.RESULTADO != null ? objItem.RESULTADO : string.Empty;


                AuditType = Claro.Web.Logging.ExecuteMethod<Service_Interaccion.AuditType>(strSesion, strTransaction,
                     () =>
                     {
                         return ServiceConfiguration.ServiceInteraccion
                             .nuevaInteraccion(strTransaction, Itype, out idInteraccion);
                     });


                strMessage = AuditType.errorMsg;
                if (idInteraccion != "")
                {
                    strFlaginsercion = "OK";
                    intTeractionid = idInteraccion;
                    blnRetorno = true;
                }
                else
                {
                    strFlaginsercion = "NO";
                    intTeractionid = "";
                    blnRetorno = false;
                }

            }
            catch (Exception ex)
            {

                Web.Logging.Error(strSesion, strTransaction, "Error WS - nuevaInteraccion :" + ex.Message);
                blnRetorno = false;
                strFlaginsercion = "NO";
                throw ex;
            }
            return (strFlaginsercion.Equals(KEY.AppSettings("InteractionMessage")));

        }

        /// <summary>
        /// Método para insertar la plantilla de la interacción mediante el servicio  SIACU.IFI.TransaccionInteracciones
        /// </summary>
        /// <param name="strIdSession"> Id de la Sesión</param>
        /// <param name="strTransaction">Id de la transacción</param>
        /// <param name="strCodeSubClase">Codigo de Clase</param>
        /// <param name="strDesTitle">Titulo de la Cola</param>
        /// <param name="strFlagSearch">Flag 1</param>
        /// <param name="strUser">Usuario de la Aplicación</param>
        /// <param name="Desc_Error">Descripción del Error</param>
        /// <param name="Cod_Error">Código de Error</param>
        ///<returns>Bool<returns>
        ///<remarks>InsertService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/01/2019.</FecCrea></item></list>   
        /// <list type="bullet">
        /// <item><ModificadoPor>Everis</ModificadoPor></item>
        /// <item><FecMod>23/01/2019.</FecMod></item></list>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool RegistrationInsertTemplateInteractionService(string strSesion, string strTransaction, InsertTemplateInteraction objItem, string strInteraccionId, out string strFlagInsercion, out string strMessage)
        {
            bool blnRetorno;
            string idInteraccionPlus = string.Empty;
            string idInteraccionPlusR = string.Empty;
            string flagCreacion = string.Empty, MsgText = string.Empty;
            Claro.Web.Logging.Info(strSesion, strTransaction, "Transaction: Entra a insert plantilla interaccion service");

            try
            {
                Service_Interaccion.nuevaInteraccionPlusRequest Req = new Service_Interaccion.nuevaInteraccionPlusRequest();
                Service_Interaccion.nuevaInteraccionPlusResponse Resp = new Service_Interaccion.nuevaInteraccionPlusResponse();

                Service_Interaccion.InteraccionPlusType Itype = new Service_Interaccion.InteraccionPlusType();

                Service_Interaccion.AuditType AuditType = new Service_Interaccion.AuditType();


                Itype.p_nro_interaccion = strInteraccionId;

                Itype.p_inter_1 = objItem._X_INTER_1 != null ? objItem._X_INTER_1 : string.Empty;

                Itype.p_inter_2 = objItem._X_INTER_2 != null ? objItem._X_INTER_2 : string.Empty;

                Itype.p_inter_3 = objItem._X_INTER_3 != null ? objItem._X_INTER_3 : string.Empty;

                Itype.p_inter_4 = objItem._X_INTER_4 != null ? objItem._X_INTER_4 : string.Empty;

                Itype.p_inter_5 = objItem._X_INTER_5 != null ? objItem._X_INTER_5 : string.Empty;

                Itype.p_inter_6 = objItem._X_INTER_6 != null ? objItem._X_INTER_6 : string.Empty;

                Itype.p_inter_7 = objItem._X_INTER_7 != null ? objItem._X_INTER_7 : string.Empty;

                Itype.p_inter_8 = objItem._X_INTER_8 != null ? objItem._X_INTER_8.ToString() : "0";

                Itype.p_inter_9 = objItem._X_INTER_9 != null ? objItem._X_INTER_9.ToString() : "0";

                Itype.p_inter_10 = objItem._X_INTER_10 != null ? objItem._X_INTER_10.ToString() : "0";

                Itype.p_inter_11 = objItem._X_INTER_11 != null ? objItem._X_INTER_11.ToString() : "0";

                Itype.p_inter_12 = objItem._X_INTER_12 != null ? objItem._X_INTER_12.ToString() : "0";

                Itype.p_inter_13 = objItem._X_INTER_13 != null ? objItem._X_INTER_13.ToString() : "0";

                Itype.p_inter_14 = objItem._X_INTER_14 != null ? objItem._X_INTER_14.ToString() : "0";

                Itype.p_inter_15 = objItem._X_INTER_15 != null ? objItem._X_INTER_15 : string.Empty;

                Itype.p_inter_16 = objItem._X_INTER_16 != null ? objItem._X_INTER_16 : string.Empty;

                Itype.p_inter_17 = objItem._X_INTER_17 != null ? objItem._X_INTER_17 : string.Empty;

                Itype.p_inter_18 = objItem._X_INTER_18 != null ? objItem._X_INTER_18 : string.Empty;

                Itype.p_inter_19 = objItem._X_INTER_19 != null ? objItem._X_INTER_19 : string.Empty;

                Itype.p_inter_20 = objItem._X_INTER_20 != null ? objItem._X_INTER_20 : string.Empty;

                Itype.p_inter_21 = objItem._X_INTER_21 != null ? objItem._X_INTER_21 : string.Empty;

                Itype.p_inter_22 = objItem._X_INTER_22 != null ? objItem._X_INTER_22.ToString() : "0";

                Itype.p_inter_23 = objItem._X_INTER_23 != null ? objItem._X_INTER_23.ToString() : "0";

                Itype.p_inter_24 = objItem._X_INTER_24 != null ? objItem._X_INTER_24.ToString() : "0";

                Itype.p_inter_25 = objItem._X_INTER_25 != null ? objItem._X_INTER_25.ToString() : "0";

                Itype.p_inter_26 = objItem._X_INTER_26 != null ? objItem._X_INTER_26.ToString() : "0";

                Itype.p_inter_27 = objItem._X_INTER_27 != null ? objItem._X_INTER_22.ToString() : "0";

                Itype.p_inter_28 = objItem._X_INTER_28 != null ? objItem._X_INTER_28.ToString() : "0";

                Itype.p_inter_29 = objItem._X_INTER_29 != null ? objItem._X_INTER_29 : string.Empty;

                Itype.p_inter_30 = objItem._X_INTER_30 != null ? objItem._X_INTER_30 : string.Empty;

                Itype.p_plus_inter2interact = objItem._X_PLUS_INTER2INTERACT != null ? objItem._X_PLUS_INTER2INTERACT.ToString() : string.Empty;

                Itype.p_adjustment_amount = objItem._X_ADJUSTMENT_AMOUNT != null ? objItem._X_ADJUSTMENT_AMOUNT.ToString() : "0";

                Itype.p_adjustment_reason = objItem._X_ADJUSTMENT_REASON != null ? objItem._X_ADJUSTMENT_REASON : string.Empty;

                Itype.p_address = objItem._X_ADDRESS != null ? objItem._X_ADDRESS : string.Empty;

                Itype.p_amount_unit = objItem._X_AMOUNT_UNIT != null ? objItem._X_AMOUNT_UNIT : string.Empty;

                if (objItem._X_BIRTHDAY.ToString("dd-MM-yyyy").Equals("01-01-0001"))
                {
                    Itype.p_birthday = "";
                }
                else
                {
                    Itype.p_birthday = objItem._X_BIRTHDAY.ToString("yyyy-MM-dd");
                }

                Itype.p_clarify_interaction = objItem._X_CLARIFY_INTERACTION != null ? objItem._X_CLARIFY_INTERACTION : string.Empty;

                Itype.p_claro_ldn1 = objItem._X_CLARO_LDN1 != null ? objItem._X_CLARO_LDN1 : string.Empty;

                Itype.p_claro_ldn2 = objItem._X_CLARO_LDN2 != null ? objItem._X_CLARO_LDN2 : string.Empty;

                Itype.p_claro_ldn3 = objItem._X_CLARO_LDN3 != null ? objItem._X_CLARO_LDN3 : string.Empty;

                Itype.p_claro_ldn4 = objItem._X_CLARO_LDN4 != null ? objItem._X_CLARO_LDN4 : string.Empty;

                Itype.p_clarolocal1 = objItem._X_CLAROLOCAL1 != null ? objItem._X_CLAROLOCAL1 : string.Empty;

                Itype.p_clarolocal2 = objItem._X_CLAROLOCAL2 != null ? objItem._X_CLAROLOCAL2 : string.Empty;

                Itype.p_clarolocal3 = objItem._X_CLAROLOCAL3 != null ? objItem._X_CLAROLOCAL3 : string.Empty;

                Itype.p_clarolocal4 = objItem._X_CLAROLOCAL4 != null ? objItem._X_CLAROLOCAL4 : string.Empty;

                Itype.p_clarolocal5 = objItem._X_CLAROLOCAL5 != null ? objItem._X_CLAROLOCAL5 : string.Empty;

                Itype.p_clarolocal6 = objItem._X_CLAROLOCAL6 != null ? objItem._X_CLAROLOCAL6 : string.Empty;

                Itype.p_contact_phone = objItem._X_CONTACT_PHONE != null ? objItem._X_CONTACT_PHONE : string.Empty;

                Itype.p_dni_legal_rep = objItem._X_DNI_LEGAL_REP != null ? objItem._X_DNI_LEGAL_REP : string.Empty;

                Itype.p_document_number = objItem._X_DOCUMENT_NUMBER != null ? objItem._X_DOCUMENT_NUMBER : string.Empty;

                Itype.p_email = objItem._X_EMAIL != null ? objItem._X_EMAIL : string.Empty;

                Itype.p_first_name = objItem._X_FIRST_NAME != null ? objItem._X_FIRST_NAME : string.Empty;

                Itype.p_fixed_number = objItem._X_FIXED_NUMBER != null ? objItem._X_FIXED_NUMBER : string.Empty;

                Itype.p_flag_change_user = objItem._X_FLAG_CHANGE_USER != null ? objItem._X_FLAG_CHANGE_USER : string.Empty;

                Itype.p_flag_legal_rep = objItem._X_FLAG_LEGAL_REP != null ? objItem._X_FLAG_LEGAL_REP : string.Empty;

                Itype.p_flag_other = objItem._X_FLAG_OTHER != null ? objItem._X_FLAG_OTHER : string.Empty;

                Itype.p_flag_titular = objItem._X_FLAG_TITULAR != null ? objItem._X_FLAG_TITULAR : string.Empty;

                Itype.p_imei = objItem._X_IMEI != null ? objItem._X_IMEI : string.Empty;

                Itype.p_last_name = objItem._X_LAST_NAME != null ? objItem._X_LAST_NAME : string.Empty;

                Itype.p_lastname_rep = objItem._X_LASTNAME_REP != null ? objItem._X_LASTNAME_REP : string.Empty;

                Itype.p_ldi_number = objItem._X_LDI_NUMBER != null ? objItem._X_LDI_NUMBER : string.Empty;

                Itype.p_name_legal_rep = objItem._X_NAME_LEGAL_REP != null ? objItem._X_NAME_LEGAL_REP : string.Empty;

                Itype.p_old_claro_ldn1 = objItem._X_OLD_CLARO_LDN1 != null ? objItem._X_OLD_CLARO_LDN1 : string.Empty;

                Itype.p_old_claro_ldn2 = objItem._X_OLD_CLARO_LDN2 != null ? objItem._X_OLD_CLARO_LDN2 : string.Empty;

                Itype.p_old_claro_ldn3 = objItem._X_OLD_CLARO_LDN3 != null ? objItem._X_OLD_CLARO_LDN3 : string.Empty;

                Itype.p_old_claro_ldn4 = objItem._X_OLD_CLARO_LDN2 != null ? objItem._X_OLD_CLARO_LDN4 : string.Empty;

                Itype.p_old_clarolocal1 = objItem._X_OLD_CLAROLOCAL1 != null ? objItem._X_OLD_CLAROLOCAL1 : string.Empty;

                Itype.p_old_clarolocal2 = objItem._X_OLD_CLAROLOCAL2 != null ? objItem._X_OLD_CLAROLOCAL2 : string.Empty;

                Itype.p_old_clarolocal3 = objItem._X_OLD_CLAROLOCAL3 != null ? objItem._X_OLD_CLAROLOCAL3 : string.Empty;

                Itype.p_old_clarolocal4 = objItem._X_OLD_CLAROLOCAL4 != null ? objItem._X_OLD_CLAROLOCAL4 : string.Empty;

                Itype.p_old_clarolocal5 = objItem._X_OLD_CLAROLOCAL5 != null ? objItem._X_OLD_CLAROLOCAL5 : string.Empty;

                Itype.p_old_clarolocal6 = objItem._X_OLD_CLAROLOCAL6 != null ? objItem._X_OLD_CLAROLOCAL6 : string.Empty;

                Itype.p_old_doc_number = objItem._X_OLD_DOC_NUMBER != null ? objItem._X_OLD_DOC_NUMBER : string.Empty;

                Itype.p_old_first_name = objItem._X_OLD_FIRST_NAME != null ? objItem._X_OLD_FIRST_NAME : string.Empty;

                Itype.p_old_fixed_phone = objItem._X_OLD_FIXED_PHONE != null ? objItem._X_OLD_FIXED_PHONE : string.Empty;

                Itype.p_old_last_name = objItem._X_OLD_LAST_NAME != null ? objItem._X_OLD_LAST_NAME : string.Empty;

                Itype.p_old_ldi_number = objItem._X_OLD_LDI_NUMBER != null ? objItem._X_OLD_LDI_NUMBER : string.Empty;

                Itype.p_old_fixed_number = objItem._X_OLD_FIXED_NUMBER != null ? objItem._X_OLD_FIXED_NUMBER : string.Empty;

                Itype.p_operation_type = objItem._X_OPERATION_TYPE != null ? objItem._X_OPERATION_TYPE : string.Empty;

                Itype.p_other_doc_number = objItem._X_OTHER_DOC_NUMBER != null ? objItem._X_OTHER_DOC_NUMBER : string.Empty;

                Itype.p_other_first_name = objItem._X_OTHER_FIRST_NAME != null ? objItem._X_OTHER_FIRST_NAME : string.Empty;

                Itype.p_other_last_name = objItem._X_OTHER_LAST_NAME != null ? objItem._X_OTHER_LAST_NAME : string.Empty;

                Itype.p_other_phone = objItem._X_OTHER_PHONE != null ? objItem._X_OTHER_PHONE : string.Empty;

                Itype.p_phone_legal_rep = objItem._X_PHONE_LEGAL_REP != null ? objItem._X_PHONE_LEGAL_REP : string.Empty;

                Itype.p_phone_legal_rep = objItem._X_PHONE_LEGAL_REP != null ? objItem._X_PHONE_LEGAL_REP : string.Empty;

                Itype.p_reference_phone = objItem._X_REFERENCE_PHONE != null ? objItem._X_REFERENCE_PHONE : string.Empty;

                Itype.p_reason = objItem._X_REASON != null ? objItem._X_REASON : string.Empty;

                Itype.p_model = objItem._X_MODEL != null ? objItem._X_MODEL : string.Empty;

                Itype.p_lot_code = objItem._X_LOT_CODE != null ? objItem._X_LOT_CODE : string.Empty;

                Itype.p_flag_registered = objItem._X_FLAG_REGISTERED != null ? objItem._X_FLAG_REGISTERED : string.Empty;

                Itype.p_registration_reason = objItem._X_REGISTRATION_REASON != null ? objItem._X_REGISTRATION_REASON : string.Empty;

                Itype.p_claro_number = objItem._X_CLARO_NUMBER != null ? objItem._X_CLARO_NUMBER : string.Empty;

                Itype.p_month = objItem._X_MONTH != null ? objItem._X_MONTH : string.Empty;

                Itype.p_ost_number = objItem._X_OST_NUMBER != null ? objItem._X_OST_NUMBER : string.Empty;

                Itype.p_basket = objItem._X_BASKET != null ? objItem._X_BASKET : string.Empty;

                if (objItem._X_EXPIRE_DATE.ToString("dd-MM-yyyy").Equals("01-01-0001"))
                {
                    Itype.p_expire_date = "";
                }
                else
                {
                    Itype.p_expire_date = objItem._X_EXPIRE_DATE.ToString("yyyy-MM-dd");
                }

                Itype.p_ADDRESS5 = objItem._X_EXPIRE_DATE.ToString("dd/MM/yyyy") != null ? objItem._X_EXPIRE_DATE.ToString("dd/MM/yyyy") : string.Empty;
                
                Itype.p_CHARGE_AMOUNT = objItem._X_CHARGE_AMOUNT != null ? objItem._X_CHARGE_AMOUNT.ToString() : "0";

                Itype.p_CITY = objItem._X_CITY != null ? objItem._X_CITY : string.Empty;

                Itype.p_CONTACT_SEX = objItem._X_CONTACT_SEX != null ? objItem._X_CONTACT_SEX : string.Empty;

                Itype.p_DEPARTMENT = objItem._X_DEPARTMENT != null ? objItem._X_DEPARTMENT : string.Empty;

                Itype.p_DISTRICT = objItem._X_DISTRICT != null ? objItem._X_DISTRICT : string.Empty;

                Itype.p_EMAIL_CONFIRMATION = objItem._X_EMAIL_CONFIRMATION != null ? objItem._X_EMAIL_CONFIRMATION : string.Empty;

                Itype.p_FAX = objItem._X_FAX != null ? objItem._X_FAX : string.Empty;

                Itype.p_FLAG_CHARGE = objItem._X_FLAG_CHARGE != null ? objItem._X_FLAG_CHARGE : string.Empty;

                Itype.p_MARITAL_STATUS = objItem._X_MARITAL_STATUS != null ? objItem._X_MARITAL_STATUS : string.Empty;

                Itype.p_OCCUPATION = objItem._X_OCCUPATION != null ? objItem._X_OCCUPATION : string.Empty;

                Itype.p_POSITION = objItem._X_POSITION != null ? objItem._X_POSITION : string.Empty;

                Itype.p_REFERENCE_ADDRESS = objItem._X_REFERENCE_ADDRESS != null ? objItem._X_REFERENCE_ADDRESS : string.Empty;

                Itype.p_TYPE_DOCUMENT = objItem._X_TYPE_DOCUMENT != null ? objItem._X_TYPE_DOCUMENT : string.Empty;

                Itype.p_ZIPCODE = objItem._X_ZIPCODE != null ? objItem._X_ZIPCODE : string.Empty;

                Itype.p_iccid = objItem._X_ICCID != null ? objItem._X_ICCID : string.Empty;


                AuditType = Claro.Web.Logging.ExecuteMethod<Service_Interaccion.AuditType>(strSesion, strTransaction,
                     () =>
                     {
                         return ServiceConfiguration.ServiceInteraccion
                             .nuevaInteraccionPlus(strTransaction, Itype, out idInteraccionPlus);
                     });


                strMessage = AuditType.errorCode;
                if (idInteraccionPlus != "")
                {
                    idInteraccionPlusR = idInteraccionPlus;
                    strFlagInsercion = "OK";
                    blnRetorno = true;
                }
                else
                {
                    strFlagInsercion = "NO";
                    idInteraccionPlusR = "";
                    blnRetorno = false;
                }

            }
            catch (Exception ex)
            {

                Web.Logging.Error(strSesion, strTransaction, "Error WS - nuevaInteraccion plantilla :" + ex.Message);
                blnRetorno = false;
                strFlagInsercion = "NO";
                throw ex;
            }
            return (strFlagInsercion.Equals(KEY.AppSettings("InteractionMessage")));

        }
        #region SendEmailWithBase64

        /// <summary>Método que envía un correo electrónico.</summary>
        /// <param name="objRequest"></param>   
        /// <returns>EntitiesEmail.SendEmailWithBase64Response</returns>
        /// <remarks>SendEmailWithBase64</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>18/03/2019</FecCrea></item></list>
        public static EntitiesEmail.SendEmailWithBase64Response SendEmailWithBase64(EntitiesEmail.SendEmailWithBase64Request objRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session,
           "Transaccion: " + objRequest.Audit.Transaction,
           string.Format("Begin SendEmailWithBase64, Parámetro de Entrada SendEmailWithBase64Request=> Sender: {0}, To: {1}, Subject: {2}, Message: {3}, HtmlFlag: {4}, CountListAttachedFile: {5}, CountListParameters: {6}",
           objRequest.Sender, objRequest.To, objRequest.Subject, objRequest.Message, objRequest.HtmlFlag, objRequest.ListAttachedFile != null ? objRequest.ListAttachedFile.Count : 0, objRequest.ListParameters != null ? objRequest.ListParameters.Count : 0));

            EntitiesEmail.SendEmailWithBase64Response objResponse = new EntitiesEmail.SendEmailWithBase64Response();

            try
            {

                //Validando la cantidad de archivos adjuntos
                int cantArchivos = 0;
                int cantParametros = 0;

                if (objRequest != null)
                {
                    if (objRequest.ListAttachedFile != null)
                    {
                        cantArchivos = objRequest.ListAttachedFile.Count;
                    }
                    if (objRequest.ListParameters != null)
                    {
                        cantParametros = objRequest.ListParameters.Count;
                    }

                    #region Request

                    //Se inicializa el Request
                    COMMON_ENVIOCORREOSB.AuditTypeRequest objAuditTypeRequest = new COMMON_ENVIOCORREOSB.AuditTypeRequest();
                    COMMON_ENVIOCORREOSB.ArchivoAdjunto[] lstArchivoAdjuntos = new COMMON_ENVIOCORREOSB.ArchivoAdjunto[cantArchivos];
                    COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[] lstParameters = new COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[cantParametros];

                    COMMON_ENVIOCORREOSB.ArchivoAdjunto objArchivo = null;
                    COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType objParametroComplexType = null;
                    //Se llena los valores al objeto Request
                    objAuditTypeRequest.idTransaccion = objRequest.Audit.Transaction;
                    objAuditTypeRequest.ipAplicacion = objRequest.Audit.IPAddress;
                    objAuditTypeRequest.codigoAplicacion = KEY.AppSettings("CodAplicacion_SIACPO").ToString();


                    if (cantArchivos > 0)
                    {
                        int indiceLstArchivoAdjuntos = 0;
                        foreach (var archivo in objRequest.ListAttachedFile)
                        {
                            objArchivo = new COMMON_ENVIOCORREOSB.ArchivoAdjunto();
                            objArchivo.nombre = archivo.Name;
                            objArchivo.cabecera = archivo.HeadBoard;
                            objArchivo.archivo = archivo.file;
                            lstArchivoAdjuntos[indiceLstArchivoAdjuntos] = objArchivo;
                            indiceLstArchivoAdjuntos++;
                        }
                    }
                    if (cantParametros > 0)
                    {
                        int indiceLstParameters = 0;
                        foreach (var parametro in objRequest.ListParameters)
                        {
                            objParametroComplexType = new COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType();
                            objParametroComplexType.clave = parametro.Key;
                            objParametroComplexType.valor = parametro.Value;
                            lstParameters[indiceLstParameters] = objParametroComplexType;
                            indiceLstParameters++;
                        }
                    }

                    #endregion

                    #region Response
                    COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[] ListParametersResponse = null;
                    COMMON_ENVIOCORREOSB.AuditTypeResponse objAuditTypeResponse = null;

                    #endregion

                    //Se ejecuta el método del WS
                    objAuditTypeResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Configuration.WebServiceConfiguration.SIACUEnvioCorreoSB.enviarCorreo(objAuditTypeRequest, objRequest.Sender, objRequest.To, objRequest.Subject, objRequest.Message, objRequest.HtmlFlag, lstArchivoAdjuntos, lstParameters, out ListParametersResponse);
                    });

                    //Se valida el response del WS
                    if (objAuditTypeResponse != null)
                    {
                        objResponse.IdTransaccion = objAuditTypeResponse.idTransaccion;
                        objResponse.ResponseCode = objAuditTypeResponse.codigoRespuesta;
                        objResponse.ResponseMessage = objAuditTypeResponse.mensajeRespuesta;
                        Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "End SendEmailWithBase64, Parámetro de Salida AuditTypeResponse => idTransaccion: " + objAuditTypeResponse.idTransaccion + ", codigoRespuesta: " + objAuditTypeResponse.codigoRespuesta + ", mensajeRespuesta: " + objAuditTypeResponse.mensajeRespuesta);
                    }
                    else
                    {
                        Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "End SendEmailWithBase64, Parámetro de Salida AuditTypeResponse => NULL");
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }

            return objResponse;
        }
        #endregion

        public static bool DisplayFileFromServerSharedFile(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        {
            bool blnResult;
            FileInfo fiInfo = null;
            FileStream fs = null;
            BinaryReader br = null;
            mydata = null;
            Siacu_Impersonation obj = new Siacu_Impersonation();
            try
            {
                string user = string.Empty;
                string pass = string.Empty;
                string domain = string.Empty;
                bool credentials = false;
                credentials = Get_Credentials(strIdSession, strTransaction, ConfigurationManager.AppSettings("ConexionSamba"), out user, out pass, out domain);
                if (obj.impersonateValidUser(user, domain, pass))
                {
                    fiInfo = new FileInfo(strPath);
                    fs = new FileStream(fiInfo.ToString(), FileMode.Open);
                    br = new BinaryReader(fs);
                    mydata = br.ReadBytes(Convert.ToInt((fs.Length - 1)));

                    blnResult = true;
                }
                else
                {

                    blnResult = false;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (br != null)
                {
                    br.Close();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                mydata = null;
                blnResult = false;
            }

            obj.undoImpersonatiom();

            return blnResult;
        }

        public static bool Get_Credentials(string strIdSession, string strTransaction, string strConexion, out string struser, out string strpass, out string strdomain)
        {
            try
            {
                ConfigurationRecord ConfigurationCredentials = new ConfigurationRecord(strConexion);
                struser = ConfigurationCredentials.LeerUsuario();

                strpass = ConfigurationCredentials.LeerContrasena();

                strdomain = ConfigurationCredentials.LeerBaseDatos();


                return true;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                struser = string.Empty;
                strpass = string.Empty;
                strdomain = string.Empty;
                return false;
            }
        }

        #region FirmaDigital
        /// <summary>Método que permite generar un certificado digital a un documento existente.</summary>
        /// <param name="objRequest"></param>   
        /// <returns>EntitiesDigitalSignature.DigitalSignatureResponse</returns>
        /// <remarks>FirmarDocumento</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/04/2019</FecCrea></item></list>
        public static EntitiesDigitalSignature.DigitalSignatureResponse FirmarDocumento(EntitiesDigitalSignature.DigitalSignatureRequest objRequest)
        {
            EntitiesDigitalSignature.DigitalSignatureResponse objResponse = null;
            COMMON_DIGITAL_SIGNATURE.firmarDocumentoResponse objFirmarDocumentoResponse = null;
            COMMON_DIGITAL_SIGNATURE.HeaderRequestType objHeaderRequest = new COMMON_DIGITAL_SIGNATURE.HeaderRequestType();
            COMMON_DIGITAL_SIGNATURE.firmarDocumentoRequest objFirmarDocumentRequest = new COMMON_DIGITAL_SIGNATURE.firmarDocumentoRequest();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest = new COMMON_DIGITAL_SIGNATURE.DatosFirmarDocumentoRequest();

            objFirmarDocumentRequest.datosFirmarDocumentoRequest.codigoPDV = objRequest.CodigoPDV;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.nombrePDV = objRequest.NombrePDV;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.tipoFirma = KEY.AppSettings("strTipoFirmaDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.tipoArchivo = KEY.AppSettings("strTipoArchivoDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.negocio = KEY.AppSettings("strNegocioDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.tipoContrato = KEY.AppSettings("strTipoContratoDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.datFirma = KEY.AppSettings("strDatFirmaDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.origenArchivo = KEY.AppSettings("strOrigenArchivoDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.codigoAplicacion = KEY.AppSettings("strCodigoAplicacionDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.posFirma = KEY.AppSettings("strPosFirmaDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.nombreArchivo = objRequest.NombreArchivo;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.ipAplicacion = objRequest.Audit.IPAddress;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.numeroArchivo = objRequest.NumeroArchivo;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.segmentoOferta = KEY.AppSettings("strSegmentoOfertaDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.plantillaBRMS = KEY.AppSettings("strPlantillaBRMS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.tipoOperacion = KEY.AppSettings("strTipoOperacionDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.tipoDocumento = objRequest.TipoDocumento;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.numeroDocumento = objRequest.NumeroDocumento;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.contenidoArchivo = String.Empty;
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.rutaArchivoDestino = KEY.AppSettings("strRutaArchivoDestinoDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.rutaArchivoOrigen = KEY.AppSettings("strRutaArchivoOrigenDS").ToString() + KEY.AppSettings("strRutaArchivoDestinoDS").ToString();
            objFirmarDocumentRequest.datosFirmarDocumentoRequest.canalAtencion = objRequest.CanalAtencion;

            objHeaderRequest.canal = objRequest.Audit.ApplicationName;
            objHeaderRequest.idAplicacion = objRequest.Audit.IPAddress;
            objHeaderRequest.idTransaccionESB = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            objHeaderRequest.idTransaccionNegocio = objRequest.Audit.Transaction;
            objHeaderRequest.usuarioAplicacion = objRequest.Audit.UserName;
            objHeaderRequest.usuarioSesion = objRequest.Audit.UserName;
            objHeaderRequest.fechaInicio = DateTime.Parse(string.Format("{0:s}", DateTime.Now));
            objHeaderRequest.nodoAdicional = new object();

            try
            {
                objFirmarDocumentoResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Configuration.WebServiceConfiguration.SIACUDigitalSignature.firmarDocumento(objFirmarDocumentRequest);
                    });

                if (objFirmarDocumentoResponse != null && objFirmarDocumentoResponse.responseStatus != null && objFirmarDocumentoResponse.responseData != null)
                {
                    objResponse = new EntitiesDigitalSignature.DigitalSignatureResponse();
                    objResponse.ResponseStatus = new EntitiesDigitalSignature.ResponseStatus();
                    objResponse.ResponseData = new EntitiesDigitalSignature.ResponseData();

                    objResponse.ResponseStatus.Estado = objFirmarDocumentoResponse.responseStatus.estado;
                    objResponse.ResponseStatus.CodigoRespuesta = objFirmarDocumentoResponse.responseStatus.codigoRespuesta;
                    objResponse.ResponseStatus.DescripcionRespuesta = objFirmarDocumentoResponse.responseStatus.descripcionRespuesta;
                    objResponse.ResponseStatus.UbicacionError = objFirmarDocumentoResponse.responseStatus.ubicacionError;
                    objResponse.ResponseStatus.Fecha = objFirmarDocumentoResponse.responseStatus.fecha;
                    objResponse.ResponseStatus.Origen = objFirmarDocumentoResponse.responseStatus.origen;

                    if (objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse != null)
                    {
                        objResponse.ResponseData.DatosFirmarDocumento = new EntitiesDigitalSignature.DataSignDocument();
                        objResponse.ResponseData.DatosFirmarDocumento.RutaArchivo = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.rutaArchivo;
                        objResponse.ResponseData.DatosFirmarDocumento.IdTransaccion = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.idTransaccion;
                        objResponse.ResponseData.DatosFirmarDocumento.FechaInicio = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.fechaInicio;
                        objResponse.ResponseData.DatosFirmarDocumento.FechaFin = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.fechaFin;
                        objResponse.ResponseData.DatosFirmarDocumento.CodigoRespuesta = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.codigoRespuesta;
                        objResponse.ResponseData.DatosFirmarDocumento.MensajeRespuesta = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.mensajeRespuesta;
                        objResponse.ResponseData.DatosFirmarDocumento.DescripcionRespuesta = objFirmarDocumentoResponse.responseData.datosFirmarDocumentoResponse.descripcionRespuesta;
                    }

                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, String.Format("Error con la respuesta del ResponseStatus => CodigoRespuesta: {0}, DescripcionRespuesta: {1} ", objResponse.ResponseStatus.CodigoRespuesta, objResponse.ResponseStatus.DescripcionRespuesta));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objResponse;
        }
        #endregion

        public static List<IGVTax> GetConsultIGV(string strIdSession, string strIdTransaction, string strAppID, string strAppName, string strAppUser)
        {
            List<IGVTax> listConsultIGV = null;
            IGVTax oConsultIGV = null;
            COMMON_IGV.consultarIGVRequest objConsultarIGVRequest = new COMMON_IGV.consultarIGVRequest();
            COMMON_IGV.AuditRequestType audi = new COMMON_IGV.AuditRequestType();
            audi.idTransaccion = strIdTransaction;
            audi.ipAplicacion = strAppID;
            audi.nombreAplicacion = strAppName;
            audi.usuarioAplicacion = strAppUser;

            objConsultarIGVRequest.auditoria = audi;

            var lista = Web.Logging.ExecuteMethod(strIdSession, strIdTransaction, ServiceConfiguration.SIACUConsultaIGV, () =>
            {
                return Configuration.ServiceConfiguration.SIACUConsultaIGV.consultarIGV(objConsultarIGVRequest);
            });

            if (lista != null && lista.listaIGVS != null)
            {
                if (lista.listaIGVS.Length > 0)
                {
                    listConsultIGV = new List<IGVTax>();
                    foreach (var item in lista.listaIGVS)
                    {
                        oConsultIGV = new IGVTax();
                        oConsultIGV.igv = item.igv;
                        oConsultIGV.igvD = item.igvD;
                        oConsultIGV.impudFecFinVigencia = item.impudFecFinVigencia;
                        oConsultIGV.impudFecIniVigencia = item.impudFecIniVigencia;
                        oConsultIGV.impudFecRegistro = item.impudFecRegistro;
                        oConsultIGV.impunTipDoc = item.impunTipDoc;
                        oConsultIGV.imputId = item.imputId;
                        oConsultIGV.impuvDes = item.impuvDes;
                        listConsultIGV.Add(oConsultIGV);
                    }
                }
            }

            return listConsultIGV;
        }

        public static Entity.IFI.Common.GetOffice.OfficeResponse GetOffice(Entity.IFI.Common.GetOffice.OfficeRequest objOfficeRequest)
        {
            var objOfficeResponse = new Entity.IFI.Common.GetOffice.OfficeResponse();
            var objOffice = new Office();
            try
            {
                DbParameter[] parameters = {
                    new DbParameter("P_CTA_RED", DbType.String, ParameterDirection.Input,objOfficeRequest.strCodeUser),  
                    new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
                };
                DbFactory.ExecuteReader(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CONSULTA_PDV_USUARIO, parameters,
                    (IDataReader dr) =>
                    {
                        while (dr.Read())
                        {
                            objOffice.strCodeUserSisac = Convert.ToString(dr["USUAN_CODIGO"]);
                            objOffice.strCodeClasificationOffice = Convert.ToString(dr["TOFIC_CODIGO"]);
                            objOffice.strCodeOffice = Convert.ToString(dr["OVENC_CODIGO"]);
                        }
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, ex.Message);
                objOffice = null;
            }
            objOfficeResponse.objOffice = objOffice;
            return objOfficeResponse;
        }

        public static List<ConsultSecurity> VerifyUser(string sessionId, string transactionId, string appId, string appName, string username, string appCode)
        {
            string errorMsgTemp;
            var objResponse = new List<ConsultSecurity>();
            EntitiesConsultaSeguridad.seguridadType[] objLstService = { };
            var transactionIdTemp = string.Empty;
            var messageService = Web.Logging.ExecuteMethod(sessionId, transactionId, ServiceConfiguration.SIACU_ConsultaSeguridad, () =>
            {
                return ServiceConfiguration.SIACU_ConsultaSeguridad.verificaUsuario(ref transactionIdTemp, appId, appName, username, Convert.ToInt64(appCode), out errorMsgTemp, out objLstService);
            });

            if (objLstService.Length > 0 && string.IsNullOrEmpty(messageService))
            {
                foreach (var item in objLstService)
                {
                    var obj = new ConsultSecurity
                    {
                        Perfccod = item.PerfcCod,
                        Usuaccod = item.UsuacCod,
                        Usuaccodvensap = item.UsuacCodVenSap
                    };
                    objResponse.Add(obj);
                }
            }

            return objResponse;
        }

        public static SECURITY.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(SECURITY.GetPagOptionXuser.PagOptionXuserRequest objPagOptionXuserMRequest)
        {

            SECURITY.GetPagOptionXuser.PagOptionXuserResponse model = new SECURITY.GetPagOptionXuser.PagOptionXuserResponse();
            model.ListConsultSecurity = new List<ConsultSecurity>();

            var opcionesUsuarioReques = new AUDIT.PaginaOpcionesUsuarioRequest();
            var opcionesPorUsuario = new AUDIT.PaginaOpcionesUsuarioResponse();

            opcionesUsuarioReques.aplicCod = objPagOptionXuserMRequest.IntAplicationCode;
            opcionesUsuarioReques.user = objPagOptionXuserMRequest.IntUser;
            AUDIT.PaginaOpcionType[] objSeg;

            opcionesPorUsuario = Claro.Web.Logging.ExecuteMethod(() =>
            {
                return WebServiceConfiguration.GRABARAUDIT.leerPaginaOpcionesPorUsuario(opcionesUsuarioReques);

            });

            model.ErrMessage = opcionesPorUsuario.mensaje;
            model.CodeErr = opcionesPorUsuario.resultado;
            objSeg = opcionesPorUsuario.listaOpciones;

            if (model.CodeErr == Claro.Constants.NumberZeroString)
            {
                if (objSeg != null)
                {
                    objSeg.ToList().ForEach(x =>
                    {
                        model.ListConsultSecurity.Add(new ConsultSecurity
                        {
                            Opciccod = x.opcicCod,
                            Opcicabrev = x.clave,
                            Opcicdes = x.opcicDes
                        });
                    });
                }
            }

            return model;
        }

        /// <summary>
        /// crea xml de desistimiento de Cambio de Plan
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy_Desis_PlanMigration</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>20/06/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Hitss</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        private static string BuildXML_Constancy_Desist_PlanMigration(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
            xml += "<FECHA_TRANSACCION_PROGRAM>" + parameters.strFechaTransaccion + "</FECHA_TRANSACCION_PROGRAM>";
            xml += "<TITULAR_CLIENTE>" + parameters.StrTitularCliente + "</TITULAR_CLIENTE>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
            xml += "<CICLO_FACTURACION>" + parameters.StrCicloFacturacion + "</CICLO_FACTURACION>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";
            xml += "<NRO_SERVICIO>" + parameters.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TIPO_TRANSACCION>" + parameters.StrTipoTransaccion + "</TIPO_TRANSACCION>";
            xml += "<FECHA_EJECUCION>" + parameters.StrFechaEjecucion + "</FECHA_EJECUCION>";
            xml += "<ACCION>" + parameters.StrAccion + "</ACCION>";
            xml += "<FECHA_DESISTIMIENTO>" + parameters.StrFecDesistimiento + "</FECHA_DESISTIMIENTO>";
            xml += "<ENVIO_CORREO>" + parameters.strEnvioCorreo + "</ENVIO_CORREO>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<CODIGO_ASESOR>" + parameters.StrCodUsuario + "</CODIGO_ASESOR>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "</PLANTILLA>";

            return xml;
        }

        /// <summary> crea xml de reinicio de velocidad  </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>BuildXML_Constancy_RestarSpeed</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>22/08/2020</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list>
        private static string BuildXML_Constancy_RestarSpeed(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<CENTRO_ATENCION_AREA>" + parameters.strCentroAtencion + "</CENTRO_ATENCION_AREA>";
            xml += "<TITULAR_CLIENTE>" + parameters.StrTitularCliente + "</TITULAR_CLIENTE>";
            xml += "<REPRES_LEGAL>" + parameters.strRepresentanteLegal + "</REPRES_LEGAL>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<FECHA_AUTORIZACION>" + parameters.strFechaHoraAtención + "</FECHA_AUTORIZACION>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";
            xml += "<CUENTA>" + parameters.strCuentaCliente + "</CUENTA>";
            xml += "<NUMERO_SERVICIO>" + parameters.strNumeroServicio + "</NUMERO_SERVICIO>";
            xml += "<TIPO_CLIENTE>" + parameters.strTipoCliente + "</TIPO_CLIENTE>";
            xml += "<PRODUCTO_ADQUIRIDO>" + parameters.strPaqueteVelDegradada + "</PRODUCTO_ADQUIRIDO>";
            xml += "<VIGENCIA>" + parameters.strVigenciaPaquete + "</VIGENCIA>";
            xml += "<TIPO_PAGO>" + parameters.strTipoVenta + "</TIPO_PAGO>";
            xml += "<PRECIO_PAQUETE>" + parameters.strPrecioPaquete + "</PRECIO_PAQUETE>";
            xml += "<MB_INCLUIDOS>" + parameters.strMBIncluidos + "</MB_INCLUIDOS>";
            xml += "<ENVIO_CORREO>" + parameters.strEnvioCorreo + "</ENVIO_CORREO>";
            xml += "<CORREO>" + parameters.strCorreoCliente + "</CORREO>";
            xml += "<CODIGO_AGENTE>" + parameters.StrCodUsuario + "</CODIGO_AGENTE>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_ASESOR>";
            xml += "</PLANTILLA>";

            return xml;
        }

        //public static List<Entity.IFI.Fixed.PostPCRFPaquetesAdic.PCRFPaquetesAdqueridos> GetPaquetesAdquiridosHistorio(string strIdSession, string strTransaction, string strMsisdn, string strSncode)
        //{
        //    Claro.Web.Logging.Info(strIdSession, strTransaction, "Transaction: GetPaquetesAdquiridosHistorio 1");
        //    DbParameter[] parameters = 
        //    {
        //        new DbParameter("PI_MSISDN", DbType.String,255, ParameterDirection.Input,strMsisdn),
        //        new DbParameter("PI_SNCODE", DbType.String,255, ParameterDirection.Input,strSncode),
        //        new DbParameter("PO_CODERROR", DbType.Int32, ParameterDirection.Output),
        //        new DbParameter("PO_MSJERROR", DbType.String,255, ParameterDirection.Output),
        //        new DbParameter("PO_CURSOR_PAQ", DbType.Object, ParameterDirection.Output)
        //    };

        //    List<Entity.IFI.Fixed.PostPCRFPaquetesAdic.PCRFPaquetesAdqueridos> listItem = null;

        //    try
        //    {
        //        Claro.Web.Logging.Info(strIdSession, strTransaction, "Transaction: GetPaquetesAdquiridosHistorio 2");
        //        DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCSSS_BSCS_HIST,
        //    parameters, (IDataReader reader) =>
        //    {
        //        listItem = new List<Entity.IFI.Fixed.PostPCRFPaquetesAdic.PCRFPaquetesAdqueridos>();

        //        while (reader.Read())
        //        {
        //            listItem.Add(new Entity.IFI.Fixed.PostPCRFPaquetesAdic.PCRFPaquetesAdqueridos
        //            {
        //                paquete = Convert.ToString(reader["PAQUETE"]),
        //                fechaCPR = Convert.ToString(reader["FECHA_VIGENCIA"]),
        //                monto = Convert.ToString(reader["MONTO"]),
        //                tipoPago = Convert.ToString(reader["TIPO_PAGO"]),
        //                fechaAct = Convert.ToString(reader["FECHA_ACTIVACION"]),
        //                estado = Convert.ToString(reader["ESTADO"])
        //            });
        //        }
        //    });
        //    }
        //    catch (Exception ex)
        //    {
        //        Web.Logging.Error(strIdSession, strTransaction, ex.Message);
        //    }
        //    return listItem;
        //}

        /// <summary> Retorna lista de SIAC_POST_BSCS para el listado paquetes adquiridos </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strMsisdn"></param>    
        /// <param name="strSncode"></param> 
        /// <returns>List</returns>
        /// <remarks>GetPaquetesAdquiridosHistorio</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>01/03/2021.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Hitss</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Entity.IFI.Common.GetPaquetesAdquiridosHistorico.PaquetesAdqueridos> GetPaquetesAdquiridosHistorio(string strIdSession, string strTransaction, string strMsisdn, string strSncode)
        {
            Claro.Web.Logging.Info(strIdSession, strTransaction, "Transaction: GetPaquetesAdquiridosHistorio 1");
            DbParameter[] parameters = 
            {
                new DbParameter("PI_MSISDN", DbType.String,255, ParameterDirection.Input,strMsisdn),
                new DbParameter("PI_SNCODE", DbType.String,255, ParameterDirection.Input,strSncode),
                new DbParameter("PO_CODERROR", DbType.Int32, ParameterDirection.Output),
                new DbParameter("PO_MSJERROR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("PO_CURSOR_PAQ", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.IFI.Common.GetPaquetesAdquiridosHistorico.PaquetesAdqueridos> listItem = null;

            try
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, "Transaction: GetPaquetesAdquiridosHistorio 2");
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCSSS_BSCS_HIST,
            parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.IFI.Common.GetPaquetesAdquiridosHistorico.PaquetesAdqueridos>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.IFI.Common.GetPaquetesAdquiridosHistorico.PaquetesAdqueridos
                    {
                        paquete = Convert.ToString(reader["PAQUETE"]),
                        fechaCPR = Convert.ToString(reader["FECHA_VIGENCIA"]),
                        monto = Convert.ToString(reader["MONTO"]),
                        tipoPago = Convert.ToString(reader["TIPO_PAGO"]),
                        fechaAct = Convert.ToString(reader["FECHA_ACTIVACION"]),
                        estado = Convert.ToString(reader["ESTADO"])
                    });
                }
            });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }   
            return listItem;
        }

        #region "DataPower - Credenciales"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAuditRequest"></param>
        /// <param name="keyUser"></param>
        /// <param name="keyPass"></param>
        /// <returns></returns>
        public static Claro.Entity.UsernameToken getCredentials(Claro.Entity.AuditRequest objAuditRequest, string keyUser, string keyPass)
        {
            Claro.Entity.UsernameToken objCrendentials = null;
            string User = KEY.AppSettings(keyUser);
            string pass = KEY.AppSettings(keyPass);
            objCrendentials = Decrypt(objAuditRequest, User, pass);
            return objCrendentials;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAuditRequest"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private static Claro.Entity.UsernameToken Decrypt(Claro.Entity.AuditRequest objAuditRequest, string user, string pass)
        {
            Claro.Entity.UsernameToken objCrendentials;
            desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
            string User = string.Empty;
            string Password = string.Empty;
            string resStatus = string.Empty;
            Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Inicio de Ejecución del WS ConsultaClaves - Método Desencriptar");
            try
            {
                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                {
                    idTransaccion = objAuditRequest.Transaction,
                    ipAplicacion = objAuditRequest.IPAddress,
                    ipTransicion = objAuditRequest.IPAddress,
                    usrAplicacion = objAuditRequest.UserName,
                    idAplicacion = objAuditRequest.ApplicationName,
                    codigoAplicacion = KEY.AppSettings("codigoAplicacion"),
                    usuarioAplicacionEncriptado = user,
                    claveEncriptado = pass,
                };

                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar(ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave);

                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Codigo resultado  0, desencriptado con exito - Método Desencriptar");
                }
                else
                {
                    Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Codigo resultado diferente de 0, error  al desencriptar - Método Desencriptar");
                }
                Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Fin de Ejecución del WS ConsultaClaves - Método Desencriptar");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }
            objCrendentials = new Claro.Entity.UsernameToken()
            {
                Username = objdesencriptarResponse.usuarioAplicacion,
                Password = new Claro.Entity.Password()
                {
                    Value = objdesencriptarResponse.clave
                }
            };
            return objCrendentials;
        }
        #endregion
    }
}

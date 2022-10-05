using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.IO;
using Claro.Data;
using System.Xml;
using Claro.SIACU.Entity.IFI.Fixed;
using CDP = Claro.SIACU.Data.IFI.Common;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeServiceAddress;
using Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi;
using Claro.SIACU.Data.IFI.Configuration;
using System.Xml.Linq;
using System.Linq;
using Newtonsoft.Json;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class ChangeServiceAddress
    {
        /// <summary>
        /// Obtener parametros configurables por nombre.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaccion"></param>
        /// <returns></returns>
        public static List<GenericItem> obtenerParametrosPorTipo(string strIdSession, string strTransaccion)
        {
            List<GenericItem> olisGenericItem = new List<GenericItem>();
            GenericItem oGenericItem = null;
            string strMensaje = string.Empty;
            DbParameter[] parameters = new DbParameter[] 
            { 
                new DbParameter("P_NOMBRE", DbType.String,ParameterDirection.Input, strTransaccion),
                new DbParameter("P_MENSAJE", DbType.Int64,ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
            };
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("ObtenerParametrosPorTipo: Parametro de Entrada => strTransaccion:{0}", strTransaccion));
            DbFactory.ExecuteReader(strIdSession, strTransaccion, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    oGenericItem = new GenericItem();
                    //oGenericItem.Codigo = Claro.Utils.CheckStr(reader["NOMBRE"]);
                    oGenericItem.Descripcion2 = Claro.Utils.CheckStr(reader["DESCRIPCION"]);
                    oGenericItem.Codigo2 = Claro.Utils.CheckStr(reader["VALOR_N"]);
                    oGenericItem.Descripcion = Claro.Utils.CheckStr(reader["VALOR_C"]);
                    olisGenericItem.Add(oGenericItem);
                }
            });
            strMensaje = parameters[1].Value.ToString();
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("ObtenerParametrosPorTipo: Parametro de Salida => Total de Registros: {0}", olisGenericItem.Count.ToString()));
            return olisGenericItem;
        }
        /// <summary>
        /// Permite registrar o actualizar dirección del servicio IFI.
        /// </summary>
        /// <param name="oregistrarDireccionRequest"></param>
        /// <returns></returns>
        public static registrarDireccionResponse registrarDireccion(registrarDireccionRequest oregistrarDireccionRequest)
        {
            registrarDireccionResponse oregistrarDireccionResponse = null;
            Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi.responseStatus oresponse = new Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi.responseStatus();
            XmlDocument xmlDoc = null;
            string strRegistroZonaMovilidad = string.Empty;
            string strNodoCodigo = string.Empty;
            string strNodoDescripcion= string.Empty;
            string strIdTransaccion = string.Empty;
            Claro.Web.Logging.Info(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, "Inicio Método : registrarDireccion");
            try
            {
                strRegistroZonaMovilidad = ConfigurationManager.AppSettings("strRegistroCON_JMS_ZonaMovilidad");
                strNodoCodigo = ConfigurationManager.AppSettings("strCodigoCON_JMS_ZonaMovilidad");
                strNodoDescripcion = ConfigurationManager.AppSettings("strDescripcionCON_JMS_ZonaMovilidad");
                strIdTransaccion = ConfigurationManager.AppSettings("strIdTranCON_JMS_ZonaMovilidad");

                Claro.Web.Logging.Info(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, string.Format("registrarDireccion -> URL : {0}", strRegistroZonaMovilidad));

                var soapNs = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");
                var v1Ns = XNamespace.Get("xmlns:v11='http://claro.com.pe/esb/data/logZonaMovilidad/v1/'");

                var requestXML = new XElement(soapNs + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "soapenv", soapNs),
                    new XElement(soapNs + "Body",
                        new XElement(v1Ns + "registroZonaMovRequest",
                            new XAttribute(XNamespace.Xmlns + "v11", v1Ns),
                              new XElement(v1Ns + "idTransaccion", oregistrarDireccionRequest.idTransaccion),
                              new XElement(v1Ns + "linea", oregistrarDireccionRequest.linea),
                              new XElement(v1Ns + "tipoDireccion", oregistrarDireccionRequest.tipoDireccion),
                              new XElement(v1Ns + "nombreDireccion", oregistrarDireccionRequest.nombreDireccion),
                              new XElement(v1Ns + "numeroDireccion", oregistrarDireccionRequest.numeroDireccion),
                              new XElement(v1Ns + "subDireccion", oregistrarDireccionRequest.subDireccion),
                              new XElement(v1Ns + "numeroSubDireccion", oregistrarDireccionRequest.numeroSubDireccion),
                              new XElement(v1Ns + "lote", oregistrarDireccionRequest.lote),
                              new XElement(v1Ns + "tipoSubDireccion", oregistrarDireccionRequest.tipoSubDireccion),
                              new XElement(v1Ns + "tipoUrbanizacion", oregistrarDireccionRequest.tipoUrbanizacion),
                              new XElement(v1Ns + "nombreUrbanizacion", oregistrarDireccionRequest.nombreUrbanizacion),
                              new XElement(v1Ns + "tipoDomicilio", oregistrarDireccionRequest.tipoDomicilio),
                              new XElement(v1Ns + "zonaEtapa", oregistrarDireccionRequest.zonaEtapa),
                              new XElement(v1Ns + "nombreZonaEtapa", oregistrarDireccionRequest.nombreZonaEtapa),
                              new XElement(v1Ns + "referencia", oregistrarDireccionRequest.referencia),
                              new XElement(v1Ns + "departamento", oregistrarDireccionRequest.departamento),
                              new XElement(v1Ns + "provincia", oregistrarDireccionRequest.provincia),
                              new XElement(v1Ns + "distrito", oregistrarDireccionRequest.distrito),
                              new XElement(v1Ns + "codigoPostal", oregistrarDireccionRequest.codigoPostal),
                              new XElement(v1Ns + "ubigeo", oregistrarDireccionRequest.ubigeo),
                              new XElement(v1Ns + "telefonoReferencia", oregistrarDireccionRequest.telefonoReferencia),
                              new XElement(v1Ns + "flagCobro", oregistrarDireccionRequest.flagCobro),
                              new XElement(v1Ns + "accion", oregistrarDireccionRequest.accion),
                              new XElement(v1Ns + "usuario", oregistrarDireccionRequest.currentUser),
                              new XElement(v1Ns + "estado", oregistrarDireccionRequest.estado)
                        )
                    )
                );

                var jsonIN = Newtonsoft.Json.JsonConvert.SerializeObject(requestXML);
                Claro.Web.Logging.Info(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, "registrarDireccion -> Parametros de entrada - actualizacion: " + jsonIN);

                xmlDoc = SoapService.PostInvoque(strRegistroZonaMovilidad, requestXML.ToString());
                XmlNodeList xmlNodoCodigo = xmlDoc.GetElementsByTagName(strNodoCodigo);
                XmlNodeList xmlNodoDescripcion = xmlDoc.GetElementsByTagName(strNodoDescripcion);
                XmlNodeList xmlNodoIdTransaccion = xmlDoc.GetElementsByTagName(strIdTransaccion);

                if ((xmlNodoCodigo.Count > 0) && (xmlNodoDescripcion.Count > 0) && (xmlNodoIdTransaccion.Count > 0))
                {
                    oregistrarDireccionResponse = new registrarDireccionResponse();
                    oregistrarDireccionResponse.responseStatus = new Entity.IFI.Postpaid.GetSaveChangeServiceAddress.responseStatus();

                    oregistrarDireccionResponse.responseStatus.codigoRespuesta = xmlDoc.GetElementsByTagName(strNodoCodigo)[0].InnerXml.ToString();
                    oregistrarDireccionResponse.responseStatus.descripcionRespuesta = xmlDoc.GetElementsByTagName(strNodoDescripcion)[0].InnerXml.ToString();
                    oregistrarDireccionResponse.responseStatus.idTransaccion = xmlDoc.GetElementsByTagName(strIdTransaccion)[0].InnerXml.ToString();

                    var jsonOUT = Newtonsoft.Json.JsonConvert.SerializeObject(oregistrarDireccionResponse.responseStatus);
                    Claro.Web.Logging.Info(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, "registrarDireccion -> Parametros de salida - actualizacion: " + jsonOUT);

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, "Error : " + ex.Message.ToString());
            }
            Claro.Web.Logging.Info(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, "Fin Método : registrarDireccion");
            return oregistrarDireccionResponse;
        }
        /// <summary>
        /// Permite validar la cobertura de la dirección para el cambio de dirección.
        /// </summary>
        /// <param name="ovalidarCoberturaMessageRequest"></param>
        /// <returns></returns>
        public static validarCoberturaMessageResponse validarCoberturaDireccion(validarCoberturaMessageRequest ovalidarCoberturaMessageRequest)
        {
            validarCoberturaMessageResponse ovalidarCoberturaMessageResponse = null;
            List<ObtenerTipoTecnologia> olisObtenerTipoTecnologia = null;
            Claro.Web.Logging.Info(ovalidarCoberturaMessageRequest.Audit.Transaction, ovalidarCoberturaMessageRequest.Audit.Session, "Inicio Método : validarCoberturaDireccion");
            try
            {
                Claro.Web.Logging.Info(ovalidarCoberturaMessageRequest.Audit.Transaction, ovalidarCoberturaMessageRequest.Audit.Session, "Request DP validarCoberturaDireccion: " + JsonConvert.SerializeObject(ovalidarCoberturaMessageRequest, Newtonsoft.Json.Formatting.Indented));
         
                ovalidarCoberturaMessageResponse = RestService.PostInvoqueDP<validarCoberturaMessageResponse>(Configuration.RestServiceConfiguration.ValidarCoberturaDireccion_DP, ovalidarCoberturaMessageRequest.Audit, ovalidarCoberturaMessageRequest, true, CDP.getCredentials(ovalidarCoberturaMessageRequest.Audit, "strUserAjustesDP", "strPassAjustesDP"));

                Claro.Web.Logging.Info(ovalidarCoberturaMessageRequest.Audit.Transaction, ovalidarCoberturaMessageRequest.Audit.Session, "Response DP validarCoberturaDireccion: " + JsonConvert.SerializeObject(ovalidarCoberturaMessageResponse, Newtonsoft.Json.Formatting.Indented));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(ovalidarCoberturaMessageRequest.Audit.Transaction, ovalidarCoberturaMessageRequest.Audit.Session, "Error : " + ex.Message.ToString());

                if ((ex.Message.IndexOf("Tiempo de espera") > 0) || (ex.Message.IndexOf("tiempo de espera") > 0) || (ex.Message.IndexOf("Timeout") > 0))
                {

                    olisObtenerTipoTecnologia = obtenerTipoTecnologia(ovalidarCoberturaMessageRequest.Audit.Transaction, ovalidarCoberturaMessageRequest.Audit.Session, KEY.AppSettings("strObtenerTipoTecnologiaCobertura"));

                    var olisParam = from oItem in olisObtenerTipoTecnologia where oItem.PARAV_VALOR1.Equals("Key_MsgErrorTimeOutIFI") select oItem;

                    ovalidarCoberturaMessageResponse = new validarCoberturaMessageResponse()
                    {
                        MessageResponse = new validarMessageResponse()
                        {
                            Header = new validarHeaderResponse()
                            {
                                Header = new Entity.IFI.Postpaid.GetDataPower.HeaderResponse()
                                {
                                    Consumer = ovalidarCoberturaMessageRequest.MessageRequest.Header.HeaderRequest.consumer,
                                    Pid = ovalidarCoberturaMessageRequest.MessageRequest.Header.HeaderRequest.pid,
                                    Status = new Entity.IFI.Postpaid.GetDataPower.HeaderStatusResponse()
                                    {
                                        Type = "30",
                                        Code = "3001",
                                        Message = "TIMEOUT DEL SERVICIO PROVEEDOR",
                                        MsgId = "DPS02-747d6877-3e9e-4724-bd62-fe2fe26b39b7"
                                    },
                                    TimeStamp = ovalidarCoberturaMessageRequest.MessageRequest.Header.HeaderRequest.timestamp,
                                    VarArg = ""
            }
                            },
                            Body = new validarBodyResponse()
                            {
                                validarCoberturaResponse = new validarCoberturaResponse()
                                {
                                    responseStatus = new Entity.IFI.Postpaid.GetCoberturaIfi.responseStatus()
                                    {
                                        codigoRespuesta = "-1",
                                        descripcionRespuesta = olisParam.ElementAt(0).PARAV_VALOR
                                    }
                                }
                            }
                        }
                    };
                }
            }

            Claro.Web.Logging.Info(ovalidarCoberturaMessageRequest.Audit.Transaction, ovalidarCoberturaMessageRequest.Audit.Session, "Fin Método : validarCoberturaDireccion");
            return ovalidarCoberturaMessageResponse;
        }

        public static List<ObtenerTipoTecnologia> obtenerTipoTecnologia(string strIdSession, string strTransaction, string strParamGrupo)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "obtenerTipoTecnologia", strParamGrupo);

            List<ObtenerTipoTecnologia> listObtenerTipoTecnologia = new List<ObtenerTipoTecnologia>();
            DbParameter[] parameters = {
                                           new DbParameter("P_PARAN_GRUPO", DbType.Int64,ParameterDirection.Input, strParamGrupo),
                                           new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
                                       
                                       };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SECSS_CON_PARAMETRO_GP, parameters, (IDataReader reader) =>
            {
                ObtenerTipoTecnologia entity = null;

                while (reader.Read())
                {
                    entity = new ObtenerTipoTecnologia();
                    entity.PARAN_CODIGO = Claro.Utils.CheckInt64(reader["PARAN_CODIGO"]);
                    entity.PARAV_DESCRIPCION = Claro.Utils.CheckStr(reader["PARAV_DESCRIPCION"]);
                    entity.PARAV_VALOR = Claro.Utils.CheckStr(reader["PARAV_VALOR"]);
                    entity.PARAV_VALOR1 = Claro.Utils.CheckStr(reader["PARAV_VALOR1"]);
                    entity.PARAN_FLAG_SISTEMA = Claro.Utils.CheckStr(reader["PARAN_FLAG_SISTEMA"]);
                    listObtenerTipoTecnologia.Add(entity);
                }
            });
            return listObtenerTipoTecnologia;
        }
    }
}
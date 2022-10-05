using System;
using Claro.Data;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Claro.SIACU.Entity.IFI.Fixed;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Fixed.GetComprarPaquetes;
using Claro.SIACU.Entity.IFI.Fixed.GetPCRFConsultation;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultarClaroPuntos;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultarPaqDisponibles;
using PCRFConnectorLTE = Claro.SIACU.ProxyService.IFI.SIACFixed.PCRFConnectorLTE;

namespace Claro.SIACU.Data.IFI.Fixed
{
    public class PackagePurchaseService
    {

        public static ConsultarClaroPuntosResponse ConsultarClaroPuntos(ConsultarClaroPuntosRequest objRequest, RestConsultarClaroPuntosRequest oRestRequest)
        {

            ConsultarClaroPuntosResponse objResponse = null;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ConsultarClaroPuntos: " + JsonConvert.SerializeObject(objRequest));
            try
            {
                Hashtable objHashtable = new Hashtable();
                objHashtable.Add("tipoConsulta",oRestRequest.MessageRequest.Body.tipoConsulta);
                objHashtable.Add("tipoDocumento", oRestRequest.MessageRequest.Body.tipoDocumento);
                objHashtable.Add("numeroDocumento", oRestRequest.MessageRequest.Body.numeroDocumento);
                objHashtable.Add("bolsa",  oRestRequest.MessageRequest.Body.bolsa);
                objHashtable.Add("tipoPuntos", oRestRequest.MessageRequest.Body.tipoPuntos);
                objResponse = RestService.GetInvoque<ConsultarClaroPuntosResponse>(Configuration.RestServiceConfiguration.ConsultarClaroPuntos_DP, objRequest.Audit, objHashtable, null);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<ConsultarClaroPuntosResponse>(result);

            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ConsultarClaroPuntos: " + JsonConvert.SerializeObject(objResponse));

            return objResponse;
        }

        public static ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(ConsultarPaqDisponiblesRequest objRequest, RestConsultarPaqDisponiblesRequest oRestRequest)
        {

            ConsultarPaqDisponiblesResponse objResponse = null;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ConsultarPaqDisponibles: " + JsonConvert.SerializeObject(objRequest));
            try
            {

                objResponse = RestService.PostInvoque<ConsultarPaqDisponiblesResponse>(Configuration.RestServiceConfiguration.ConsultarPaqDisponibles_DP, objRequest.Audit, oRestRequest, true);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<ConsultarPaqDisponiblesResponse>(result);

            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ConsultarPaqDisponibles: " + JsonConvert.SerializeObject(objResponse));

            return objResponse;
        }

        

        public static ComprarPaquetesBodyResponse ComprarPaquetesRest(ComprarPaquetesMessageRequest objRequest)
        {

            ComprarPaquetesBodyResponse objResponse = null;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ComprarPaquetes: " + JsonConvert.SerializeObject(objRequest));
            try
            {
                //CAYCHO
                Hashtable objHashtable = new Hashtable();
                objHashtable.Add("idTransaccion", objRequest.Audit.Transaction);
                objHashtable.Add("msgid", objRequest.Audit.Transaction);
                objHashtable.Add("userId", objRequest.Audit.UserName);
                objHashtable.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                objHashtable.Add("idAplicacion", objRequest.Audit.ApplicationName);
                objResponse = RestService.PostInvoqueSDP<ComprarPaquetesBodyResponse>(Configuration.RestServiceConfiguration.ComprarPaquetes_DP, objHashtable, objRequest.ComprarPaquetesRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<ComprarPaquetesBodyResponse>(result);

            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ComprarPaquetes: " + JsonConvert.SerializeObject(objResponse));

            return objResponse;
        }
        public static PCRFConnectorResponse ConsultarPCRFDegradacion(PCRFConnectorRequest objRequest)
        {
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio Método - ConsultarPCRFDegradacion");
            PCRFConnectorLTE.consultarResponse objResponse = new PCRFConnectorLTE.consultarResponse();
            PCRFConnectorResponse objPCRFSuscriberQuota = null;
            string strAccion = KEY.AppSettings("str_ConsultaSucr1");
            try
            {

                PCRFConnectorLTE.listaParametrosParametro[] lstParamParam = new PCRFConnectorLTE.listaParametrosParametro[1];
                PCRFConnectorLTE.listaParametros[] lstParam = new PCRFConnectorLTE.listaParametros[1];

                PCRFConnectorLTE.listaParametrosParametro listaParamParam = new PCRFConnectorLTE.listaParametrosParametro()
                {
                    campo = KEY.AppSettings("str_campoPCRF"),
                    valor = objRequest.strLinea,
                };

                lstParamParam[0] = listaParamParam;

                PCRFConnectorLTE.listaParametros listaParam = new PCRFConnectorLTE.listaParametros()
                {
                    nombreLista = KEY.AppSettings("str_nombreListaPCRF"),
                    parametro = lstParamParam,
                    subListaParametros = new PCRFConnectorLTE.subListaParametros[0],
                };

                lstParam[0] = listaParam;

                PCRFConnectorLTE.consultarRequest objConnectorRequest = new PCRFConnectorLTE.consultarRequest()
                {
                    auditRequest = new PCRFConnectorLTE.parametrosAuditRequest()
                    {
                        idTransaccion = objRequest.Audit.Transaction,
                        ipAplicacion = objRequest.Audit.IPAddress,
                        nombreAplicacion = objRequest.Audit.ApplicationName,
                        usuarioAplicacion = objRequest.Audit.UserName,
                    },

                    accionRequest = new PCRFConnectorLTE.accionType()
                    {
                        idAccion = strAccion,
                        listaParametros = lstParam,
                    },
                };
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedPCRFConnector, () =>
                {
                    return ServiceConfiguration.SiacFixedPCRFConnector.consultar(objConnectorRequest);
                });
                objPCRFSuscriberQuota = new PCRFConnectorResponse();

                if (objResponse != null && objResponse.auditResponse.codRespuesta == Constants.ZeroNumber)
                {
                    objPCRFSuscriberQuota.listaSuscriberQuota = new List<SuscriberQuota>();
                    objPCRFSuscriberQuota.codRespuesta = objResponse.auditResponse.codRespuesta;
                    objPCRFSuscriberQuota.msjRespuesta = objResponse.auditResponse.msjRespuesta;
                    if (objResponse.accionResponse.idAccion == strAccion)
                    {

                        PCRFConnectorLTE.listaParametros[] lstResponse = objResponse.accionResponse.listaParametros;

                        if (lstResponse[0].parametro != null)
                        {
                            var objSuscriberQuotaParametro = new SuscriberQuota();

                            for (int j = 0; j < lstResponse[0].parametro.Length; j++)
                            {

                                if (lstResponse[0].parametro[j].campo.Equals("QTATIMESTAMP"))
                                {
                                    objSuscriberQuotaParametro.QTATIMESTAMP = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTARSTDATETIME"))
                                {
                                    objSuscriberQuotaParametro.QTARSTDATETIME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTALASTRSTDATETIME"))
                                {
                                    objSuscriberQuotaParametro.QTALASTRSTDATETIME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTASTATUS"))
                                {
                                    objSuscriberQuotaParametro.QTASTATUS = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTACONSUMPTION"))
                                {
                                    objSuscriberQuotaParametro.QTACONSUMPTION = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTABALANCE"))
                                {
                                    objSuscriberQuotaParametro.QTABALANCE = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTAVALUE"))
                                {
                                    objSuscriberQuotaParametro.QTAVALUE = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTANAME"))
                                {
                                    objSuscriberQuotaParametro.QTANAME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("SRVNAME"))
                                {
                                    objSuscriberQuotaParametro.SRVNAME = lstResponse[0].parametro[j].valor;
                                }

                            }

                            objPCRFSuscriberQuota.listaSuscriberQuota.Add(objSuscriberQuotaParametro);

                        }

                        if (lstResponse[0].subListaParametros != null) 
                        {
                            for (int j = 0; j < lstResponse[0].subListaParametros.Length; j++)
                            {
                                var objSuscriberQuotasubLista = new SuscriberQuota();
                                for (int i = 0; i < lstResponse[0].subListaParametros[j].parametro.Length; i++)
                                {
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTATIMESTAMP"))
                                    {
                                        objSuscriberQuotasubLista.QTATIMESTAMP = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTARSTDATETIME"))
                                    {
                                        objSuscriberQuotasubLista.QTARSTDATETIME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTALASTRSTDATETIME"))
                                    {
                                        objSuscriberQuotasubLista.QTALASTRSTDATETIME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTASTATUS"))
                                    {
                                        objSuscriberQuotasubLista.QTASTATUS = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTACONSUMPTION"))
                                    {
                                        objSuscriberQuotasubLista.QTACONSUMPTION = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTABALANCE"))
                                    {
                                        objSuscriberQuotasubLista.QTABALANCE = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTAVALUE"))
                                    {
                                        objSuscriberQuotasubLista.QTAVALUE = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTANAME"))
                                    {
                                        objSuscriberQuotasubLista.QTANAME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("SRVNAME"))
                                    {
                                        objSuscriberQuotasubLista.SRVNAME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }

                            }

                                objPCRFSuscriberQuota.listaSuscriberQuota.Add(objSuscriberQuotasubLista);

                            }

                        }

                    }
                }
                else
                {
                    objPCRFSuscriberQuota.msjRespuesta = "Error al cargar PCRF";
                    Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFDegradacion" + objPCRFSuscriberQuota.msjRespuesta);
                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFDegradacion: " + ex.Message);
            }
            return objPCRFSuscriberQuota;
        }

         public static List<Entity.IFI.Common.Client> GetDatosporNroDocumentos(string strIdSession,string strTransaction, string strTipDoc, string strDocumento, string strEstado)
        {

            List<Entity.IFI.Common.Client> ListDatos = new List<Entity.IFI.Common.Client>();
            try
            {
                DbParameter[] parameters ={   
                                            new DbParameter("PI_TIPODOCUMENTO", DbType.Int32,ParameterDirection.Input,strTipDoc),
                                            new DbParameter("PI_DOCUMENTO", DbType.String,ParameterDirection.Input,strDocumento),
                                            new DbParameter("PI_STATUS", DbType.String,ParameterDirection.Input,strEstado),
                                            new DbParameter("PO_CURSOR", DbType.Object,ParameterDirection.Output),
                                            new DbParameter("PO_CODERROR", DbType.Int32,ParameterDirection.Output),
                                            new DbParameter("PO_MSGERROR", DbType.String,200,ParameterDirection.Output)
                };


                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCSSS_OBT_DATOS, parameters, (IDataReader reader) =>
                    {

                        while (reader.Read())
                        {
                            ListDatos.Add(new Entity.IFI.Common.Client()
                            {
                                CONTRATO_ID = (reader["CONTRATO"]).ToString(),
                                RESPONSABLE_PAGO = (reader["DESCRIPCION_PLAN"]).ToString(),
                                // FECHA_ACT =  Convert.ToDateTime(reader["FECHA_ALTA"]),
                                CARGO = (reader["CARGO_TOTAL"]).ToString(),
                                ESTADO_CUENTA = (reader["STATUS"]).ToString(),
                                CICLO_FACTURACION = (reader["CICLO"]).ToString(),
                                CUSTOMER_ID = (reader["CUSTOMER"]).ToString(),
                                S_APELLIDOS = (reader["TITULAR"]).ToString(),
                                TELEFONO = (reader["NUMERO"]).ToString(),
                                TIPO_CLIENTE = (reader["TIPO_CLIENTE"]).ToString(),

                            });
                        }

                    });

            }
            catch (Exception ex)
            {
                ListDatos = null;
                Web.Logging.Error(strIdSession, strTransaction, "Error  - GetDatosporNroDocumentos: " + ex.Message);
            }

            return ListDatos;
        }
    }
}

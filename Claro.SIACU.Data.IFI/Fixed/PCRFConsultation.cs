using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Claro.Data;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.IFI.Fixed.GetPCRFConsultation;
using Claro.SIACU.Entity.IFI.Fixed;
using Claro.SIACU.Data.IFI.Configuration;
using PCRFConnectorLTE = Claro.SIACU.ProxyService.IFI.SIACFixed.PCRFConnectorLTE;
using Newtonsoft.Json;//INI - RF-04 - RF-05 Evalenzs
using Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic;
using System.Collections;///INI - RF-04 - RF-05 Evalenzs

namespace Claro.SIACU.Data.IFI.Fixed
{
    public class PCRFConsultation
    {
        /// <summary>
        /// ObtenerTelefonosClienteLTE
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ObtenerTelefonosClienteLTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static PCRFConnectorResponse ObtenerTelefonosClienteLTE(PCRFConnectorRequest objRequest)
        {
            PCRFConnectorResponse objResponse = null;
            DbParameter[] parameters = 
            {   
                new DbParameter("p_co_id", DbType.Int64,22,ParameterDirection.Input, objRequest.strAccountId),
                new DbParameter("p_cursor", DbType.Object,ParameterDirection.Output),
                new DbParameter("p_resultado", DbType.Int64,22,ParameterDirection.Output),
                new DbParameter("p_msgerr", DbType.String,300,ParameterDirection.Output)};


            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_LISTA_TELEFONO_LTE, parameters, (IDataReader reader) =>
                        {
                            objResponse = new PCRFConnectorResponse();

                            while (reader.Read())
                            {
                                var item = new GenericItem
                                {
                                    Descripcion = reader["DN_NUM"].ToString(),
                                };
                               objResponse.strTelefonoLTE = item.Descripcion;
                            }
                        });
                });
              
            }
            catch(Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        /// <summary>
        /// consultar PCRF Cuotas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ConsultarPCRFSuscriber_Quota</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static PCRFConnectorResponse ConsultarPCRFSuscriber_Quota(PCRFConnectorRequest objRequest)
        {
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio Método - ConsultarPCRFSuscriber_Quota");
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
                    valor = objRequest.strLinea
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
                        usuarioAplicacion = objRequest.Audit.UserName
                    },

                    accionRequest = new PCRFConnectorLTE.accionType()
                    {
                        idAccion = strAccion,
                        listaParametros = lstParam
                    },
                };
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedPCRFConnector, () =>
                {
                    return ServiceConfiguration.SiacFixedPCRFConnector.consultar(objConnectorRequest);
                });
                objPCRFSuscriberQuota = new PCRFConnectorResponse();

                objPCRFSuscriberQuota.bBono = false;
                if(objResponse != null && objResponse.auditResponse.codRespuesta == Constants.ZeroNumber)
                {
                    objPCRFSuscriberQuota.listSuscriberQuota = new SuscriberQuota();
                    objPCRFSuscriberQuota.strMensajeQuota = objResponse.auditResponse.msjRespuesta;
                    if(objResponse.accionResponse.idAccion == strAccion)
                    {
                        string QTACONSUMPTIONTemp = string.Empty;
                        PCRFConnectorLTE.listaParametros[] lstResponse = objResponse.accionResponse.listaParametros;

                        if (lstResponse[0].parametro != null) {
                            for (int j = 0; j < lstResponse[0].parametro.Length; j++)
                            {
                                if (lstResponse[0].parametro[j].campo.Equals("QTATIMESTAMP"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTATIMESTAMP = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTARSTDATETIME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTARSTDATETIME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTALASTRSTDATETIME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTALASTRSTDATETIME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTASTATUS"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTASTATUS = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTACONSUMPTION"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION = lstResponse[0].parametro[j].valor;

                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTABALANCE"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTABALANCE = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTAVALUE"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTAVALUE = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTANAME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTANAME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("SRVNAME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.SRVNAME = lstResponse[0].parametro[j].valor;


                                    /**/
                                    string strSRVNAME = lstResponse[0].parametro[j].valor.Replace("S_RTDD_", "");
                                    int temp;
                                    if (!int.TryParse(strSRVNAME, out temp))
                                    {
                                        objPCRFSuscriberQuota.bBono = true;
                                        QTACONSUMPTIONTemp = objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION;
                                    }
                                }
                            }
                        }

                        if (lstResponse[0].subListaParametros != null)
                        {

                            foreach (var item in lstResponse[0].subListaParametros)
                            {
                                for (int j = 0; j < item.parametro.Length; j++)
                                {
                                    if (item.parametro[j].campo.Equals("QTATIMESTAMP"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTATIMESTAMP = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("QTARSTDATETIME"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTARSTDATETIME = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("QTALASTRSTDATETIME"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTALASTRSTDATETIME = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("QTASTATUS"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTASTATUS = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("QTACONSUMPTION"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION = item.parametro[j].valor;

                                    }
                                    if (item.parametro[j].campo.Equals("QTABALANCE"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTABALANCE = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("QTAVALUE"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTAVALUE = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("QTANAME"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.QTANAME = item.parametro[j].valor;
                                    }
                                    if (item.parametro[j].campo.Equals("SRVNAME"))
                                    {
                                        objPCRFSuscriberQuota.listSuscriberQuota.SRVNAME = item.parametro[j].valor;


                                        /**/
                                        string strSRVNAME = item.parametro[j].valor.Replace("S_RTDD_", "");
                                        int temp;
                                        if (!int.TryParse(strSRVNAME, out temp))
                                        {
                                            objPCRFSuscriberQuota.bBono = true;
                                            QTACONSUMPTIONTemp = objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION;
                                        }
                                    }
                                }
                            }
                            for (int j = 0; j < lstResponse[0].subListaParametros.Length; j++)
                            {
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTATIMESTAMP"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTATIMESTAMP = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTARSTDATETIME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTARSTDATETIME = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTALASTRSTDATETIME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTALASTRSTDATETIME = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTASTATUS"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTASTATUS = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTACONSUMPTION"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION = lstResponse[0].subListaParametros[j].parametro[j].valor;

                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTABALANCE"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTABALANCE = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTAVALUE"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTAVALUE = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("QTANAME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.QTANAME = lstResponse[0].subListaParametros[j].parametro[j].valor;
                                }
                                if (lstResponse[0].subListaParametros[j].parametro[j].campo.Equals("SRVNAME"))
                                {
                                    objPCRFSuscriberQuota.listSuscriberQuota.SRVNAME = lstResponse[0].subListaParametros[j].parametro[j].valor;


                                    /**/
                                    string strSRVNAME = lstResponse[0].subListaParametros[j].parametro[j].valor.Replace("S_RTDD_", "");
                                    int temp;
                                    if (!int.TryParse(strSRVNAME, out temp))
                                    {
                                        objPCRFSuscriberQuota.bBono = true;
                                        QTACONSUMPTIONTemp = objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION;
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(QTACONSUMPTIONTemp))
                            objPCRFSuscriberQuota.listSuscriberQuota.QTACONSUMPTION = QTACONSUMPTIONTemp;
                    }
                }
                else
                {
                    objPCRFSuscriberQuota.strMensajeQuota = "Error al cargar PCRF";
                    Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFSuscriber_Quota" + objPCRFSuscriberQuota.strMensajeQuota);
                }

            }
            catch(Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFSuscriber_Quota: " + ex.Message);
            }
            return objPCRFSuscriberQuota;
        }
        /// <summary>
        /// Consultar PCRF
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ConsultarPCRFSuscriber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static PCRFConnectorResponse ConsultarPCRFSuscriber(PCRFConnectorRequest objRequest)
        {
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio Método - ConsultarPCRFSuscriber");
            PCRFConnectorLTE.consultarResponse objResponse = new PCRFConnectorLTE.consultarResponse();
            string strAccion = KEY.AppSettings("str_ConsultaSucr2");
            PCRFConnectorResponse objPCRFSuscriber = null;
            try
            {
                PCRFConnectorLTE.listaParametrosParametro[] lstParamParam = new PCRFConnectorLTE.listaParametrosParametro[1];
                PCRFConnectorLTE.listaParametros[] lstParam = new PCRFConnectorLTE.listaParametros[1];

                PCRFConnectorLTE.listaParametrosParametro listaParamParam = new PCRFConnectorLTE.listaParametrosParametro()
                {
                    campo = KEY.AppSettings("str_campoPCRF"),
                    valor = objRequest.strLinea
                };

                lstParamParam[0] = listaParamParam;

                PCRFConnectorLTE.listaParametros listaParam = new PCRFConnectorLTE.listaParametros()
                {
                    nombreLista = KEY.AppSettings("str_nombreListaPCRF"),
                    parametro = lstParamParam,
                    subListaParametros = new PCRFConnectorLTE.subListaParametros[0]
                };

                lstParam[0] = listaParam;

                PCRFConnectorLTE.consultarRequest objConnectorRequest = new PCRFConnectorLTE.consultarRequest()
                {
                    auditRequest = new PCRFConnectorLTE.parametrosAuditRequest()
                    {
                        idTransaccion = objRequest.Audit.Session,
                        ipAplicacion = objRequest.Audit.Transaction,
                        nombreAplicacion = objRequest.Audit.ApplicationName,
                        usuarioAplicacion = objRequest.Audit.UserName
                    },

                    accionRequest = new PCRFConnectorLTE.accionType()
                    {
                        idAccion = strAccion,
                        listaParametros = lstParam
                    },
                };

                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedPCRFConnector, () =>
                {
                    return ServiceConfiguration.SiacFixedPCRFConnector.consultar(objConnectorRequest);
                });

                objPCRFSuscriber = new PCRFConnectorResponse();
                if (objResponse != null && objResponse.auditResponse.codRespuesta == Constants.ZeroNumber)
                {
                    objPCRFSuscriber.listSuscriber = new Suscriber();
                    objPCRFSuscriber.strMensaje = objResponse.auditResponse.msjRespuesta;
                    if(objResponse.accionResponse.idAccion == strAccion)
                    {
                        PCRFConnectorLTE.listaParametros[] lstResponse = objResponse.accionResponse.listaParametros;
                        for (int j = 0; j < lstResponse[0].parametro.Length; j++)
                        {
                            if (lstResponse[0].parametro[j].campo.Equals("USRIMSI"))
                            {
                                objPCRFSuscriber.listSuscriber.USRIMSI = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRHOMESRVZONE"))
                            {
                                objPCRFSuscriber.listSuscriber.USRHOMESRVZONE = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRBILLCYCLEDATE"))
                            {
                                objPCRFSuscriber.listSuscriber.USRBILLCYCLEDATE = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRSTATION"))
                            {
                                objPCRFSuscriber.listSuscriber.USRSTATION = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRCATEGORY"))
                            {
                                objPCRFSuscriber.listSuscriber.USRCATEGORY = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRPAIDTYPE"))
                            {
                                objPCRFSuscriber.listSuscriber.USRPAIDTYPE = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRSTATE"))
                            {
                                objPCRFSuscriber.listSuscriber.USRSTATE = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRMSISDN"))
                            {
                                objPCRFSuscriber.listSuscriber.USRMSISDN = lstResponse[0].parametro[j].valor;
                            }
                            if (lstResponse[0].parametro[j].campo.Equals("USRIDENTIFIER"))
                            {
                                objPCRFSuscriber.listSuscriber.USRIDENTIFIER = lstResponse[0].parametro[j].valor;
                            }
                        }
                    }
                }
                else
                {
                    objPCRFSuscriber.strMensaje = "Error PCRF";
                    Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFSuscriber" + objPCRFSuscriber.strMensaje);
                }
            }
            catch(Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFSuscriber: " + ex.Message);
            }
            return objPCRFSuscriber;
        }
        
        //INI - RF-04 - RF-05 Evalenzs
        public static PCRFPaquetesAdicBodyResponse ConsultarPCRFPaquetesAdic(PCRFPaquetesAdicRequest objRequest, RestPCRFPaquetesAdicMessageRequest oRestRequest)
        {

            PCRFPaquetesAdicBodyResponse objResponse = null;

                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ConsultarPCRFPaquetesAdic: " + JsonConvert.SerializeObject(objRequest));
                try
                {
                    Hashtable objHashtable = new Hashtable();
                    objHashtable.Add("idTransaccion", objRequest.Audit.Transaction);
                    objHashtable.Add("msgid", objRequest.Audit.Transaction);
                    objHashtable.Add("userId", objRequest.Audit.UserName);
                    objHashtable.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                    objHashtable.Add("idAplicacion", objRequest.Audit.ApplicationName);
                    objResponse = RestService.PostInvoqueSDP<PCRFPaquetesAdicBodyResponse>(Configuration.RestServiceConfiguration.ConsultarPCRFPaquetesAdic_DP, objHashtable, oRestRequest.Body);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                    string sep = " - ";
                    int posResponse = ex.Message.IndexOf(sep);
                    string result = ex.Message.Substring(posResponse + sep.Length);
                    objResponse = JsonConvert.DeserializeObject<PCRFPaquetesAdicBodyResponse>(result);
                    
                }
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ConsultarPCRFPaquetesAdic: " + JsonConvert.SerializeObject(objResponse));

                return objResponse;
        }
    }
}

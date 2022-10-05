using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid.ServiceLock;
using Claro.SIACU.ProxyService.IFI.SIACU.IFI.LockLine;
using Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment;
using Claro.Transversal.ProxyService.ConsultaClaves;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class ServiceLock
    {
        /// <summary>
        /// obtiene respuestas de seguridad
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTypeCustomer"></param>
        /// <param name="strGroupCustomer"></param>
        /// <returns></returns>
        ///  <remarks>GetAnswersSecurity</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<ItemGeneric> GetAnswersSecurity(string strIdSession, string strTransaction, string strTypeCustomer, string strGroupCustomer)
        {
            try
            {
                DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("P_TIPOCLIENTE", DbType.String,100,ParameterDirection.Input,strTypeCustomer),			
									                new DbParameter("P_GRUPOCLIENTE", DbType.String,100,ParameterDirection.Input,strGroupCustomer),		
												   new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
												  
            
                 };

                List<ItemGeneric> listItem = null;
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACSS_RESPUESTAS_SEGURIDAD, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ItemGeneric>();

                    while (reader.Read())
                    {
                        listItem.Add(new ItemGeneric()
                        {

                            Code = Claro.Convert.ToString(reader["RESSN_OBJIDPREGUNTA"]),
                            Description = Claro.Convert.ToString(reader["RESSV_DESCRIPCIONRESPUESTA"])
                        });
                    }

                });
                return listItem;
            }
            catch (Exception e)
            {
                Web.Logging.Info(strIdSession, strTransaction, string.Format("Error() {0}", e.Message));
                throw e;
            }


        }
        /// <summary>
        /// obtiene preguntas de seguridad
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTypeCustomer"></param>
        /// <param name="strGroupCustomer"></param>
        /// <returns></returns>
        ///  <remarks>GetQuestionsSecurity</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<ItemGeneric> GetQuestionsSecurity(string strIdSession, string strTransaction, string strTypeCustomer, string strGroupCustomer)
        {

            try
            {
                DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("P_TIPOCLIENTE", DbType.String,100,ParameterDirection.Input,strTypeCustomer),			
									                new DbParameter("P_GRUPOCLIENTE", DbType.String,100,ParameterDirection.Input,strGroupCustomer),		
												   new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
												  
            
                 };

                List<ItemGeneric> listItem = null;
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACSS_PREGUNTAS_SEGURIDAD, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ItemGeneric>();

                    while (reader.Read())
                    {
                        listItem.Add(new ItemGeneric()
                        {

                            Code = Claro.Convert.ToString(reader["PRESN_OBJID"]),
                            Description = Claro.Convert.ToString(reader["PRESV_DESCRIPCIONPREGUNTA"])
                        });
                    }

                });
                return listItem;
            }
            catch (Exception e)
            {
                Web.Logging.Info(strIdSession, strTransaction, string.Format("Error() {0}", e.Message));
                throw e;
            }


        }
        /// <summary>
        /// bloquea linea
        /// </summary>
        /// <param name="objServiceLockRequest"></param>
        /// <returns></returns>
        /// <remarks>GetServiceLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>

        public static bool GetServiceLock(ServiceLockRequest objServiceLockRequest)
        {
            parametrosTypeObjetoOpcional[] listaRequestOpcional = new parametrosTypeObjetoOpcional[1];

            listaRequestOpcional[0] = new parametrosTypeObjetoOpcional() { campo = "", valor = "" };
            Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, capa Data");
            try
            {
                HeaderRequestType objHeaderRequest = new HeaderRequestType()
                {
                    country = Claro.ConfigurationManager.AppSettings("consCountry"),
                    language = Claro.ConfigurationManager.AppSettings("consLanguage"),
                    consumer = Claro.ConfigurationManager.AppSettings("NombreAplicacion"),
                    system = string.Empty,
                    modulo = string.Empty,
                    pid = objServiceLockRequest.Audit.Transaction,
                    userId = objServiceLockRequest.Audit.UserName,
                    dispositivo = objServiceLockRequest.Audit.IPAddress,
                    wsIp = objServiceLockRequest.Audit.IPAddress,
                    operation = "bloqueoLinea",
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    msgType = "Request",
                    VarArg = new ArgType[1]
                            {
                                new ArgType()
                                {
                                    key = String.Empty,
                                    value = String.Empty,
                                }
                            },

                };
                Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, Request HeaderRequestType ");

                bloqueoLineaRequest objbloqueoLineaRequest = new bloqueoLineaRequest()
                {
                    auditRequest = new auditRequestType()
                    {
                        idTransaccion = objServiceLockRequest.Audit.Transaction,
                        ipAplicacion = objServiceLockRequest.Audit.IPAddress,
                        nombreAplicacion = objServiceLockRequest.Audit.ApplicationName,
                        usuarioAplicacion = objServiceLockRequest.Audit.UserName


                    },
                    bloqueo = new bloqueoType()
                    {
                        coId = objServiceLockRequest.coId,
                        reason = Claro.ConfigurationManager.AppSettings("strReasonBloqIFI"),
                        ticklerCode = ((objServiceLockRequest.ticklerCode != null) && (objServiceLockRequest.ticklerCode.Length >= 8)) ? objServiceLockRequest.ticklerCode.Substring(0, 8) : "",
                        ticklerDes = "",
                        userName = objServiceLockRequest.Audit.UserName
                    },

                    listaRequestOpcional = listaRequestOpcional
                };
                Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, Request bloqueoLineaRequest ");

                if (objServiceLockRequest.ticklerCode != null)
                {
                    Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, BLOQ_ROB1 ");

                    if (objServiceLockRequest.ticklerCode == "BLOQ_ROB1")
                    {
                        objbloqueoLineaRequest.bloqueo.ticklerDes = "Bloqueo por Robo";
                    }
                    Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, BLOQ_ROB2 ");

                    if (objServiceLockRequest.ticklerCode == "BLOQ_ROB2")
                    {
                        objbloqueoLineaRequest.bloqueo.ticklerDes = "Bloqueo por Hurto";
                    }
                    Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, BLOQ_PER ");

                    if (objServiceLockRequest.ticklerCode == "BLOQ_PER")
                    {
                        objbloqueoLineaRequest.bloqueo.ticklerDes = "Bloqueo por Perdida";
                    }
                }
                Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, paso validacion objServiceLockRequest.ticklerCode != null ");




                bloqueoLineaResponse objbloqueoLineaResponse = new bloqueoLineaResponse();
                HeaderResponseType objHeaderResponseType;

                using (new System.ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.strServiceLock.InnerChannel))
                {
                    Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, header credenciales ");

                    System.ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add
                  (
                      new Claro.Entity.SecurityHeader(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.strUser, objServiceLockRequest.strPass)
                  );
                    Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, datapower bloqueoLinea ");
                    objHeaderResponseType = Web.Logging.ExecuteMethod<HeaderResponseType>(objServiceLockRequest.Audit.Session, objServiceLockRequest.Audit.Transaction, () =>
                    {
                        return ServiceConfiguration.strServiceLock.bloqueoLinea(objHeaderRequest, objbloqueoLineaRequest, out objbloqueoLineaResponse);
                    });
                }
                Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, paso datapower ");

                if (objHeaderResponseType.Status.code == Claro.Constants.NumberZeroString)
                {
                    Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, paso validacion objHeaderResponseType.Status.code ==0 ");

                    if (objbloqueoLineaResponse.auditResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
                    {
                        Claro.Web.Logging.Info(objServiceLockRequest.Audit.Transaction, objServiceLockRequest.Audit.Transaction, "Entro metodo GetServiceLock, paso validacion objbloqueoLineaResponse.auditResponse.codigoRespuesta==0 ");

                        return true;
                    }

                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServiceLockRequest.Audit.Session, objServiceLockRequest.Audit.Transaction, ex.Message);

                return false;
            }

            return false;

        }
        /// <summary>
        /// obtiene actual bloqueo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="codId"></param>
        /// <returns></returns>
        ///  <remarks>CurrentBlock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Annotation> CurrentBlock(string strIdSession, string strTransaction, string codId)
        {
            string status;
            string tipo;
            List<Annotation> lstAnnotation = new List<Annotation>();
            try
            {
                DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("p_co_id", DbType.Int64,65,ParameterDirection.Input,codId),											            
												   new DbParameter("P_Cursor", DbType.Object,ParameterDirection.Output)
												  
            
                 };

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_IFI_LOCK_CURRENTS, parameters, (IDataReader reader) =>
                {


                    while (reader.Read())
                    {
                        status = Claro.Convert.ToString(reader["tickler_status"]);


                        if (status == "OPEN")
                        {
                            Annotation item = new Annotation();
                            item._Nro_Tickler = Claro.Convert.ToString(reader["tickler_number"]);
                            item._sFecha = Claro.Convert.ToString(reader["created_date"]);
                            item._Fecha = Claro.Convert.ToDate(reader["created_date"]);
                            tipo = Claro.Convert.ToString(reader["tickler_code"]);
                            item._tipo = tipo;
                            switch (tipo)
                            {
                                case "BLOQ_ROB":
                                    item._desc_tipo = "Robo";
                                    break;
                                case "BLOQ_FRA":
                                    item._desc_tipo = "Fraude";
                                    break;
                                case "BLOQ_FIN":
                                    item._desc_tipo = "Financiamiento";
                                    break;
                                case "BLOQ_LC":
                                    item._desc_tipo = "Límite de Crédito";
                                    break;
                                case "BLOQ_COB":
                                    item._desc_tipo = "Cobranza";
                                    break;

                                case "BLOQ_S20":
                                    item._desc_tipo = "Bloqueo S20";
                                    break;
                                case "BLOQ_OSP":
                                    item._desc_tipo = "Bloqueo Osiptel";
                                    break;

                                default:
                                    item._desc_tipo = tipo;
                                    break;
                            }
                            item._Descripcion = Claro.Convert.ToString(reader["long_description"]);
                            item._Usuario_Seguimiento = Claro.Convert.ToString(reader["CREATED_BY"]);
                            item._Estado = "";
                            lstAnnotation.Add(item);
                        }

                    }

                });

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return lstAnnotation;
        }
        /// <summary>
        /// bloquea equipo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="Imei"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        ///  <remarks>GetEquipmentLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetEquipmentLock(string strIdSession, string strTransaction, string Imei, out string strMessage)
        {
            string result;
            BloqueoDesbloqueoIMEIResponse objBloqueoDesbloqueoIMEIResponse;
            BloqueoDesbloqueoIMEIRequest objBloqueoDesbloqueoIMEIRequest = new BloqueoDesbloqueoIMEIRequest()
            {
                IMEI = Imei
            };
            try
            {
                objBloqueoDesbloqueoIMEIResponse = Web.Logging.ExecuteMethod<BloqueoDesbloqueoIMEIResponse>(strIdSession, strTransaction, () =>
                {
                    return ServiceConfiguration.BLOQDESEQUIPO.bloqueoIMEI(objBloqueoDesbloqueoIMEIRequest);
                });
                strMessage = objBloqueoDesbloqueoIMEIResponse.mensaje;
                result = objBloqueoDesbloqueoIMEIResponse.resultado;
                if (result == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                strMessage = ex.Message;
            }

            return true;
        }


        /// <summary>
        /// Inserta bloqueo linea per
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="codeLock"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        ///  <remarks>InsertLockLinePer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertLockLinePer(string strIdSession, string strTransaction, Lock objLock, string codeLock, ref string rFlagInsercion, ref string rMsgText)
        {
            bool result = false;
            DbParameter[] parameters = 
            {
                

                new DbParameter("P_BLO_CUENTA", DbType.String,30, ParameterDirection.Input, objLock._BLO_CUENTA),
                new DbParameter("P_BLO_TELEFONO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TELEFONO),
                new DbParameter("P_BLO_COD_APLICA", DbType.String,5, ParameterDirection.Input, objLock._BLO_COD_APLICA),
                new DbParameter("P_BLO_USUARIO", DbType.String,7, ParameterDirection.Input, objLock._BLO_USUARIO),
                new DbParameter("P_BLO_AUTORIZA", DbType.String,7, ParameterDirection.Input, objLock._BLO_AUTORIZA),
                new DbParameter("P_BLO_ESTADO", DbType.String,20, ParameterDirection.Input, objLock._BLO_ESTADO),
                new DbParameter("P_BLO_TIPO", DbType.String,20, ParameterDirection.Input, objLock._BLO_TIPO),
                new DbParameter("P_BLO_CODIGO", DbType.String,1000, ParameterDirection.Input, codeLock),
                new DbParameter("P_FLAG_INSERT", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,255, ParameterDirection.Output)
            };

            try
            {

                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_INSERTAR_BLOQUEO_LINEA_PER, parameters);
                result = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                result = false;
            }
            finally
            {


                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return result;

        }
        /// <summary>
        /// inserta bloquea de equipo per
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="codeLock"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        ///  <remarks>InsertLockEquipmentPer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool InsertLockEquipmentPer(string strIdSession, string strTransaction, Lock objLock, string codeLock, ref string rFlagInsercion, ref string rMsgText)
        {
            bool result = false;
            DbParameter[] parameters = 
            {
                new DbParameter("P_BLO_CUENTA", DbType.String,30, ParameterDirection.Input, objLock._BLO_CUENTA),
                new DbParameter("P_BLO_TELEFONO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TELEFONO),
                new DbParameter("P_BLO_COD_APLICA", DbType.String,5, ParameterDirection.Input, objLock._BLO_COD_APLICA),
                new DbParameter("P_BLO_USUARIO", DbType.String,7, ParameterDirection.Input, objLock._BLO_USUARIO),
                new DbParameter("P_BLO_AUTORIZA", DbType.String,7, ParameterDirection.Input, ""),
                new DbParameter("P_BLO_ESTADO", DbType.String,20, ParameterDirection.Input, objLock._BLO_ESTADO),
                new DbParameter("P_BLO_IMEI", DbType.String,30, ParameterDirection.Input, objLock._BLO_IMEI),
                new DbParameter("P_BLO_CODIGO", DbType.String,1000, ParameterDirection.Input, objLock._BLO_CODIGO),
                new DbParameter("P_FLAG_INSERT", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,255, ParameterDirection.Output)

             
            };

            try
            {

                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_INSERTAR_BLOQUEO_EQUIPO_PER, parameters);
                result = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                result = false;
            }
            finally
            {


                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return result;

        }

        /// <summary>
        /// obtiene IMEIS
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strLine"></param>
        /// <returns></returns>
        ///  <remarks>GetImeis</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Terminal> GetImeis(string strIdSession, string strTransaction, string strLine)
        {

            List<Terminal> lstTerminal = new List<Terminal>();
            try
            {
                DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("P_CONSULTAS", DbType.Object,65,ParameterDirection.Output),	
										           new DbParameter("NroLinea", DbType.String,65,ParameterDirection.Input,strLine),	 
												   new DbParameter("Transaccion", DbType.Int32,ParameterDirection.Input,0)
												  
            
                 };
                //cambiar para produccion a cobs
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_OBTENERIMEI, parameters, (IDataReader reader) =>
                {


                    while (reader.Read())
                    {
                        lstTerminal.Add(
                        new Terminal()
                        {
                            _strCodigoBloqueo = Claro.Convert.ToString(reader["ID_TERMOVIMIENTO"]),
                            _strNumeroIMEI = Claro.Convert.ToString(reader["NUMERO_IMEI"]),
                            _strMarca = Claro.Convert.ToString(reader["TER_DESCRIPCION"]),
                            _strModelo = "",
                            _strEstado = Claro.Convert.ToString(reader["TER_ESTADO"]),
                            _strFechaMovimiento = Claro.Convert.ToString(reader["FECHA_MOVIMIENTO"]),
                            _strFechaRegistro = Claro.Convert.ToString(reader["FECHA_REGISTRO"]),
                            _strTipoMovimiento = Claro.Convert.ToString(reader["TIPO_MOTIVO"])

                        });


                    }

                });

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return lstTerminal;
        }
        //made13
        /// <summary>
        /// Eliminar bloqueo de linea
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="rMsgText"></param>
        /// <param name="rFlagDelete"></param>
        /// <returns></returns>
        ///  <remarks>DeleteLockLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool DeleteLockLine(string strIdSession, string strTransaction, Lock objLock, ref string rMsgText, ref string rFlagDelete)
        {
            bool result = false;
            DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("P_BLO_TELEFONO", DbType.String,30,ParameterDirection.Input,objLock._BLO_TELEFONO),	
		                                           new DbParameter("P_BLO_TIPO", DbType.String,20,ParameterDirection.Input,objLock._BLO_TIPO),											               
												   new DbParameter("P_FLAG_DELETE", DbType.Int32,ParameterDirection.Output),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)


									
            
                 };
            try
            {



                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_DELETE_LOCK_LINE, parameters);
                result = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                result = false;
            }
            finally
            {


                rFlagDelete = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return result;
        }

        /// <summary>
        /// elimina bloqueo de equipo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="rMsgText"></param>
        /// <param name="rFlagDelete"></param>
        /// <returns></returns>
        ///  <remarks>DeleteLockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool DeleteLockEquipment(string strIdSession, string strTransaction, Lock objLock, ref string rMsgText, ref string rFlagDelete)
        {
            bool result = false;
            DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("P_BLO_TELEFONO", DbType.String,30,ParameterDirection.Input,objLock._BLO_TELEFONO),	
		                                           new DbParameter("P_BLO_IMEI", DbType.String,20,ParameterDirection.Input,objLock._BLO_IMEI),											               
												   new DbParameter("P_FLAG_DELETE", DbType.Int32,ParameterDirection.Output),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)


									
            
                 };

            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_DELETE_INSERTA_EQUIPO, parameters);
                result = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                result = false;
            }
            finally
            {


                rFlagDelete = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

            }
            return result;
        }
    }
}

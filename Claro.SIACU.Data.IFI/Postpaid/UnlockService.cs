using System;
using System.Collections.Generic;
using Claro.SIACU.Data.IFI.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.ProxyService.IFI.SIACU.IFI.UnlockLine;
using Claro.SIACU.Entity.IFI.Postpaid.UnlockService;
using Claro.SIACU.ProxyService.IFI.SIACU.LockEquipment;
using Claro.Data;
using System.Data;
using Claro.SIACU.Entity.IFI.Postpaid;
namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class UnlockService
    {
        /// <summary>
        /// desbloquea la linea 
        /// </summary>
        /// <param name="objUnlockServiceRequest"></param>
        /// <returns></returns>
        ///  <remarks>GetUnlockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetUnlockService(UnlockServiceRequest objUnlockServiceRequest)
        {
            parametrosTypeObjetoOpcional[] listaRequestOpcional = new parametrosTypeObjetoOpcional[1];

            listaRequestOpcional[0] = new parametrosTypeObjetoOpcional() { campo = "", valor = "" };

            try
            {
                HeaderRequestType objHeaderRequest = new HeaderRequestType()
                {
                    country = Claro.ConfigurationManager.AppSettings("consCountry"),
                    language = Claro.ConfigurationManager.AppSettings("consLanguage"),
                    consumer = Claro.ConfigurationManager.AppSettings("NombreAplicacion"),
                    system = string.Empty,
                    modulo = string.Empty,
                    pid = objUnlockServiceRequest.Audit.Transaction,
                    userId = objUnlockServiceRequest.Audit.UserName,
                    dispositivo = objUnlockServiceRequest.Audit.IPAddress,
                    wsIp = objUnlockServiceRequest.Audit.IPAddress,//"172.17.26.44"
                    operation = "desbloqueoLinea",
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
                desbloqueoLineaRequest objdesbloqueoLineaRequest = new desbloqueoLineaRequest()
                {
                    auditRequest = new auditRequestType()
                    {
                        idTransaccion = objUnlockServiceRequest.Audit.Transaction,
                        ipAplicacion = objUnlockServiceRequest.Audit.IPAddress,
                        nombreAplicacion = objUnlockServiceRequest.Audit.ApplicationName,
                        usuarioAplicacion = objUnlockServiceRequest.Audit.UserName
                       

                    },
                    desbloqueo = new desbloqueoType()
                    {
                       

                        coId = objUnlockServiceRequest.coId,

                        ticklerCode = ((objUnlockServiceRequest.ticklerCode != null) && (objUnlockServiceRequest.ticklerCode.Length >= 8)) ? objUnlockServiceRequest.ticklerCode.Substring(0, 8) : "",

                        userName = objUnlockServiceRequest.Audit.UserName
                    },

                    listaRequestOpcional = listaRequestOpcional
                };




                desbloqueoLineaResponse objdesbloqueoLineaResponse = new desbloqueoLineaResponse();
                HeaderResponseType objHeaderResponseType;

                using (new System.ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.strServiceUnlock.InnerChannel))
                {
                    System.ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add
                  (
                      new Claro.Entity.SecurityHeader(objUnlockServiceRequest.Audit.Transaction, objUnlockServiceRequest.strUser, objUnlockServiceRequest.strPass)
                  );
                    objHeaderResponseType = Web.Logging.ExecuteMethod<HeaderResponseType>(objUnlockServiceRequest.Audit.Session, objUnlockServiceRequest.Audit.Transaction, () =>
                    {
                        return ServiceConfiguration.strServiceUnlock.desbloqueoLinea(objHeaderRequest, objdesbloqueoLineaRequest, out objdesbloqueoLineaResponse);
                    });
                }
                if (objHeaderResponseType.Status.code == Claro.Constants.NumberZeroString)
                {

                    Claro.Web.Logging.Info(objUnlockServiceRequest.Audit.Session, objUnlockServiceRequest.Audit.Transaction, "codigoRespuesta" + objdesbloqueoLineaResponse.auditResponse.codigoRespuesta);
                    Claro.Web.Logging.Info(objUnlockServiceRequest.Audit.Session, objUnlockServiceRequest.Audit.Transaction, "mensajeRespuesta" + objdesbloqueoLineaResponse.auditResponse.mensajeRespuesta);
                    if (objdesbloqueoLineaResponse.auditResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
                    {
                        return true;
                    }

                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objUnlockServiceRequest.Audit.Session, objUnlockServiceRequest.Audit.Transaction, ex.Message);

                return false;
            }

            return false;

        }
        /// <summary>
        /// desbloquea IMEI
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="Imei"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        ///  <remarks>GetEquipmentUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetEquipmentUnLock(string strIdSession, string strTransaction, string Imei, out string strMessage)
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
                    return ServiceConfiguration.BLOQDESEQUIPO.desbloqueoIMEI(objBloqueoDesbloqueoIMEIRequest);
                });
              

                strMessage = objBloqueoDesbloqueoIMEIResponse.mensaje;
                
                result = objBloqueoDesbloqueoIMEIResponse.resultado;
               
                if (result == "0")
                {
                   
                    return true;
                }
                else
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, "77777");
                    return false;

                }
             
            }
            catch (Exception ex)
            {
               
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                strMessage = ex.Message;
            }
           
            return false;
        }


        /// <summary>
        /// Actualiza desbloqueo de linea de codigo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="codeUnlock"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        ///  <remarks>UpdateUnlockLineRollback</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateUnlockLineCode(string strIdSession, string strTransaction, Lock objLock, string codeUnlock, ref string rFlagInsercion, ref string rMsgText)
        {
            bool result = false;
            DbParameter[] parameters = 
            {
                new DbParameter("P_BLO_TELEFONO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TELEFONO),
                new DbParameter("P_BLO_USUARIO", DbType.String,30, ParameterDirection.Input, objLock._BLO_USUARIO),
                new DbParameter("P_BLO_AUTORIZA", DbType.String,30, ParameterDirection.Input, objLock._BLO_AUTORIZA),
                new DbParameter("P_BLO_ESTADO", DbType.String,30, ParameterDirection.Input, objLock._BLO_ESTADO),
                new DbParameter("P_BLO_TIPO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TIPO),
                new DbParameter("P_BLO_CODIGODES", DbType.String,1000, ParameterDirection.Input, codeUnlock),
                new DbParameter("P_FLAG_UPDATE", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255,ParameterDirection.Output)
              

               
            };
            try
            {

                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_UPDATECODIGODESBLQ, parameters);
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
            if (rFlagInsercion == "OK")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;

        }
        /// <summary>
        /// Actualiza desbloqueo de equipo codigo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="codeUnlock"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        ///  <remarks>UpdateUnlockLineRollback</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateUnlockEquipmentCode(string strIdSession, string strTransaction, Lock objLock, string codeUnlock, ref string rFlagInsercion, ref string rMsgText)
        {
            bool result = false;
            DbParameter[] parameters = 
            {
                new DbParameter("P_BLO_TELEFONO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TELEFONO),
                new DbParameter("P_BLO_USUARIO", DbType.String,30, ParameterDirection.Input, objLock._BLO_USUARIO),
                new DbParameter("P_BLO_AUTORIZA", DbType.String,30, ParameterDirection.Input, objLock._BLO_AUTORIZA),
                new DbParameter("P_BLO_ESTADO", DbType.String,30, ParameterDirection.Input, objLock._BLO_ESTADO),
                new DbParameter("P_BLO_IMEI", DbType.String,30, ParameterDirection.Input, objLock._BLO_IMEI),
                new DbParameter("P_BLO_CODIGODES", DbType.String,1000, ParameterDirection.Input, codeUnlock),
                new DbParameter("P_FLAG_UPDATE", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255,ParameterDirection.Output)
              

               
            };

            try
            {

                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_UPDATECODIGODESBLQEqui, parameters);
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
            if (rFlagInsercion == "OK")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;

        }
        
        /// <summary>
        /// Actualiza desbloqueo de linea roolback
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        /// <remarks>UpdateUnlockLineRollback</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateUnlockLineRollback(string strIdSession, string strTransaction, Lock objLock, ref string rFlagInsercion, ref string rMsgText)
        {
            Web.Logging.Error(strIdSession, strTransaction, "ingreso UpdateUnlockLineRollback ");
            bool result = false;
            DbParameter[] parameters = 
            {
                new DbParameter("P_BLO_TELEFONO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TELEFONO),
                new DbParameter("P_BLO_USUARIO", DbType.String,30, ParameterDirection.Input),
                new DbParameter("P_BLO_AUTORIZA", DbType.String,30, ParameterDirection.Input),
                new DbParameter("P_BLO_ESTADO", DbType.String,30, ParameterDirection.Input, objLock._BLO_ESTADO),
                new DbParameter("P_BLO_TIPO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TIPO),
                new DbParameter("P_FLAG_UPDATE", DbType.Int32,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255,ParameterDirection.Output)
              

            };
                try
            {
                    
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_SP_UPDATECODIGODESBLQROLL, parameters);
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
            if (rFlagInsercion == "0")
            {
                 result = true;
            }
            else
            {
                   result = false;
            }
              return result;

        }
        /// <summary>
        /// Actualiza desbloqueo de equipo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objLock"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        ///  <remarks>UpdateUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateUnlockEquipment(string strIdSession, string strTransaction, Lock objLock, ref string rFlagInsercion, ref string rMsgText)
        {
            bool result = false;
            DbParameter[] parameters = 
            {
                new DbParameter("P_BLO_TELEFONO", DbType.String,30, ParameterDirection.Input, objLock._BLO_TELEFONO),
                new DbParameter("P_BLO_USUARIO", DbType.String,30, ParameterDirection.Input),
                new DbParameter("P_BLO_AUTORIZA", DbType.String,30, ParameterDirection.Input),
                new DbParameter("P_BLO_ESTADO", DbType.String,30, ParameterDirection.Input, objLock._BLO_ESTADO),
                new DbParameter("P_BLO_IMEI", DbType.String,30, ParameterDirection.Input, objLock._BLO_IMEI),
                new DbParameter("P_FLAG_UPDATE", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255,ParameterDirection.Output)
              
                 
									
            };
            try
            {

                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_IFI_UPDATE_DESBLOQUEO_EQUIPO, parameters);
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
            if (rFlagInsercion == "OK")
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;

        }
    }
}

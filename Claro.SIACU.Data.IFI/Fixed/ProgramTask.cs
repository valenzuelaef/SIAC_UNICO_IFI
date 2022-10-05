using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Fixed;

using System;
using System.Data;
using Claro.Data;
using System.Collections.Generic;

using Claro.Web;
using WSServicioLTE = Claro.SIACU.ProxyService.IFI.SIACFixed.ServAddLTE;

using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;

namespace Claro.SIACU.Data.IFI.Fixed
{
    public class ProgramTask
    {


        /// <summary>
        /// eliminar tareas programadas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strServiCoId"></param>
        /// <param name="codId"></param>
        /// <param name="strServiceState"></param>
        /// <param name="strApplicationCode"></param>
        /// <param name="strApplicationName"></param>
        /// <param name="strUserApp"></param>
        /// <returns></returns>
        /// <remarks>GetDeleteScheduledTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool GetDeleteScheduledTransaction(string strIdSession, string strTransaction,
             string strServiCoId, string codId, string strServiceState, string strApplicationCode, string strApplicationName, string strUserApp)
        {
            WSServicioLTE.parametrosAuditRequest objAuditReq = new WSServicioLTE.parametrosAuditRequest();
            WSServicioLTE.parametrosAuditResponse objAuditRes = new WSServicioLTE.parametrosAuditResponse();
            WSServicioLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new WSServicioLTE.parametrosRequestObjetoRequestOpcional[0];
         

            WSServicioLTE.tareasProgramadasEliminarRequest objReq = new WSServicioLTE.tareasProgramadasEliminarRequest();
            WSServicioLTE.tareasProgramadasEliminarResponse objRes = new WSServicioLTE.tareasProgramadasEliminarResponse();
            objAuditReq.idTransaccion = strTransaction;
            objAuditReq.ipAplicacion = strApplicationCode;
            objAuditReq.nombreAplicacion = strApplicationName;
            objAuditReq.usuarioAplicacion = strUserApp;
            objReq.auditRequest = objAuditReq;

            objReq.serviCod = strServiCoId;
            objReq.codId = codId;
            objReq.servcEstado = strServiceState;

            objReq.listaRequestOpcional = listaOpcionalRequest;


            objRes = Web.Logging.ExecuteMethod(strIdSession, strTransaction, ServiceConfiguration.SiacFixedActivationDesactivacionLte, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionLte.tareasProgramadasEliminar(objReq);
            });

            objAuditRes = objRes.auditResponse;
            if (objAuditRes.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// obtiene transacciones programadas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strIdTransactionTask"></param>
        /// <param name="strApplicationCode"></param>
        /// <param name="strApplicationName"></param>
        /// <param name="strUserApp"></param>
        /// <param name="strServiCoId"></param>
        /// <param name="strStartDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="strServiceState"></param>
        /// <param name="strAdvisor"></param>
        /// <param name="strAccount"></param>
        /// <param name="strTransactionType"></param>
        /// <param name="strCodeInteraction"></param>
        /// <param name="strNameCacdac"></param>
        /// <param name="correctProcess"></param>
        /// <returns></returns>
        /// <remarks>GetListScheduledTransactions</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<ScheduledTransaction> GetListScheduledTransactions(string strIdSession, string strTransaction,
            string strIdTransactionTask, string strApplicationCode, string strApplicationName, string strUserApp, string strServiCoId,
            string strStartDate, string strEndDate, string strServiceState, string strAdvisor, string strAccount,
            string strTransactionType, string strCodeInteraction, string strNameCacdac, out bool correctProcess)
        {
            List<ScheduledTransaction> list = new List<ScheduledTransaction>();

            WSServicioLTE.parametrosAuditRequest objAuditReq = new WSServicioLTE.parametrosAuditRequest();
            WSServicioLTE.parametrosAuditResponse objAuditRes = new WSServicioLTE.parametrosAuditResponse();

            WSServicioLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new WSServicioLTE.parametrosRequestObjetoRequestOpcional[0];
            WSServicioLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new WSServicioLTE.parametrosResponseObjetoResponseOpcional[0];


            WSServicioLTE.tareasProgramadasConsultarRequest objReq = new WSServicioLTE.tareasProgramadasConsultarRequest();
            WSServicioLTE.tareasProgramadasConsultarResponse objRes = new WSServicioLTE.tareasProgramadasConsultarResponse();

            objAuditReq.idTransaccion = strIdTransactionTask;
            objAuditReq.ipAplicacion = strApplicationCode;
            objAuditReq.nombreAplicacion = strApplicationName;
            objAuditReq.usuarioAplicacion = strUserApp;

            objReq.auditRequest = objAuditReq;
            objReq.servicoid = strServiCoId;
            if (strStartDate != string.Empty && strEndDate != string.Empty)
            {
                objReq.fechadesde = string.Format("{0:yyyy-MM-dd}", Convert.ToDate(strStartDate));
                objReq.fechahasta = string.Format("{0:yyyy-MM-dd}", Convert.ToDate(strEndDate));
            }
            else
            {
                objReq.fechadesde = string.Empty;
                objReq.fechahasta = string.Empty;
            }
            objReq.estado = strServiceState;
            objReq.asesor = strAdvisor;
            objReq.cuenta = strAccount;
            objReq.tipoTransaccion = strTransactionType;
            objReq.codInteraccion = strCodeInteraction;
            objReq.cadDac = strNameCacdac;

            objReq.listaRequestOpcional = listaOpcionalRequest;

            objRes = Web.Logging.ExecuteMethod(strIdSession, strTransaction, ServiceConfiguration.SiacFixedActivationDesactivacionLte, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionLte.tareasProgramadasConsultar(objReq);
            });

            if (objRes != null)
            {
                if (objRes.pcursor.Length > 0)
                {
                    for (int i = 0; i < objRes.pcursor.Length; i++)
                    {
                        ScheduledTransaction objServicioLinea = new ScheduledTransaction();
                        objServicioLinea.CO_ID = Claro.Utils.CheckStr(objRes.pcursor[i].co_id);
                        objServicioLinea.DESC_STATE = Claro.Utils.CheckStr(objRes.pcursor[i].desc_estado);
                        objServicioLinea.DESC_SERVICE = Claro.Utils.CheckStr(objRes.pcursor[i].desc_servi);
                        objServicioLinea.CUSTOMER_ID = Claro.Utils.CheckStr(objRes.pcursor[i].customer_id); //SD-794552 - RPB - PROY-20152.INC000000726842
                        objServicioLinea.SERVC_CO_SER = Claro.Utils.CheckStr(objRes.pcursor[i].servc_co_ser);
                        objServicioLinea.SERVC_CODE_INTERACTION = Claro.Utils.CheckStr(objRes.pcursor[i].servc_codigo_interaccion);
                        objServicioLinea.SERVC_DES_CO_SER = Claro.Utils.CheckStr(objRes.pcursor[i].servc_des_co_ser);
                        objServicioLinea.SERVC_ISBATCH = Claro.Utils.CheckStr(objRes.pcursor[i].servc_esbatch);
                        objServicioLinea.SERVC_STATE = Claro.Utils.CheckStr(objRes.pcursor[i].servc_estado);
                        objServicioLinea.SERVC_NUMBERACCOUNT = Claro.Utils.CheckStr(objRes.pcursor[i].servc_nrocuenta);
                        objServicioLinea.SERVC_POINTSALE = Claro.Utils.CheckStr(objRes.pcursor[i].servc_puntoventa);
                        objServicioLinea.SERVC_TYPE_REG = Claro.Utils.CheckStr(objRes.pcursor[i].servc_tipo_reg);
                        objServicioLinea.SERVC_TYPE_SERV = Claro.Utils.CheckStr(objRes.pcursor[i].servc_tipo_serv);
                        objServicioLinea.SERVD_DATE_EJEC = Claro.Utils.CheckStr(objRes.pcursor[i].servd_fecha_ejec);
                        objServicioLinea.SERVD_DATE_REG = Claro.Utils.CheckStr(objRes.pcursor[i].servd_fecha_reg);
                        objServicioLinea.SERVD_DATEPROG = Claro.Utils.CheckStr(objRes.pcursor[i].servd_fechaprog);
                        objServicioLinea.SERVI_COD = Claro.Utils.CheckStr(objRes.pcursor[i].servi_cod);
                        objServicioLinea.SERVV_COD_ERROR = Claro.Utils.CheckStr(objRes.pcursor[i].servv_cod_error);
                        objServicioLinea.SERVV_EMAIL_USER_APP = Claro.Utils.CheckStr(objRes.pcursor[i].servv_email_usuario_app);
                        objServicioLinea.SERVV_ID_BATCH = Claro.Utils.CheckStr(objRes.pcursor[i].servv_id_batch);
                        objServicioLinea.SERVV_ID_EAI_SW = Claro.Utils.CheckStr(objRes.pcursor[i].servv_id_eai_sw);
                        objServicioLinea.SERVV_MEN_ERROR = Claro.Utils.CheckStr(objRes.pcursor[i].servv_men_error);
                        objServicioLinea.SERVV_MSISDN = Claro.Utils.CheckStr(objRes.pcursor[i].servv_msisdn);
                        objServicioLinea.SERVV_USER_APLICATION = Claro.Utils.CheckStr(objRes.pcursor[i].servv_usuario_aplicacion);
                        objServicioLinea.SERVV_USER_SYSTEM = Claro.Utils.CheckStr(objRes.pcursor[i].servv_usuario_sistema);
                        objServicioLinea.SERVV_XMLENTRY = Claro.Utils.CheckStr(objRes.pcursor[i].servv_xmlentrada);
                        list.Add(objServicioLinea);
                    }
                }

                objAuditRes = objRes.auditResponse;
            }

            if (objAuditRes.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
                correctProcess = true;
            else
                correctProcess = false;

            return list;
        }
        /// <summary>
        /// eliminar tareas programadas HFC
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vstrServCod"></param>
        /// <param name="vstrCodId"></param>
        /// <param name="vstrServCEstado"></param>
        /// <returns></returns>
        /// <remarks>DeleteProgramTaskHfc</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool DeleteProgramTaskHfc(string strIdSession, string strTransaction, string vstrServCod, string vstrCodId, string vstrServCEstado)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_cod", DbType.String, ParameterDirection.Input, vstrServCod),	
                new DbParameter("p_cod_id", DbType.String, ParameterDirection.Input, vstrCodId),	
                new DbParameter("p_servc_estado", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_cod_error", DbType.Int32, ParameterDirection.Output),	
                new DbParameter("p_men_error", DbType.String, 255, ParameterDirection.Output)
            };

            bool salida;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_TIMEAI, DbCommandConfiguration.SIACU_HFCPOST_SP_BORRAR_PROGRAMACION, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                var resultInt = Claro.Utils.CheckInt(parameters[3].Value.ToString());
                salida = resultInt == 0;
            }

            return salida;
        }
        /// <summary>
        /// Actualiza tarea programadas HFC
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vstrServCod"></param>
        /// <param name="vstrCodId"></param>
        /// <param name="vstrCustomerId"></param>
        /// <param name="vstrServFProg"></param>
        /// <param name="vstrServIdBat"></param>
        /// <param name="vstrServFEjec"></param>
        /// <param name="vstrServCEstado"></param>
        /// <param name="vstrServMenErr"></param>
        /// <param name="vstrServCodErr"></param>
        /// <returns></returns>
        /// <remarks>UpdateProgramTaskHfc</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateProgramTaskHfc(string strIdSession, string strTransaction, string vstrServCod, string vstrCodId, string vstrCustomerId, string vstrServFProg, string vstrServIdBat, string vstrServFEjec, string vstrServCEstado, string vstrServMenErr, string vstrServCodErr)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_cod", DbType.String,ParameterDirection.Input, vstrServCod),
                new DbParameter("p_cod_id", DbType.String,ParameterDirection.Input, vstrCodId),
                new DbParameter("p_customer_id", DbType.String,ParameterDirection.Input, vstrCustomerId),
                new DbParameter("p_servd_fecha_prog", DbType.String, ParameterDirection.Input, vstrServFProg),
                new DbParameter("p_servv_id_batch", DbType.String, ParameterDirection.Input, vstrServIdBat),
                new DbParameter("p_servd_fecha_ejec", DbType.String, ParameterDirection.Input, vstrServFEjec),
                new DbParameter("p_servc_estado", DbType.String, ParameterDirection.Input, vstrServCEstado),
                new DbParameter("p_servv_men_error", DbType.String, ParameterDirection.Input, vstrServMenErr),
                new DbParameter("p_servv_cod_error", DbType.String, ParameterDirection.Input, vstrServCodErr),
                new DbParameter("p_cod_error", DbType.Int32, 255, ParameterDirection.Output),
                new DbParameter("p_men_error", DbType.String, 255, ParameterDirection.Output)
            };

            bool salida;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_TIMEAI, DbCommandConfiguration.SIACU_HFCPOST_SP_ACTUALIZA_PROGRAMACION, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                var resultInt = Claro.Utils.CheckInt(parameters[9].Value.ToString());
                salida = resultInt == 0;
            }

            return salida;
        }
        /// <summary>
        /// Actualiza tarea programadas LTE
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="codigoAplicacion"></param>
        /// <param name="nombreAplicacion"></param>
        /// <param name="usuarioApp"></param>
        /// <param name="serviCod"></param>
        /// <param name="conId"></param>
        /// <param name="serviEstado"></param>
        /// <param name="fechaProg"></param>
        /// <returns></returns>
        /// <remarks>UpdateProgramTaskLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateProgramTaskLte(string strIdSession, string strTransaction, string codigoAplicacion, string nombreAplicacion, string usuarioApp, string serviCod, string conId, string serviEstado, string fechaProg)
        {
            try
            {
                string mensajeRespuesta = string.Empty;
                WSServicioLTE.parametrosAuditRequest objAuditReq = new WSServicioLTE.parametrosAuditRequest();
                WSServicioLTE.parametrosAuditResponse objAuditRes = new WSServicioLTE.parametrosAuditResponse();

                WSServicioLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new WSServicioLTE.parametrosRequestObjetoRequestOpcional[0];
                WSServicioLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new WSServicioLTE.parametrosResponseObjetoResponseOpcional[0];

                WSServicioLTE.tareasProgramadasEditarRequest objReq = new WSServicioLTE.tareasProgramadasEditarRequest();
                WSServicioLTE.tareasProgramadasEditarResponse objRes = new WSServicioLTE.tareasProgramadasEditarResponse();

                objAuditReq.idTransaccion = strTransaction;
                objAuditReq.ipAplicacion = codigoAplicacion;
                objAuditReq.nombreAplicacion = nombreAplicacion;
                objAuditReq.usuarioAplicacion = usuarioApp;

                objReq.auditRequest = objAuditReq;

                objReq.serviCod = serviCod;
                objReq.codId = conId;
                objReq.servcEstado = serviEstado;
                objReq.servdFechaProg = fechaProg;

                objReq.listaRequestOpcional = listaOpcionalRequest;

                objRes = Logging.ExecuteMethod(strIdSession, strTransaction, ServiceConfiguration.SiacFixedActivationDesactivacionLte, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.tareasProgramadasEditar(objReq);
                });

                objAuditRes = objRes.auditResponse;

                Logging.Info(strIdSession, strTransaction, "Parámetros de Salida -> Mensaje respuesta: " + objAuditRes.mensajeRespuesta);

                if (objAuditRes.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
                return false;
            }
        }

    }
}

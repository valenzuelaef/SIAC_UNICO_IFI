using System;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI;
using Claro.SIACU.Entity.IFI.Fixed.GetReconeService;
using Claro.Web;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;

using ActDesactLTE = Claro.SIACU.ProxyService.IFI.SIACFixed.ServAddLTE;
using ActDesactHFC = Claro.SIACU.ProxyService.IFI.SIACFixed.ServAddHFC;
using KEY = Claro.ConfigurationManager;
using Claro.Data;
using System.Data;

namespace Claro.SIACU.Data.IFI.Fixed
{
    public class SuspensionService
    {
        /// <summary>
        /// ejecuta la suspension 
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>EjecutaSuspensionDeServicioCodRes</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(
            EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest)
        {
            string mensajeRespuesta = string.Empty;

            var objResponse = new EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse();
            try
            {
                ActDesactHFC.ParametroType[] listaOpcionalRequest = new ActDesactHFC.ParametroType[0];
                ActDesactHFC.ParametroType[] listaOpcionalResponse = new ActDesactHFC.ParametroType[0];
                var idTransaccion = string.Empty;

                var result = Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedActivationDesactivacionHfc, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionHfc.ejecutarSuspencionServicio(
                        ref idTransaccion, objRequest.Suspension.codigoAplicacion, objRequest.Suspension.ipAplicacion,
                        objRequest.Suspension.flagAccion, objRequest.Suspension.fechaProgramacion,
                        objRequest.Suspension.coId, objRequest.Suspension.nroDias, objRequest.Suspension.fideliza,
                        objRequest.Suspension.fechaSuspension, objRequest.Suspension.ticklerCode,
                        objRequest.Suspension.desTickler, objRequest.Suspension.usuario,
                        objRequest.Suspension.codCliente,
                        objRequest.Suspension.coState, objRequest.Suspension.reason,
                        objRequest.Suspension.telefono, objRequest.Suspension.tipoServicio, objRequest.Suspension.coSer,
                        objRequest.Suspension.tipoRegistro, objRequest.Suspension.usuarioSistema,
                        objRequest.Suspension.usuarioApp, objRequest.Suspension.emailUsuarioApp,
                        objRequest.Suspension.desCoSer,
                        objRequest.Suspension.codigoInteraccion, objRequest.Suspension.nroCuenta, listaOpcionalRequest,
                        out mensajeRespuesta, out listaOpcionalResponse);
                });

                objResponse.idtrans = idTransaccion;
                objResponse.result = result;
                objResponse.ResultMethod = result.Equals(Claro.Constants.NumberZeroString);
            }
            catch (Exception ex)
            {
                Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;

        }
        /// <summary>
        /// reconecta el servicio
        /// </summary>
        /// <param name="objContent"></param>
        /// <returns></returns>
        /// <remarks>GetReconectionService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static ReconeServiceResponse GetReconectionService(ReconeServiceRequest objContent)
        {
            var model = new ReconeServiceResponse();
            string msgRespta = string.Empty;
            ActDesactHFC.ParametroType[] listaOpcionalRequest = new ActDesactHFC.ParametroType[0];
            ActDesactHFC.ParametroType[] listaOpcionalResponse = new ActDesactHFC.ParametroType[0];

            var idTransaction = string.Empty;
            var result = Claro.Web.Logging.ExecuteMethod(objContent.Audit.Session, objContent.Audit.Transaction,
               ServiceConfiguration.SiacFixedActivationDesactivacionHfc, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionHfc.ejecutarSuspencionServicio(
                        ref idTransaction,
                        objContent.GetReconection.CodeAplication,
                        objContent.GetReconection.IpAplication,
                        objContent.GetReconection.FlagAccion,
                        objContent.GetReconection.ProgramationDate,
                        objContent.GetReconection.CoId,
                        objContent.GetReconection.NroDay,
                        objContent.GetReconection.Fideliza,
                        objContent.GetReconection.SuspentionDate,
                        objContent.GetReconection.CodeTicker,
                        objContent.GetReconection.DesTickler,
                        objContent.GetReconection.Users,
                        objContent.GetReconection.CodCustomer,
                        objContent.GetReconection.CoState,
                        objContent.GetReconection.Reason,
                        objContent.GetReconection.Telephone,
                        objContent.GetReconection.TypeService,
                        objContent.GetReconection.CoSer,
                        objContent.GetReconection.TypeRegister,
                        objContent.GetReconection.UserSystem,
                        objContent.GetReconection.UserApp,
                        objContent.GetReconection.EmailUserApp,
                        objContent.GetReconection.DesCoser,
                        objContent.GetReconection.CodeInteraction,
                        objContent.GetReconection.NroAcount,
                        listaOpcionalRequest,
                        out msgRespta,
                        out listaOpcionalResponse
                    );

                });

            if (result.Equals(Claro.Constants.NumberZeroString))
            {
                model.BoolResult = true;
            }
            else
            {
                model.BoolResult = false;
            }
            model.IdTransaction = idTransaction;
            model.Result = msgRespta;
            return model;
        }
        /// <summary>
        /// suspende servicio lte
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>EjecutaSuspensionDeServicioLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse();
            try
            {
                string mensajeRespuesta = string.Empty;

                ActDesactLTE.parametrosAuditRequest objAuditReq = new ActDesactLTE.parametrosAuditRequest();
                ActDesactLTE.parametrosAuditResponse objAuditRes = new ActDesactLTE.parametrosAuditResponse();


                ActDesactLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new ActDesactLTE.parametrosResponseObjetoResponseOpcional[0];

                
                ActDesactLTE.ejecutarSuspencionServicioRequest objReq = new ActDesactLTE.ejecutarSuspencionServicioRequest();
                ActDesactLTE.ejecutarSuspencionServicioResponse objRes = new ActDesactLTE.ejecutarSuspencionServicioResponse();

              

                var objContent = objRequest.Suspension;

                objAuditReq.idTransaccion = objRequest.Audit.Transaction;
                objAuditReq.ipAplicacion = objContent.ipAplicacion;
                objAuditReq.nombreAplicacion = objContent.nombreAplicacion;
                objAuditReq.usuarioAplicacion = objContent.usuarioApp;

                objReq.auditRequest = objAuditReq;
                objReq.codCliente = objContent.codCliente;
                objReq.codigoInteraccion = objContent.codigoInteraccion;
                objReq.coId = objContent.coId;
                objReq.coSer = objContent.coSer;
                objReq.coState = objContent.coState;
                objReq.fideliza = objContent.fideliza;
                objReq.desCoSer = objContent.desCoSer;
                objReq.nroDias = objContent.nroDias;
                objReq.emailUsuarioApp = objContent.emailUsuarioApp;
                objReq.fechaProgramacion = objContent.fechaProgramacion.ToString("yyyy-MM-dd");
                objReq.fechaSuspension = objContent.fechaSuspension.ToString("yyyy-MM-dd");
                objReq.flagAccion = objContent.flagAccion;
                objReq.codigoInteraccion = objContent.codigoInteraccion;
                objReq.listaRequestOpcional = new ActDesactLTE.parametrosRequestObjetoRequestOpcional[1]
                            {
                                new ActDesactLTE.parametrosRequestObjetoRequestOpcional()
                                {
                                    campo = "PRODUCTO",
                                    valor = "IFI",
                                }
                            };
                objReq.nroCuenta = objContent.nroCuenta;
                objReq.reason = objContent.reason;
                objReq.telefono = objContent.telefono;
                objReq.ticklerCode = objContent.ticklerCode;
                objReq.tipoRegistro = objContent.tipoRegistro;
                objReq.tipoServicio = objContent.tipoServicio;
                objReq.usuario = objContent.usuario;
                objReq.usuarioApp = objContent.usuarioApp;
                objReq.usuarioSistema = objContent.usuarioSistema;
                objReq.desTickler = objContent.desTickler;

                objRes = Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedActivationDesactivacionLte, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.ejecutarSuspencionServicio(objReq);
                });

                objAuditRes = objRes.auditResponse;

                if (objAuditRes.codigoRespuesta.Equals("0"))
                {
                    objResponse.ResponseStatus = true;
                }
                else
                {
                    objResponse.ResponseStatus = false;
                }

            }
            catch (Exception ex)
            {
                Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                objResponse.ResponseStatus = false;
            }

            return objResponse;
        }
        /// <summary>
        /// reconecta servicio lte
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>EjecutaReconexionDeServicioLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostReconexionLte.ReconexionLteResponse();
            try
            {

                string mensajeRespuesta = string.Empty;

                ActDesactLTE.parametrosAuditRequest objAuditReq = new ActDesactLTE.parametrosAuditRequest();
                ActDesactLTE.parametrosAuditResponse objAuditRes = new ActDesactLTE.parametrosAuditResponse();

                ActDesactLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new ActDesactLTE.parametrosRequestObjetoRequestOpcional[1];
                ActDesactLTE.parametrosRequestObjetoRequestOpcional objparametrosRequestObjetoRequestOpcional = new ActDesactLTE.parametrosRequestObjetoRequestOpcional()
                {
                    campo = "PRODUCTO",
                    valor = "IFI"
                };
                listaOpcionalRequest[0] = objparametrosRequestObjetoRequestOpcional;

                ActDesactLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new ActDesactLTE.parametrosResponseObjetoResponseOpcional[0];

                ActDesactLTE.ejecutarReconexionServicioRequest objReq = new ActDesactLTE.ejecutarReconexionServicioRequest();
                ActDesactLTE.ejecutarReconexionServicioResponse objRes = new ActDesactLTE.ejecutarReconexionServicioResponse();
             
                var objContent = objRequest.ReconexionLte;

                objAuditReq.idTransaccion = objRequest.Audit.Transaction; //JOH - 23-03-2018 (CAMBIO)
                objAuditReq.ipAplicacion = objContent.codigoAplicacion;
                objAuditReq.nombreAplicacion = objContent.nombreAplicacion;
                objAuditReq.usuarioAplicacion = objContent.usuarioApp;

                objReq.auditRequest = objAuditReq;
                objReq.codCliente = objContent.codCliente;
                objReq.codigoInteraccion = objContent.codigoInteraccion;
                objReq.coId = objContent.coId;
                objReq.coSer = objContent.coSer;
                objReq.coState = objContent.coState;
                objReq.desCoSer = objContent.desCoSer;
                objReq.emailUsuarioApp = objContent.emailUsuarioApp;
                objReq.fechaProgramacion = objContent.fechaProgramacion.ToString("yyyy-MM-dd");
                objReq.flagAccion = objContent.flagAccion;
                objReq.interaccion = objContent.codigoInteraccion;
                objReq.listaRequestOpcional = listaOpcionalRequest;
                objReq.montoOcc = objContent.montoOCC;
                objReq.nroCuenta = objContent.nroCuenta;
                objReq.reason = objContent.reason;
                objReq.telefono = objContent.telefono;
                objReq.ticklerCode = objContent.ticklerCode;
                objReq.tipoRegistro = objContent.tipoRegistro;
                objReq.tipoServicio = objContent.tipoServicio;
                objReq.usuario = objContent.usuario;
                objReq.usuarioApp = objContent.usuarioApp;
                objReq.usuarioSistema = objContent.usuarioSistema;


                objRes = Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedActivationDesactivacionLte, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.ejecutarReconexionServicio(objReq);
                });

                objAuditRes = objRes.auditResponse;

                if (objAuditRes.codigoRespuesta.Equals("0"))
                {
                    objResponse.ResponseStatus = true;
                }
                else
                {
                    objResponse.ResponseStatus = false;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                objResponse.ResponseStatus = false;
            }

            return objResponse;
        }
        /// <summary>
        /// actualiza campo inter29
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="p_objid"></param>
        /// <param name="p_texto"></param>
        /// <param name="p_orden"></param>
        /// <param name="rFlagInsercion"></param>
        /// <param name="rMsgText"></param>
        /// <returns></returns>
        /// <remarks>UpdateInter29</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static bool UpdateInter29(string strIdSession, string strTransaction, string p_objid, string p_texto, string p_orden, ref string rFlagInsercion, ref string rMsgText)
        {

            var salida = false;


            DbParameter[] parameters = {
                new DbParameter("P_INTERACT_ID", DbType.Int64,ParameterDirection.Input),
				new DbParameter("P_TEXTO", DbType.String,1000,ParameterDirection.Input),
				new DbParameter("P_ORDEN", DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_INSERCION", DbType.String,255,ParameterDirection.Output),
				new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
		};


            foreach (var t in parameters)
                t.Value = DBNull.Value;

            var i = 0;
            if (!string.IsNullOrEmpty(p_objid))
                parameters[i].Value = Convert.ToInt64(p_objid);

            if (!string.IsNullOrEmpty(p_texto))
                parameters[1].Value = p_texto;

            if (!string.IsNullOrEmpty(p_orden))
                parameters[2].Value = p_orden;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_UPDATE_INTERACT_X_INTER29, parameters);
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

    }
}

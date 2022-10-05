using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using OPlanMigration = Claro.SIACU.Entity.IFI.Postpaid;
using Newtonsoft.Json;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class PlanMigration
    {

        /// <summary>
        /// Método que obtiene una lista de planes
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">id de transacción</param>
        /// <param name="strMap">código del plano</param>
        /// <param name="strOffer">codigo de oferta</param>
        /// <param name="strProductType">código del tipo de producto</param>
        /// <returns>retorna una lista de planes</returns>
        public static List<OPlanMigration.ProductPlan> GetPlans(string strIdSession, string strTransaction, string strOffice, string strOfficeDefault, string strOffer, string strProductType, string strFlagEjecution)
        {
            Claro.Web.Logging.Info("IFIGetPlans", "IFIPlanMigration", "Entró GetPlans");
            Claro.Web.Logging.Info("IFIGetPlans", "IFIPlanMigration", "Input strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strOffice:" + strOffice + ";strOfficeDefault:" + strOfficeDefault + ";strOffer:" + strOffer + ";strProductType:" + strProductType + ";strFlagEjecution:" + strFlagEjecution);
            List<OPlanMigration.ProductPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_OFERTA", DbType.String,255, ParameterDirection.Input,strOffer),
                new DbParameter("PI_OFICINA", DbType.String, 255, ParameterDirection.Input, strOffice),
                new DbParameter("PI_OFICINADEFAULT", DbType.String, 255, ParameterDirection.Input, strOfficeDefault),
                new DbParameter("PI_TIPO_PRODUCTO",DbType.String,255,ParameterDirection.Input,strProductType),
                new DbParameter("PI_FLAG_EJECUCION", DbType.String, 255, ParameterDirection.Input, strFlagEjecution)
            };

            try
            {
                list = DbFactory.ExecuteReader<List<OPlanMigration.ProductPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_CAMPANA, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("IFIGetPlans", "IFIPlanMigration", "Finalizó GetPlans");
            return list;
        }

        /// <summary>
        /// Método que obtiene una lista de los servicios según el plan
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">id de transacción</param>
        /// <param name="strIdPlan">id del plan</param>
        /// <param name="strProductType">código del tipo de producto</param>
        /// <returns>retorna una lista de servicios según el plan</returns>
        public static List<OPlanMigration.ServiceByPlan> GetServicesByPlan(string strIdSession, string strTransaction, string strIdPlan, string strProductType)
        {
            Claro.Web.Logging.Info("IFIGetServicesByPlan", "IFIPlanMigration", "Entró GetServicesByPlan");
            Claro.Web.Logging.Info("IFIGetServicesByPlan", "IFIPlanMigration", "los parametros que recibe el método son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strIdPlan:" + strIdPlan + ";strProductType:" + strProductType);
            
            DbParameter[] parameters = new DbParameter[] {                
                new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input,strIdPlan),
                new DbParameter("p_tipo_producto",DbType.String,255,ParameterDirection.Input,strProductType),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)                
            };

            var lstServiceByPlan = new List<OPlanMigration.ServiceByPlan>();
            OPlanMigration.ServiceByPlan objServiceByPlan = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        objServiceByPlan = new OPlanMigration.ServiceByPlan();
                        objServiceByPlan.SisactPlanCode = Claro.Utils.CheckStr(dr["COD_PLAN_SISACT"]);
                        objServiceByPlan.SisactPlanDes = Claro.Utils.CheckStr(dr["DES_PLAN_SISACT"]);
                        objServiceByPlan.TmCode = Claro.Utils.CheckStr(dr["TMCODE"]);
                        objServiceByPlan.Solution = Claro.Utils.CheckStr(dr["SOLUCION"]);
                        objServiceByPlan.SisactServCode = Claro.Utils.CheckStr(dr["COD_SERV_SISACT"]);
                        objServiceByPlan.SnCode = Claro.Utils.CheckStr(dr["SNCODE"]);
                        objServiceByPlan.SpCode = Claro.Utils.CheckStr(dr["SPCODE"]);
                        objServiceByPlan.ServiceTypeCode = Claro.Utils.CheckStr(dr["COD_TIPO_SERVICIO"]);
                        objServiceByPlan.ServiceTypeDes = Claro.Utils.CheckStr(dr["TIPO_SERVICIO"]);
                        objServiceByPlan.SisactServDes = Claro.Utils.CheckStr(dr["DES_SERV_SISACT"]);
                        objServiceByPlan.PrincipalGroupCode = Claro.Utils.CheckStr(dr["GSRVC_PRINCIPAL"]);
                        objServiceByPlan.ServiceGroupCode = Claro.Utils.CheckStr(dr["COD_GRUPO_SERV"]);
                        objServiceByPlan.EquipmentGroupCode = Claro.Utils.CheckStr(dr["COD_GRUPO_EQU"]);
                        objServiceByPlan.ServiceGroupDes = Claro.Utils.CheckStr(dr["GRUPO_SERV"]);
                        objServiceByPlan.CF = Claro.Utils.CheckStr(dr["CF"]);
                        objServiceByPlan.IdEquipment = Claro.Utils.CheckStr(dr["IDEQUIPO"]);
                        objServiceByPlan.EquipmentDes = Claro.Utils.CheckStr(dr["EQUIPO"]);
                        objServiceByPlan.EquipmentQty = Claro.Utils.CheckStr(dr["CANT_EQUIPO"]);
                        objServiceByPlan.EquipTypeCode = Claro.Utils.CheckStr(dr["CODTIPEQU"]);
                        objServiceByPlan.SapDes = Claro.Utils.CheckStr(dr["DSCEQU"]);
                        objServiceByPlan.EquipmentType = Claro.Utils.CheckStr(dr["TIPEQU"]);
                        objServiceByPlan.ExternalCode = Claro.Utils.CheckStr(dr["COD_EXTERNO"]);
                        objServiceByPlan.ExternalCodeDes = Claro.Utils.CheckStr(dr["DES_COD_EXTERNO"]);
                        objServiceByPlan.CreationUserServ = Claro.Utils.CheckStr(dr["SERVV_USUARIO_CREA"]);
                        lstServiceByPlan.Add(objServiceByPlan);
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(lstServiceByPlan));
            Claro.Web.Logging.Info("IFIGetServicesByPlan", "IFIPlanMigration", "Finalizó GetServicesByPlan");

            return lstServiceByPlan;
        }

        /// <summary>
        /// Método que obtiene una lista de los servicios del plan actual
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">id de transacción</param>
        /// <param name="strContractId">id del contrato</param>
        /// <param name="strTmCode">TmCode</param>
        /// <returns>retorna una lista de servicios del plan actual</returns>
        public static List<OPlanMigration.ServiceByCurrentPlan> GetServicesByCurrentPlan(string strIdSession, string strTransaction, string strContractId, ref string strTmCode)
        {
            Claro.Web.Logging.Info("IFIGetServicesByCurrentPlan", "IFIPlanMigration", "Entró GetServicesByCurrentPlan");
            Claro.Web.Logging.Info("IFIGetServicesByCurrentPlan", "IFIPlanMigration", "los parametros que recibe el método son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strContractId:" + strContractId);
            
            List<OPlanMigration.ServiceByCurrentPlan> ServicesByCurrentPlan;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.String, ParameterDirection.Input,strContractId),
                new DbParameter("p_tmdes", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String, 255, ParameterDirection.Output)
            };
            try
            {
                ServicesByCurrentPlan = DbFactory.ExecuteReader<List<OPlanMigration.ServiceByCurrentPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_LISTA_SERVICIOS_TELEFONO, parameters);
                strTmCode = parameters[1].Value.ToString();
            }
            catch (Exception ex)
            {
                ServicesByCurrentPlan = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
            }
            Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(ServicesByCurrentPlan));
            Claro.Web.Logging.Info("IFIGetServicesByCurrentPlan", "IFIPlanMigration", "Finalizó GetServicesByCurrentPlan");

            return ServicesByCurrentPlan;
        }

        /// <summary>
        /// Método que obtiene una lista de datos de los equipos del plan actual
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">id de transacción</param>
        /// <param name="strContractId">id del contrato</param>
        /// <returns>retorna una lista de datos de los equipos del plan actual</returns>
        public static List<OPlanMigration.EquipmentByCurrentPlan> GetEquipmentByCurrentPlan(string strIdSession, string strTransaction, string strContractId)
        {
            Claro.Web.Logging.Info("IFIGetEquipmentByCurrentPlan", "IFIPlanMigration", "Entró GetEquipmentByCurrentPlan");
            Claro.Web.Logging.Info("IFIGetEquipmentByCurrentPlan", "IFIPlanMigration", "los parametros que recibe el método son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strContractId:" + strContractId);

            List<OPlanMigration.EquipmentByCurrentPlan> EquipmentByCurrentPlan;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AN_COD_ID", DbType.Int32, ParameterDirection.Input, strContractId),
                new DbParameter("AC_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("AN_ERROR", DbType.Int32, ParameterDirection.Output),
                new DbParameter("AV_ERROR", DbType.String, 255, ParameterDirection.Output)
            };
            try
            {
                EquipmentByCurrentPlan = DbFactory.ExecuteReader<List<OPlanMigration.EquipmentByCurrentPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_IFI_LISTA_DATOS_EQUIPO, parameters);
            }
            catch (Exception ex)
            {
                EquipmentByCurrentPlan = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));
            }
            Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(EquipmentByCurrentPlan));
            Claro.Web.Logging.Info("IFIGetEquipmentByCurrentPlan", "IFIPlanMigration", "Finalizó GetEquipmentByCurrentPlan");

            return EquipmentByCurrentPlan;
        }

        /// <summary>
        /// Método que registra el cambio de plan
        /// </summary>
        /// <param name="objPlanMigrationRequest">Datos de envío</param>
        /// <returns>retorna una respuesta del proceso</returns>
        public static OPlanMigration.PostPlanMigration.PlanMigrationResponse PostPlanMigration(OPlanMigration.PostPlanMigration.PlanMigrationRequest objPlanMigrationRequest, OPlanMigration.PostPlanMigration.RestPlanMigrationRequest objRestPlanMigrationRequest)
        {
            OPlanMigration.PostPlanMigration.PlanMigrationResponse objPlanMigrationResponse = null;
            try
            {
                Claro.Web.Logging.Info(objPlanMigrationRequest.Audit.Session, objPlanMigrationRequest.Audit.Transaction, "Request DP PostPlanMigration: " + JsonConvert.SerializeObject(objRestPlanMigrationRequest));

                objPlanMigrationResponse = RestService.PostInvoque<OPlanMigration.PostPlanMigration.PlanMigrationResponse>(Configuration.RestServiceConfiguration.PlanMigration_Service_DP, objPlanMigrationRequest.Audit, objRestPlanMigrationRequest, true);

                Claro.Web.Logging.Info(objPlanMigrationRequest.Audit.Session, objPlanMigrationRequest.Audit.Transaction, "Response DP PostPlanMigration: " + JsonConvert.SerializeObject(objPlanMigrationResponse));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlanMigrationRequest.Audit.Session, objPlanMigrationRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objPlanMigrationResponse = JsonConvert.DeserializeObject<OPlanMigration.PostPlanMigration.PlanMigrationResponse>(result);
            }

            return objPlanMigrationResponse;
        }

    }
}

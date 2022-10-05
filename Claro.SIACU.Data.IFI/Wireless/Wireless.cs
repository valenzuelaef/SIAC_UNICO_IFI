using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using CSTS = Claro.Utils;
using Claro.SIACU.Entity.IFI.Wireless.GetTransactionScheduled;
using Claro.SIACU.Entity.IFI.Wireless;



namespace Claro.SIACU.Data.IFI.Wireless
{
    //WIRELESS
    public class Wireless
    {
        /// <summary>
        /// obtener calendario transacciones
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vstrCoId"></param>
        /// <param name="vstrCuenta"></param>
        /// <param name="vstrFDesde"></param>
        /// <param name="vstrFHasta"></param>
        /// <param name="vstrEstado"></param>
        /// <param name="vstrAsesor"></param>
        /// <param name="vstrTipoTran"></param>
        /// <param name="vstrCodInter"></param>
        /// <param name="vstrCacDac"></param>
        /// <returns></returns>
        ///  <remarks>GetTransactionScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06/02/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static TransactionScheduledResponse GetTransactionScheduled(string strIdSession, string strTransaction, string vstrCoId, string vstrCuenta,
                                                                         string vstrFDesde, string vstrFHasta, string vstrEstado, string vstrAsesor,
                                                                         string vstrTipoTran, string vstrCodInter, string vstrCacDac)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_coid", DbType.String,255, ParameterDirection.Input, vstrCoId),

                new DbParameter("p_fecha_desde",DbType.Date,ParameterDirection.Input, DBNull.Value),
                new DbParameter("p_fecha_hasta",DbType.Date,ParameterDirection.Input, DBNull.Value),

                new DbParameter("p_estado", DbType.String,255, ParameterDirection.Input, vstrEstado),
                new DbParameter("p_asesor", DbType.String,255, ParameterDirection.Input, vstrAsesor),
                new DbParameter("p_cuenta", DbType.String,255, ParameterDirection.Input, vstrCuenta),
                new DbParameter("p_tipotransaccion", DbType.String,255, ParameterDirection.Input, vstrTipoTran),
                new DbParameter("p_codinteraccion", DbType.String,255, ParameterDirection.Input, vstrCodInter),
                new DbParameter("p_caddac", DbType.String,255, ParameterDirection.Input, vstrCacDac),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            if (!string.IsNullOrEmpty(vstrFDesde))
            {
                parameters[1].Value = Convert.ToDate(vstrFDesde);
            }

            if (!string.IsNullOrEmpty(vstrFHasta))
            {
                parameters[2].Value = Convert.ToDate(vstrFHasta);
            }

            List<TransactionScheduled> listItem = new List<TransactionScheduled>();
            TransactionScheduled item = null;
            var objResponse = new TransactionScheduledResponse();

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_TIMEAI, DbCommandConfiguration.SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        item = new TransactionScheduled();
                        item.CO_ID = Convert.ToString(dr["CO_ID"]);
                        item.CUSTOMER_ID = Convert.ToString(dr["CUSTOMER_ID"]);
                        item.SERVD_FECHAPROG = Convert.ToString(dr["SERVD_FECHAPROG"]);
                        item.SERVD_FECHA_REG = Convert.ToString(dr["SERVD_FECHA_REG"]);
                        item.SERVD_FECHA_EJEC = Convert.ToString(dr["SERVD_FECHA_EJEC"]);
                        item.SERVC_ESTADO = Convert.ToString(dr["SERVC_ESTADO"]);
                        item.DESC_ESTADO = Convert.ToString(dr["DESC_ESTADO"]);
                        item.SERVC_ESBATCH = Convert.ToString(dr["SERVC_ESBATCH"]);
                        item.SERVV_MEN_ERROR = Convert.ToString(dr["SERVV_MEN_ERROR"]);
                        item.SERVV_COD_ERROR = Convert.ToString(dr["SERVV_COD_ERROR"]);
                        item.SERVV_USUARIO_SISTEMA = Convert.ToString(dr["SERVV_USUARIO_SISTEMA"]);
                        item.SERVV_ID_EAI_SW = Convert.ToString(dr["SERVV_ID_EAI_SW"]);
                        item.SERVI_COD = Convert.ToString(dr["SERVI_COD"]);
                        item.DESC_SERVI = Convert.ToString(dr["DESC_SERVI"]);
                        item.SERVV_MSISDN = Convert.ToString(dr["SERVV_MSISDN"]);
                        item.SERVV_ID_BATCH = Convert.ToString(dr["SERVV_ID_BATCH"]);
                        item.SERVV_USUARIO_APLICACION = Convert.ToString(dr["SERVV_USUARIO_APLICACION"]);
                        item.SERVV_EMAIL_USUARIO_APP = Convert.ToString(dr["SERVV_EMAIL_USUARIO_APP"]);
                        item.SERVV_XMLENTRADA = Convert.ToString(dr["SERVV_XMLENTRADA"]);
                        item.SERVC_NROCUENTA = Convert.ToString(dr["SERVC_NROCUENTA"]);
                        item.SERVC_CODIGO_INTERACCION = Convert.ToString(dr["SERVC_CODIGO_INTERACCION"]);
                        item.SERVC_PUNTOVENTA = Convert.ToString(dr["SERVC_PUNTOVENTA"]);
                        item.SERVC_TIPO_SERV = Convert.ToString(dr["SERVC_TIPO_SERV"]);
                        item.SERVC_CO_SER = Convert.ToString(dr["SERVC_CO_SER"]);
                        item.SERVC_TIPO_REG = Convert.ToString(dr["SERVC_TIPO_REG"]);
                        item.SERVC_DES_CO_SER = Convert.ToString(dr["SERVC_DES_CO_SER"]);

                        listItem.Add(item);
                    }
                });

                objResponse.ListTransactionScheduled = listItem;

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }           
            Web.Logging.Info(strIdSession, strTransaction, "GetTransactionScheduled Lista Resultado: " + listItem.Count);

            return objResponse;
        }
    }
}

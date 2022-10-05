using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class ChangeBillingCycle
    {
        /// <summary>
        /// obtiene ciclo de facturacion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTypeCustomer"></param>
        /// <returns></returns>
        /// <remarks>GetBillingCycle</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>XXX</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<BillingCycle> GetBillingCycle(string strIdSession, string strTransaction, string strTypeCustomer)
        {
            List<BillingCycle> lstBillingCycle = new List<BillingCycle>();
            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_TIPO_CLIENTE", DbType.String, ParameterDirection.Input, strTypeCustomer),
                    new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                    new DbParameter("P_CODE_ERR", DbType.String,255, ParameterDirection.Output),
                    new DbParameter("P_MSG_ERR", DbType.String,255, ParameterDirection.Output)

                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_MGRSS_TIM_SP_VALIDA_CICLO_FACT, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstBillingCycle.Add(new BillingCycle
                        {
                            strBicicle = dr["BILLCYCLE"].ToString(),
                            strDescription = dr["DESCRIPTION"].ToString(),
                            strValidForm = dr["VALID_FROM"].ToString(),
                            strTypeCustomer = dr["TIPO_CLIENTE"].ToString()
                        });
                    }
                });

            return lstBillingCycle;
        }
    }
}

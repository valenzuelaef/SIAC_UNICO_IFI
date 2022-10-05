using Claro.Data;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.SIACU.Entity.IFI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEY = Claro.ConfigurationManager;
//using Service_Triacion = Claro.SIACU.ProxyService.IFI.SIACU.TriacionPostpagoWS;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class DuplicateReceipts
    {
     
 
        /// <summary>
        /// guarda duplicado de recibos
        /// </summary>
        /// <param name="strSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="objItem"></param>
        /// <param name="strResult"></param>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        /// <remarks>SaveDuplicateReceipts</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string SaveDuplicateReceipts(string strSession, string strTransaction, Client objItem, out string strResult, out string strMessage)
        {
            Claro.Web.Logging.Info(strSession, strTransaction, "Transaction: Entra a Insert");

            DbParameter[] parameters = new DbParameter[] 
            {
                                                   new DbParameter("P_OBJID", DbType.Int64,ParameterDirection.Input),
												   new DbParameter("P_TEL_REFERENCIAL", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("P_FAX", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("P_EMAIL", DbType.String,80,ParameterDirection.Input),
												   new DbParameter("P_FEC_NAC",  DbType.Date,ParameterDirection.Input),													  
												   new DbParameter("P_SEXO", DbType.String,1,ParameterDirection.Input),
												   new DbParameter("P_EST_CIVIL", DbType.String,40,ParameterDirection.Input),
												   new DbParameter("P_OCUPACION", DbType.String,ParameterDirection.Input),
												   new DbParameter("P_NOM_COMERCIAL", DbType.String,ParameterDirection.Input),
												   new DbParameter("P_CONTACTO_CLIENTE", DbType.String,1,ParameterDirection.Input),
												   new DbParameter("P_PAIS", DbType.String,ParameterDirection.Input),
                                                   new DbParameter("P_MENSAJE", DbType.String,200,ParameterDirection.Input),
												   new DbParameter("P_RESULTADO" ,DbType.String,10,ParameterDirection.Output)
            
            };



            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }
            parameters[0].Value = Convert.ToInt64(objItem.OBJID_CONTACTO);
            parameters[1].Value = objItem.TELEF_REFERENCIA;
            parameters[2].Value = objItem.FAX;
            parameters[3].Value = objItem.EMAIL;
            parameters[4].Value = Convert.ToDate(objItem.FECHA_NAC);
            parameters[5].Value = objItem.SEXO;
            parameters[6].Value = objItem.ESTADO_CIVIL_ID;
            parameters[7].Value = objItem.CARGO;
            parameters[8].Value = objItem.NOMBRE_COMERCIAL;
            parameters[9].Value = objItem.CONTACTO_CLIENTE;
            parameters[10].Value = objItem.LUGAR_NACIMIENTO_DES;





            int result = 0;

            result = DbFactory.ExecuteNonQuery(strSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_UPDATE_CUSTOMER_CLF, parameters);

            strResult = Claro.Utils.CheckStr(parameters[11].Value);
            strMessage = Claro.Utils.CheckStr(parameters[12].Value);




            return strMessage;
        }
    }
}

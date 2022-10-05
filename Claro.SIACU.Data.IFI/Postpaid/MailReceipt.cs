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
    public class MailReceipt
    {
        /// <summary>
        /// activar servicio email
        /// </summary>
        /// <param name="strSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="pCustomerID"></param>
        /// <param name="pCuenta"></param>
        /// <param name="pEmail"></param>
        /// <param name="pFlag"></param>
        /// <param name="pTelRef"></param>
        /// <param name="pNumCla"></param>
        /// <param name="pObjID"></param>
        /// <param name="pTelConfSMS"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        /// <remarks>GetActivateServiceMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static string GetActivateServiceMail(string strSession, string strTransaction, string pCustomerID, string pCuenta, string pEmail, string pFlag, string pTelRef, string pNumCla,
            string pObjID, string pTelConfSMS, ref string strMensaje)
        {



            DbParameter[] parameters = new DbParameter[] 
            {
                                                   new DbParameter("p_customer_id", DbType.String,30,ParameterDirection.Input),
												   new DbParameter("p_cuenta", DbType.String,80,ParameterDirection.Input),
												   new DbParameter("p_email", DbType.String,200,ParameterDirection.Input),
												   new DbParameter("p_flag_act", DbType.String,1,ParameterDirection.Input),
												   new DbParameter("p_result", DbType.String,10,ParameterDirection.Output),
												   new DbParameter("p_error", DbType.String,100,ParameterDirection.Output)
												  
            
            };
          

            for (int j = 0; j < parameters.Length; j++)

                parameters[j].Value = System.DBNull.Value;
            parameters[0].Value = pCustomerID;
            parameters[1].Value = pCuenta;
            parameters[2].Value = pEmail;
            parameters[3].Value = pFlag;
            Web.Logging.Info(strSession, strTransaction, string.Format("Parámetros de Entrada => P_CUSTOMER_ID: {0},P_CUENTA: {1},P_EMAIL: {2},P_FLAG_ACT :{3}", parameters[0].Value, parameters[1].Value, parameters[2].Value, parameters[3].Value));


            string strSalida = "NO OK";

            try
            {
                DbFactory.ExecuteNonQuery(strSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_IFI_INSERTA_EMAIL, parameters);

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strSession, strTransaction, string.Format("Error: {0}", ex.Message));
                strMensaje = ex.Message.ToString();
                return strSalida;
            }
            finally
            {
                IDataParameter parSalida1;
                IDataParameter parSalida2;
                parSalida1 = parameters[parameters.Length - 2];
                parSalida2 = parameters[parameters.Length - 1];
                strSalida = Claro.Utils.CheckStr(parSalida1.Value);
                strMensaje = Claro.Utils.CheckStr(parSalida2.Value);
                Web.Logging.Info(strSession, strTransaction, string.Format("Parametros de salida ----> P_RESULT: {0},P_ERROR: {1}", strSalida, strMensaje));

            }
            return strSalida;
        }
        /// <summary>
        /// obtiene lineas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="vCUSTOMER_ID"></param>
        /// <returns></returns>
        /// <remarks>GetLinesTelephone</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static List<Line> GetLinesTelephone(string strIdSession, string strTransaction, string vCUSTOMER_ID)
        {

            try
            {
                DbParameter[] parameters = new DbParameter[] 
                 {
                                                   new DbParameter("p_customer_id", DbType.Int64,30,ParameterDirection.Input,vCUSTOMER_ID),												  
												   new DbParameter("p_cursor", DbType.Object,ParameterDirection.Output)
												  
            
                 };

                List<Line> listLine = null;
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_IFI_LINEAS_CLIENTE, parameters, (IDataReader reader) =>
                {
                    listLine = new List<Line>();

                    while (reader.Read())
                    {
                        listLine.Add(new Line()
                        {
                            ContractID = Claro.Convert.ToString(reader["CO_ID"]),
                            PhoneNumber = Claro.Convert.ToString(reader["TELEFONO"]),
                            LineStatus = Claro.Convert.ToString(reader["ESTADO"]),
                            TariffPlan = Claro.Convert.ToString(reader["PLAN"])


                        });
                    }

                });
                return listLine;
            }
            catch (Exception e)
            {
                Web.Logging.Info(strIdSession, strTransaction, string.Format("Error() {0}", e.Message));
                throw e;
            }


        }
        /// <summary>
        /// obtiene datos facturas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerCode"></param>
        /// <returns></returns>
        /// <remarks>GetDataInvoice</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public static Entity.IFI.Common.Receipt GetDataInvoice(string strIdSession, string strTransaction, string strCustomerCode)
        {
            Receipt objReceipt;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_CODIGOCLIENTE", DbType.String,24, ParameterDirection.Input, strCustomerCode),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };
            Claro.Web.Logging.Info(strIdSession, strTransaction, "Metodo: GetDataInvoice - Parametros de Entrada: strCustomerCode-" + strCustomerCode);
            objReceipt = DbFactory.ExecuteReader<Receipt>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters);
            if (objReceipt == null)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaction, "GetDataInvoice: El valor retornado es null");
            }
            return objReceipt;
        }

    }
}

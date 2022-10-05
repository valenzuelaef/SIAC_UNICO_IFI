using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Data.IFI.Configuration;
using Claro.Data;
using System.Data;
using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Postpaid;


namespace Claro.SIACU.Data.IFI.Postpaid
{
   public class RetentionCancelServ
    {
    
        /// <summary>
        /// obtiene acuerdo
        /// </summary>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDataAccord</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
       public static RetentionCancel GetDataAccord(RetentionCancel oRequest)
       {
           RetentionCancel oResponse = new RetentionCancel();

           DbParameter[] parameters =
            {   
                new DbParameter("p_tipo", DbType.String, 10,ParameterDirection.Input, Claro.Constants.LetterT), 
                new DbParameter("p_valor", DbType.String, 10,ParameterDirection.Input, oRequest.NroTelefono), 
                new DbParameter("cursor_Salida", DbType.Object, ParameterDirection.Output)
            };
           
           try
           {
               
               Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
               {
                   DbFactory.ExecuteReader(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SIGA,
                       DbCommandConfiguration.SIACU_POST_SIGA_CONSULTA_ACUERDO, parameters, reader =>
                       {
                            if (reader.Read())
                           {

                               oResponse.Estado_Acuerdo = reader["FIN_VIGENCIA_REAL"].ToString();
                               oResponse.Fecha_Fin_Acuerdo = reader["ESTADO_ACUERDO_DES"].ToString();



                           }
                       });
               });

           }
           catch (Exception ex)
           {
               Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
           }

           return oResponse;
       }

        /// <summary>
        /// carga total cargo fijo
        /// </summary>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        /// <remarks>GetLoadStaidTotal</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
       public static RetentionCancel GetLoadStaidTotal(RetentionCancel oRequest)
       {
           RetentionCancel oResponse = new RetentionCancel();
           double vcagoFijoTotal= 0;
           DbParameter[] parameters =
            {   
                new DbParameter("P_CUSTOMER_ID", DbType.String , 50,ParameterDirection.Input, oRequest.CustomerId), 
                new DbParameter("P_FLAG", DbType.Int32,ParameterDirection.Input, oRequest.flgBuscar), 
                new DbParameter("V_CARGOFIJO", DbType.Object, ParameterDirection.ReturnValue)
            };

           try
           {

               Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
               {
                   DbFactory.ExecuteReader(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                       DbCommandConfiguration.SIACU_POST_BSCS_CARGOFIJO_SERV_X_CLIENTE, parameters, reader =>
                       {

                       });
               });
               vcagoFijoTotal =Convert.ToDouble(parameters[2].Value.ToString());
               oResponse.Cago_Fijo_Total = vcagoFijoTotal.ToString();

           }
           catch (Exception ex)
           {
               Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
           }

           return oResponse;
       }


    }
}

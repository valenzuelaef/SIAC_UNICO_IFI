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
using Service_Triacion = Claro.SIACU.ProxyService.IFI.SIACU.TriacionPostpagoWS;
//using Service_NumContactos = Claro.SIACU.ProxyService.IFI.SIACU.NumeroContactosClienteWS;

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class ChangeMinor
    {

        /// <summary>
        /// Permite Actualizar los datos menores de un cliente en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="strSession">/param> Envia el codigo de la session
        /// <param name="strTransaction">/param> Envia el codigo de la transaccion 
        /// <param name="objItem">/param> Objeto la cual contiene todos los campos con la informacion a guardar
        /// <param name="strResult">/param> parametro de salida que almacenará el resultado de la ejecucion del SP
        /// <param name="strMessage">/param> parametro de salida que almacenará el mensaje de la ejecucion del SP
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public static string SaveChangeMinorClarify(string strSession, string strTransaction, Client objItem, out string strResult, out string strMessage)
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


        /// <summary>
        /// Permite Actualizar los datos menores de un cliente en la BD (BSCS70_DESA) mediante este servicio
        /// </summary>
        /// <param name="strSession">/param> Envia el codigo de la session
        /// <param name="strTransaction">/param> Envia el codigo de la transaccion 
        /// <param name="objItem">/param> Objeto la cual contiene todos los campos con la informacion a guardar
        /// <param name="intTeractionid">/param> parametro que contiene el id de la interacción
        /// <param name="strFlaginsercion">/param> parametro que contiene el id de la interacción
        /// <param name="strMessage">/param> parametro de salida que almacenará el mensaje de la ejecucion del SP
        /// <returns>bool</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public static bool SaveChangeMinor(string strSession, string strTransaction, Client objItem, out string intTeractionid, out string strFlaginsercion, out string strMessage)
        {

            string xintTeractionid = "", xstrFlaginsercion = "", xstrMessage = "";
            intTeractionid = xintTeractionid;
            strFlaginsercion = xstrFlaginsercion;
            strMessage = xstrMessage;
            bool blnRetorno = false;

            try
            {
                Service_Triacion.bscs_cambioDatosMenoresRequest objbscs_cambioDatosMenoresRequest = new Service_Triacion.bscs_cambioDatosMenoresRequest();
                Service_Triacion.bscs_cambioDatosMenoresResponse objbscs_cambioDatosMenoresResponse = new Service_Triacion.bscs_cambioDatosMenoresResponse();

                //   objbscs_cambioDatosMenoresRequest.


                if (objItem.CUSTOMER_ID != null)
                    objbscs_cambioDatosMenoresRequest.p_CustomerID = Claro.Utils.CheckInt(objItem.CUSTOMER_ID);
                if (objItem.CARGO != null)
                    objbscs_cambioDatosMenoresRequest.p_cargo = objItem.CARGO;

                if (objItem.TELEF_REFERENCIA != null)
                    objbscs_cambioDatosMenoresRequest.p_telefono = objItem.TELEF_REFERENCIA;
                if (objItem.TELEFONO != null)
                    objbscs_cambioDatosMenoresRequest.p_celular = objItem.TELEFONO;
                if (objItem.FAX != null)
                    objbscs_cambioDatosMenoresRequest.p_fax = objItem.FAX;
                if (objItem.EMAIL != null)
                    objbscs_cambioDatosMenoresRequest.p_email = objItem.EMAIL;
                if (objItem.NOMBRE_COMERCIAL != null)
                    objbscs_cambioDatosMenoresRequest.p_nombrecomercial = objItem.NOMBRE_COMERCIAL;
                if (objItem.CONTACTO_CLIENTE != null)
                    objbscs_cambioDatosMenoresRequest.p_contactocliente = objItem.CONTACTO_CLIENTE;
                if (objItem.FECHA_NAC!= DateTime.MinValue)
                {
                    objbscs_cambioDatosMenoresRequest.p_fechanacimientoSpecified = true;
                    objbscs_cambioDatosMenoresRequest.p_fechanacimiento = Convert.ToDate(objItem.FECHA_NAC);
                }
                else
                {
                    objbscs_cambioDatosMenoresRequest.p_fechanacimientoSpecified = false;
                }
                if (objItem.LUGAR_NACIMIENTO_ID != null)
                {
                    objbscs_cambioDatosMenoresRequest.p_nacionalidadSpecified = true;
                    objbscs_cambioDatosMenoresRequest.p_nacionalidad = Claro.Utils.CheckInt(objItem.LUGAR_NACIMIENTO_ID);
                }
                else
                {
                    objbscs_cambioDatosMenoresRequest.p_nacionalidadSpecified = false;
                }
                if (objItem.SEXO != null)
                    objbscs_cambioDatosMenoresRequest.p_sexo = objItem.SEXO;
                if (objItem.ESTADO_CIVIL != null)
                {
                    objbscs_cambioDatosMenoresRequest.p_estadocivil = Claro.Utils.CheckInt(objItem.ESTADO_CIVIL_ID);
                }



                objbscs_cambioDatosMenoresResponse = Claro.Web.Logging.ExecuteMethod<Service_Triacion.bscs_cambioDatosMenoresResponse>(strSession, strTransaction,
                     () =>
                     {
                         return ServiceConfiguration.ServiceTriacion
                             .actualizaDatosMenores(objbscs_cambioDatosMenoresRequest);
                     });
                int Resp = objbscs_cambioDatosMenoresResponse.P_Result;

                if (Resp == 1)
                {
                    blnRetorno = true;
                }
                else
                {
                    blnRetorno = false;
                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strSession, strTransaction, "Error WS - desactivarContrato_LTE :" + ex.Message);
                blnRetorno = false;
                throw ex;
            }
            return blnRetorno;
        }

        /// <summary>
        /// Permite Actualizar los datos menores de un cliente en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="strSession">/param> Envia el codigo de la session
        /// <param name="strTransaction">/param> Envia el codigo de la transaccion 
        /// <param name="objItem">/param> Objeto la cual contiene todos los campos con la informacion a guardar
        /// <param name="strResult">/param> parametro de salida que almacenará el resultado de la ejecucion del SP
        /// <param name="strMessage">/param> parametro de salida que almacenará el mensaje de la ejecucion del SP
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public static string SaveChangeMinorFijoClarify(string strSession, string strTransaction, Client objItem, out string strResult, out string strMessage)
        {
            Claro.Web.Logging.Info(strSession, strTransaction, "Transaction: Entra a Insert");

            DbParameter[] parameters = new DbParameter[] 
            {                                                  

                                                    new DbParameter("p_phonesearches", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("p_occupation", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("p_company", DbType.String,60,ParameterDirection.Input),
												   new DbParameter("p_phone", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("p_fax_number", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("p_email", DbType.String,80,ParameterDirection.Input),
												   new DbParameter("p_birthday", DbType.Date,ParameterDirection.Input),
												   new DbParameter("p_sex", DbType.String,1,ParameterDirection.Input),
												   new DbParameter("flag_creacion", DbType.String,10,ParameterDirection.Output),
												   new DbParameter("p_msg_text", DbType.String,200,ParameterDirection.Output)
            
            };



            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }
          


            parameters[0].Value = objItem.TELEFONO;
            parameters[1].Value = objItem.CARGO;
            parameters[2].Value = objItem.NOMBRE_COMERCIAL;
            parameters[3].Value = objItem.TELEFONO; 
            parameters[4].Value = objItem.FAX;
            parameters[5].Value = objItem.EMAIL;
            parameters[6].Value = objItem.FECHA_NAC;
            parameters[7].Value = objItem.SEXO;


            int result = 0;

            result = DbFactory.ExecuteNonQuery(strSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_UPDATE_PHONE_FIJO, parameters);

            strResult = Claro.Utils.CheckStr(parameters[8].Value);
            strMessage = Claro.Utils.CheckStr(parameters[9].Value);




            return strMessage;
        }

        /// <summary>
        /// Permite Actualizar los datos menores de un cliente en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="strSession">/param> Envia el codigo de la session
        /// <param name="strTransaction">/param> Envia el codigo de la transaccion 
        /// <param name="objItem">/param> Objeto la cual contiene todos los campos con la informacion a guardar
        /// <param name="strResult">/param> parametro de salida que almacenará el resultado de la ejecucion del SP
        /// <param name="strMessage">/param> parametro de salida que almacenará el mensaje de la ejecucion del SP
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public static string SaveChangeMinorClientDataAdd(string strSession, string strTransaction, Client objItem, out string strResult, out string strMessage)
        {
            Claro.Web.Logging.Info(strSession, strTransaction, "Transaction: Entra a Insert");

            DbParameter[] parameters = new DbParameter[] 
            {                                                  

                                               

                                                   new DbParameter("p_nro_interaccion",DbType.String,255,ParameterDirection.Input),
												   new DbParameter("p_interact2contact",DbType.String,255,ParameterDirection.Input),
												   new DbParameter("p_PHONE_1",DbType.String,25,ParameterDirection.Input),
												   new DbParameter("p_PHONE_2",DbType.String,25,ParameterDirection.Input),
												   new DbParameter("p_E_MAIL1",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("p_E_MAIL2",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("p_COUNTRY",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("p_DEPARTAMENTO",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("p_PROVINCIA",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("p_DISTRITO",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("p_DIRECCION",DbType.String,200,ParameterDirection.Input),
												   new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
												   new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
												   new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)
            
            };



            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }

            int i = 0;

            if (objItem.INTERACT2CONTACT != null)
            {
                parameters[i].Value = objItem.INTERACT2CONTACT;// P_NRO_INTERACCION 
                 
            }
           
            i++;
            if (objItem.OBJID_CONTACTO != null)
            {
                parameters[i].Value = objItem.OBJID_CONTACTO;// p_interact2contact
               
            }

            i++;
            if (objItem.PHONE1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.PHONE1);// p_PHONE_1

            i++;
            if (objItem.PHONE2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.PHONE2);// p_PHONE_2

            i++;
            if (objItem.EMAIL1 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.EMAIL1);// p_E_MAIL1

            i++;
            if (objItem.EMAIL2 != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.EMAIL2);// p_E_MAIL2

            i++;
            if (objItem.COUNTRY_ID != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.COUNTRY_ID);// p_COUNTRY

            i++;
            if (objItem.DEPARTAMENTO != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.DEPARTAMENTO);// p_DEPARTAMENTO

            i++;
            if (objItem.PROVINCIA != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.PROVINCIA);// p_PROVINCIA

            i++;
            if (objItem.DISTRITO != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.DISTRITO);// p_DISTRITO

            i++;
            if (objItem.ADDRESS != null)
                parameters[i].Value = Claro.Utils.CheckStr(objItem.ADDRESS);// p_DIRECCION

            int result = 0;

            result = DbFactory.ExecuteNonQuery(strSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SIACSI_AGREGAR_DATOS_ADIC, parameters);

            strResult = Claro.Utils.CheckStr(parameters[11].Value);
            strMessage = Claro.Utils.CheckStr(parameters[12].Value);

             return strMessage;
        }
      
    }
}

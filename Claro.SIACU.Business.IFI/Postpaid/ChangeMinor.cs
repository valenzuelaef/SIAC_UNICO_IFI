using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Postpaid.GetDataCustomer;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinor;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorClarify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorFijoClarify;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorClientDataAdd;
using Claro.SIACU.Entity.IFI.Postpaid.GetClientDataAdd;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class ChangeMinor
    {

        /// <summary>Permite Actualizar los datos menores de un cliente en la BD (BSCS70_DESA)</summary>
        /// <param name="objRequest">/param> Envia objeto tipo request       
        /// <returns>objResponse</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public static SaveChangeMinorResponse SaveChangeMinor(SaveChangeMinorRequest objRequest)
        {
            SaveChangeMinorResponse objResponse = new SaveChangeMinorResponse();

            string intTeractionid = "";
            string strFlaginsercion = "";
            string strMessage = "";

            objResponse.StrResult = Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.ChangeMinor.SaveChangeMinor(objRequest.strSession, objRequest.strTransaction, objRequest.objCliente, out intTeractionid, out strFlaginsercion, out strMessage);
            });

            objResponse.intTeractionid = intTeractionid;
            objResponse.strFlaginsercion = strFlaginsercion;
            objResponse.strMessage = strMessage;

            return objResponse;
        }


        /// <summary>Permite Actualizar los datos menores de un cliente en la BD de Clarify (TIMPRB)</summary>
        /// <param name="objRequest">/param> Envia objeto tipo request      
        /// <returns>objResponse</returns>
        /// <remarks>SaveChangeMinorClarify</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static SaveChangeMinorClarifyResponse SaveChangeMinorClarify(SaveChangeMinorClarifyRequest objRequest)
        {
            SaveChangeMinorClarifyResponse objResponse = new SaveChangeMinorClarifyResponse();

            string intTeractionid = "";
            string strFlaginsercion = "";
            string strMessage = "";

            objResponse.StrResult = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.ChangeMinor.SaveChangeMinorClarify(objRequest.strSession, objRequest.strTransaction, objRequest.objCliente, out strFlaginsercion, out strMessage);
            });

            objResponse.intTeractionid = intTeractionid;
            objResponse.strFlaginsercion = strFlaginsercion;
            objResponse.strMessage = strMessage;

            return objResponse;
        }


        /// <summary>Método que obtiene los datos del cliente</summary>
        /// <param name="objRequest">/param> Envia objeto tipo request      
        /// <returns>DataCustomerResponse</returns>
        /// <remarks>GetDataCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static DataCustomerResponse GetDataCustomer(DataCustomerRequest objRequest)
        {
            DataCustomerResponse objResponse = new DataCustomerResponse();
            Client objcliente = new Client();
           
            string strMessage = "";

            objResponse.StrResponse = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.Postpaid.GetDataCustomer(objRequest.strIdSession, objRequest.strTransaccion, objRequest.strcustomerid, objRequest.strtelefono, ref objcliente, ref strMessage);
            });

            objResponse.Cliente = objcliente;
            objResponse.Message = strMessage;


            return objResponse;
        }


        /// <summary>Permite obtener los datos adicionales y direccion alternativa de un cliente - Datos menores en la BD de Clarify (TIMPRB)</summary>
        /// <param name="objResponse"></param>   
        /// <returns>objResponse</returns>
        /// <remarks>GetClientDataAdd</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>09/11/2018</FecCrea></item></list>
        public static ClientDataAddResponse GetClientDataAdd(ClientDataAddRequest objRequest)
        {
            ClientDataAddResponse objResponse = new ClientDataAddResponse();
            Client objcliente = new Client();
                        

            objResponse = Web.Logging.ExecuteMethod<ClientDataAddResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.Postpaid.GetClientDataAdd(objRequest);
            });

           
            return objResponse;
        }
        

        /// <summary>Permite Actualizar los datos menores de un cliente (Numeros Fijos) en la BD de Clarify (TIMPRB)</summary>
        /// <param name="objRequest">/param> Envia objeto tipo request      
        /// <returns>objResponse</returns>
        /// <remarks>SaveChangeMinorFijoClarify</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>09/11/2018</FecCrea></item></list>
        public static SaveChangeMinorFijoClarifyResponse SaveChangeMinorFijoClarify(SaveChangeMinorFijoClarifyRequest objRequest)
        {
            SaveChangeMinorFijoClarifyResponse objResponse = new SaveChangeMinorFijoClarifyResponse();

            string intTeractionid = "";
            string strFlaginsercion = "";
            string strMessage = "";

            objResponse.StrResult = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.ChangeMinor.SaveChangeMinorFijoClarify(objRequest.strSession, objRequest.strTransaction, objRequest.objCliente, out strFlaginsercion, out strMessage);
            });

            objResponse.intTeractionid = intTeractionid;
            objResponse.strFlaginsercion = strFlaginsercion;
            objResponse.strMessage = strMessage;

            return objResponse;
        }


        /// <summary>Permite guardar los datos adicionales y direccion alternativa de un cliente - Datos menores en la BD de Clarify (TIMPRB)</summary>
        /// <param name="objRequest">/param> Envia objeto tipo request      
        /// <returns>objResponse</returns>
        /// <remarks>SaveChangeMinorClientDataAdd</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>13/11/2018</FecCrea></item></list>
        public static SaveChangeMinorClientDataAddResponse SaveChangeMinorClientDataAdd(SaveChangeMinorClientDataAddRequest objRequest)
        {
            SaveChangeMinorClientDataAddResponse objResponse = new SaveChangeMinorClientDataAddResponse();

            string intTeractionid = "";
            string strFlaginsercion = "";
            string strMessage = "";

            objResponse.StrResult = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.ChangeMinor.SaveChangeMinorClientDataAdd(objRequest.strSession, objRequest.strTransaction, objRequest.objCliente, out strFlaginsercion, out strMessage);
            });

            objResponse.intTeractionid = intTeractionid;
            objResponse.strFlaginsercion = strFlaginsercion;
            objResponse.strMessage = strMessage;

            return objResponse;
        }
    
    }
}

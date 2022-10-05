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

namespace Claro.SIACU.Data.IFI.Postpaid
{
    public class ChangePostalAddress
    {

        /// <summary> Actualiza los datos de la direccion de facturacion por servicio</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="item"></param>      
        /// <returns>bool</returns>
        /// <remarks>GetSaveChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>22/10/2018.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>12/11/2018.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Actualizar metodo de sp a webservice</Mot></item></list>
        public static bool GetSaveChangePostalAddress(string strIdSession, string strTransaction, Client objItem)
        {
            bool blnRetorno = false;
            try
            {
                Service_Triacion.bscs_cambioDireccionPostal_request Req = new Service_Triacion.bscs_cambioDireccionPostal_request();
                Service_Triacion.bscs_cambioDireccionPostal_response Resp = new Service_Triacion.bscs_cambioDireccionPostal_response();

                if (objItem.CUSTOMER_ID != null)
                    Req.p_CustomerID = Claro.Utils.CheckInt(objItem.CUSTOMER_ID);

                if (objItem.DOMICILIO != null)
                    Req.p_Direccion = objItem.DOMICILIO;

                if (objItem.REFERENCIA != null)
                    Req.p_NotasDireccion = objItem.REFERENCIA;

                if (objItem.DISTRITO != null)
                    Req.p_Distrito = objItem.DISTRITO;

                if (objItem.PROVINCIA != null)
                    Req.p_Provincia = objItem.PROVINCIA;

                if (objItem.ZIPCODE != null)
                    Req.p_CodigoPostal = objItem.ZIPCODE;

                if (objItem.DEPARTAMENTO != null)
                    Req.p_Departamento = objItem.DEPARTAMENTO;

                if (objItem.PAIS_FAC != null)
                    Req.p_Pais = objItem.PAIS_FAC;

                Resp = Claro.Web.Logging.ExecuteMethod<Service_Triacion.bscs_cambioDireccionPostal_response>(strIdSession, strTransaction,
                     () =>
                     {
                         return ServiceConfiguration.ServiceTriacion
                             .cambioDireccionPostal(Req);
                     });
                int result = Resp.p_result;

                if (result == 1)
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
                Web.Logging.Error(strIdSession, strTransaction, "Error WS - desactivarContrato_LTE :" + ex.Message);
                blnRetorno = false;
                throw ex;
            }
            return blnRetorno;

        }
        
    }
}

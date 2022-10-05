using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Wireless.GenerateOCC;
using Claro.SIACU.Entity.IFI.Wireless.GetApadece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.IFI.Wireless
{
    public class RetentionCancelServ
    {

        ///<summary>Servicio que obtiene el contrato desactivado</summary>
        ///<param name="objRequest"></param>
        ///<returns>bool<returns>
        ///<remarks>GetDesactivatedContract_INT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static bool GetDesactivatedContract_INT(Customer objRequest)
        {
            bool resultado = false;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Data.IFI.Wireless.RetentionCancelServ.GetDesactivatedContract_INT(objRequest);
                }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;

        }


        ///<summary>Servicio que genera el OCC</summary>
        ///<param name="objRequest"></param>
        ///<returns>OCCResponse<returns>
        ///<remarks>GenerateOCC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static OCCResponse GenerateOCC(OCCRequest objRequest)
        {
            OCCResponse objOCCResponse = new OCCResponse();
            try
            {
                objOCCResponse.result = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Data.IFI.Wireless.RetentionCancelServ.GenerateOCC(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Audit.IPAddress, objRequest.dcCustomerId, objRequest.dcMonto);
                }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return objOCCResponse;

        }


        ///<summary>Servicio que obtiene el acuerdo para la adquisición de equipos con descuento especial(APADECE)</summary>
        ///<param name="objRequest"></param>
        ///<returns>ApadeceResponse<returns>
        ///<remarks>GetApadece</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static ApadeceResponse GetApadece(ApadeceRequest objRequest)
        {
            ApadeceResponse objApadeceResponse = new ApadeceResponse();
            try
            {
                objApadeceResponse.monto = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Data.IFI.Wireless.RetentionCancelServ.GetApadece(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Audit.IPAddress, objRequest.Audit.ApplicationName, objRequest.Audit.UserName, objRequest.strMsisdn, objRequest.strCoId);
                }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return objApadeceResponse;

        }
    }
}

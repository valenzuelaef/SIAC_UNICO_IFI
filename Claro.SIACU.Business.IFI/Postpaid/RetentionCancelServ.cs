using Claro.SIACU.Entity.IFI.Postpaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class RetentionCancelServ
    {

        /// <summary>Método que obtiene los datos del acuerdo</summary>
        /// <param name="oRequest"></param>
        /// <returns>RetentionCancel</returns>
        /// <remarks>GetDataAccord</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static RetentionCancel GetDataAccord(RetentionCancel oRequest)
        {
            RetentionCancel oResponse = new RetentionCancel();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<RetentionCancel>(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Postpaid.RetentionCancelServ.GetDataAccord(oRequest);
                });
            
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session.ToString(), oRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return oResponse;
        }


        /// <summary>Método que obtiene cargo fijo total del cliente</summary>
        /// <param name="oRequest"></param>
        /// <returns>RetentionCancel</returns>
        /// <remarks>GetLoadStaidTotal</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static RetentionCancel GetLoadStaidTotal(RetentionCancel oRequest)
        {
            RetentionCancel oResponse = new RetentionCancel();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<RetentionCancel>(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Postpaid.RetentionCancelServ.GetLoadStaidTotal(oRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session.ToString(), oRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return oResponse;
        }
    }
}

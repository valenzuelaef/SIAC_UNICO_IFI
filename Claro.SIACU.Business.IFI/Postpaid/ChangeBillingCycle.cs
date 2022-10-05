using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Postpaid.GetBillingCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class ChangeBillingCycle
    {

        ///<summary>Método que obtiene el ciclo de facturación</summary>
        ///<param name="objBillingCycleRequest"></param>
        ///<returns>BillingCycleResponse<returns>
        ///<remarks>GetBillingCycle</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static BillingCycleResponse GetBillingCycle(BillingCycleRequest objBillingCycleRequest)
        {
            BillingCycleResponse objBillingCycleResponse = new BillingCycleResponse();
            objBillingCycleResponse.LstBillingCycleResponse = new List<BillingCycle>();
            List<BillingCycle> lstobjBillingCycle = new List<BillingCycle>();
            try
            {
                lstobjBillingCycle = Claro.Web.Logging.ExecuteMethod<List<BillingCycle>>(objBillingCycleRequest.Audit.Session, objBillingCycleRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.ChangeBillingCycle.GetBillingCycle(objBillingCycleRequest.Audit.Session, objBillingCycleRequest.Audit.Transaction, objBillingCycleRequest.strTypeCustomer);
                });

                objBillingCycleResponse.LstBillingCycleResponse = lstobjBillingCycle;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBillingCycleRequest.Audit.Session, objBillingCycleRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objBillingCycleResponse;
        }
    }
}

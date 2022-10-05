using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesWireless = Claro.SIACU.Entity.IFI.Wireless;
using DataWireless = Claro.SIACU.Data.IFI.Wireless;

namespace Claro.SIACU.Business.IFI.Wireless
{
    public class Wireless
    {
        ///<summary>Servicio que obtiene el contrato desactivado</summary>
        ///<param name="objRequest"></param>
        ///<returns>bool<returns>
        ///<remarks>GetDesactivatedContract_INT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>01/01/2018.</FecCrea></item></list>
        public static EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesWireless.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            var objResponse = new EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return DataWireless.Wireless.GetTransactionScheduled(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vstrCoId, objRequest.vstrCuenta, objRequest.vstrFDesde, objRequest.vstrFHasta, objRequest.vstrEstado, objRequest.vstrAsesor, objRequest.vstrTipoTran, objRequest.vstrCodInter, objRequest.vstrCacDac);
                    });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
    }
}

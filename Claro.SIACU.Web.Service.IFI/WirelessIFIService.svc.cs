using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntitiesWireless = Claro.SIACU.Entity.IFI.Wireless;
using EntitiesCommon = Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Wireless.GenerateOCC;
using Claro.SIACU.Entity.IFI.Wireless.GetApadece;


namespace Claro.SIACU.Web.Service.IFI
{
    public class WirelessTransacService : IWirelessIFIService
    {
        /// <summary>
        /// obtiene transacciones programadas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetListScheduledTransactions</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse GetListScheduledTransactions(EntitiesWireless.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse objResponse = new EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                () =>
                {
                    return Business.IFI.Wireless.Wireless.GetTransactionScheduled(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message.ToString());
            }

            return objResponse;
        }
        /// <summary>
        /// desactiva contrato
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDesactivatedContract_INT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public bool GetDesactivatedContract_INT(Customer objRequest)
        {
            bool resultado = false;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Wireless.RetentionCancelServ.GetDesactivatedContract_INT(objRequest);
                }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;

        }
        /// <summary>
        /// genera OCC
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GenerateOCC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public OCCResponse GenerateOCC(OCCRequest objRequest)
        {
            OCCResponse objOCCResponse = new OCCResponse();
            try
            {
                objOCCResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Wireless.RetentionCancelServ.GenerateOCC(objRequest);
                }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return objOCCResponse;

        }
        /// <summary>
        /// Obtiene apadece
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetApadece</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public ApadeceResponse GetApadece(ApadeceRequest objRequest)
        {
            ApadeceResponse objApadeceResponse = new ApadeceResponse();
            try
            {
                objApadeceResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Wireless.RetentionCancelServ.GetApadece(objRequest);
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

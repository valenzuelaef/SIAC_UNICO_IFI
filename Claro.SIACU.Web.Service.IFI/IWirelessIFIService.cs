using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntitiesWireless = Claro.SIACU.Entity.IFI.Wireless;
using EntitiesCommon = Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Wireless.GenerateOCC;
using Claro.SIACU.Entity.IFI.Wireless.GetApadece;

namespace Claro.SIACU.Web.Service.IFI
{
    [ServiceContract]
    public interface IWirelessIFIService
    {
        [OperationContract]
        EntitiesWireless.GetTransactionScheduled.TransactionScheduledResponse GetListScheduledTransactions(EntitiesWireless.GetTransactionScheduled.TransactionScheduledRequest request);

        [OperationContract]
        bool GetDesactivatedContract_INT(EntitiesCommon.Customer objRequest);
        [OperationContract]
        OCCResponse GenerateOCC(OCCRequest objRequest);
        [OperationContract]
        ApadeceResponse GetApadece(ApadeceRequest objRequest);

    }
}

using System.Collections.Generic;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;

namespace Claro.SIACU.Business.IFI.Fixed
{
    public class ProgramTask
    {

        /// <summary>Método que permite eliminar las tareas programadas.</summary>
        /// <param name="request"></param>
        /// <returns>EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse</returns>
        /// <remarks>GetDeleteScheduledTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse GetDeleteScheduledTransaction(EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionRequest request)
        {
            bool correctProcess = false;

            correctProcess = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
               {
                   return Data.IFI.Fixed.ProgramTask.GetDeleteScheduledTransaction(
                       request.Audit.Session,
                       request.Audit.Transaction,
                       request.strServiCoId,
                       request.codId,
                       request.strServiceState,
                       request.strApplicationCode,
                       request.strApplicationName,
                       request.strUserApp
                 );
               });

            EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse objResponse = new EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse()
            {

                CorrectProcess = correctProcess
            };

            return objResponse;
        }


        /// <summary>Método que permite listar las tareas programadas.</summary>
        /// <param name="request"></param>
        /// <returns>EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse</returns>
        /// <remarks>GetListScheduledTransactions</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse GetListScheduledTransactions(EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsRequest request)
        {
            bool correctProcess = false;

            List<EntitiesFixed.ScheduledTransaction> list = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    return Data.IFI.Fixed.ProgramTask.GetListScheduledTransactions(request.Audit.Session, request.Audit.Transaction,
                        request.IdTransaction, request.ApplicationCode, request.ApplicationName, request.UserApp, request.ServiCoId, request.StrStartDate, request.StrEndDate,
                        request.ServiceState, request.Advisor, request.Account, request.TransactionType, request.CodeInteraction, request.NameCACDAC, out correctProcess);
                });

            EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse objResponse = new EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse()            {
                LstTransactions = list,
                CorrectProcess = correctProcess
            };

            return objResponse;
        }


        /// <summary>Método que permite actualizar las tareas programadas LTE.</summary>
        /// <param name="request"></param>
        /// <returns>EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse</returns>
        /// <remarks>UpdateProgTaskLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse UpdateProgTaskLte(EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteRequest request)
        {
            EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse objResponse = new EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse();

            objResponse.ResultStatus = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Data.IFI.Fixed.ProgramTask.UpdateProgramTaskLte(request.StrIdSession, request.StrTransaction, request.CodigoAplicacion, request.NombreAplicacion, request.UsuarioApp, request.ServiCod, request.ConId, request.ServiEstado, request.FechaProg);
            });

            return objResponse;
        }
    }
}

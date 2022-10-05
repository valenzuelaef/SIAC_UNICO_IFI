using Claro.SIACU.Entity.IFI.Fixed.GetCaseInsert;
using System;
using System.ServiceModel;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;
using EntityCommon = Claro.SIACU.Entity.IFI.Common;

using Claro.SIACU.Entity.IFI.Fixed;
using System.Collections.Generic;

using FUNCTIONS = Claro.Utils;

namespace Claro.SIACU.Web.Service.IFI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FixedTransacService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FixedTransacService.svc or FixedTransacService.svc.cs at the Solution Explorer and start debugging.
    public class FixedTransacService : IFixedIFIService
    {


        /// <summary>
        /// elimina transacciones programadas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetDeleteScheduledTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse GetDeleteScheduledTransaction(EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionRequest request)
        {
            EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.ProgramTask.GetDeleteScheduledTransaction(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }



            return objResponse;
        }
        /// <summary>
        /// obtiene transacciones programadas
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>GetListScheduledTransactions</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse GetListScheduledTransactions(EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsRequest request)
        {
            EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.ProgramTask.GetListScheduledTransactions(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        /// <summary>
        /// obtiene clientes
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetCustomer.CustomerResponse GetCustomer(
            EntitiesFixed.GetCustomer.GetCustomerRequest objRequest)
        {
            EntitiesFixed.GetCustomer.CustomerResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.IFI.Fixed.Fixed.GetCustomer(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        /// <summary>
        /// obtiene clientes clarify
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        ///  <remarks>GetCustomerClf</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetCustomer.CustomerResponse GetCustomerClf(
          EntitiesFixed.GetCustomer.GetCustomerRequest objRequest)
        {
            EntitiesFixed.GetCustomer.CustomerResponse objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(() =>
                        {
                            return Business.IFI.Fixed.Fixed.GetCustomerClf(objRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        #region JCAA








        /// <summary>
        /// obtiene servicios por interaccion
        /// </summary>
        /// <param name="objInteractionServiceRequest"></param>
        /// <returns></returns>
        /// <remarks>GetServicesByInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse
            GetServicesByInteraction(
                Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceRequest
                    objInteractionServiceRequest)
        {
            Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse
                objInteractionServicesResponse = null;
            try
            {
                objInteractionServicesResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction,
                            () =>
                            {
                                return Business.IFI.Fixed.Fixed.GetServicesByInteraction(
                                    objInteractionServiceRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionServiceRequest.Audit.Session,
                    objInteractionServiceRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objInteractionServicesResponse;
        }

        #endregion

        #region RetenciónCancelación
        /// <summary>
        /// obtiene lista de acciones
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetListarAccionesRC</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetListarAccionesRC(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetListarAcciones(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vNivel.ToString(), objRequest.vtransaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene motivo de cancelacion
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetMotCancelacion</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetMotCancelacion(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetMotCancelacion(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vEstado.ToString(), objRequest.vtransaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        ///  obtiene submotivo de cancelacion
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetSubMotiveCancel</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSubMotiveCancel(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetSubMotiveCancel(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vIdMotive.ToString(), objRequest.vtransaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene tipos de trabajo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTypeWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetTypeWork(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetTypeWork(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        /// <summary>
        /// obtiene subtipos de trabajo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetSubTypeWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSubTypeWork(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse =
                new EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.RetentionCancelServ.GetSubTypeWork(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene motivo sot
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetMotiveSOT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetMotiveSOT(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetMotiveSOT(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// agrega un dia de trabajo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetAddDayWork</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetAddDayWork.AddDayWorkResponse GetAddDayWork(
            EntitiesFixed.GetAddDayWork.AddDayWorkRequest objRequest)
        {
            EntitiesFixed.GetAddDayWork.AddDayWorkResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.RetentionCancelServ.GetAddDayWork(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene parametros terminal TPI
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetObtainParameterTerminalTPI</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetObtainParameterTerminalTPI(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ
                            .GetObtainParameterTerminalTPI(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// Obtiene solo TFI postpago
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetSoloTFIPostpago</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSoloTFIPostpago(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetSoloTFIPostpago(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene penalidad 
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetObtainPenalidadExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetObtainPenalidadExt(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetObtainPenalidadExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// Obtener Datos BSCSExt
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ObtenerDatosBSCSExt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse ObtenerDatosBSCSExt(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.ObtenerDatosBSCSExt(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        /// <summary>
        /// valida customer Id
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        ///  <remarks>GetValidateCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse GetValidateCustomerId(
            EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdRequest objRequest)
        {
            EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetValidateCustomerId(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        /// <summary>
        /// registra customer ID
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetRegisterCustomerId</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetCustomer.CustomerResponse GetRegisterCustomerId(
            Entity.IFI.Fixed.Customer objRequest)
        {
            EntitiesFixed.GetCustomer.CustomerResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.RetentionCancelServ.GetRegisterCustomerId(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }




        /// <summary>
        /// GetCaseInsert
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetCaseInsert</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetCaseInsert.CaseInsertResponse GetCaseInsert(
            EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {
            CaseInsertResponse oResponse = new CaseInsertResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.RetentionCancelServ.GetCaseInsert(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return oResponse;
        }
        /// <summary>
        /// obtiene apadece retencion
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetApadeceCancelRet</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetApadeceCancelRet(
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest)
        {
            EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetApadeceCancelRet(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.vNivel.ToString(), objRequest.vtransaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return objResponse;
        }










        /// <summary>
        /// desactiva contrato
        /// </summary>
        /// <param name="objRequestCliente"></param>
        /// <returns></returns>
        /// <remarks>GetDesactivatedContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public bool GetDesactivatedContract(Customer objRequestCliente)
        {
            bool resultado = false;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.RetentionCancelServ
                        .GetDesactivatedContract(objRequestCliente);
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
        /// desactiva contrato
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDesactivatedContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public bool GetDesactivatedContract_LTE(Customer objRequest, ref string msj)
        {
            string message = "";
            bool resultado = false;
            try
            {
                resultado = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.RetentionCancelServ.GetDesactivatedContract_LTE(objRequest, ref message);
                }
                );
                msj = message;
            }

            catch (Exception)
            {
                msj = "Ocurrio Un error en la Transaccion";
                throw;
            }
            return resultado;

        }

        #endregion
        /// <summary>
        /// obtiene telefono por codigo de contrato
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTelephoneByContractCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetService.ServiceResponse GetTelephoneByContractCode(
            EntitiesFixed.GetService.ServiceRequest objRequest)
        {
            EntitiesFixed.GetService.ServiceResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.Fixed.GetTelephoneByContractCode(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }



        #region External/ Internal Transfer



        #endregion


        #region Punto Adicional



        #endregion








        #region "Inst/Desinst Decodificadores"



        /// <summary>
        /// obtiene tipos de trabajo
        /// </summary>
        /// <param name="objJobTypesRequest"></param>
        /// <returns></returns>
        /// <remarks>GetJobTypes</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.GetJobTypes.JobTypesResponse GetJobTypes(
            Entity.IFI.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        {
            Entity.IFI.Fixed.GetJobTypes.JobTypesResponse objJobTypesResponse = null;
            try
            {
                objJobTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                        {
                            return Business.IFI.Fixed.Fixed.GetJobTypes(objJobTypesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction,
                    FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objJobTypesResponse;
        }
        /// <summary>
        /// valida flujo ETA
        /// </summary>
        /// <param name="objPlansRequest"></param>
        /// <returns></returns>
        /// <remarks>ETAFlowValidate</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(
            Entity.IFI.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest)
        {
            Entity.IFI.Fixed.ETAFlowValidate.ETAFlowResponse objPlansResponse = null;
            try
            {
                objPlansResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                        {
                            return Business.IFI.Fixed.Fixed.ETAFlowValidate(objPlansRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objPlansResponse;
        }
        /// <summary>
        /// Obtiene tipo de orden
        /// </summary>
        /// <param name="objOrderTypesRequest"></param>
        /// <returns></returns>
        /// <remarks>GetOrderType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.GetOrderType.OrderTypesResponse GetOrderType(
            Entity.IFI.Fixed.GetOrderType.OrderTypesRequest objOrderTypesRequest)
        {
            Entity.IFI.Fixed.GetOrderType.OrderTypesResponse objOrderTypesResponse = null;
            try
            {
                objOrderTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objOrderTypesRequest.Audit.Session, objOrderTypesRequest.Audit.Transaction, () =>
                        {
                            return Business.IFI.Fixed.Fixed.GetOrderType(objOrderTypesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOrderTypesRequest.Audit.Session, objOrderTypesRequest.Audit.Transaction,
                    FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objOrderTypesResponse;
        }
        /// <summary>
        /// Obtiene subtipo de orden
        /// </summary>
        /// <param name="objSubTypesRequest"></param>
        /// <returns></returns>
        /// <remarks>GetOrderSubType</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(
            Entity.IFI.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest)
        {
            Entity.IFI.Fixed.GetOrderSubType.OrderSubTypesResponse objSubTypesResponse = null;
            try
            {
                objSubTypesResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction, () =>
                        {
                            return Business.IFI.Fixed.Fixed.GetOrderSubType(objSubTypesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSubTypesRequest.Audit.Session, objSubTypesRequest.Audit.Transaction,
                    FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objSubTypesResponse;
        }




        /// <summary>
        /// inserta detalle de servicio interaccion
        /// </summary>
        /// <param name="objInsertDetailServiceInteractionRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInsertDetailServiceInteraction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse
            GetInsertDetailServiceInteraction(
                Entity.IFI.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionRequest
                    objInsertDetailServiceInteractionRequest)
        {
            Entity.IFI.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse
                objInsertDetailServiceInteractionResponse = null;
            try
            {
                objInsertDetailServiceInteractionResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objInsertDetailServiceInteractionRequest.Audit.Session,
                            objInsertDetailServiceInteractionRequest.Audit.Transaction, () =>
                            {
                                return Business.IFI.Fixed.Fixed.GetInsertDetailServiceInteraction(
                                    objInsertDetailServiceInteractionRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertDetailServiceInteractionRequest.Audit.Session,
                    objInsertDetailServiceInteractionRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objInsertDetailServiceInteractionResponse;
        }


        /// <summary>
        /// inserta transaccion
        /// </summary>
        /// <param name="objInsertTransactionRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInsertTransaction</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Fixed.GetInsertTransaction.InsertTransactionResponse GetInsertTransaction(
            Entity.IFI.Fixed.GetInsertTransaction.InsertTransactionRequest objInsertTransactionRequest)
        {
            Entity.IFI.Fixed.GetInsertTransaction.InsertTransactionResponse objInsertTransactionResponse =
                null;
            try
            {
                objInsertTransactionResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            objInsertTransactionRequest.Audit.Session, objInsertTransactionRequest.Audit.Transaction,
                            () =>
                            {
                                return Business.IFI.Fixed.Fixed.GetInsertTransaction(
                                    objInsertTransactionRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertTransactionRequest.Audit.Session,
                    objInsertTransactionRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));

            }
            return objInsertTransactionResponse;
        }





        #endregion
        /// <summary>
        /// inserta interaccion negocio
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInsertInteractionBusiness</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessResponse
            GetInsertInteractionBusiness(
                EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessRequest objRequest)
        {
            EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.Fixed.GetInsertInteractionBusiness(objRequest);
                    });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }






        /// <summary>
        /// valida cliente
        /// </summary>
        /// <param name="oGetCustomerRequest"></param>
        /// <returns></returns>
        /// <remarks>GetValidateCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetCustomer.CustomerResponse GetValidateCustomer(
            EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest)
        {

            EntitiesFixed.GetCustomer.CustomerResponse oCustomerResponse =
                new EntitiesFixed.GetCustomer.CustomerResponse();
            try
            {
                oCustomerResponse = Claro.Web.Logging.ExecuteMethod(oGetCustomerRequest.Audit.Session,
                    oGetCustomerRequest.Audit.Transaction, () =>
                    {
                        return Business.IFI.Fixed.Fixed.GetValidateCustomer(oGetCustomerRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction,
                    FUNCTIONS.GetExceptionMessage(ex));
            }

            return oCustomerResponse;
        }




        /// <summary>
        /// crea caso
        /// </summary>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        ///  <remarks>GetCreateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.Interaction GetCreateCase(EntitiesFixed.Interaction oRequest)
        {

            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Fixed.Fixed.GetCreateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }
        /// <summary>
        /// inserta caso
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>GetInsertCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.Interaction GetInsertCase(
            EntitiesFixed.Interaction oItem)
        {

            Interaction oResponse = new Interaction();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                    {
                        return Business.IFI.Fixed.Fixed.GetInsertCase(oItem);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }
        /// <summary>
        /// inserta plantilla caso
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>GetInsertTemplateCase</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.CaseTemplate GetInsertTemplateCase(
            EntitiesFixed.CaseTemplate oItem)
        {

            CaseTemplate oResponse = new CaseTemplate();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                    {
                        return Business.IFI.Fixed.Fixed.GetInsertTemplateCase(oItem);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }
        /// <summary>
        /// inserta plantilla caso contingencia
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>GetInsertTemplateCaseContingent</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.CaseTemplate GetInsertTemplateCaseContingent(
            EntitiesFixed.CaseTemplate oItem)
        {

            CaseTemplate oResponse = new CaseTemplate();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                    {
                        return Business.IFI.Fixed.Fixed.GetInsertTemplateCaseContingent(oItem);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }
        /// <summary>
        /// Actualiza Plantilla Caso
        /// </summary>
        /// <param name="oItem"></param>
        /// <returns></returns>
        /// <remarks>ActualizaPlantillaCaso</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.CaseTemplate ActualizaPlantillaCaso(
    EntitiesFixed.CaseTemplate oItem)
        {

            CaseTemplate oResponse = new CaseTemplate();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(
                    oItem.Audit.Session, oItem.Audit.Transaction, () =>
                    {
                        return Business.IFI.Fixed.Fixed.ActualizaPlantillaCaso(oItem);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }




        /// <summary>
        /// consulta servicios por contrato
        /// </summary>
        /// <param name="oConsultationServiceByContractRequest"></param>
        /// <returns></returns>
        /// <remarks>GetConsultationServiceByContract</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse
            GetConsultationServiceByContract(
                EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractRequest
                    oConsultationServiceByContractRequest)
        {
            EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse
                oConsultationServiceByContractResponse;

            try
            {
                oConsultationServiceByContractResponse = Claro.Web.Logging
                    .ExecuteMethod(() =>
                    {
                        return Business.IFI.Fixed.Fixed.GetConsultationServiceByContract(
                            oConsultationServiceByContractRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oConsultationServiceByContractRequest.Audit.Session,
                    oConsultationServiceByContractRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return oConsultationServiceByContractResponse;
        }


        /// <summary>
        /// obtiene motivo SOT por tipo de trabajo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetMotiveSOTByTypeJob</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse GetMotiveSOTByTypeJob(
            EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobRequest objRequest)
        {
            EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse oResponse =
                new EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.IFI.Fixed.RetentionCancelServ.GetMotiveSOTByTypeJob(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }


        /// <summary>
        /// obtiene transacciones programadas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTransactionScheduled</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse oResponse = new EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Fixed.Fixed.GetTransactionScheduled(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return oResponse;
        }




        /// <summary>
        /// Ejecuta Suspension De Servicio
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>EjecutaSuspensionDeServicioCodRes</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest)
        {

            var objResponse = new EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.IFI.Fixed.SuspensionService.EjecutaSuspensionDeServicioCodRes(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// reconecta servicios
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetReconectionService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetReconeService.ReconeServiceResponse GetReconectionService(EntitiesFixed.GetReconeService.ReconeServiceRequest objRequest)
        {

            var objResponse = new EntitiesFixed.GetReconeService.ReconeServiceResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.IFI.Fixed.SuspensionService.GetReconectionService(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// obtiene linea de cliente
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetCustomerLineNumber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse GetCustomerLineNumber(EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractRequest objRequest)
        {
            EntitiesFixed.GetConsultationServiceByContract.ConsultationServiceByContractResponse model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.IFI.Fixed.Fixed.GetCustomerLineNumber(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return model;
        }
        /// <summary>
        /// obtiene interaccion por caso id
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetInteractIDforCaseID</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.CaseInsertResponse model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.IFI.Fixed.Fixed.GetInteractIDforCaseID(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session,
                    objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return model;
        }
        /// <summary>
        /// actualiza tareas programadas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>UpdateProgTaskLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse UpdateProgTaskLte(EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteRequest objRequest)
        {
            EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse model;

            try
            {
                model = Claro.Web.Logging.ExecuteMethod(() => { return Business.IFI.Fixed.ProgramTask.UpdateProgTaskLte(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.GetExceptionMessage(ex));
            }
            return model;
        }
        /// <summary>
        /// Ejecuta Suspension De Servicio Lte
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>EjecutaSuspensionDeServicioLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest)
        {

            var objResponse = new EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.IFI.Fixed.SuspensionService.EjecutaSuspensionDeServicioLte(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// Ejecuta Reconexion De Servicio Lte
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>EjecutaReconexionDeServicioLte</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest)
        {

            var objResponse = new EntitiesFixed.PostReconexionLte.ReconexionLteResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Business.IFI.Fixed.SuspensionService.EjecutaReconexionDeServicioLte(objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// actualiza campo inter29
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetUpdateInter29</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetUpdateInter29.UpdateInter29Response GetUpdateInter29(
          EntitiesFixed.GetUpdateInter29.UpdateInter29Request objRequest)
        {
            EntitiesFixed.GetUpdateInter29.UpdateInter29Response objResponse;
            try
            {
                objResponse =
                    Claro.Web.Logging
                        .ExecuteMethod(
                            () =>
                            {
                                return Business.IFI.Fixed.SuspensionService.GetUpdateInter29(objRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        #region ConsultarPCRF
        /// <summary>
        /// Obtener Telefonos Cliente LTE
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ObtenerTelefonosClienteLTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ObtenerTelefonosClienteLTE(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest)
        {
            EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse objPCRFSuscriberQuota = new EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse();
            try
            {
                objPCRFSuscriberQuota = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Fixed.PCRFConsultation.ObtenerTelefonosClienteLTE(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objPCRFSuscriberQuota;
        }

        /// <summary>
        /// Consultar PCRF cuota
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ConsultarPCRFSuscriber_Quota</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFSuscriber_Quota(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest)
        {
            EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse objPCRFSuscriberQuota = new EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse();
            try 
            {
                objPCRFSuscriberQuota = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Fixed.PCRFConsultation.ConsultarPCRFSuscriber_Quota(objRequest);
                });
            }
            catch(Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objPCRFSuscriberQuota;
        }
        /// <summary>
        /// Consultar PCRF 
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ConsultarPCRFSuscriber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFSuscriber(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest)
        {
            EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse objPCRFSuscriberQuota = new EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse();
            try
            {
                objPCRFSuscriberQuota = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.IFI.Fixed.PCRFConsultation.ConsultarPCRFSuscriber(objRequest);
                });
            }
            catch(Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
           
            return objPCRFSuscriberQuota;
        }

        public EntitiesFixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicBodyResponse ConsultarPCRFPaquetesAdic(EntitiesFixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicRequest objRequest)
        {
            EntitiesFixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicBodyResponse objResponse = new EntitiesFixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicBodyResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Business.IFI.Fixed.PCRFConsultation.ConsultarPCRFPaquetesAdic(objRequest);
            });
            return objResponse;
        }

    
        public EntitiesFixed.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse ConsultarClaroPuntos(EntitiesFixed.GetConsultarClaroPuntos.ConsultarClaroPuntosRequest objRequest)
        {
            EntitiesFixed.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse objResponse = new EntitiesFixed.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Business.IFI.Fixed.PackagePurchaseService.ConsultarClaroPuntos(objRequest);
            });
            return objResponse;
        }
        public EntitiesFixed.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(EntitiesFixed.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesRequest objRequest)
        {
            EntitiesFixed.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse objResponse = new EntitiesFixed.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Business.IFI.Fixed.PackagePurchaseService.ConsultarPaqDisponibles(objRequest);
            });
            return objResponse;
        }

        //public EntitiesFixed.GetComprarPaquetes.ComprarPaquetesResponse ComprarPaquetes(EntitiesFixed.GetComprarPaquetes.ComprarPaquetesRequest objRequest)
        //{
        //    EntitiesFixed.GetComprarPaquetes.ComprarPaquetesResponse objResponse = new EntitiesFixed.GetComprarPaquetes.ComprarPaquetesResponse();
        //    objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
        //    {
        //        return Claro.SIACU.Business.IFI.Fixed.PackagePurchaseService.ComprarPaquetes(objRequest);
        //    });
        //    return objResponse;
        //}

        public EntitiesFixed.GetComprarPaquetes.ComprarPaquetesBodyResponse ComprarPaquetesRest(EntitiesFixed.GetComprarPaquetes.ComprarPaquetesMessageRequest objRequest)
        {
            EntitiesFixed.GetComprarPaquetes.ComprarPaquetesBodyResponse objResponse = new EntitiesFixed.GetComprarPaquetes.ComprarPaquetesBodyResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Business.IFI.Fixed.PackagePurchaseService.ComprarPaquetesRest(objRequest);
            });
            return objResponse;
        }
        public EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFDegradacion(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest)
        {
            EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse objResponse = new EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse();

                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                 () =>
                 {
                     return Claro.SIACU.Business.IFI.Fixed.PackagePurchaseService.ConsultarPCRFDegradacion(objRequest);
                 });

            return objResponse;
        }
        public List<Entity.IFI.Common.Client> GetDatosporNroDocumentos(string strIdSession, string strTransaction, string strTipDoc, string strDocumento, string strEstado)
        {
            var objResponse = new List<Entity.IFI.Common.Client>();
            objResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strTransaction,
             () =>
             {
                 return Business.IFI.Fixed.PackagePurchaseService.GetDatosporNroDocumentos(strIdSession, strTransaction, strTipDoc, strDocumento, strEstado);
             });

            return objResponse;
        }     
        #endregion

    }
}


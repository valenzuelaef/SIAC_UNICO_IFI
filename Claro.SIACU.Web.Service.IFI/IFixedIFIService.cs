using Claro.SIACU.Entity.IFI.Fixed;
using System.ServiceModel;
using EntitiesFixed = Claro.SIACU.Entity.IFI.Fixed;

using System.Collections.Generic;
using EntityCommon = Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Fixed.GetConsultationServiceByContract;




namespace Claro.SIACU.Web.Service.IFI
{
    [ServiceContract]
    public interface IFixedIFIService
    {
        [OperationContract]
        EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionResponse GetDeleteScheduledTransaction(EntitiesFixed.GetDeleteScheduledTransaction.DeleteScheduledTransactionRequest request);

        [OperationContract]
        EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse GetListScheduledTransactions(EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsRequest request);
        [OperationContract]
        EntitiesFixed.GetCustomer.CustomerResponse GetCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest objRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceResponse GetServicesByInteraction(Entity.IFI.Fixed.GetServicesByInteraction.InteractionServiceRequest objInteractionServiceRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetJobTypes.JobTypesResponse GetJobTypes(Entity.IFI.Fixed.GetJobTypes.JobTypesRequest objJobTypesRequest);
        [OperationContract]
        Entity.IFI.Fixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(Entity.IFI.Fixed.ETAFlowValidate.ETAFlowRequest objPlansRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetOrderType.OrderTypesResponse GetOrderType(Entity.IFI.Fixed.GetOrderType.OrderTypesRequest objOrderTypesRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(Entity.IFI.Fixed.GetOrderSubType.OrderSubTypesRequest objSubTypesRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetListarAccionesRC(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetMotCancelacion(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSubMotiveCancel(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetTypeWork(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSubTypeWork(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetMotiveSOT(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetAddDayWork.AddDayWorkResponse GetAddDayWork(EntitiesFixed.GetAddDayWork.AddDayWorkRequest objRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetObtainParameterTerminalTPI(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetSoloTFIPostpago(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetObtainPenalidadExt(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesResponse ObtenerDatosBSCSExt(Entity.IFI.Fixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse GetValidateCustomerId(EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetCustomer.CustomerResponse GetRegisterCustomerId(Customer objRequest);
        [OperationContract]
        EntitiesFixed.GetCaseInsert.CaseInsertResponse GetCaseInsert(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesResponse GetApadeceCancelRet(EntitiesFixed.GetRetentionCancelServices.RetentionCancelServicesRequest objRequest);
        [OperationContract]
        bool GetDesactivatedContract(Customer objRequestCliente);
        [OperationContract]
        EntitiesFixed.GetService.ServiceResponse GetTelephoneByContractCode(EntitiesFixed.GetService.ServiceRequest objRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse GetInsertDetailServiceInteraction(Entity.IFI.Fixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionRequest objInsertDetailServiceInteractionRequest);
        [OperationContract]
        Entity.IFI.Fixed.GetInsertTransaction.InsertTransactionResponse GetInsertTransaction(Entity.IFI.Fixed.GetInsertTransaction.InsertTransactionRequest objInsertTransactionRequest);
        [OperationContract]
        EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessResponse GetInsertInteractionBusiness(EntitiesFixed.GetInsertInteractionBusiness.InsertInteractionBusinessRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetCustomer.CustomerResponse GetValidateCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest);
        [OperationContract]
        Interaction GetCreateCase(Interaction oRequest);
        [OperationContract]
        Interaction GetInsertCase(Interaction oItem);
        [OperationContract]
        CaseTemplate GetInsertTemplateCase(CaseTemplate oItem);
        [OperationContract]
        CaseTemplate GetInsertTemplateCaseContingent(CaseTemplate oItem);
        [OperationContract]
        CaseTemplate ActualizaPlantillaCaso(CaseTemplate oItem);
        [OperationContract]
        ConsultationServiceByContractResponse GetConsultationServiceByContract(ConsultationServiceByContractRequest oConsultationServiceByContractRequest);
        [OperationContract]
        EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse GetMotiveSOTByTypeJob(EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest);
        [OperationContract]
        bool GetDesactivatedContract_LTE(Customer objRequest,ref string  message);
        [OperationContract]
        EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetReconeService.ReconeServiceResponse GetReconectionService(EntitiesFixed.GetReconeService.ReconeServiceRequest objRequest);
        [OperationContract]
        ConsultationServiceByContractResponse GetCustomerLineNumber(ConsultationServiceByContractRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest);
        [OperationContract]
        EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse UpdateProgTaskLte(EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteRequest objRequest);
        [OperationContract]
        EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest);
        [OperationContract]
        EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetUpdateInter29.UpdateInter29Response GetUpdateInter29(EntitiesFixed.GetUpdateInter29.UpdateInter29Request objRequest);

        [OperationContract]
        EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ObtenerTelefonosClienteLTE(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFSuscriber_Quota(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFSuscriber(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest);
        [OperationContract]        
        EntitiesFixed.GetCustomer.CustomerResponse GetCustomerClf(EntitiesFixed.GetCustomer.GetCustomerRequest objRequest);
        //INI -RF-02 - RF-04 - RF-05 Evalenzs
        [OperationContract]
        EntitiesFixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicBodyResponse ConsultarPCRFPaquetesAdic(EntitiesFixed.PostPCRFPaquetesAdic.PCRFPaquetesAdicRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse ConsultarClaroPuntos(EntitiesFixed.GetConsultarClaroPuntos.ConsultarClaroPuntosRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(EntitiesFixed.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesRequest objRequest);
        //[OperationContract]
        //EntitiesFixed.GetComprarPaquetes.ComprarPaquetesResponse ComprarPaquetes(EntitiesFixed.GetComprarPaquetes.ComprarPaquetesRequest objRequest);
        [OperationContract]
        EntitiesFixed.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFDegradacion(EntitiesFixed.GetPCRFConsultation.PCRFConnectorRequest objRequest);
        [OperationContract]
        List<Entity.IFI.Common.Client> GetDatosporNroDocumentos(string strIdSession, string strTransaction, string strTipDoc, string strDocumento, string strEstado);
        
        //20201028 CAYCHOJJ
        [OperationContract]
        EntitiesFixed.GetComprarPaquetes.ComprarPaquetesBodyResponse ComprarPaquetesRest(EntitiesFixed.GetComprarPaquetes.ComprarPaquetesMessageRequest objRequest);
    }
}

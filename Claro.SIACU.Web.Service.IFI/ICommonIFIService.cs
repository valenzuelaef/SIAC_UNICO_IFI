using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Common.GetGenerateConstancy;
using Claro.SIACU.Entity.IFI.Common.GetUpdateInter30;
using Claro.SIACU.Entity.IFI.Common.GetUploadDocumentOnBase;
using Claro.SIACU.Entity.IFI.Common.GetValidateMail;
using Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi;
using System.Collections.Generic;
using System.ServiceModel;
using COMMON = Claro.SIACU.Entity.IFI.Common;

namespace Claro.SIACU.Web.Service.IFI
{
    [ServiceContract]
    public interface ICommonIFIService
    {
        [OperationContract]
        COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetInsertBusinnesInteraction2(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request);
        [OperationContract]
        Entity.IFI.Common.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(Entity.IFI.Common.GetInsertEvidence.InsertEvidenceRequest objRequest);
        [OperationContract]
        Entity.IFI.Common.GetMotiveSot.MotiveSotResponse GetMotiveSot(Entity.IFI.Common.GetMotiveSot.MotiveSotRequest objMotiveSotRequest);
        [OperationContract]
        bool ValidateSchedule(Claro.SIACU.Entity.IFI.Common.GetSchedule.ScheduleRequest objScheduleRequest);
        [OperationContract]
        Entity.IFI.Common.GetSaveAudit.SaveAuditResponse SaveAudit(Entity.IFI.Common.GetSaveAudit.SaveAuditRequest objGrabarAuditReq);
        [OperationContract]
        COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyPDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request);
        [OperationContract]
        COMMON.GetInsertLogTrx.InsertLogTrxResponse InsertLogTrx(COMMON.GetInsertLogTrx.InsertLogTrxRequest request);
        [OperationContract]
        COMMON.GetParameterData.ParameterDataResponse GetParameterData(COMMON.GetParameterData.ParameterDataRequest request);
        [OperationContract]
        Entity.IFI.Common.GetBusinessRules.BusinessRulesResponse GetBusinessRules(Entity.IFI.Common.GetBusinessRules.BusinessRulesRequest objBusinessRulesRequest);
        [OperationContract]
        Entity.IFI.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.IFI.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetTypification.TypificationResponse GetTypification(Entity.IFI.Common.GetTypification.TypificationRequest objTypificationRequest);
        [OperationContract]
        COMMON.GetInsertInt.InsertIntResponse InsertInt(COMMON.GetInsertInt.InsertIntRequest objrequest);
        [OperationContract]
        COMMON.GetClient.ClientResponse GetObClient(COMMON.GetClient.ClientRequest objrequest);
        [OperationContract]
        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse GetInsertInteractionTemplate(COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest objresquest);
        [OperationContract]
        COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse GetInsInteractionTemplate(COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest objrequest);
        [OperationContract]
        COMMON.GetInsertInteract.InsertInteractResponse InsertInteract(COMMON.GetInsertInteract.InsertInteractRequest objrequest);
        [OperationContract]
        COMMON.GetInsertGeneral.InsertGeneralResponse GetinsertInteractionGeneral(COMMON.GetInsertGeneral.InsertGeneralRequest request);
        [OperationContract]
        COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse GetinsertInteractionTemplateGeneral(COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest request);
        [OperationContract]
        Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMResponse SaveAuditM(Claro.SIACU.Entity.IFI.Common.GetSaveAuditM.SaveAuditMRequest objRegAuditReq);
        [OperationContract]
        COMMON.GeneratePDF.GeneratePDFDataResponse GeneratePDF(COMMON.GeneratePDF.GeneratePDFDataRequest objPlansRequest);
        [OperationContract]
        Claro.SIACU.Entity.IFI.Common.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(Claro.SIACU.Entity.IFI.Common.GetInsertInteractHFC.InsertInteractHFCRequest objRegAuditReq);
        [OperationContract]
        Claro.SIACU.Entity.IFI.Common.GetInsertInteract.InsertInteractResponse GetInsertInteract(Claro.SIACU.Entity.IFI.Common.GetInsertInteract.InsertInteractRequest objRegAuditReq);
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmail(COMMON.GetSendEmail.SendEmailRequest objrequest);

        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmailAlt(COMMON.GetSendEmail.SendEmailRequest objresuqest);
        [OperationContract]
        COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse GetfileDefaultImpersonation(COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest objrequest);
        [OperationContract]
        COMMON.GetDatTempInteraction.DatTempInteractionResponse GetInfoInteractionTemplate(COMMON.GetDatTempInteraction.DatTempInteractionRequest objRequest);
        [OperationContract]
        COMMON.GetNumberGWP.NumberGWPResponse GetNumberGWP(COMMON.GetNumberGWP.NumberGWPRequest objRequest);
        [OperationContract]
        COMMON.GetNumberEAI.NumberEAIResponse GetNumberEAI(COMMON.GetNumberEAI.NumberEAIRequest objRequest);
        [OperationContract]
        COMMON.GetEmployeByUser.EmployeeResponse GetEmployeByUser(COMMON.GetEmployeByUser.EmployeeRequest objEmployeeRequest);
        [OperationContract]
        COMMON.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUser(COMMON.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest);
        [OperationContract]
        COMMON.CheckingUser.CheckingUserResponse CheckingUser(COMMON.CheckingUser.CheckingUserRequest objCheckingUserRequest);
        [OperationContract]
        COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest);
        [OperationContract]
        COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount_DCM(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest);
        [OperationContract]
        COMMON.GetWorkType.WorkTypeResponse GetWorkType(COMMON.GetWorkType.WorkTypeRequest objWorkTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationResponse ValidateRedirectCommunication(Entity.IFI.Common.GetValidateCommunication.ValidateCommunicationRequest objValidateCommunicationRequest);
        [OperationContract]
        COMMON.GetUser.UserResponse GetUser(COMMON.GetUser.UserRequest objRequest);
        [OperationContract]
        COMMON.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(COMMON.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest);
        [OperationContract]
        COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(
            COMMON.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest);
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmailFixed(COMMON.GetSendEmail.SendEmailRequest objrequest);
        [OperationContract]
        COMMON.GetSendEmail.SendEmailResponse GetSendEmailAltFixed(COMMON.GetSendEmail.SendEmailRequest objrequest);
     
        [OperationContract]
        COMMON.GetStateType.StateTypeResponse GetStateType(COMMON.GetStateType.StateTypeRequest objStateTypeRequest);

        [OperationContract]
        COMMON.GetTransactionType.TransactionTypeResponse GetTransactionType(COMMON.GetTransactionType.TransactionTypeRequest objTransactionTypeRequest);
        [OperationContract]
        UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest);
        [OperationContract]
        Entity.IFI.Common.GetEstCivType.EstCivTypeResponse GetEstCivType(Entity.IFI.Common.GetEstCivType.EstCivTypeRequest objEstCivTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetViasType.ViasTypeResponse GetViasType(Entity.IFI.Common.GetViasType.ViasTypeRequest objViasTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetManzanasType.ManzanasTypeResponse GetManzanasType(Entity.IFI.Common.GetManzanasType.ManzanasTypeRequest objManzanasTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetInterioresType.InterioresTypeResponse GetInterioresType(Entity.IFI.Common.GetInterioresType.InterioresTypeRequest objInterioresTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetUrbsType.UrbsTypeResponse GetUrbsType(Entity.IFI.Common.GetUrbsType.UrbsTypeRequest objUrbsTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetZonesType.ZonesTypeResponse GetZonesType(Entity.IFI.Common.GetZonesType.ZonesTypeRequest objZonesTypeRequest);
        [OperationContract]
        Entity.IFI.Common.GetUbigeosType.UbigeosTypeResponse GetUbigeosType(Entity.IFI.Common.GetUbigeosType.UbigeosTypeRequest objUbigeosTypeRequest,int dep,int prov);
        [OperationContract]
        Entity.IFI.Common.GetNacType.NacTypeResponse GetNacType(Entity.IFI.Common.GetNacType.NacTypeRequest objNacTypeRequest);
        [OperationContract]
        ValidateMailResponse GetValidateMail(ValidateMailRequest objRequest);
        [OperationContract]
        Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSResponse GetNumberSMS(Claro.SIACU.Entity.IFI.Common.GetNumberSMS.NumberSMSRequest objRequest);
        [OperationContract]
        Entity.IFI.Common.GetLastInvoiceData.LastInvoiceDataResponse GetLastInvoiceData(Entity.IFI.Common.GetLastInvoiceData.LastInvoiceDataRequest objLastInvoiceDataRequest, string strCustomerCode);
        [OperationContract]
        Entity.IFI.Common.IsOkGetKey.IsOkGetKeyResponse IsOkGetKey(COMMON.IsOkGetKey.IsOkGetKeyRequest objIsOkGetKeyRequest);
        [OperationContract]
        UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest);
        [OperationContract]
        GenerateConstancyResponse GetConstancyPDFWithOnbase(GenerateConstancyRequest request);
        [OperationContract]
        COMMON.GetSequenceCode.SequenceCodeResponse GetSequenceCode(COMMON.GetSequenceCode.SequenceCodeRequest request);

        [OperationContract]
        COMMON.GetQueuesCase.QueuesCaseResponse GetQueuesCase(COMMON.GetQueuesCase.QueuesCaseRequest objQueuesCaseRequest);

        [OperationContract]
        COMMON.GetCaseLa.GetCaseLaResponse GetCaseLa(COMMON.GetCaseLa.GetCaseLaRequest objCaseLaRequest);

        [OperationContract]
        COMMON.GetSendEmailWithBase64.SendEmailWithBase64Response SendEmailWithBase64(COMMON.GetSendEmailWithBase64.SendEmailWithBase64Request objSendEmailWithBase64Request);

        [OperationContract]
        COMMON.GetDigitalSignature.DigitalSignatureResponse FirmarDocumento(COMMON.GetDigitalSignature.DigitalSignatureRequest objDigitalSignatureRequest);

        [OperationContract]
        COMMON.GetConsultIGV.ConsultIGVResponse GetConsultIGV(COMMON.GetConsultIGV.ConsultIGVRequest objRequest);

        [OperationContract]
        COMMON.GetOffice.OfficeResponse GetOffice(COMMON.GetOffice.OfficeRequest objOfficeRequest);

        [OperationContract]
        COMMON.GetVerifyUser.VerifyUserResponse GetVerifyUser(COMMON.GetVerifyUser.VerifyUserRequest objRequest);

        [OperationContract]
        COMMON.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(COMMON.GetPagOptionXuser.PagOptionXuserRequest objRequest);
                
        [OperationContract]
        COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoResponse GetPaquetesAdquiridosHistorico(COMMON.GetPaquetesAdquiridosHistorico.PaquetesAdquiridosHistoricoRequest objRequest, string strMsisdn, string strSncode);
   
        [OperationContract]
        List<ObtenerTipoTecnologia> obtenerTipoTecnologia(string strIdSession, string strTransaction, string strParamGrupo);
    }
}

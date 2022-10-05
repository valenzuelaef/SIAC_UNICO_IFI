using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid.GetTypeTransactionBRMS;

using POSTPAID = Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinor;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorClarify;
using Claro.SIACU.Entity.IFI.Postpaid.GetActivateServiceMail;
using Claro.SIACU.Entity.IFI.Postpaid.GetLinesTelephone;
using Claro.SIACU.Entity.IFI.Postpaid.GetDataCustomer;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddress;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddressClarify;
using Claro.SIACU.Entity.IFI.Postpaid.GetStatedebt;
using Claro.SIACU.Entity.IFI.Postpaid.GetDuplicateReceipts;
using Claro.SIACU.Entity.IFI.Postpaid.GetTestSecurity;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorFijoClarify;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorClientDataAdd;
using Claro.SIACU.Entity.IFI.Postpaid.GetClientDataAdd;
using Claro.SIACU.Entity.IFI.Postpaid.GetBillingCycle;
using Claro.SIACU.Entity.IFI.Postpaid.ServiceLock;
using Claro.SIACU.Entity.IFI.Postpaid.CurrentLock;
using Claro.SIACU.Entity.IFI.Postpaid.GetLockUnlockEquipment;
using Claro.SIACU.Entity.IFI.Postpaid.InsertLockLinePer;
using Claro.SIACU.Entity.IFI.Postpaid.InsertLockEquipmentPer;
using Claro.SIACU.Entity.IFI.Postpaid.GetImeis;
using Claro.SIACU.Entity.IFI.Postpaid.UnlockService;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockLineCode;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipmentCode;
using Claro.SIACU.Entity.IFI.Postpaid.DeleteLockLine;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockLineRollback;
using Claro.SIACU.Entity.IFI.Postpaid.InsertTerminalLockUnlockEquipment;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipment;
using Claro.SIACU.Entity.IFI.Postpaid.DeleteLockEquipment;
using Claro.SIACU.Entity.IFI.Fixed;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeServiceAddress;
using Claro.SIACU.Entity.IFI.Postpaid.GetCoberturaIfi;

namespace Claro.SIACU.Web.Service.IFI
{
    [ServiceContract]
    public interface IPostIFIService
    {

        [OperationContract]
        POSTPAID.GetDataLine.DataLineResponse GetDataLine(POSTPAID.GetDataLine.DataLineRequest objRequest);
        [OperationContract]
        POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse GetAmountIncomingCall(POSTPAID.GetAmountIncomingCall.AmountIncomingCallRequest objRequest);
        [OperationContract]
        TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest);
        [OperationContract]
        SaveChangeMinorResponse SaveChangeMinor(SaveChangeMinorRequest objRequest);
        [OperationContract]
        SaveChangeMinorClarifyResponse SaveChangeMinorClarify(SaveChangeMinorClarifyRequest objRequest);
        [OperationContract]
        GetActivateServiceMailResponse GetActivateServiceMail(GetActivateServiceMailRequest objRequest);
        [OperationContract]
        GetLinesTelephoneResponse GetLinesTelephone(GetLinesTelephoneRequest objRequest);
        [OperationContract]
        POSTPAID.GetDataCustomer.DataCustomerResponse GetDataCustomer(POSTPAID.GetDataCustomer.DataCustomerRequest objRequest);
        [OperationContract]
        SaveDuplicateReceiptsResponse SaveDuplicateReceipts(SaveDuplicateReceiptsRequest objRequest);
        [OperationContract]
        TestSecurityResponse GetTestSecurity(TestSecurityRequest objRequest);
        [OperationContract]
        POSTPAID.GetSaveChangePostalAddress.SaveChangePostalAddressResponse GetSaveChangePostalAddress(POSTPAID.GetSaveChangePostalAddress.SaveChangePostalAddressRequest objSaveChangePostalAddressRequest);
        [OperationContract]
        GetStatedebtResponse GetStatedebt(GetStatedebtRequest objRequest);
        [OperationContract]
        SaveChangeMinorFijoClarifyResponse SaveChangeMinorFijoClarify(SaveChangeMinorFijoClarifyRequest objRequest);
        [OperationContract]
        SaveChangeMinorClientDataAddResponse SaveChangeMinorClientDataAdd(SaveChangeMinorClientDataAddRequest objRequest);
        [OperationContract]
        ClientDataAddResponse GetClientDataAdd(ClientDataAddRequest objRequest);
        [OperationContract]
        BillingCycleResponse GetBillingCycle(BillingCycleRequest objRequest);
        [OperationContract]
        ServiceLockResponse GetServiceLock(ServiceLockRequest objRequest);
        [OperationContract]
        CurrentLockResponse CurrentBlock(CurrentLockRequest objRequest);
        [OperationContract]
        LockUnlockEquipmentResponse GetEquipmentLock(LockUnlockEquipmentRequest objRequest);
        [OperationContract]
        LockUnlockEquipmentResponse GetEquipmentUnLock(LockUnlockEquipmentRequest objRequest);
        [OperationContract]
        InsertLockLinePerResponse InsertLockLinePer(InsertLockLinePerRequest objRequest);
        [OperationContract]
        InsertLockEquipmentPerResponse InsertLockEquipmentPer(InsertLockEquipmentPerRequest objRequest);
        [OperationContract]
        ImeisResponse GetImeis(ImeisRequest objRequest);
        [OperationContract]
        UnlockServiceResponse GetUnlockService(UnlockServiceRequest objRequest);
        [OperationContract]
        UpdateUnlockLineCodeResponse UpdateUnlockLineCode(UpdateUnlockLineCodeRequest objRequest);
        [OperationContract]
        UpdateUnlockEquipmentCodeResponse UpdateUnlockEquipmentCode(UpdateUnlockEquipmentCodeRequest objRequest);
        [OperationContract]
        DeleteLockLineResponse DeleteLockLine(DeleteLockLineRequest objRequest);
        [OperationContract]
        UpdateUnlockLineRollbackResponse UpdateUnlockLineRollback(UpdateUnlockLineRollbackRequest objRequest);
        [OperationContract]
        InsertTerminalLockUnlockEquipmentResponse InsertTerminalLockUnlockEquipment(InsertTerminalLockUnlockEquipmentRequest objRequest);
        [OperationContract]
        UpdateUnlockEquipmentResponse UpdateUnlockEquipment(UpdateUnlockEquipmentRequest objRequest);
        [OperationContract]
        DeleteLockEquipmentResponse DeleteLockEquipment(DeleteLockEquipmentRequest objRequest);

        [OperationContract]
        POSTPAID.GetPlans.PlansResponse GetPlans(POSTPAID.GetPlans.PlansRequest objRequest);

        [OperationContract]
        POSTPAID.GetServicesByPlan.PlanServiceResponse GetServicesByPlan(POSTPAID.GetServicesByPlan.PlanServiceRequest objRequest);

        [OperationContract]
        POSTPAID.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse GetServicesByCurrentPlan(POSTPAID.GetServicesByCurrentPlan.ServicesByCurrentPlanRequest objRequest);

        [OperationContract]
        POSTPAID.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanResponse GetEquipmentByCurrentPlan(POSTPAID.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanRequest objRequest);

        [OperationContract]
        POSTPAID.PostPlanMigration.PlanMigrationResponse PostPlanMigration(POSTPAID.PostPlanMigration.PlanMigrationRequest objRequest);

        [OperationContract]
        List<GenericItem> obtenerParametrosPorTipo(string strIdSession, string strTransaccion);
        /// <summary>
        /// Permite registrar o actualizar dirección del servicio IFI.
        /// </summary>
        /// <param name="oheaderRequest">Información de cabecera.</param>
        /// <param name="oregistrarDireccionRequest">Información del cambio de dirección IFI.</param>
        /// <returns></returns>
        [OperationContract]
        registrarDireccionResponse registrarDireccion(registrarDireccionRequest oregistrarDireccionRequest);
        /// <summary>
        /// Permite validar la cobertura de la dirección para el cambio de dirección.
        /// </summary>
        /// <param name="ovalidarCoberturaMessageRequest"></param>
        /// <returns></returns>
        [OperationContract]
        validarCoberturaMessageResponse consultarCoberturaDireccion(validarCoberturaMessageRequest ovalidarCoberturaMessageRequest);
    }
}

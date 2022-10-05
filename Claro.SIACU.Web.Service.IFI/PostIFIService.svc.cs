using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POSTPAID = Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinor;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangeMinorClarify;
using Claro.SIACU.Entity.IFI.Postpaid.GetActivateServiceMail;
using Claro.SIACU.Entity.IFI.Postpaid.GetLinesTelephone;
using Claro.SIACU.Entity.IFI.Postpaid.GetDataCustomer;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddress;
using Claro.SIACU.Entity.IFI.Postpaid.GetSaveChangePostalAddressClarify;
using Claro.SIACU.Entity.IFI.Postpaid.GetStatedebt;
using Claro.SIACU.Entity.IFI.Postpaid.GetTestSecurity;
using Claro.SIACU.Entity.IFI.Postpaid.GetDuplicateReceipts;
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
    public class PostIFIService : IPostIFIService
    {
        /// <summary>
        /// constructor 
        /// </summary>
        /// <remarks>PostIFIService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public PostIFIService()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        /// obtiene cantidad de llamadas entrantes
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetAmountIncomingCall</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse GetAmountIncomingCall(POSTPAID.GetAmountIncomingCall.AmountIncomingCallRequest objRequest)
        {
            POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse>(() =>
                {
                    return Business.IFI.Postpaid.Postpaid.GetAmountIncomingCall(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene datos de la linea
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDataLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public POSTPAID.GetDataLine.DataLineResponse GetDataLine(POSTPAID.GetDataLine.DataLineRequest objRequest)
        {
            POSTPAID.GetDataLine.DataLineResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetDataLine.DataLineResponse>(() =>
                {
                    return Business.IFI.Postpaid.Postpaid.GetDataLine(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// obtiene tipo de transaccioness BRMS
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTypeTransactionBRMS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest)
        {
            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.Postpaid.GetTypeTransactionBRMS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        /// <summary>
        /// Permite Actualizar los datos menores de un cliente en la BD (BSCS70_DESA)
        /// </summary>
        /// <param name="objRequest">/param> nvia objeto tipo request         
        /// <returns>objResponse</returns>
        /// <remarks>SaveChangeMinor</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public SaveChangeMinorResponse SaveChangeMinor(SaveChangeMinorRequest objRequest)
        {
            SaveChangeMinorResponse objResponse = new SaveChangeMinorResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangeMinor.SaveChangeMinor(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// Permite Actualizar los datos menores de un cliente en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="objRequest">/param> Envia objeto tipo request       
        /// <returns>objResponse</returns>
        /// <remarks>SaveChangeMinorClarify</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public SaveChangeMinorClarifyResponse SaveChangeMinorClarify(SaveChangeMinorClarifyRequest objRequest)
        {
            SaveChangeMinorClarifyResponse objResponse = new SaveChangeMinorClarifyResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangeMinor.SaveChangeMinorClarify(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// Activa servicio mail
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetActivateServiceMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public GetActivateServiceMailResponse GetActivateServiceMail(GetActivateServiceMailRequest objRequest)
        {
            GetActivateServiceMailResponse objResponse = new GetActivateServiceMailResponse();



            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.MailReceipt.GetActivateServiceMail(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;


        }

        /// <summary>
        /// obtiene lineas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetLinesTelephone</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public  GetLinesTelephoneResponse GetLinesTelephone(GetLinesTelephoneRequest objRequest)
        {
            GetLinesTelephoneResponse objResponse = new GetLinesTelephoneResponse();



            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.MailReceipt.GetLinesTelephone(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;


        }

        /// <summary>
        /// obtiene datos del cliente
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetDataCustomer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public DataCustomerResponse GetDataCustomer(DataCustomerRequest objRequest)
        {
            DataCustomerResponse objResponse = new DataCustomerResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangeMinor.GetDataCustomer(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        /// <summary>
        /// Permite obtener los datos adicionales y la direccion alternativa registrada en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="objRequest">/param> Envia objeto tipo request       
        /// <returns>objResponse</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>16-11-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public ClientDataAddResponse GetClientDataAdd(ClientDataAddRequest objRequest)
        {
            ClientDataAddResponse objResponse = new ClientDataAddResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangeMinor.GetClientDataAdd(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }       
        /// <summary>
        /// guarda chambios de direccion postal
        /// </summary>
        /// <param name="objSaveChangePostalAddressRequest"></param>
        /// <returns></returns>
        /// <remarks>GetSaveChangePostalAddress</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public Entity.IFI.Postpaid.GetSaveChangePostalAddress.SaveChangePostalAddressResponse GetSaveChangePostalAddress(Entity.IFI.Postpaid.GetSaveChangePostalAddress.SaveChangePostalAddressRequest objSaveChangePostalAddressRequest)
        {
            Entity.IFI.Postpaid.GetSaveChangePostalAddress.SaveChangePostalAddressResponse objSaveChangePostalAddressResponse = new Entity.IFI.Postpaid.GetSaveChangePostalAddress.SaveChangePostalAddressResponse();

            try
            {
                objSaveChangePostalAddressResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangePostalAddress.GetSaveChangePostalAddress(objSaveChangePostalAddressRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSaveChangePostalAddressRequest.Audit.Session, objSaveChangePostalAddressRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objSaveChangePostalAddressResponse;
        }

        /// <summary>
        /// Permite registrar la informacion de DuplicateReceipts
        /// </summary>
        /// <param name="objRequest">/param> envia objeto tipo request         
        /// <returns>objResponse</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>24-10-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public SaveDuplicateReceiptsResponse SaveDuplicateReceipts(SaveDuplicateReceiptsRequest objRequest)
        {
         SaveDuplicateReceiptsResponse objResponse = new SaveDuplicateReceiptsResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.DuplicateReceipts.SaveDuplicateReceipts(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// obtiene estado de deuda
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetStatedebt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>06-02-2019</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public GetStatedebtResponse GetStatedebt(GetStatedebtRequest objRequest)
        {
            GetStatedebtResponse objResponse = new GetStatedebtResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.MailReceipt.GetStatedebt(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// obtiene preguntas de seguridad
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetTestSecurity</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public TestSecurityResponse GetTestSecurity(TestSecurityRequest objRequest)
        {
            TestSecurityResponse objResponse = new TestSecurityResponse();




            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ServiceLock.GetTestSecurity(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        /// <summary>
        /// Permite Actualizar los datos menores de un cliente Fijo en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="objRequest">/param> Envia objeto tipo request       
        /// <returns>objResponse</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public SaveChangeMinorFijoClarifyResponse SaveChangeMinorFijoClarify(SaveChangeMinorFijoClarifyRequest objRequest)
        {
            SaveChangeMinorFijoClarifyResponse objResponse = new SaveChangeMinorFijoClarifyResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangeMinor.SaveChangeMinorFijoClarify(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        /// <summary>
        /// Permite guardar la informacion de los datos adicionales y direccion alternativa en la BD de Clarify (TIMPRB)
        /// </summary>
        /// <param name="objRequest">/param> Envia objeto tipo request       
        /// <returns>objResponse</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14-09-2018</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu></FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot></Mot></item></list></remarks>
        public SaveChangeMinorClientDataAddResponse SaveChangeMinorClientDataAdd(SaveChangeMinorClientDataAddRequest objRequest)
        {
            SaveChangeMinorClientDataAddResponse objResponse = new SaveChangeMinorClientDataAddResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.IFI.Postpaid.ChangeMinor.SaveChangeMinorClientDataAdd(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        /// <summary>
        /// obtine cliclo de facturacion
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetBillingCycle</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public BillingCycleResponse GetBillingCycle(BillingCycleRequest objRequest)
        {
            BillingCycleResponse objBillingCycleResponse = new BillingCycleResponse();

            try
            {
                objBillingCycleResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.IFI.Postpaid.GetBillingCycle.BillingCycleResponse>(() => { return Business.IFI.Postpaid.ChangeBillingCycle.GetBillingCycle(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objBillingCycleResponse;
    }
        /// <summary>
        /// bloqueo de linea
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetServiceLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public ServiceLockResponse GetServiceLock(ServiceLockRequest objRequest)
        {
            ServiceLockResponse objServiceLockResponse = new ServiceLockResponse();

            try
            {
                objServiceLockResponse = Claro.Web.Logging.ExecuteMethod<ServiceLockResponse>(() => { return Business.IFI.Postpaid.ServiceLock.GetServiceLock(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objServiceLockResponse;
        }

        /// <summary>
        /// obtiene el actual bloqueo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>CurrentBlock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public CurrentLockResponse CurrentBlock(CurrentLockRequest objRequest)
        {
            CurrentLockResponse objCurrentLockResponse = new CurrentLockResponse();

            try
            {
                objCurrentLockResponse = Claro.Web.Logging.ExecuteMethod<CurrentLockResponse>(() => { return Business.IFI.Postpaid.ServiceLock.CurrentBlock(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objCurrentLockResponse;
        }
        /// <summary>
        /// bloqueo de equipo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEquipmentLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public LockUnlockEquipmentResponse GetEquipmentLock(LockUnlockEquipmentRequest objRequest)
        {
            LockUnlockEquipmentResponse objCurrentLockResponse = new LockUnlockEquipmentResponse();

            try
            {
                objCurrentLockResponse = Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponse>(() => { return Business.IFI.Postpaid.ServiceLock.GetEquipmentLock(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objCurrentLockResponse;
        }
        /// <summary>
        /// desbloqueo de equipo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetEquipmentUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public LockUnlockEquipmentResponse GetEquipmentUnLock(LockUnlockEquipmentRequest objRequest)
        {
            LockUnlockEquipmentResponse objCurrentLockResponse = new LockUnlockEquipmentResponse();

            try
            {
                objCurrentLockResponse = Claro.Web.Logging.ExecuteMethod<LockUnlockEquipmentResponse>(() => { return Business.IFI.Postpaid.UnlockService.GetEquipmentUnLock(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objCurrentLockResponse;
        }
        /// <summary>
        /// inserta bloqueo de liena per
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>InsertLockLinePer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public InsertLockLinePerResponse InsertLockLinePer(InsertLockLinePerRequest objRequest)
        {
            InsertLockLinePerResponse objInsertLockLinePerResponse = new InsertLockLinePerResponse();

            try
            {
                objInsertLockLinePerResponse = Claro.Web.Logging.ExecuteMethod<InsertLockLinePerResponse>(() => { return Business.IFI.Postpaid.ServiceLock.InsertLockLinePer(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objInsertLockLinePerResponse;
        }
        /// <summary>
        /// inserta bloqueo de equipo per
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>InsertLockEquipmentPer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public InsertLockEquipmentPerResponse InsertLockEquipmentPer(InsertLockEquipmentPerRequest objRequest)
        {
            InsertLockEquipmentPerResponse objInsertLockEquipmentPerResponse = new InsertLockEquipmentPerResponse();

            try
            {
                objInsertLockEquipmentPerResponse = Claro.Web.Logging.ExecuteMethod<InsertLockEquipmentPerResponse>(() => { return Business.IFI.Postpaid.ServiceLock.InsertLockEquipmentPer(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objInsertLockEquipmentPerResponse;
        }
        /// <summary>
        /// obtiene imeis
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetImeis</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public ImeisResponse GetImeis(ImeisRequest objRequest)
        {
            ImeisResponse objImeisResponse = new ImeisResponse();

            try
            {
                objImeisResponse = Claro.Web.Logging.ExecuteMethod<ImeisResponse>(() => { return Business.IFI.Postpaid.ServiceLock.GetImeis(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objImeisResponse;
        }
        /// <summary>
        /// desbloqueo linea
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>GetUnlockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UnlockServiceResponse GetUnlockService(UnlockServiceRequest objRequest)
        {
            UnlockServiceResponse objUnlockServiceResponse = new UnlockServiceResponse();

            try
            {
                objUnlockServiceResponse = Claro.Web.Logging.ExecuteMethod<UnlockServiceResponse>(() => { return Business.IFI.Postpaid.UnlockService.GetUnlockService(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objUnlockServiceResponse;
        }
        /// <summary>
        /// actualiza desbloqueo linea codigo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>UpdateUnlockLineCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UpdateUnlockLineCodeResponse UpdateUnlockLineCode(UpdateUnlockLineCodeRequest objRequest)
        {
            UpdateUnlockLineCodeResponse objUpdateUnlockLineCodeResponse = new UpdateUnlockLineCodeResponse();

            try
            {
                objUpdateUnlockLineCodeResponse = Claro.Web.Logging.ExecuteMethod<UpdateUnlockLineCodeResponse>(() => { return Business.IFI.Postpaid.UnlockService.UpdateUnlockLineCode(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objUpdateUnlockLineCodeResponse;
        }
        /// <summary>
        /// actualiza desbloqueo de equipo codigo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>UpdateUnlockEquipmentCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UpdateUnlockEquipmentCodeResponse UpdateUnlockEquipmentCode(UpdateUnlockEquipmentCodeRequest objRequest)
        {
            UpdateUnlockEquipmentCodeResponse objUpdateUnlockEquipmentCodeResponse = new UpdateUnlockEquipmentCodeResponse();

            try
            {
                objUpdateUnlockEquipmentCodeResponse = Claro.Web.Logging.ExecuteMethod<UpdateUnlockEquipmentCodeResponse>(() => { return Business.IFI.Postpaid.UnlockService.UpdateUnlockEquipmentCode(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objUpdateUnlockEquipmentCodeResponse;
        }
        /// <summary>
        /// Eliminar bloqueo de linea
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>DeleteLockLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public DeleteLockLineResponse DeleteLockLine(DeleteLockLineRequest objRequest)
        {
            DeleteLockLineResponse objDeleteLockLineResponse = new DeleteLockLineResponse();

            try
            {
                objDeleteLockLineResponse = Claro.Web.Logging.ExecuteMethod<DeleteLockLineResponse>(() => { return Business.IFI.Postpaid.ServiceLock.DeleteLockLine(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objDeleteLockLineResponse;
        }
        /// <summary>
        /// actualiza bloqueo de linea rollback
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>UpdateUnlockLineRollback</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UpdateUnlockLineRollbackResponse UpdateUnlockLineRollback(UpdateUnlockLineRollbackRequest objRequest)
        {
            UpdateUnlockLineRollbackResponse objUpdateUnlockLineRollbackResponse = new UpdateUnlockLineRollbackResponse();

            try
            {
                objUpdateUnlockLineRollbackResponse = Claro.Web.Logging.ExecuteMethod<UpdateUnlockLineRollbackResponse>(() => { return Business.IFI.Postpaid.UnlockService.UpdateUnlockLineRollback(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objUpdateUnlockLineRollbackResponse;

        }
        /// <summary>
        /// inserta terminal bloqueo y desbloqueo de equipo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>InsertTerminalLockUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public InsertTerminalLockUnlockEquipmentResponse InsertTerminalLockUnlockEquipment(InsertTerminalLockUnlockEquipmentRequest objRequest)
        {
            InsertTerminalLockUnlockEquipmentResponse objInsertTerminalLockUnlockEquipmentResponse = new InsertTerminalLockUnlockEquipmentResponse();

            try
            {
                objInsertTerminalLockUnlockEquipmentResponse = Claro.Web.Logging.ExecuteMethod<InsertTerminalLockUnlockEquipmentResponse>(() => { return Business.IFI.Postpaid.Postpaid.InsertTerminalLockUnlockEquipment(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objInsertTerminalLockUnlockEquipmentResponse;
        }
        /// <summary>
        /// actualiza desbloqueo de equipo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>UpdateUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public UpdateUnlockEquipmentResponse UpdateUnlockEquipment(UpdateUnlockEquipmentRequest objRequest)
        {
            UpdateUnlockEquipmentResponse objUpdateUnlockEquipmentResponse = new UpdateUnlockEquipmentResponse();

            try
            {
                objUpdateUnlockEquipmentResponse = Claro.Web.Logging.ExecuteMethod<UpdateUnlockEquipmentResponse>(() => { return Business.IFI.Postpaid.UnlockService.UpdateUnlockEquipment(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objUpdateUnlockEquipmentResponse;
        }
        /// <summary>
        /// elimina bloqueo de equipo
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>DeleteLockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
        public DeleteLockEquipmentResponse DeleteLockEquipment(DeleteLockEquipmentRequest objRequest)
        {
            DeleteLockEquipmentResponse objDeleteLockEquipmentResponse = new DeleteLockEquipmentResponse();

            try
            {
                objDeleteLockEquipmentResponse = Claro.Web.Logging.ExecuteMethod<DeleteLockEquipmentResponse>(() => { return Business.IFI.Postpaid.ServiceLock.DeleteLockEquipment(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return objDeleteLockEquipmentResponse;
        }

        public POSTPAID.GetPlans.PlansResponse GetPlans(POSTPAID.GetPlans.PlansRequest objPlansRequest)
        {
            POSTPAID.GetPlans.PlansResponse objPlansResponse = null;
            try
            {
                objPlansResponse = Claro.Web.Logging.ExecuteMethod(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, () =>
                    {
                        return Business.IFI.Postpaid.PlanMigration.GetPlans(objPlansRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlansRequest.Audit.Session, objPlansRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objPlansResponse;
        }

        public POSTPAID.GetServicesByPlan.PlanServiceResponse GetServicesByPlan(POSTPAID.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            POSTPAID.GetServicesByPlan.PlanServiceResponse objServicesResponse = null;
            try
            {
                objServicesResponse =
                    Claro.Web.Logging.ExecuteMethod(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                        {
                            return Business.IFI.Postpaid.PlanMigration.GetServicesByPlan(objServicesRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));

            }
            return objServicesResponse;
        }

        public POSTPAID.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse GetServicesByCurrentPlan(POSTPAID.GetServicesByCurrentPlan.ServicesByCurrentPlanRequest objServicesRequest)
        {
            POSTPAID.GetServicesByCurrentPlan.ServicesByCurrentPlanResponse objServicesResponse = null;
            try
            {
                objServicesResponse =
                    Claro.Web.Logging.ExecuteMethod(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                            {
                                return Business.IFI.Postpaid.PlanMigration.GetServicesByCurrentPlan(objServicesRequest);
                            });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objServicesResponse;
        }

        public POSTPAID.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanResponse GetEquipmentByCurrentPlan(POSTPAID.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanRequest objEquipmentRequest)
        {
            POSTPAID.GetEquipmentByCurrentPlan.EquipmentByCurrentPlanResponse objEquipmentResponse = null;
            try
            {
                objEquipmentResponse =
                    Claro.Web.Logging.ExecuteMethod(objEquipmentRequest.Audit.Session, objEquipmentRequest.Audit.Transaction, () =>
                        {
                            return Business.IFI.Postpaid.PlanMigration.GetEquipmentByCurrentPlan(objEquipmentRequest);
                        });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objEquipmentRequest.Audit.Session, objEquipmentRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }

            return objEquipmentResponse;
    }
         
        public POSTPAID.PostPlanMigration.PlanMigrationResponse PostPlanMigration(POSTPAID.PostPlanMigration.PlanMigrationRequest objPlanMigrationRequest)
        {
            POSTPAID.PostPlanMigration.PlanMigrationResponse objPlanMigrationResponse = null;
            try
            {
                objPlanMigrationResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.PostPlanMigration.PlanMigrationResponse>(() =>
                    {
                        return Business.IFI.Postpaid.PlanMigration.PostPlanMigration(objPlanMigrationRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlanMigrationRequest.Audit.Session, objPlanMigrationRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
          
            return objPlanMigrationResponse;
        }

        public List<GenericItem> obtenerParametrosPorTipo(string strIdSession, string strTransaccion)
        {
            List<GenericItem> olisGenericItem = null;
            try 
            {
                olisGenericItem = Claro.Web.Logging.ExecuteMethod<List<GenericItem>>(() =>
                {
                    return Business.IFI.Postpaid.ChangeServiceAddress.obtenerParametrosPorTipo(strIdSession, strTransaccion);
                });
            }
            catch (Exception ex) 
            {
                Claro.Web.Logging.Error(strIdSession, strTransaccion, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return olisGenericItem;
        }
        /// <summary>
        /// Permite registrar o actualizar dirección del servicio IFI.
        /// </summary>
        /// <param name="oheaderRequest">Información de cabecera.</param>
        /// <param name="oregistrarDireccionRequest">Información del cambio de dirección IFI.</param>
        /// <returns></returns>
        public registrarDireccionResponse registrarDireccion(registrarDireccionRequest oregistrarDireccionRequest)
        {
            registrarDireccionResponse oregistrarDireccionResponse = null;
            try
            {
                oregistrarDireccionResponse = Claro.Web.Logging.ExecuteMethod<registrarDireccionResponse>(() =>
                {
                    return Business.IFI.Postpaid.ChangeServiceAddress.registrarDireccion(oregistrarDireccionRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oregistrarDireccionRequest.idTransaccion, oregistrarDireccionRequest.idTransaccion, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return oregistrarDireccionResponse;
        }
        /// <summary>
        /// Permite validar la cobertura de la dirección para el cambio de dirección.
        /// </summary>
        /// <param name="ovalidarCoberturaMessageRequest"></param>
        /// <returns></returns>
        public validarCoberturaMessageResponse consultarCoberturaDireccion(validarCoberturaMessageRequest ovalidarCoberturaMessageRequest)
        {
            validarCoberturaMessageResponse ovalidarCoberturaMessageResponse = null;
            try
            {
                ovalidarCoberturaMessageResponse = Claro.Web.Logging.ExecuteMethod<validarCoberturaMessageResponse>(() =>
                {
                    return Business.IFI.Postpaid.ChangeServiceAddress.validarCoberturaDireccion(ovalidarCoberturaMessageRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(ovalidarCoberturaMessageRequest.MessageRequest.Header.HeaderRequest.pid, ovalidarCoberturaMessageRequest.MessageRequest.Header.HeaderRequest.pid, Claro.Utils.GetExceptionMessage(ex));
                throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
            }
            return ovalidarCoberturaMessageResponse;
        }
    }
    
}

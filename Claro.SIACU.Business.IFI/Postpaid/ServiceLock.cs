using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.IFI.Postpaid.GetTestSecurity;
using Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid.ServiceLock;
using Claro.SIACU.Entity.IFI.Common.IsOkGetKey;
using Claro.SIACU.Entity.IFI.Postpaid.CurrentLock;
using Claro.SIACU.Entity.IFI.Postpaid.GetLockUnlockEquipment;
using Claro.SIACU.Entity.IFI.Common.GetSequenceCode;
using Claro.SIACU.Entity.IFI.Postpaid.InsertLockLinePer;
using Claro.SIACU.Entity.IFI.Postpaid.InsertLockEquipmentPer;
using Claro.SIACU.Entity.IFI.Postpaid.GetImeis;
using Claro.SIACU.Entity.IFI.Postpaid.DeleteLockLine;
using Claro.SIACU.Entity.IFI.Postpaid.DeleteLockEquipment;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class ServiceLock
    {

        /// <summary>Método que obtiene preguntas de seguridad.</summary>
        /// <param name="objRequest"></param>
        /// <returns>TestSecurityResponse</returns>
        /// <remarks>GetTestSecurity</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static TestSecurityResponse GetTestSecurity(TestSecurityRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init GetTestSecurity");
            TestSecurityResponse objResponse = new TestSecurityResponse();

            List<ItemGeneric> lstAnswers = null;
            List<ItemGeneric> lstQuestions = null;
            List<Answer> lstAnswer = null;
            try
            {
                lstAnswers = Data.IFI.Postpaid.ServiceLock.GetAnswersSecurity(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                                                             objRequest.strTypeCustomer, objRequest.strGroupCustomer);
                lstQuestions = Data.IFI.Postpaid.ServiceLock.GetQuestionsSecurity(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                                                               objRequest.strTypeCustomer, objRequest.strGroupCustomer);

                List<TestSecurity> listTestSecu = new List<TestSecurity>();

                if (lstQuestions != null && lstAnswers != null)
                {
                    foreach (var itemQues in lstQuestions)
                    {
                        lstAnswer = new List<Answer>();
                        foreach (var itemAnsw in lstAnswers)
                        {

                            if (itemAnsw.Code == itemQues.Code)
                            {

                                lstAnswer.Add(new Answer()
                                {
                                    codAns = itemAnsw.Code,
                                    desAns = itemAnsw.Description
                                });
                               
                            }

                        }
                        listTestSecu.Add(new TestSecurity()
                        {
                            codQues = itemQues.Code,
                            desQues = itemQues.Description,
                            ListAnsw = lstAnswer
                        }
                                                );

                    }
                }


                objResponse.lstTestSecurity = listTestSecu;
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que permite el bloqueo de la linea.</summary>
        /// <param name="objRequest"></param>
        /// <returns>ServiceLockResponse</returns>
        /// <remarks>GetServiceLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static ServiceLockResponse GetServiceLock(ServiceLockRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init GetTestSecurity");

            ServiceLockResponse objResponse = null;
            try
            {
                IsOkGetKeyRequest objIsOkGetKeyRequest = objRequest.objIsOkGetKeyRequest;
                var objIsOkGetKeyResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.IsOkGetKey.IsOkGetKeyResponse>(() => { return Business.IFI.Common.IsOkGetKey(objIsOkGetKeyRequest); });


                if (objIsOkGetKeyResponse.result)
                {
                    objRequest.strPass = objIsOkGetKeyResponse.Pass;
                    objRequest.strUser = objIsOkGetKeyResponse.User;
                    objResponse = new ServiceLockResponse
                   {

                       resul = Claro.Web.Logging.ExecuteMethod<bool>
                         (objRequest.Audit.Session,
                             objRequest.Audit.Transaction,
                             () =>
                             {
                                 return Data.IFI.Postpaid.ServiceLock.GetServiceLock(objRequest);
                             })
                   };
                }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que obtiene el actual bloqueo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>CurrentLockResponse</returns>
        /// <remarks>CurrentBlock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static CurrentLockResponse CurrentBlock(CurrentLockRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init CurrentBlock");

            CurrentLockResponse objResponse = null;
            try
            {

                objResponse = new CurrentLockResponse
                {

                    lstAnnotation = Claro.Web.Logging.ExecuteMethod<List<Annotation>>
                        (objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            () =>
                            {
                                return Data.IFI.Postpaid.ServiceLock.CurrentBlock(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.codId);
                            })
                };

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que obtiene el bloqueo de equipo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>LockUnlockEquipmentResponse</returns>
        /// <remarks>GetEquipmentLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static LockUnlockEquipmentResponse GetEquipmentLock(LockUnlockEquipmentRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init CurrentBlock");
            string strMessage = string.Empty;
            LockUnlockEquipmentResponse objResponse = null;
            try
            {

                objResponse = new LockUnlockEquipmentResponse
                {

                    Result = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.ServiceLock.GetEquipmentLock(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Imei, out  strMessage);
                               }),
                    strMessage = strMessage
                };


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que obtiene el codigo de desbloqueo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>SequenceCodeResponse</returns>
        /// <remarks>GetSequenceCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static SequenceCodeResponse GetSequenceCode(SequenceCodeRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init GetSequenceCode");
            string strMessage = string.Empty;
            SequenceCodeResponse objResponse = null;
            try
            {

                objResponse = new SequenceCodeResponse
                {
                    code = Claro.Web.Logging.ExecuteMethod<string>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Common.GetSequenceCode(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.isFlagUnlock);
                               })

                };


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que inserta el bloqueo por linea</summary>
        /// <param name="objRequest"></param>
        /// <returns>InsertLockLinePerResponse</returns>
        /// <remarks>InsertLockLinePer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static InsertLockLinePerResponse InsertLockLinePer(InsertLockLinePerRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init InsertLockLinePer");
            string rMsgText = string.Empty;
            string rFlagInsercion = string.Empty;
            InsertLockLinePerResponse objResponse = null;
            try
            {

                objResponse = new InsertLockLinePerResponse
                {

                    Result = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.ServiceLock.InsertLockLinePer(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, objRequest.codeLock, ref rFlagInsercion, ref rMsgText);
                               })

                };
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que inserta el bloqueo por equipo</summary>
        /// <param name="objRequest"></param>
        /// <returns>InsertLockEquipmentPerResponse</returns>
        /// <remarks>InsertLockEquipmentPer</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static InsertLockEquipmentPerResponse InsertLockEquipmentPer(InsertLockEquipmentPerRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init InsertLockLinePer");
            string rMsgText = string.Empty;
            string rFlagInsercion = string.Empty;
            InsertLockEquipmentPerResponse objResponse = null;
            try
            {

                objResponse = new InsertLockEquipmentPerResponse
                {

                    Result = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.ServiceLock.InsertLockEquipmentPer(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, objRequest.codeLock, ref rFlagInsercion, ref rMsgText);
                               })

                };
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que obtiene el numero de IMEI</summary>
        /// <param name="objRequest"></param>
        /// <returns>ImeisResponse</returns>
        /// <remarks>GetImeis</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static ImeisResponse GetImeis(ImeisRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init GetImeis");

            ImeisResponse objResponse = null;
            try
            {

                objResponse = new ImeisResponse
                {

                    lstTerminal = Claro.Web.Logging.ExecuteMethod<List<Terminal>>
                        (objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            () =>
                            {
                                return Data.IFI.Postpaid.ServiceLock.GetImeis(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strLine);
                            })
                };

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que elimina el bloqueo de linea</summary>
        /// <param name="objRequest"></param>
        /// <returns>DeleteLockLineResponse</returns>
        /// <remarks>DeleteLockLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static DeleteLockLineResponse DeleteLockLine(DeleteLockLineRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init InsertLockLinePer");
            string rMsgText = string.Empty;
            string rFlagDelete = string.Empty;
            DeleteLockLineResponse objResponse = null;
            try
            {

                objResponse = new DeleteLockLineResponse
                {

                    resul = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.ServiceLock.DeleteLockLine(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, ref rMsgText, ref rFlagDelete);
                               })

                };
                objResponse.rFlagDelete = rFlagDelete;
                objResponse.rMsgText = rMsgText;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que elimina el bloqueo de equipo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>DeleteLockEquipmentResponse</returns>
        /// <remarks>DeleteLockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static DeleteLockEquipmentResponse DeleteLockEquipment(DeleteLockEquipmentRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init DeleteLockEquipment");
            string rMsgText = string.Empty;
            string rFlagDelete = string.Empty;
            DeleteLockEquipmentResponse objResponse = null;
            try
            {

                objResponse = new DeleteLockEquipmentResponse
                {

                    resul = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.ServiceLock.DeleteLockEquipment(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, ref rMsgText, ref rFlagDelete);
                               })

                };
                objResponse.rFlagDelete = rFlagDelete;
                objResponse.rMsgText = rMsgText;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }

    }
}

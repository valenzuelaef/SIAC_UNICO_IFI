using Claro.SIACU.Entity.IFI.Common.IsOkGetKey;
using Claro.SIACU.Entity.IFI.Postpaid.GetLockUnlockEquipment;
using Claro.SIACU.Entity.IFI.Postpaid.UnlockService;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipment;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockEquipmentCode;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockLineCode;
using Claro.SIACU.Entity.IFI.Postpaid.UpdateUnlockLineRollback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class UnlockService
    {

        /// <summary>Método que obtiene el desbloqueo del servicio.</summary>
        /// <param name="objRequest"></param>
        /// <returns>UnlockServiceResponse</returns>
        /// <remarks>GetUnlockService</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static UnlockServiceResponse GetUnlockService(UnlockServiceRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init GetUnlockService");

            UnlockServiceResponse objResponse = null;
            try
            {
                IsOkGetKeyRequest objIsOkGetKeyRequest = objRequest.objIsOkGetKeyRequest;
                var objIsOkGetKeyResponse = Claro.Web.Logging.ExecuteMethod<Entity.IFI.Common.IsOkGetKey.IsOkGetKeyResponse>(() => { return Business.IFI.Common.IsOkGetKey(objIsOkGetKeyRequest); });


                if (objIsOkGetKeyResponse.result)
                {
                    objRequest.strPass = objIsOkGetKeyResponse.Pass;
                    objRequest.strUser = objIsOkGetKeyResponse.User;
                    objResponse = new UnlockServiceResponse
                    {

                        resul = Claro.Web.Logging.ExecuteMethod<bool>
                          (objRequest.Audit.Session,
                              objRequest.Audit.Transaction,
                              () =>
                              {
                                  return Data.IFI.Postpaid.UnlockService.GetUnlockService(objRequest);
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


        /// <summary>Método que obtiene el desbloqueo del equipo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>LockUnlockEquipmentResponse</returns>
        /// <remarks>GetEquipmentUnLock</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static LockUnlockEquipmentResponse GetEquipmentUnLock(LockUnlockEquipmentRequest objRequest)
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
                                   return Data.IFI.Postpaid.UnlockService.GetEquipmentUnLock(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Imei, out  strMessage);
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


        /// <summary>Método que actualiza el codigo de desbloqueo de linea.</summary>
        /// <param name="objRequest"></param>
        /// <returns>UpdateUnlockLineCodeResponse</returns>
        /// <remarks>UpdateUnlockLineCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static UpdateUnlockLineCodeResponse UpdateUnlockLineCode(UpdateUnlockLineCodeRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init CurrentBlock");
            string strMessage = string.Empty;
            string strFlag = string.Empty;
            UpdateUnlockLineCodeResponse objResponse = null;
            try
            {

                objResponse = new UpdateUnlockLineCodeResponse
                {

                    resul = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.UnlockService.UpdateUnlockLineCode(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, objRequest.codeUnlock, ref strFlag, ref strMessage);
                               }),
                    rMsgText = strMessage,
                    rFlagInsercion = strFlag
                };


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que actualiza el codigo de desbloqueo del equipo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>UpdateUnlockEquipmentCodeResponse</returns>
        /// <remarks>UpdateUnlockEquipmentCode</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static UpdateUnlockEquipmentCodeResponse UpdateUnlockEquipmentCode(UpdateUnlockEquipmentCodeRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init CurrentBlock");
            string strMessage = string.Empty;
            string strFlag = string.Empty;
            UpdateUnlockEquipmentCodeResponse objResponse = null;
            try
            {

                objResponse = new UpdateUnlockEquipmentCodeResponse
                {

                    resul = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.UnlockService.UpdateUnlockEquipmentCode(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, objRequest.codeUnlock, ref strFlag, ref strMessage);
                               }),
                    rMsgText = strMessage,
                    rFlagInsercion = strFlag
                };


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que hace rollback a la actualizacion del desbloqueo de linea.</summary>
        /// <param name="objRequest"></param>
        /// <returns>UpdateUnlockLineRollbackResponse</returns>
        /// <remarks>UpdateUnlockLineRollback</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static UpdateUnlockLineRollbackResponse UpdateUnlockLineRollback(UpdateUnlockLineRollbackRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init UpdateUnlockLineRollback");
            string strMessage = string.Empty;
            string strFlag = string.Empty;
            UpdateUnlockLineRollbackResponse objResponse = null;
            try
            {

                objResponse = new UpdateUnlockLineRollbackResponse
                {

                    resul = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.UnlockService.UpdateUnlockLineRollback(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, ref strFlag, ref strMessage);
                               }),
                    rMsgText = strMessage,
                    rFlagInsercion = strFlag
                };


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }


        /// <summary>Método que actualiza el desbloqueo del equipo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>UpdateUnlockEquipmentResponse</returns>
        /// <remarks>UpdateUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static UpdateUnlockEquipmentResponse UpdateUnlockEquipment(UpdateUnlockEquipmentRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "init UpdateUnlockEquipment");
            string strMessage = string.Empty;
            string strFlag = string.Empty;
            UpdateUnlockEquipmentResponse objResponse = null;
            try
            {

                objResponse = new UpdateUnlockEquipmentResponse
                {

                    resul = Claro.Web.Logging.ExecuteMethod<bool>
                           (objRequest.Audit.Session,
                               objRequest.Audit.Transaction,
                               () =>
                               {
                                   return Data.IFI.Postpaid.UnlockService.UpdateUnlockEquipment(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.objLock, ref strFlag, ref strMessage);
                               }),
                    rMsgText = strMessage,
                    rFlagInsercion = strFlag
                };


            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }
    }
}

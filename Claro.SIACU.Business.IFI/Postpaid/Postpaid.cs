using Claro.SIACU.Entity;
using COMMON = Claro.SIACU.Entity.IFI.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KEY = Claro.ConfigurationManager;
using POSTPAID = Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid;
using Claro.SIACU.Entity.IFI.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.Entity.IFI.Postpaid.InsertTerminalLockUnlockEquipment;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class Postpaid
    {

        /// <summary>Método que obtiene los datos de la linea </summary>
        /// <param name="objRequest"></param>
        /// <returns>POSTPAID.GetDataLine.DataLineResponse</returns>
        ///  <remarks>GetDataLine</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static POSTPAID.GetDataLine.DataLineResponse GetDataLine(POSTPAID.GetDataLine.DataLineRequest objRequest)
        {
            string message = "";
            COMMON.Line dataLine = new COMMON.Line();
            POSTPAID.GetDataLine.DataLineResponse objResponse = new POSTPAID.GetDataLine.DataLineResponse()
            {
                StrResponse = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.Postpaid.GetDataline(objRequest.Audit.Session, objRequest.Audit.Transaction, int.Parse(objRequest.ContractID), ref dataLine, ref message);
                })
            };

            objResponse.Message = message;
            objResponse.DataLine = dataLine;
            return objResponse;
        }

        /// <summary>Método que obtiene el monto de las llamadas entrantes</summary>
        /// <param name="objRequest"></param>
        /// <returns>POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse</returns>
        ///  <remarks>GetAmountIncomingCall</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse GetAmountIncomingCall(POSTPAID.GetAmountIncomingCall.AmountIncomingCallRequest objRequest)
        {
            string Message = "";
            POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse objResponse = new POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse()
            {
                ListAmountIncomingCall = Claro.Web.Logging.ExecuteMethod<List<AmountIncomingCall>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.Postpaid.GetAmountIncomingCall(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Name, ref Message);
                })
            };
            objResponse.Message = Message;
            return objResponse;
        }


        /// <summary>Método que obtiene el tipo de transacciones BRMS</summary>
        /// <param name="objRequest"></param>
        /// <returns>TypeTransactionBRMSResponse</returns>
        /// <remarks>GetTypeTransactionBRMS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest)
        {
            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();
            objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.Postpaid.GetTypeTransactionBRMS(objRequest);
            });
            return objResponse;
        }


        /// <summary>Método que inserta el terminal bloqueo o desbloqueo de equipo.</summary>
        /// <param name="objRequest"></param>
        /// <returns>InsertTerminalLockUnlockEquipmentResponse</returns>
        ///  <remarks>InsertTerminalLockUnlockEquipment</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static InsertTerminalLockUnlockEquipmentResponse InsertTerminalLockUnlockEquipment(InsertTerminalLockUnlockEquipmentRequest objRequest)
        {
            string resulState;
            int resulMovem;

            Terminal objTerminal = new Terminal();
            InsertTerminalLockUnlockEquipmentResponse objResponse = new InsertTerminalLockUnlockEquipmentResponse();
            objResponse.resTerminal = 0;
            try
            {
                objTerminal = UpdateDataTerminal(objRequest);

                resulState = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.IFI.Postpaid.Postpaid.GetStateEquipment(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strNumeroLinea);
                });
                  if (resulState == objTerminal._strEstado)
                {
                       resulMovem = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.IFI.Postpaid.Postpaid.InsertMovement(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strNumeroLinea, objTerminal._strReportante, objTerminal._strAsesorServicio, objTerminal._strMarca, objTerminal._strModelo, objTerminal._strEstado, objTerminal._strTipoMovimiento);

                    });
                }
                else
                {
                    Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        Data.IFI.Postpaid.Postpaid.UpdateStateBrand(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strMarca, objTerminal._strEstado);

                    });
                    resulMovem = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.IFI.Postpaid.Postpaid.InsertMovement(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strNumeroLinea, objTerminal._strReportante, objTerminal._strAsesorServicio, objTerminal._strMarca, objTerminal._strModelo, objTerminal._strEstado, objTerminal._strTipoMovimiento);

                    });
                    if (resulMovem == 0)
                    {
                        if (objTerminal._strEstado == "DESBLOQUEADO")
                        {
                            Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                            {
                                Data.IFI.Postpaid.Postpaid.UpdateStateBrand(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strMarca, "BLOQUEADO");

                            });
                        }
                        else
                        {
                            Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                            {
                                Data.IFI.Postpaid.Postpaid.UpdateStateBrand(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strMarca, "DESBLOQUEADO");

                            });
                        }

                    }
                }
                objResponse.resTerminal = resulMovem;
                return objResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Entro a excepcion InsertTerminalLockUnlockEquipment " + ex.Message);
                if (objTerminal._strEstado == "DESBLOQUEADO")
                {
                    Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                           {
                               Data.IFI.Postpaid.Postpaid.UpdateStateBrand(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strMarca, "BLOQUEADO");

                           });
                }
                else
                {
                    Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        Data.IFI.Postpaid.Postpaid.UpdateStateBrand(objRequest.Audit.Session, objRequest.Audit.Transaction, objTerminal._strNumeroIMEI, objTerminal._strMarca, "DESBLOQUEADO");

                    });
                }


            }
            return objResponse;
        }
        
        
        /// <summary>Método que permite actualizar los datos del terminal</summary>
        /// <param name="objRequest"></param>
        /// <returns>Terminal</returns>
        /// <remarks>UpdateDataTerminal</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static Terminal UpdateDataTerminal(InsertTerminalLockUnlockEquipmentRequest objRequest)
        {
            objRequest.objTerminal._strNomCliente = "";
            objRequest.objTerminal._strApeCliente = "";
            objRequest.objTerminal._strAsesorServicio = objRequest.Audit.UserName;
            objRequest.objTerminal._strCanal = "SIACUNICO";
            objRequest.objTerminal._strModelo = "";         
            if (objRequest.isTransactionLock)
            {            
                objRequest.objTerminal._strEstado = "BLOQUEADO";

            }
            else
            {               
                objRequest.objTerminal._strEstado = "DESBLOQUEADO";
                objRequest.objTerminal._strTipoMovimiento = "RECUPERADO";
            }

            return objRequest.objTerminal;


        }


    }
}

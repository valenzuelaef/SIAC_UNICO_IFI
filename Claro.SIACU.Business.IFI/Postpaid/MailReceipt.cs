using Claro.SIACU.Entity.IFI.Common;
using Claro.SIACU.Entity.IFI.Postpaid.GetActivateServiceMail;
using Claro.SIACU.Entity.IFI.Postpaid.GetStatedebt;
using Claro.SIACU.Entity.IFI.Postpaid.GetLinesTelephone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.IFI.Postpaid
{
    public class MailReceipt
    {

        /// <summary>Método que permite activar servicio de correo para la afiliación de correos</summary>
        /// <param name="objRequest"></param>  
        /// <returns>GetActivateServiceMailResponse</returns>
        /// <remarks>GetActivateServiceMail</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static GetActivateServiceMailResponse GetActivateServiceMail(GetActivateServiceMailRequest objRequest)
        {
            GetActivateServiceMailResponse objResponse = new GetActivateServiceMailResponse();

            string strMessage = "";

            objResponse.strResult = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.MailReceipt.GetActivateServiceMail(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                                                            objRequest.pCustomerID, objRequest.pCuenta, objRequest.pEmail,
                                                                            objRequest.pFlag, objRequest.pTelRef, objRequest.pNumCla,
                                                                            objRequest.pObjID, objRequest.pTelConfSMS, ref strMessage);
            });

            objResponse.strMensaje = strMessage;

            return objResponse;
        }


        /// <summary>Método que obtiene las lineas del cliente</summary>
        /// <param name="objRequest"></param>  
        /// <returns>GetLinesTelephoneResponse</returns>
        /// <remarks>GetLinesTelephone</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static GetLinesTelephoneResponse GetLinesTelephone(GetLinesTelephoneRequest objRequest)
        {
            GetLinesTelephoneResponse objResponse = new GetLinesTelephoneResponse();


            objResponse.lstLine = Web.Logging.ExecuteMethod<List<Line>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.MailReceipt.GetLinesTelephone(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                                                           objRequest.vCUSTOMER_ID);
            });

            if (objResponse.lstLine != null && objResponse.lstLine.Count > 0)
            {
                foreach (var item in objResponse.lstLine)
                {
                    if (string.IsNullOrEmpty(objRequest.strTelephone))
                    {
                        objResponse.blResult = false;
                        return objResponse;
                    }
                    if (objRequest.strTelephone.Equals(item.PhoneNumber))
                    {
                        objResponse.blResult = true;
                        return objResponse;
                    }
                }

            }
            objResponse.blResult = false;
            return objResponse;
        }


        /// <summary>Método que obtiene el historial de facturación del cliente</summary>
        /// <param name="objRequest"></param>  
        /// <returns>GetStatedebtResponse</returns>
        /// <remarks>GetStatedebt</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>14/09/2018</FecCrea></item></list>
        public static GetStatedebtResponse GetStatedebt(GetStatedebtRequest objRequest)
        {
            GetStatedebtResponse objResponse = new GetStatedebtResponse();


            Receipt objReceipt = Web.Logging.ExecuteMethod<Receipt>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.IFI.Postpaid.MailReceipt.GetDataInvoice(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                                                           objRequest.strCustomerCode);
            });

            if (objReceipt != null)
            {

                if (DateTime.Parse(objReceipt.FECHA_VENCIMIENTO) >= DateTime.Now)
                {
                    objResponse.blResult = true;
                    return objResponse;
                }
            }
            objResponse.blResult = false;
            return objResponse;
        }
    }
}

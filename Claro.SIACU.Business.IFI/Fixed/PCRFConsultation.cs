using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.IFI.Fixed;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.IFI.Fixed.GetPCRFConsultation;
using Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic; //INI - RF-04 - RF-05 Evalenzs

namespace Claro.SIACU.Business.IFI.Fixed
{
    public class PCRFConsultation
    {

        /// <summary>Método que obtiene los número de teléfono del cliente LTE.</summary>
        /// <param name="objRequest"></param>
        /// <returns>PCRFConnectorResponse</returns>
        /// <remarks>ObtenerTelefonosClienteLTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static PCRFConnectorResponse ObtenerTelefonosClienteLTE(PCRFConnectorRequest objRequest)
        {
            PCRFConnectorResponse objTelefonosLTE = new PCRFConnectorResponse();
            try
            {
                objTelefonosLTE = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Claro.SIACU.Data.IFI.Fixed.PCRFConsultation.ObtenerTelefonosClienteLTE(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objTelefonosLTE;
        }


        /// <summary>Método que consulta la cuota del suscriptor PCRF.</summary>
        /// <param name="objRequest"></param>
        /// <returns>PCRFConnectorResponse</returns>
        /// <remarks>ConsultarPCRFSuscriber_Quota</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static PCRFConnectorResponse ConsultarPCRFSuscriber_Quota(PCRFConnectorRequest objRequest)
        {
            PCRFConnectorResponse objPCRFSuscriberQuota = new PCRFConnectorResponse();
            objPCRFSuscriberQuota = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.IFI.Fixed.PCRFConsultation.ConsultarPCRFSuscriber_Quota(objRequest);
                });
            return objPCRFSuscriberQuota;
        }


        /// <summary>Método que consulta los datos del suscriptor PCRF.</summary>
        /// <param name="objRequest"></param>
        /// <returns>PCRFConnectorResponse</returns>
        /// <remarks>ConsultarPCRFSuscriber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public static PCRFConnectorResponse ConsultarPCRFSuscriber(PCRFConnectorRequest objRequest)
        {
            PCRFConnectorResponse objPCRFSuscriberQuota = new PCRFConnectorResponse();
            objPCRFSuscriberQuota = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.IFI.Fixed.PCRFConsultation.ConsultarPCRFSuscriber(objRequest);
            });
            return objPCRFSuscriberQuota;
        }
        //INI - RF-04 - RF-05 Evalenzs 
        public static PCRFPaquetesAdicBodyResponse ConsultarPCRFPaquetesAdic(PCRFPaquetesAdicRequest objRequest)
        {
            PCRFPaquetesAdicBodyResponse objResponse = new PCRFPaquetesAdicBodyResponse();

            Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicRequest oRestRequest = new Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicRequest
           {
               MessageRequest = new Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicMessageRequest
               {
                   Header = new Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicHeader
                   {
                       HeaderRequest = new Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicHeaderRequest
                       {
                           consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
                           country = objRequest.MessageRequest.Header.HeaderRequest.country,
                           dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                           language = objRequest.MessageRequest.Header.HeaderRequest.language,
                           modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
                           msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
                           operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
                           pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
                           system = objRequest.MessageRequest.Header.HeaderRequest.system,
                           timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
                           userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
                           wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
                       }
                   },
                   Body = new Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicBodyRequest
                   {
                       consultarRequest = new Claro.SIACU.Entity.IFI.Fixed.PostPCRFPaquetesAdic.RestPCRFPaquetesAdicConsultarRequest
                       {
                           msisdn = objRequest.MessageRequest.Body.consultarRequest.msisdn,
                           flagHistorico = objRequest.MessageRequest.Body.consultarRequest.flagHistorico,
                           cantDias=objRequest.MessageRequest.Body.consultarRequest.cantDias
                       }
                   }
               }
           };
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.IFI.Fixed.PCRFConsultation.ConsultarPCRFPaquetesAdic(objRequest, oRestRequest.MessageRequest);
            });

            return objResponse;
        }


    }
}

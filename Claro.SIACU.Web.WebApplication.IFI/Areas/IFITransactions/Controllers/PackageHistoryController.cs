using Claro.SIACU.Web.WebApplication.IFI.FixedIFIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{//INI - RF-05 Evalenzs
    public class PackageHistoryController : Controller
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.FixedIFIService.FixedIFIServiceClient ServiceFixed = new FixedIFIService.FixedIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient oServicePostpaid = new PostIFIService.PostIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient oServiceCommon = new CommonIFIService.CommonIFIServiceClient();


        // GET: IFITransactions/PackageHistory
        public ActionResult PackageHistory()
        {

            ViewData["TiposPaquetesHistoricoPaquetes"] = ConfigurationManager.AppSettings("TiposPaquetesHistoricoPaquetes");
            ViewData["RangoDiasHistoricoPaquetes"] = ConfigurationManager.AppSettings("RangoDiasHistoricoPaquetes");
            return View();
        }



        ///<summary>Metodo que carga el Historial de paquetes</summary>
        ///<param name="oModel"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>PageLoad</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>10/08/2020.</FecCrea></item></list>
        public JsonResult PageLoad(string strIdSession, string strAccountIdSession)
        {
            strIdSession = "11111111212121";
            FixedIFIService.AuditRequest oAuditRequest = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "Inicio Método : ConsultarPCRFPaquetesAdic");
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "Inicio Método : Session: " + strIdSession);
            
            //PCRFPaquetesAdicBodyResponse objResponse = null;

            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.PaquetesAdquiridosHistoricoResponse objResponseData1 = null;
            Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest oAuditRequest2 = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.AuditRequest>(strIdSession);

            string CadenaPaquetesSncodesIFI = ConfigurationManager.AppSettings("PaquetesSncodesIFI").ToString().ToUpper();
            string[] PaquetesSncodesIFI = CadenaPaquetesSncodesIFI.Split(',');
            
            try
            {

             //   FixedIFIService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedIFIService.AuditRequest>(oModel.strIdSession);

                PCRFConnectorRequest objRequest2 = new PCRFConnectorRequest()
                {
                    audit = oAuditRequest,
                    strAccountId = strAccountIdSession
                };
                PCRFConnectorResponse objResponse2 = Claro.Web.Logging.ExecuteMethod(oAuditRequest.Session, oAuditRequest.transaction, () =>
                {
                    return ServiceFixed.ObtenerTelefonosClienteLTE(objRequest2);
                });

                string strTelephone = objResponse2.strTelefonoLTE;
                //string strTelephone = "871053983";

                //FixedIFIService.HeaderRequest oHeaderRequest = new FixedIFIService.HeaderRequest
                //{
                //    consumer = ConfigurationManager.AppSettings("consumer"),
                //    country = ConfigurationManager.AppSettings("country"),
                //    dispositivo = ConfigurationManager.AppSettings("strDPDispositivo"),
                //    language = ConfigurationManager.AppSettings("language"),
                //    modulo = ConfigurationManager.AppSettings("modulo"),
                //    msgType = ConfigurationManager.AppSettings("msgType"),
                //    operation = ConfigurationManager.AppSettings("strOperatioPCRFPaqAdicionales"),
                //    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                //    system = ConfigurationManager.AppSettings("system"),
                //    timestamp = DateTime.Now.ToString("o"),
                //    userId = App_Code.Common.CurrentUser,
                //    wsIp =  App_Code.Common.GetApplicationIp()//Descomentar
                //};

                //PCRFPaquetesAdicConsultarRequest objconsultarRequest = new PCRFPaquetesAdicConsultarRequest
                //{
                //    msisdn = strTelephone,
                //    flagHistorico = "1",
                //    cantDias = ConfigurationManager.AppSettings("RangoDiasHistoricoPaquetes")
                //};

                //PCRFPaquetesAdicBodyRequest oBodyRequest = new PCRFPaquetesAdicBodyRequest
                //{
                //    consultarRequest = objconsultarRequest
                //};

                //PCRFPaquetesAdicHeaderRequest oPCRFPaquetesAdicHeader = new PCRFPaquetesAdicHeaderRequest
                //{
                //    HeaderRequest = oHeaderRequest
                //};

                //PCRFPaquetesAdicMessageRequest oMessageRequest = new PCRFPaquetesAdicMessageRequest
                //{
                //    Header = oPCRFPaquetesAdicHeader,
                //    Body = oBodyRequest,
                //};
                //PCRFPaquetesAdicRequest objRequest = new PCRFPaquetesAdicRequest()
                //{
                //    MessageRequest = oMessageRequest,
                //    audit = oAuditRequest
                //};                                

                //objResponse = Claro.Web.Logging.ExecuteMethod(oAuditRequest.Session, oAuditRequest.transaction, () =>
                //{
                //    return ServiceFixed.ConsultarPCRFPaquetesAdic(objRequest);
                //});

                
                
                //Paquetes adquiridos BSCS ---------------------------------------------------------------
                

                Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.PaquetesAdquiridosHistoricoRequest objRequestP = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.PaquetesAdquiridosHistoricoRequest()
                {
                    audit = oAuditRequest2
                };

                objRequestP.audit.Session = "111111112121212";
                oAuditRequest.Session = "111111112121213";
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "PackageHistory msisdn " + strTelephone);
                Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "PackageHistory Sncode " + PaquetesSncodesIFI[0].ToString());

                objResponseData1 = Claro.Web.Logging.ExecuteMethod(oAuditRequest.Session, oAuditRequest.transaction, () =>
                 {
                     return oServiceCommon.GetPaquetesAdquiridosHistorico(objRequestP, strTelephone, PaquetesSncodesIFI[0].ToString());
                 });

                //----------------------------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oAuditRequest.Session, oAuditRequest.transaction, ex.Message.ToString());
            }
            Claro.Web.Logging.Info(oAuditRequest.Session, oAuditRequest.transaction, "Inicio Fin : ConsultarPCRFPaquetesAdic");

            //if (objResponse.consultarPaquetesResponseType.responseData.listaFact != null)
            //{
            //    objResponse.consultarPaquetesResponseType.responseData.listaFact = objResponse.consultarPaquetesResponseType.responseData.listaFact.OrderByDescending(x => x.fechaAct).ToList();
            //}
            //return Json(new { data = objResponse.consultarPaquetesResponseType.responseData });

            if (objResponseData1.listaFact != null)
            {
                objResponseData1.listaFact = objResponseData1.listaFact.OrderByDescending(x => x.fechaAct).ToList();
            }
            return Json(new { data = objResponseData1 });


        }


    }
}
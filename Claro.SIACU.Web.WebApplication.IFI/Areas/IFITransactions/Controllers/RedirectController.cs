using System;
using System.Web.Mvc;


namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class RedirectController : Controller
    {

        ///<summary>Metodo que retorna la vista del controlador para Index</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>PlanMigration</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public ActionResult Index()
        {
            return View();
        }


        ///<summary>Metodo que retorna la vista del controlador del puente de la redirección</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>BritgeRedirect</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public ActionResult BritgeRedirect(string secuencia)
        {

            ViewBag.sequence = secuencia;

            return View();

        }


        ///<summary>Metodo que retorna la vista parcial de cambio de número</summary>
        ///<returns>ActionResult<returns>
        ///<remarks>ChangeNumber</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public ActionResult ChangeNumber()
        {
            return PartialView();
        }


        ///<summary>Metodo que obtiene los datos de la redirección</summary>
        ///<param name="sequence"></param>
        ///<returns>JsonResult<returns>
        ///<remarks>GetRedirect</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>  
        /// <item><FecCrea>10/01/2018.</FecCrea></item></list>
        public JsonResult GetRedirect(string sequence)
        {

            string strServerName = System.Web.HttpContext.Current.Server.MachineName;
            string strNroNodo = string.Empty;
            string strIdSession = App_Code.Common.GetTransactionID();

            string[] response = new string[4];
            CommonIFIService.ValidateCommunicationResponse objValidateRedirectComResponseDashboard = new CommonIFIService.ValidateCommunicationResponse();
            CommonIFIService.ValidateCommunicationRequest objValidateRedirectComRequestDashboard = new CommonIFIService.ValidateCommunicationRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<CommonIFIService.AuditRequest>(strIdSession),
                Sequence = sequence,
                Server = App_Code.Common.GetApplicationIp()
            };

            try
            {

                objValidateRedirectComResponseDashboard = new CommonIFIService.CommonIFIServiceClient().ValidateRedirectCommunication(objValidateRedirectComRequestDashboard);

            }
            catch (Exception ex)
            {
                objValidateRedirectComResponseDashboard = new CommonIFIService.ValidateCommunicationResponse();
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, ex.Message);
            }

            try
            {
                if (objValidateRedirectComResponseDashboard.ResultValCommunication)
                {
                    Claro.Web.Logging.Info(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, objValidateRedirectComResponseDashboard.ResultValCommunication.ToString());
                    Claro.Web.Logging.Info(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, "URL =>" + objValidateRedirectComResponseDashboard.url);
                    Claro.Web.Logging.Info(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, "Availability =>" + objValidateRedirectComResponseDashboard.Availability);
                    Claro.Web.Logging.Info(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, "JsonString =>" + objValidateRedirectComResponseDashboard.JsonString);
                    Claro.Web.Logging.Info(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, "ErrorMessage =>" + objValidateRedirectComResponseDashboard.ErrorMessage);

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, ex.Message);
            }

            if (strServerName.Length > 1)
            {
                strNroNodo = strServerName.Substring((strServerName.Length - 2), 2);
            }

            if (objValidateRedirectComResponseDashboard.ResultValCommunication)
            {
                response[0] = "/RetentionCancelServices/RetentionCancelServices";// objValidateRedirectComResponseDashboard.url;
                response[1] = objValidateRedirectComResponseDashboard.Availability;
                response[2] = objValidateRedirectComResponseDashboard.JsonString;
                response[3] = strNroNodo;
            }
            else
            {
                Claro.Web.Logging.Error(strIdSession, objValidateRedirectComRequestDashboard.audit.transaction, objValidateRedirectComResponseDashboard.ErrorMessage);
                throw new MessageException(objValidateRedirectComResponseDashboard.ErrorMessage);
            }


            return Json(response);

        }



        }
    }

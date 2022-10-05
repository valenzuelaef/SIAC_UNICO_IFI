using Claro.SIACU.Web.WebApplication.IFI.CommonIFIService;
using Claro.SIACU.Web.WebApplication.IFI.PostIFIService;
using Claro.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSTPAID = Claro.SIACU.Web.WebApplication.IFI.PostIFIService;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers
{
    public class ChangeBillingCycleController : Controller
    {
        private readonly Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient _oServiceCommon = new Claro.SIACU.Web.WebApplication.IFI.CommonIFIService.CommonIFIServiceClient();
        private readonly Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient _oServicePostpaid = new Claro.SIACU.Web.WebApplication.IFI.PostIFIService.PostIFIServiceClient();

        /// <summary>Método que permite mostrar la vista</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>ChangeBillingCycle</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public ActionResult ChangeBillingCycle()
        {
            return View();
        }

        /// <summary>Método que la fecha de la aplicación</summary>
        /// <param name="strIdSession"></param>
        /// <param name="value"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetDateAplication</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetDateAplication(string strIdSession, string value)
        {
            DateTime dtmDateNow = DateTime.Now;
            DateTime dtmAddMonth = DateTime.MinValue;
            string strDateCyclyBillingLessOneTemp = string.Empty;
            string strDateCyclyBillingLessOne = string.Empty;

            if (Convert.ToInt(dtmDateNow.Day) >= Convert.ToInt(value))
            {
                dtmAddMonth = dtmDateNow.AddMonths(1);
                strDateCyclyBillingLessOneTemp = value + "/" + dtmAddMonth.Month.ToString() + "/" + dtmAddMonth.Year.ToString();
                dtmAddMonth = Convert.ToDate(strDateCyclyBillingLessOneTemp);
                strDateCyclyBillingLessOne = dtmAddMonth.AddDays(-1).ToShortDateString();
            }
            else if (Convert.ToInt(dtmDateNow.Day) < Convert.ToInt(value))
            {
                if (Convert.ToInt(dtmDateNow.Day) == Convert.ToInt(Convert.ToDate(value + "/" + dtmAddMonth.Month.ToString() + "/" + dtmAddMonth.Year.ToString()).AddDays(-1).Day))
                {
                    strDateCyclyBillingLessOneTemp = value + "/" + dtmDateNow.Month.ToString() + "/" + dtmDateNow.Year.ToString();
                    dtmAddMonth = Convert.ToDate(strDateCyclyBillingLessOneTemp);
                    dtmAddMonth = dtmAddMonth.AddMonths(1);
                    strDateCyclyBillingLessOne = dtmAddMonth.AddDays(-1).ToShortDateString();
                }
                else
                {
                    strDateCyclyBillingLessOneTemp = value + "/" + dtmDateNow.Month.ToString() + "/" + dtmDateNow.Year.ToString();
                    dtmAddMonth = Convert.ToDate(strDateCyclyBillingLessOneTemp);
                    strDateCyclyBillingLessOne = dtmAddMonth.AddDays(-1).ToShortDateString();
                }
            }

            return Json(new { data = strDateCyclyBillingLessOne });
        }

        /// <summary>Método que obtiene el listado del ciclo de facturación</summary>
        /// <param name="strIdSession"></param>
        /// <param name="strCustomerID"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetListCyclesBilling</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public JsonResult GetListCyclesBilling(string strIdSession, string strCustomerID)
        {
            List<ListItem> listItem =
                    new List<ListItem>();
            POSTPAID.AuditRequest audit =
                 App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.IFI.PostIFIService.AuditRequest>(strIdSession);
            POSTPAID.BillingCycleRequest objBillingCycleRequest = new POSTPAID.BillingCycleRequest()
            {
                audit = audit,
                strTypeCustomer = strCustomerID
            };


            POSTPAID.BillingCycleResponse objBillingCycleResponse = new POSTPAID.BillingCycleResponse();


            try
            {
                objBillingCycleResponse =
                    Logging.ExecuteMethod<POSTPAID.BillingCycleResponse>(() =>
                    {
                        return _oServicePostpaid.GetBillingCycle(objBillingCycleRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objBillingCycleRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }


            if (objBillingCycleResponse != null && objBillingCycleResponse.LstBillingCycleResponse != null)
            {



                foreach (BillingCycle item in objBillingCycleResponse.LstBillingCycleResponse)
                {
                    listItem.Add(new ListItem()
                    {
                        Code = item.strBicicle,
                        Description = item.strBicicle
                    });
                }

            }

            return Json(new { data = listItem });
        }
    }
}
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Controllers.AuthUser
{
    public class AuthController : Controller
    {

        /// <summary>Método que permite mostrar la vista</summary>
        /// <returns>ActionResult</returns>
        /// <remarks>AuthUserHtml</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019.</FecCrea></item></list>
        public ActionResult AuthUserHtml()
        {
            return View();
        }
	}
}
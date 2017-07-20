using System.Web.Mvc;

namespace LuStoreWebService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Lu Store";

            return View();
        }
    }
}
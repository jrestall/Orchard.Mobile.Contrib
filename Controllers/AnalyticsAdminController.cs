using System.Web.Mvc;
using Orchard.UI.Admin;

namespace Orchard.Mobile.Contrib.Controllers
{
    [Admin]
    public class AnalyticsAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
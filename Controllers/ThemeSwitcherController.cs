using System.Web.Mvc;
using System.Web.Routing;

namespace Orchard.Mobile.Contrib.Controllers
{
    public class ThemeSwitcherController : Controller
    {
        private readonly IWorkContextAccessor _workContextAccessor;

        public ThemeSwitcherController(IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
        }

        public ActionResult SetThemeGroup(string group)
        {
            var context = _workContextAccessor.GetContext();
            var session = context.HttpContext.Session;
            if (session != null)
            {
                session[context.CurrentSite.SiteName + "MobileContrib.ThemeSwitcher.DeviceGroup"] = group;
            }

            return RedirectToHome();
        }

        private static ActionResult RedirectToHome()
        {
            return new RedirectToRouteResult("",
                                             new RouteValueDictionary {
                                                 {"area", "HomePage"},
                                                 {"controller", "Home"},
                                                 {"action", "Index"}
                                             });
        }
    }
}
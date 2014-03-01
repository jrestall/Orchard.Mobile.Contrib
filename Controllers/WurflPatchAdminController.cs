using System.Web.Mvc;
using Orchard.Localization;
using Orchard.Mobile.Contrib.Services;
using Orchard.UI.Admin;

namespace Orchard.Mobile.Contrib.Controllers
{
    [Admin]
    public class WurflPatchAdminController : Controller
    {
        private IWurflService _wurflService;

        public WurflPatchAdminController(IOrchardServices services)
        {
            Services = services;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        public IOrchardServices Services { get; set; }

        public ActionResult Index()
        {
            //IEnumerable<PatchFile> patchFiles = _wurflService.GetPatchFiles();
            //var model = new WurflPatchIndexViewModel { PatchFiles = patchFiles };
            return View();
        }
    }
}
using System.Web.Mvc;
using OpenDDR.Service.Services;
using Orchard;
using Orchard.Localization;
using Contrib.Mobile.ViewModels;
using Orchard.Mvc;
using Orchard.UI.Admin;

namespace Contrib.Mobile.Controllers
{
    [Admin]
    public class OpenDDRAdminController : Controller
    {
        private readonly IOddrDeviceService _wurflService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OpenDDRAdminController(IOrchardServices services, IOddrDeviceService wurflService, IHttpContextAccessor httpContextAccessor)
        {
            _wurflService = wurflService;
            _httpContextAccessor = httpContextAccessor;
            Services = services;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        public IOrchardServices Services { get; set; }

        public ActionResult UserAgentSearch()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();

            return View(new UserAgentSearchViewModel());
        }

        [HttpPost]
        public ActionResult UserAgentSearch(UserAgentSearchOptions options)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();

            // Use the current browsers user agent if none is given.
            if (string.IsNullOrEmpty(options.UserAgentText))
            {
                options.UserAgentText = _httpContextAccessor.Current().Request.UserAgent;
                ModelState.Clear(); // HTML helpers such as TextAreaFor will always bind to modelstate over an explicitly passed model object.
            }

            var device = _wurflService.GetDevice(options.UserAgentText);

            return View(new UserAgentSearchViewModel { Device = device, Options = options });
        }
    }
}
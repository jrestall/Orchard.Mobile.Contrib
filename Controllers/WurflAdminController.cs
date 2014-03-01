using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.Localization;
using Orchard.Mobile.Contrib.Services;
using Orchard.Mobile.Contrib.ViewModels;
using Orchard.Mvc;
using Orchard.UI.Admin;

namespace Orchard.Mobile.Contrib.Controllers
{
    [Admin]
    public class WurflAdminController : Controller
    {
        private readonly IWurflService _wurflService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WurflAdminController(IOrchardServices services, IWurflService wurflService, IHttpContextAccessor httpContextAccessor)
        {
            _wurflService = wurflService;
            _httpContextAccessor = httpContextAccessor;
            Services = services;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        public IOrchardServices Services { get; set; }

        public ActionResult Index()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();



            return View();
        }

        public ActionResult UploadWurfl()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();



            return View();
        }

        public ActionResult UploadImages()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();



            return View();
        }

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

            var capabilities = _wurflService.GetCapabilities(options.UserAgentText);

            // Extract the wurfl capabilities into a nice list
            var wurflcapabilities = new List<DeviceCapability>();
            foreach (var key in capabilities.Keys)
            {
                string keyText = Convert.ToString(key);
                if (keyText == "WurflCapabilities")
                {
                    var dictionary = capabilities[key] as IDictionary;
                    if (dictionary != null)
                    {
                        foreach (var wurflKey in dictionary.Keys)
                        {
                            wurflcapabilities.Add(new DeviceCapability
                            {
                                Name = Convert.ToString(wurflKey),
                                Value = Convert.ToString(dictionary[wurflKey])
                            });
                        }
                    }
                    continue;
                }

                wurflcapabilities.Add(new DeviceCapability
                {
                    Name = keyText,
                    Value = Convert.ToString(capabilities[key])
                });
            }

            if(!string.IsNullOrEmpty(options.CapabilityFilter))
            {
                wurflcapabilities = wurflcapabilities.Where(
                        x => x.Name.Contains(options.CapabilityFilter) || x.Value.Contains(options.CapabilityFilter)).ToList();
            }

            return View(new UserAgentSearchViewModel { Capabilities = wurflcapabilities, Options = options });
        }

        public ActionResult ViewLogs()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();

            var viewModel = new WurflViewLogsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ViewLogs(WurflViewLogsViewModel model)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageWurfl, T("Not allowed to manage wurfl")))
                return new HttpUnauthorizedResult();

            return View(model);
        }
    }
}
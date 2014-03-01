using System;
using System.Web.Routing;
using Orchard.Localization;
using Orchard.Themes;
using Orchard.UI.Admin;

namespace Orchard.Mobile.Contrib.Services
{
    public class DeviceThemeSelector : IThemeSelector
    {
        private readonly IDeviceGroupService _deviceGroupService;
        private readonly IWorkContextAccessor _workContextAccessor;

        private ThemeSelectorResult _result;

        public DeviceThemeSelector(IDeviceGroupService deviceGroupService, IWorkContextAccessor workContextAccessor)
        {
            _deviceGroupService = deviceGroupService;
            _workContextAccessor = workContextAccessor;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public ThemeSelectorResult GetTheme(RequestContext context)
        {
            if (AdminFilter.IsApplied(context)) return null;

            if (_result != null) return _result;

            try
            {
                var workContext = _workContextAccessor.GetContext();
                var session = workContext.HttpContext.Session;
                if (session != null)
                {
                    string groupName = session[workContext.CurrentSite.SiteName + "MobileContrib.ThemeSwitcher.DeviceGroup"] as string;
                    if(groupName != null)
                    {
                        var devicegroup = _deviceGroupService.GetGroup(groupName);

                        _result = new ThemeSelectorResult
                        {
                            Priority = 50,
                            ThemeName = devicegroup.Theme
                        };
                        return _result;
                    }
                }

                var part = _deviceGroupService.GetCurrentGroup();
                if (part != null)
                {
                    _result = new ThemeSelectorResult
                    {
                        Priority = 50,
                        ThemeName = part.Theme
                    };
                    return _result;
                }
            }
            catch (Exception)
            {
                // HACK (jamesr): The theme selector seems to be getting called before the DeviceGroupRecord database table is created. Need to find a proper fix.
            }

            return null;
        }
    }
}
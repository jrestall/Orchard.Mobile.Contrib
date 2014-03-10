using System;
using System.Collections.Generic;
using System.Linq;
using OpenDDR.Service.Services;
using Orchard.Mvc;
using Orchard.Widgets.Services;

namespace Contrib.Mobile.Providers
{
    public class BrowserCapabilitiesFilterProvider : IRuleProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOddrDeviceService _oddrDeviceService;
        private readonly List<string> _functions = new List<string> { "device", "device-property", "browser-property", "os-property"};

        public BrowserCapabilitiesFilterProvider(IHttpContextAccessor httpContextAccessor, IOddrDeviceService oddrDeviceService) {
            _httpContextAccessor = httpContextAccessor;
            _oddrDeviceService = oddrDeviceService;
        }

        public void Process(RuleContext ruleContext) {

            //Not my game - return
            if (!_functions.Any(x => String.Equals(ruleContext.FunctionName, x, StringComparison.OrdinalIgnoreCase))) return;

            var request = _httpContextAccessor.Current().Request;
            var device = _oddrDeviceService.GetDevice(request.UserAgent);

            if (String.Equals(ruleContext.FunctionName, "device", StringComparison.OrdinalIgnoreCase )) {
                if (ruleContext.Arguments.Any(x => String.Equals("tablet", x.ToString(), StringComparison.OrdinalIgnoreCase)) && device.IsTablet) {
                    ruleContext.Result = true;
                    return;
                }

                if (ruleContext.Arguments.Any(x => String.Equals("phone", x.ToString(), StringComparison.OrdinalIgnoreCase) && device.IsPhone))
                {
                    ruleContext.Result = true;
                    return;
                }

                if (ruleContext.Arguments.Any(x => String.Equals("desktop", x.ToString(), StringComparison.OrdinalIgnoreCase)) && device.IsDesktop)
                {
                    ruleContext.Result = true;
                    return;
                }
            }

            ruleContext.Result = false;
        }
    }
}
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
        private readonly List<string> _functions = new List<string> { "device", "device_property", "browser_property", "os_property" };

        public BrowserCapabilitiesFilterProvider(IHttpContextAccessor httpContextAccessor, IOddrDeviceService oddrDeviceService)
        {
            _httpContextAccessor = httpContextAccessor;
            _oddrDeviceService = oddrDeviceService;
        }

        public void Process(RuleContext ruleContext)
        {

            //Not my game - return
            if (!_functions.Any(x => String.Equals(ruleContext.FunctionName, x, StringComparison.OrdinalIgnoreCase))) return;

            var request = _httpContextAccessor.Current().Request;
            var device = _oddrDeviceService.GetDevice(request.UserAgent);

            //Device - IsPhone, IsTablet, IsDesktop 
            if (String.Equals(ruleContext.FunctionName, "device", StringComparison.OrdinalIgnoreCase))
            {
                if (ruleContext.Arguments.Any(x => String.Equals("tablet", x.ToString(), StringComparison.OrdinalIgnoreCase)) && device.IsTablet)
                {
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

                ruleContext.Result = false;
                return;
            }

            //Device properties
            if (String.Equals(ruleContext.FunctionName, "device_property", StringComparison.OrdinalIgnoreCase))
            {
                CheckProperty(device.DeviceProperties, ruleContext);
                return;
            }

            //Browser properties
            if (String.Equals(ruleContext.FunctionName, "browser_property", StringComparison.OrdinalIgnoreCase))
            {
                CheckProperty(device.BrowserProperties, ruleContext);
                return;
            }

            //OS properties
            if (String.Equals(ruleContext.FunctionName, "os_property", StringComparison.OrdinalIgnoreCase))
            {
                CheckProperty(device.OsProperties, ruleContext);
                return;
            }
        }

        private static void CheckProperty(IDictionary<string, string> dictionary, RuleContext ruleContext) {
            var argumentsAsString = ruleContext.Arguments.Select(x => x.ToString()).ToList();

            var equalArguments = argumentsAsString.Where(x => x.Contains("=")).ToList();
            var boolArguments = argumentsAsString.Where(x => !x.Contains("=")).ToList();

            if (equalArguments
                .Select(equalsArgument => equalsArgument.Split('='))
                .Any(split => dictionary.ContainsKey(split[0]) &&
                              dictionary[split[0]].Equals(split[1], StringComparison.OrdinalIgnoreCase)))
            {

                ruleContext.Result = true;
                return;
            }

            if (boolArguments.Any(x => dictionary.ContainsKey(x) &&
                                       dictionary[x].Equals("true", StringComparison.OrdinalIgnoreCase)))
            {
                ruleContext.Result = true;
                return;
            }

            ruleContext.Result = false;
        }
    }
}
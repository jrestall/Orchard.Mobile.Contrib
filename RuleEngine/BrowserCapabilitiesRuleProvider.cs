using System;
using Orchard.Mobile.Contrib.Services;
using Orchard.Mvc;

namespace Orchard.Mobile.Contrib.RuleEngine
{
    public class BrowserCapabilitiesFilterProvider : IDeviceGroupRuleProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrowserCapabilitiesFilterProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Process(RuleContext ruleContext)
        {
            var obj = _httpContextAccessor.Current().Request.Browser[ruleContext.FunctionName];
            if (obj == null)
                return;

            var capability = Convert.ToString(obj);
            if(string.IsNullOrEmpty(capability))
                return;

            if (ruleContext.Arguments.Length <= 0)
            {
                ruleContext.Result = true;
                return;
            }

            var capabilityToCompare = Convert.ToString(ruleContext.Arguments[0]);
            if (!String.Equals(capability, capabilityToCompare, StringComparison.OrdinalIgnoreCase))
            {
                ruleContext.Result = false;
                return;
            }

            ruleContext.Result = true;
        }
    }
}
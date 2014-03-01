using System.Collections.Generic;

namespace Orchard.Mobile.Contrib.ViewModels
{
    public class UserAgentSearchViewModel
    {
        public UserAgentSearchOptions Options { get; set; }

        public IList<DeviceCapability> Capabilities { get; set; }
    }

    public class UserAgentSearchOptions
    {
        public string UserAgentText { get; set; }

        public string CapabilityFilter { get; set; }
    }

    public class DeviceCapability
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
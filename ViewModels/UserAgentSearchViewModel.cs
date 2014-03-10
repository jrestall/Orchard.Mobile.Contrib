using System.Collections.Generic;
using OpenDDR.Service.Modes;

namespace Contrib.Mobile.ViewModels
{
    public class UserAgentSearchViewModel
    {
        public UserAgentSearchOptions Options { get; set; }

        public Device Device { get; set; }
    }

    public class UserAgentSearchOptions
    {
        public string UserAgentText { get; set; }

        public string CapabilityFilter { get; set; }
    }
}
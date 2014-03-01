using System.Collections.Generic;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.ViewModels
{
    public class ThemesEditViewModel
    {
        public DeviceGroupRecord DeviceGroup { get; set; }

        public IEnumerable<string> Themes { get; set; }
    }
}
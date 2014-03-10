using System.Collections.Generic;
using Contrib.Mobile.Models;

namespace Contrib.Mobile.ViewModels
{
    public class ThemesEditViewModel
    {
        public DeviceGroupRecord DeviceGroup { get; set; }

        public IEnumerable<string> Themes { get; set; }
    }
}
using System.Collections.Generic;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.ViewModels
{
    public class ThemesListViewModel
    {
        public IEnumerable<DeviceGroupPart> DeviceGroups { get; set; }
    }
}
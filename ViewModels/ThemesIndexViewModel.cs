using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.ViewModels
{
    public class ThemesIndexViewModel
    {
        public IEnumerable<DeviceGroupRecord> DeviceGroups { get; set; }

        public IEnumerable<ThemeEntry> Themes { get; set; }
    }

    public class ThemeEntry
    {
        public ExtensionDescriptor Descriptor { get; set; }

        public string Id { get { return Descriptor.Id; } }
        public string Name { get { return Descriptor.Name; } }
        public string ThemePath(string path)
        {
            return Descriptor.Location + "/" + Descriptor.Id + path;
        }
    }
}
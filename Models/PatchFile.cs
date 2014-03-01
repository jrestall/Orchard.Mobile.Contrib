using System;

namespace Orchard.Mobile.Contrib.Models
{
    public class PatchFile
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
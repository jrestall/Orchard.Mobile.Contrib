using System.Collections.Generic;

namespace Contrib.Mobile.ViewModels
{
    public class EditDeviceGroupViewModel
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string SelectionRule { get; set; }

        public virtual string Theme { get; set; }

        public IEnumerable<string> Themes { get; set; }

        public bool SwitcherEnabled { get; set; }

        public string SwitcherText { get; set; }

        public string SwitcherPosition { get; set; }

        public string SwitcherZone { get; set; }
    }
}
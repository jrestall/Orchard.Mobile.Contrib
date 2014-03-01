using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Orchard.Mobile.Contrib.Models
{
    public class DeviceGroupRecord : ContentPartRecord
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string IconPath { get; set; }

        public virtual string SelectionRule { get; set; }

        public virtual string Theme { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual int Position { get; set; }

        public virtual bool SwitcherEnabled { get; set; }

        public virtual string SwitcherText { get; set; }

        public virtual string SwitcherPosition { get; set; }

        public virtual string SwitcherZone { get; set; }
    }

    public class DeviceGroupPart : ContentPart<DeviceGroupRecord>
    {
        [Required]
        public string Name 
        { 
            get { return Record.Name;  }
            set { Record.Name = value;  }
        }

        [Required]
        public string Description
        {
            get { return Record.Description; }
            set { Record.Description = value; }
        }

        public string IconPath
        {
            get { return Record.IconPath; }
            set { Record.IconPath = value; }
        }

        [Required]
        public string SelectionRule
        {
            get { return Record.SelectionRule; }
            set { Record.SelectionRule = value; }
        }

        [Required]
        public string Theme
        {
            get { return Record.Theme; }
            set { Record.Theme = value; }
        }

        public bool Enabled
        {
            get { return Record.Enabled; }
            set { Record.Enabled = value; }
        }

        public int Position
        {
            get { return Record.Position; }
            set { Record.Position = value; }
        }

        public string SwitcherText
        {
            get { return Record.SwitcherText; }
            set { Record.SwitcherText = value; }
        }

        public string SwitcherPosition
        {
            get { return Record.SwitcherPosition; }
            set { Record.SwitcherPosition = value; }
        }

        public string SwitcherZone
        {
            get { return Record.SwitcherZone; }
            set { Record.SwitcherZone = value; }
        }

        public bool SwitcherEnabled
        {
            get { return Record.SwitcherEnabled; }
            set { Record.SwitcherEnabled = value; }
        }
    }

}
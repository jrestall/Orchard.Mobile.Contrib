using System;
using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.Mobile.Contrib.Models;
using Orchard.Mobile.Contrib.Services;
using Orchard.Mobile.Contrib.ViewModels;

namespace Orchard.Mobile.Contrib.Drivers
{
    public class DeviceGroupDriver : ContentPartDriver<DeviceGroupPart>
    {
        private readonly IDeviceGroupService _deviceGroupService;
        private readonly IRuleManager _ruleManager;

        public DeviceGroupDriver(IDeviceGroupService deviceGroupService, IRuleManager ruleManager)
        {
            _deviceGroupService = deviceGroupService;
            _ruleManager = ruleManager;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix
        {
            get { return "DeviceGroup"; }
        }

        //GET
        protected override DriverResult Editor(
            DeviceGroupPart part, dynamic shapeHelper)
        {
            var results = new List<DriverResult>
                {
                    ContentShape("Parts_DeviceGroup_Edit",
                        () => shapeHelper.EditorTemplate(
                                TemplateName: "Parts/DeviceGroup",
                                Model: BuildEditorViewModel(part),
                                Prefix: Prefix))
                };
            
            if (part.Id > 0)
                results.Add(ContentShape("DeviceGroup_DeleteButton",
                    deleteButton => deleteButton));

            return Combined(results.ToArray());
        }

        //POST
        protected override DriverResult Editor(
            DeviceGroupPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if(updater.TryUpdateModel(part, Prefix, null, null))
            {
                try
                {
                    _ruleManager.Matches(part.SelectionRule);
                }
                catch (Exception e)
                {
                    updater.AddModelError("SelectionRule", T("The rule is not valid: {0}", e.Message));
                }
            }
            return Editor(part, shapeHelper);
        }

        private EditDeviceGroupViewModel BuildEditorViewModel(DeviceGroupPart part)
        {
            var viewModel = new EditDeviceGroupViewModel
                                {
                                    Name = part.Name,
                                    Description = part.Description,
                                    SelectionRule = part.SelectionRule,
                                    Theme = part.Theme,
                                    Themes = _deviceGroupService.GetThemes(),
                                    SwitcherEnabled = part.SwitcherEnabled,
                                    SwitcherPosition = part.SwitcherPosition,
                                    SwitcherText = part.SwitcherText,
                                    SwitcherZone = part.SwitcherZone
                                };

            return viewModel;
        }
    }
}
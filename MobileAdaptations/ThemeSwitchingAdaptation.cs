using System.Web.Mvc;
using Orchard.DisplayManagement;
using Orchard.Mobile.Contrib.Services;
using Orchard.Mvc.Filters;
using Orchard.UI.Admin;

namespace Orchard.Mobile.Contrib.MobileAdaptations
{
    public class ThemeSwitchingAdaptation : FilterProvider, IResultFilter
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IDeviceGroupService _deviceGroupService;

        public ThemeSwitchingAdaptation(IWorkContextAccessor workContextAccessor, IDeviceGroupService deviceGroupService, IShapeFactory shapeFactory)
        {
            _workContextAccessor = workContextAccessor;
            _deviceGroupService = deviceGroupService;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult == null)
                return;

            var workContext = _workContextAccessor.GetContext(filterContext);

            if (workContext == null ||
                workContext.Layout == null ||
                workContext.CurrentSite == null ||
                AdminFilter.IsApplied(filterContext.RequestContext))
            {
                return;
            }

            var group = _deviceGroupService.GetCurrentGroup();
            if (group == null)
                return;

            if (group.Record.SwitcherEnabled && !string.IsNullOrWhiteSpace(group.Record.SwitcherText))
            {
                if (!string.IsNullOrWhiteSpace(group.Record.SwitcherZone))
                {
                    dynamic switcherShape = Shape.ThemeSwitcher_Links(SwitcherText: group.Record.SwitcherText);
                    var zone = workContext.Layout.Zones[group.Record.SwitcherZone];
                    zone.Add(switcherShape, group.Record.SwitcherPosition);
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
        }
    }
}
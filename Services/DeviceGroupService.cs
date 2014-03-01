using System.Collections.Generic;
using System.Linq;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.Environment.Descriptor.Models;
using Orchard.Environment.Extensions;
using Orchard.Environment.Extensions.Models;
using Orchard.Localization;
using Orchard.Mobile.Contrib.Models;
using Orchard.UI.Notify;

namespace Orchard.Mobile.Contrib.Services
{
    public class DeviceGroupService : IDeviceGroupService
    {
        private readonly IContentManager _contentManager;
        private readonly ISignals _signals;
        private readonly ICacheManager _cacheManager;
        private readonly IExtensionManager _extensionManager;
        private readonly ShellDescriptor _shellDescriptor;
        private readonly IRuleManager _ruleManager;
        private readonly INotifier _notifier;

        public DeviceGroupService(
            IContentManager contentManager, 
            ISignals signals,
            ICacheManager cacheManager,
            IExtensionManager extensionManager,
            ShellDescriptor shellDescriptor,
            IRuleManager ruleManager, 
            INotifier notifier
            )
        {
            _contentManager = contentManager;
            _signals = signals;
            _cacheManager = cacheManager;
            _extensionManager = extensionManager;
            _shellDescriptor = shellDescriptor;
            _ruleManager = ruleManager;
            _notifier = notifier;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<DeviceGroupPart> Get(VersionOptions versionOptions)
        {
            return _contentManager.Query(versionOptions, "DeviceGroup").List().Select(ci => ci.As<DeviceGroupPart>()); ;
        }

        public IEnumerable<string> GetThemes()
        {
            return _extensionManager.AvailableExtensions()
                    .Where(d => DefaultExtensionTypes.IsTheme(d.ExtensionType))
                    .Select(s => s.Id);
        }

        public ContentItem Get(int id, VersionOptions versionOptions)
        {
            return _contentManager.Get(id, versionOptions);
        }

        public void DeleteGroup(int id)
        {
            _contentManager.Remove(Get(id, VersionOptions.Latest));
        }

        //TODO: Cache per request
        public DeviceGroupPart GetCurrentGroup()
        {
            var deviceGroups = Get(VersionOptions.Latest);

            foreach (var record in deviceGroups.Where(g => g.Enabled).OrderBy(x => x.Position))
            {
                try
                {
                    bool matches = _ruleManager.Matches(record.SelectionRule);
                    if (matches)
                    {
                        return record;
                    }
                }
                catch (OrchardException ex)
                {
                    _notifier.Error(T("Contrib.Mobile module is configured incorrectly: {0}", ex.Message));
                }
            }

            return null;
        }

        public DeviceGroupPart GetGroup(string groupName)
        {
            return Get(VersionOptions.Latest).Where(g => g.Name == groupName).FirstOrDefault();
        }

        public void SetPosition(int id, bool moveUp)
        {
            var groups = Get(VersionOptions.Latest);
            if (groups == null)
                return;

            // Set the group's position
            var groupToChange = groups.Where(x => x.Id == id).FirstOrDefault();
            if (groupToChange == null)
                return;

            int newPosition;
            if (moveUp)
            {
                newPosition = groupToChange.Position - 1;
            }
            else
            {
                newPosition = groupToChange.Position + 1;
            }

            if (newPosition <= 0 || newPosition > groups.Count())
                return;

            groupToChange.Position = newPosition;

            var orderedgroups = groups.OrderBy(x => x.Position)
                                      .ThenBy(x =>  {
                                                        if(moveUp)
                                                        {
                                                            return x.Id == id ? 0 : 1;
                                                        }
                                                        return x.Id == id ? 1 : 0;
                                                    });

            int positionCount = 1;
            foreach (var group in orderedgroups)
            {
                group.Position = positionCount++;
            }
        }
    }
}
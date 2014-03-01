using System.Collections.Generic;
using Orchard.ContentManagement;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.Services
{
    public interface IDeviceGroupService : IDependency
    {
        IEnumerable<DeviceGroupPart> Get(VersionOptions versionOptions);
        IEnumerable<string> GetThemes();
        ContentItem Get(int id, VersionOptions versionOptions);
        void DeleteGroup(int id);
        DeviceGroupPart GetCurrentGroup();
        DeviceGroupPart GetGroup(string groupName);
        void SetPosition(int id, bool moveUp);
    }
}
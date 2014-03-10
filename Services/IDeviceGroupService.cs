using System.Collections.Generic;
using Orchard;
using Orchard.ContentManagement;
using Contrib.Mobile.Models;

namespace Contrib.Mobile.Services
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
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Contrib.Mobile.Models;

namespace Contrib.Mobile.Handlers
{
    [UsedImplicitly]
    public class DeviceGroupHandler : ContentHandler
    {
        public DeviceGroupHandler(IRepository<DeviceGroupRecord> repository, IContentManager contentManager)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
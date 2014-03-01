using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.Handlers
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
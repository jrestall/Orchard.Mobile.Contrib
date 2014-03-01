using Orchard.UI.Resources;

namespace Orchard.Mobile.Contrib
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            builder.Add().DefineStyle("MobileContribAdmin").SetUrl("mobile-contrib-admin.css");
        }
    }
}
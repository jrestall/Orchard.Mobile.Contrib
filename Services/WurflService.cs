using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Configuration;
using FiftyOne.Foundation.Mobile.Detection;

namespace Orchard.Mobile.Contrib.Services
{
    public class WurflService : IWurflService
    {
        //private readonly IWurflFolder _wurflFolder;

        //public WurflService(IWurflFolder wurflFolder)
        //{
        //    _wurflFolder = wurflFolder;
        //}

        //public IEnumerable<PatchFile> GetPatchFiles()
        //{
        //    return _wurflFolder.ListPatches();
        //}

        public IDictionary GetCapabilities(string userAgent)
        {
            var baseCapabilities = new HttpBrowserCapabilities
            {
                Capabilities = new Hashtable { { string.Empty, userAgent } }
            };
            var factory = new BrowserCapabilitiesFactory();
            factory.ConfigureBrowserCapabilities(new NameValueCollection(), baseCapabilities);

            // Get the new and overridden capabilities.
            IDictionary overrideCapabilities = Factory.Create(userAgent);

            var capabilities = baseCapabilities.Capabilities;
            if (overrideCapabilities != null)
            {
                // Create a new browser capabilities instance combining the two.
                var customCapabilities = new FiftyOneBrowserCapabilities(baseCapabilities, overrideCapabilities);
                capabilities = customCapabilities.Capabilities;
            }

            return capabilities;
        }
    }
}
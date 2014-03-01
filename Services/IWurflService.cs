using System.Collections;

namespace Orchard.Mobile.Contrib.Services
{
    public interface IWurflService : IDependency
    {
        //IEnumerable<PatchFile> GetPatchFiles();

        IDictionary GetCapabilities(string userAgent);
    }
}
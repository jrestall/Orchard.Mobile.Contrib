using System;
using System.IO;
using System.Web.Configuration;
using Orchard.Environment;
using Orchard.Environment.Descriptor.Models;
using Orchard.Environment.Extensions.Models;
using Orchard.FileSystems.AppData;
using Orchard.FileSystems.WebSite;
using Orchard.Logging;

namespace Orchard.Mobile.Contrib
{
    public class ModuleInstaller : IFeatureEventHandler, IOrchardShellEvents
    {
        // TODO: (jamesr) Cleanup constants, add string[] for patches etc, move logic to IWurflFolder etc.
        const string WurflFolderPath = "~/Modules/Orchard.Mobile.Contrib/App_Data/";
        const string WurflFileName = "wurfl.xml.gz";

        private readonly IWebSiteFolder _webSiteFolder;
        private readonly IAppDataFolder _appDataFolder;
        private readonly ShellDescriptor _shellDescriptor;
        private string _wurflDestinationPath;
        private string _wurflPatchDestinationPath;

        public ModuleInstaller(IWebSiteFolder webSiteFolder, IAppDataFolder appDataFolder, ShellDescriptor shellDescriptor)
        {
            _webSiteFolder = webSiteFolder;
            _appDataFolder = appDataFolder;
            _shellDescriptor = shellDescriptor;

            _wurflDestinationPath = _appDataFolder.MapPath(_appDataFolder.Combine("Browsers", WurflFileName));
            _wurflPatchDestinationPath = _appDataFolder.MapPath(_appDataFolder.Combine("Browsers", "Patches"));

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Install(Feature feature)
        {

        }

        public void Enable(Feature feature)
        {
            SetCustomBrowserCapabilitiesProvider();
        }

        public void Disable(Feature feature)
        {
            try
            {
                RestoreDefaultBrowserCapabilitiesProvider();

                DeleteBrowsersDirectory();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An unexpected error occured while uninstalling the wurfl file.");

                throw;
            }
        }

        void IOrchardShellEvents.Activated()
        {
            if (!ModuleIsInstalled())
            {
                // Copies the wurfl file distributed with the module to the websites App_Data folder
                // We don't copy to each tenant as that would complicate wurfl file updates and maintenance.
                InstallToWebsiteBrowsersDirectory();
            }
            else
            {
                SetCustomBrowserCapabilitiesProvider();
            }
        }

        void IOrchardShellEvents.Terminating()
        {

        }

        public void Uninstall(Feature feature)
        {
            
        }

        private bool ModuleIsInstalled()
        {
            return _appDataFolder.FileExists(_wurflDestinationPath);
        }

        private void SetCustomBrowserCapabilitiesProvider()
        {
            // Enable the mobile detection provider.
            // We use the WURFL .NET API from http://51degrees.codeplex.com/
            HttpCapabilitiesBase.BrowserCapabilitiesProvider = new FiftyOne.Foundation.Mobile.Detection.MobileCapabilitiesProvider();
        }

        private void RestoreDefaultBrowserCapabilitiesProvider()
        {
            HttpCapabilitiesBase.BrowserCapabilitiesProvider = new HttpCapabilitiesDefaultProvider();
        }

        private void InstallToWebsiteBrowsersDirectory()
        {
            try
            {
                DeleteBrowsersDirectory();

                EnsureBrowsersDirectoryExists();

                // TODO: (jamesr) Need some checks on wurfl version in case the version we are deleting is newer than the default.
                CopyDefaultWurflToWebsite();

                CopyDefaultPatchesToWebsite();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An unexpected error occured while installing the wurfl file.");

                throw;
            }
        }

        private void CopyDefaultPatchesToWebsite()
        {
            string defaultPatchesFolder = Path.Combine(WurflFolderPath, "Patches");
            foreach(var file in _webSiteFolder.ListFiles(defaultPatchesFolder, false))
            {
                string detinationPatchPath = Path.Combine(_wurflPatchDestinationPath, Path.GetFileName(file));
                CopyFile(file, detinationPatchPath);
            }
        }

        private void CopyFile(string fromPath, string toPath)
        {
            using(var fromStream = new MemoryStream())
            {
                _webSiteFolder.CopyFileTo(fromPath, fromStream);
                fromStream.Seek(0, SeekOrigin.Begin);

                using(var toStream = _appDataFolder.CreateFile(toPath))
                {
                    toStream.Write(fromStream.GetBuffer(), 0, (int)fromStream.Length);
                }
            }
        }

        private void CopyDefaultWurflToWebsite()
        {
            CopyFile(Path.Combine(WurflFolderPath, WurflFileName), _wurflDestinationPath);
        }

        private void EnsureBrowsersDirectoryExists()
        {
            var directory = new DirectoryInfo(_appDataFolder.MapPath("Browsers"));
            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        private void DeleteBrowsersDirectory()
        {
            var directory = new DirectoryInfo(_appDataFolder.MapPath("Browsers"));
            if (directory.Exists)
            {
                directory.Delete(true);
            }
        }

        #region IFeatureEventHandler Members

        public void Disabled(Feature feature)
        {
            Disable(feature);
        }

        public void Disabling(Feature feature)
        {
            
        }

        public void Enabled(Feature feature)
        {
            Enable(feature);
        }

        public void Enabling(Feature feature)
        {
            
        }

        public void Installed(Feature feature)
        {
            
        }

        public void Installing(Feature feature)
        {
            
        }

        public void Uninstalled(Feature feature)
        {
            
        }

        public void Uninstalling(Feature feature)
        {
            
        }

        #endregion
    }
}
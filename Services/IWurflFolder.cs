using System.Collections.Generic;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.Services
{
    public interface IWurflFolder
    {
        IEnumerable<PatchFile> ListPatches();
    }
}
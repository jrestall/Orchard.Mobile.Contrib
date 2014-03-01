using System.Collections.Generic;
using Orchard.Mobile.Contrib.Models;

namespace Orchard.Mobile.Contrib.ViewModels
{
    public class WurflPatchIndexViewModel
    {
        public IEnumerable<PatchFile> PatchFiles { get; set; }
    }
}
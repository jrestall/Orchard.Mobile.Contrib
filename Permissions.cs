using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Orchard.Mobile.Contrib
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageWurfl = new Permission { Description = "Modifying Wurfl Files", Name = "ManageWurfl" };

        public static readonly Permission ManageMobileThemes = new Permission { Description = "Manage Mobile Themes", Name = "ManageMobileThemes" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] { ManageWurfl, ManageMobileThemes };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new [] { ManageWurfl, ManageMobileThemes }
                },
            };
        }
    }
}
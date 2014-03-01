using System.Web.Mvc;

namespace Orchard.Mobile.Contrib.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string DeviceGroupCreate(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Create", "ThemesAdmin", new { area = "Orchard.Mobile.Contrib" });
        }

        public static string ManageThemes(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Index", "Admin", new { area = "Orchard.Themes" });
        }

        public static string IncreaseGroupsPosition(this UrlHelper urlHelper, int id)
        {
            return urlHelper.Action("SetPriority", "ThemesAdmin", new { area = "Orchard.Mobile.Contrib", id, up = true });
        }

        public static string DecreaseGroupsPosition(this UrlHelper urlHelper, int id)
        {
            return urlHelper.Action("SetPriority", "ThemesAdmin", new { area = "Orchard.Mobile.Contrib", id, up = false });
        } 
    }
}
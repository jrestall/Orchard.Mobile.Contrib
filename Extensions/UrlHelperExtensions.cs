using System.Web.Mvc;

namespace Contrib.Mobile.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string DeviceGroupCreate(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Create", "ThemesAdmin", new { area = "Contrib.Mobile" });
        }

        public static string ManageThemes(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Index", "Admin", new { area = "Orchard.Themes" });
        }

        public static string IncreaseGroupsPosition(this UrlHelper urlHelper, int id)
        {
            return urlHelper.Action("SetPriority", "ThemesAdmin", new { area = "Contrib.Mobile", id, up = true });
        }

        public static string DecreaseGroupsPosition(this UrlHelper urlHelper, int id)
        {
            return urlHelper.Action("SetPriority", "ThemesAdmin", new { area = "Contrib.Mobile", id, up = false });
        } 
    }
}
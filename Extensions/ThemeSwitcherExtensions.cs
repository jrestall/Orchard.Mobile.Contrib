using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Contrib.Mobile.Extensions
{
    public static class ThemeSwitcherExtensions
    {
        private static readonly Regex _themeSwitcherRegex = new Regex(@"\[(?<Description>[^\|]+)\|(?<DeviceGroupName>[^]]+)]", RegexOptions.Compiled);

        public static MvcHtmlString GenerateSwitcherLinks(this HtmlHelper htmlHelper, string switcherText)
        {
            string switcherUrl = _themeSwitcherRegex.Replace(switcherText, delegate(Match match)
            {
                string desc = match.Groups["Description"].Value;
                string groupName = match.Groups["DeviceGroupName"].Value;
                return htmlHelper.ActionLink(desc, "SetThemeGroup", new { controller = "ThemeSwitcher", area = "Contrib.Mobile", group = groupName }).ToString();
            });

            return new MvcHtmlString(switcherUrl);
        }
    }
}
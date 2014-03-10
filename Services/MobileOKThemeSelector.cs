using System.Web.Routing;
using Orchard.Mvc;
using Orchard.Themes;

namespace Contrib.Mobile.Services
{
    public class MobileOkThemeSelector : IThemeSelector
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MobileOkThemeSelector(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ThemeSelectorResult GetTheme(RequestContext context)
        {
            var themeName = _httpContextAccessor.Current().Request.QueryString["theme"];
            return string.IsNullOrEmpty(themeName) ? null : new ThemeSelectorResult { Priority = 101, ThemeName = themeName };
        }
    }
}
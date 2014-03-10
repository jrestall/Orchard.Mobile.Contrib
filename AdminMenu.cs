using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Contrib.Mobile
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("mobile-contrib")
                .Add(T("Mobile"), "20", menu => menu.Action("List", "ThemesAdmin", new { area = "Contrib.Mobile" }).Permission(Permissions.ManageMobileThemes)
                        .Add(T("Theming"), "0", item => item.Action("List", "ThemesAdmin", new { area = "Contrib.Mobile" }).Permission(Permissions.ManageMobileThemes).LocalNav()));
                        //.Add(T("Wurfl"), "1", item => item.Action("Index", "WurflAdmin", new { area = "Contrib.Mobile" }).Permission(Permissions.ManageWurfl).LocalNav())
                        //.Add(T("Analytics"), "2", item => item.Action("Index", "AnalyticsAdmin", new { area = "Contrib.Mobile" }).Permission(Permissions.ManageMobileThemes).LocalNav()));
        }
    }
}
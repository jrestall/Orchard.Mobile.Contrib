using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Orchard.Mobile.Contrib
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("mobile-contrib")
                .Add(T("Mobile"), "20", menu => menu.Action("List", "ThemesAdmin", new { area = "Orchard.Mobile.Contrib" }).Permission(Permissions.ManageMobileThemes)
                        .Add(T("Theming"), "0", item => item.Action("List", "ThemesAdmin", new { area = "Orchard.Mobile.Contrib" }).Permission(Permissions.ManageMobileThemes).LocalNav()));
                        //.Add(T("Wurfl"), "1", item => item.Action("Index", "WurflAdmin", new { area = "Orchard.Mobile.Contrib" }).Permission(Permissions.ManageWurfl).LocalNav())
                        //.Add(T("Analytics"), "2", item => item.Action("Index", "AnalyticsAdmin", new { area = "Orchard.Mobile.Contrib" }).Permission(Permissions.ManageMobileThemes).LocalNav()));
        }
    }
}
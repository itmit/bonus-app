using bonus.app.Views;
using Xamarin.Forms;

namespace bonus.app
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = InitMainTabbedPage();
        }
        public TabbedPage InitMainTabbedPage()
        {
            var tabbedNavigation = new MainTabbedPage(NavigationContainerNames.MainContainer);

            tabbedNavigation.CurrentPage = tabbedNavigation.AddTab<CategoriesPageModel>(null, "ic_action_home.png");
            tabbedNavigation.AddTab<FavoritesPageModel>(null, "star_2.png");
            tabbedNavigation.AddTab<RatingPageModel>(null, "ic_action_search.png");
            tabbedNavigation.AddTab<MenuPageModel>(null, "ic_action_dehaze.png");

            tabbedNavigation.Effects.Add(new NoShiftEffect());
            tabbedNavigation.On<TabBar.Android>()
                            .SetToolbarPlacement(ToolbarPlacement.Bottom);
            tabbedNavigation.On<TabBar.Android>()
                            .SetIsSwipePagingEnabled(false);
            tabbedNavigation.BarBackgroundColor = Color.FromHex("#228bcc");
            tabbedNavigation.SelectedTabColor = Color.Black;
            tabbedNavigation.UnselectedTabColor = Color.White;

            return tabbedNavigation;
        }
        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

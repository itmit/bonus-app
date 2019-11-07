using bonus.app.Views;
using Realms;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;
using TabbedPage = Xamarin.Forms.TabbedPage;
using TabBar = Xamarin.Forms.PlatformConfiguration;
using bonus.app.Page;

namespace bonus.app
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AuthorizationPage();
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

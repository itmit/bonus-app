using Xamarin.Essentials;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;
using AuthorizationPage = bonus.app.Core.Page.Auth.AuthorizationPage;

namespace bonus.app.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

			
			MainPage = new NavigationPage(new AuthorizationPage());
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

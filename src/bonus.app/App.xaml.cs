using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using XF.Material.Forms;
using Application = Xamarin.Forms.Application;

namespace bonus.app.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
			On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
			Material.Init(this);
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

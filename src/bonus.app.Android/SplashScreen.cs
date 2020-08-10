
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using bonus.app.Core;
using MvvmCross.Forms.Platforms.Android.Views;

namespace bonus.app.Droid
{
	[Activity(
		MainLauncher = true
		, Icon = "@mipmap/ic_launcher"
		, Theme = "@style/MainTheme"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, CoreApp, App>
	{
		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

		protected override Task RunAppStartAsync(Bundle bundle)
		{
			StartActivity(typeof(FormsActivity));
			return Task.CompletedTask;
		}
	}
}

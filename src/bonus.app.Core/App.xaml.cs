using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Forms.Core;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using XF.Material.Forms;
using Application = Xamarin.Forms.Application;

namespace bonus.app.Core
{
	public partial class App : MvxFormsApplication
	{
		#region .ctor
		public App()
		{
			InitializeComponent();
			On<Android>()
				.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
			Material.Init(this);
		}
		#endregion

		#region Overrided
		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnStart()
		{
		}
		#endregion
	}
}

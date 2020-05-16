using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Platform;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.Permissions;
using Rg.Plugins.Popup;
using Xamarin.Forms;
using XF.Material.Droid;
using ZXing.Net.Mobile.Forms.Android;
using PermissionsHandler = ZXing.Net.Mobile.Android.PermissionsHandler;

namespace bonus.app.Droid
{
	[Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		#region Properties
		public static string AppPackageName
		{
			get;
			private set;
		}
		#endregion

		#region Overrided
		public override void OnBackPressed()
		{
			if (Popup.SendBackPressed(base.OnBackPressed))
			{
				// Do something if there are some pages in the `PopupStack`
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnCreate(Bundle bundle)
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

			AppPackageName = ApplicationContext.PackageName;
			Platform.Init();
			Popup.Init(this, bundle);

			Forms.Init(this, bundle);
			FormsMaterial.Init(this, bundle);
			
			base.OnCreate(bundle);
			Material.Init(this, bundle);

			CachedImageRenderer.Init(true);
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
		}

		private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			Crashes.TrackError(e.Exception, new Dictionary<string, string> {
				{ "Sender", sender.GetType().FullName }
			});
		}

		private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Crashes.TrackError(e.ExceptionObject as Exception, new Dictionary<string, string> {
				{ "Sender", sender.GetType().FullName }
			});
		}
		#endregion
	}
}

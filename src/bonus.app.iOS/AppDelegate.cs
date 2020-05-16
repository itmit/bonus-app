using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading.Forms.Platform;
using Foundation;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Platforms.Ios.Core;
using Rg.Plugins.Popup;
using UIKit;
using Xamarin.Forms;
using XF.Material.iOS;
using ZXing.Net.Mobile.Forms.iOS;

namespace bonus.app.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		#region Overrided
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

			Platform.Init();
			Popup.Init();
			Forms.Init();
			FormsMaterial.Init();
			CachedImageRenderer.Init();
			Material.Init();

			return base.FinishedLaunching(app, options);
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

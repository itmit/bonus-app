using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.iOS.Services;
using FFImageLoading.Forms.Platform;
using Firebase.CloudMessaging;
using Firebase.Core;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Forms.Core;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using XF.Material.iOS;
using ZXing.Net.Mobile.Forms.iOS;
using PropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;

namespace bonus.app.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{
		private Xamarin.Forms.Application _application;

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
			AppCenter.Start("654ba5c1-5011-4899-ad27-179fb54321e4",
							typeof(Analytics), typeof(Crashes));
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

			Platform.Init();
			Popup.Init();
			Forms.Init();
			FormsMaterial.Init();
			CachedImageRenderer.Init();
			Material.Init();
			App.Configure();

			RegisterForRemoteNotification();

			base.FinishedLaunching(app, options);

			if (!Mvx.IoCProvider.TryResolve(out IMvxFormsSetup setup))
			{
				return true;
			}

			_application = setup.FormsApplication;
			setup.FormsApplication.PropertyChanged += ApplicationOnPropertyChanged;

			return true;
		}

		private void ApplicationOnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "MainPage")
			{
				UpdateMainPage();
			}
		}

		private void UpdateMainPage()
		{
			if (_application.MainPage == null)
			{
				return;
			}
			Window.RootViewController = _application.MainPage.CreateViewController();
		}


		private void RegisterForRemoteNotification()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
																	  (granted, error) =>
																	  {
																		  if (granted)
																		  {
																			  InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
																		  }
																	  });

			}
			else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
					UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else
			{
				const UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}
		}


		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			Analytics.TrackEvent(error.Description);
		}

		[Export("messaging:didRefreshRegistrationToken:")]
		public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
		{
			Analytics.TrackEvent($"iOS FCM Token (DidRefreshRegistrationToken): {fcmToken}");
		}

		public string DeviceToken { get; set; }

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			Analytics.TrackEvent($"Local notification is received: {DeviceToken}");
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			//DeviceToken = Regex.Replace(deviceToken.ToString(), "[^0-9a-zA-Z]+", "");
			//Replace the above line whick worked up to iOS12 with the code below:
			var bytes = deviceToken.ToArray<byte>();
			var hexArray = bytes.Select(b => b.ToString("x2")).ToArray();
			DeviceToken = string.Join(string.Empty, hexArray);

			Analytics.TrackEvent($"iOS device token (RegisteredForRemoteNotifications): {DeviceToken}");
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			(Mvx.IoCProvider.Resolve<IMessagingService>() as MessagingService)?.ReceiveMessage();

			Analytics.TrackEvent($"Remote notification is received: {DeviceToken}");
		}

		private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			Crashes.TrackError(e.Exception, new Dictionary<string, string> {
				{ "Sender", sender.GetType().FullName }
			});
		}

		private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Crashes.TrackError(e.ExceptionObject as Exception, new Dictionary<string, string> {
				{ "Sender", sender.GetType().FullName }
			});
		}
		#endregion
	}
}

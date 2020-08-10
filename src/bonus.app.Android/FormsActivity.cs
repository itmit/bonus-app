using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using bonus.app.Core.Services;
using bonus.app.Droid.Services;
using FFImageLoading.Forms.Platform;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Rg.Plugins.Popup;
using VKontakte;
using Xamarin.Facebook.AppEvents;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Material.Droid;
using PermissionsHandler = ZXing.Net.Mobile.Android.PermissionsHandler;
using Platform = ZXing.Net.Mobile.Forms.Android.Platform;

namespace bonus.app.Droid
{
	[Activity(Icon = "@mipmap/ic_launcher"
			  , Theme = "@style/MainTheme"
			  , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		#region Properties
		public static string AppPackageName
		{
			get;
			private set;
		}
		#endregion

		private const string Tag = "BonusFormsActivity";
		internal const string ChannelId = "bonus_notification_channel";

		protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			var task = VKSdk.OnActivityResultAsync(requestCode, resultCode, data, out var vkResult);

			if (!vkResult)
			{
				base.OnActivityResult(requestCode, resultCode, data);
				AndroidFacebookService.Instance.OnActivityResult(requestCode, (int)resultCode, data);
				return;
			}

			try
			{
				var token = await task;
				AndroidVkService.Instance.SetUserToken(token);
			}
			catch (Exception e)
			{
				if (!(e is VKException vkException) || vkException.Error.ErrorCode != VKontakte.API.VKError.VkCanceled)
				{
					AndroidVkService.Instance.SetErrorResult(e.Message);
				}
				else
				{
					AndroidVkService.Instance.SetCanceledResult();
				}
			}
		}

		private void IsPlayServicesAvailable()
		{
			var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
				{
					Log.Debug(Tag, GoogleApiAvailability.Instance.GetErrorString(resultCode));
				}
				else
				{
					Log.Debug(Tag, "This device is not supported");
					Finish();
				}

				return;
			}

			Log.Debug(Tag, "Google Play Services is available.");
		}

		private void CreateNotificationChannel()
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				return;
			}

			const string channelName = ChannelId;
			var channelDescription = string.Empty;
			var channel = new NotificationChannel(ChannelId, channelName, NotificationImportance.Default)
			{
				Description = channelDescription
			};

			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.CreateNotificationChannel(channel);
		}

		#region Overrided
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnResume()
		{
			base.OnResume();
			AppEventsLogger.ActivateApp(Application);
		}

		protected override void OnCreate(Bundle bundle)
		{
			AppCenter.Start("db598eca-83b6-46d8-9748-68abcdea9a02",
							typeof(Analytics), typeof(Crashes));
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

			AppPackageName = ApplicationContext.PackageName;
			Platform.Init();
			Popup.Init(this, bundle);

			Forms.Init(this, bundle);
			FormsMaterial.Init(this, bundle);
			
			base.OnCreate(bundle);

			CrossCurrentActivity.Current.Init(this, bundle);
			this.GetActivity();

			if (Intent.Extras != null)
			{
				foreach (var key in Intent.Extras.KeySet())
				{
					if (key == null)
					{
						continue;
					}

					var value = Intent.Extras.GetString(key);
					Log.Debug(Tag, "Key: {0} Value: {1}", key, value);
				}
			}

			IsPlayServicesAvailable();
			CreateNotificationChannel();

			Material.Init(this, bundle);

			CachedImageRenderer.Init(true);
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
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

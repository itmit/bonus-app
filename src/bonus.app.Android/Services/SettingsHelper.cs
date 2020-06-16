using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using bonus.app.Core.Services;
using MvvmCross;
using Org.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using LoginResult = bonus.app.Core.Services.LoginResult;
using Uri = Android.Net.Uri;

namespace bonus.app.Droid.Services
{
	public class SettingsHelper : ISettingsHelper
	{
		#region ISettingsHelper members
		public void OpenAppSettings()
		{
			var intent = new Intent(Settings.ActionApplicationDetailsSettings);
			intent.AddFlags(ActivityFlags.NewTask);
			var uri = Uri.FromParts("package", FormsActivity.AppPackageName, null);
			intent.SetData(uri);
			Application.Context.StartActivity(intent);
		}
		#endregion
	}
}

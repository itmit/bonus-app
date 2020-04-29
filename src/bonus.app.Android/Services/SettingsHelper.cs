using Android.App;
using Android.Content;
using Android.Net;
using Android.Provider;
using bonus.app.Core.Services;

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

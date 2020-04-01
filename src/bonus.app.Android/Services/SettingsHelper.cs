using Android.App;
using Android.Content;
using bonus.app.Core.Services;

namespace bonus.app.Droid.Services
{
	public class SettingsHelper : ISettingsHelper
	{
		public void OpenAppSettings()
		{
			var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
			intent.AddFlags(ActivityFlags.NewTask);
			var uri = Android.Net.Uri.FromParts("package", FormsActivity.AppPackageName, null);
			intent.SetData(uri);
			Application.Context.StartActivity(intent);
		}
	}
}

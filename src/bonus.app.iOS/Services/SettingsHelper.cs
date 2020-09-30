using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using Foundation;
using UIKit;

namespace bonus.app.iOS.Services
{
	public class SettingsHelper : ISettingsHelper
	{
		#region ISettingsHelper members
		public void OpenAppSettings()
		{
			UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
		}
		#endregion
	}
}

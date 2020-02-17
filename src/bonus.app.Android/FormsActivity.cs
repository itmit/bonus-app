using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace bonus.app.Droid
{
	[Activity(Icon = "@mipmap/icon", 
		Theme = "@style/MainTheme",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			Rg.Plugins.Popup.Popup.Init(this, bundle);
			Xamarin.Forms.Forms.Init(this, bundle);
			Xamarin.Forms.FormsMaterial.Init(this, bundle);
			base.OnCreate(bundle);
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
		}
		public override void OnBackPressed()
		{
			if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
			{
				// Do something if there are some pages in the `PopupStack`
			}
			else
			{
				// Do something if there are not any pages in the `PopupStack`
			}
		}
	}
}

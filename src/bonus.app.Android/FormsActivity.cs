using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using Plugin.Permissions;
using FFImageLoading.Forms.Platform;

namespace bonus.app.Droid
{
	[Activity(Icon = "@mipmap/icon", 
		Theme = "@style/MainTheme",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		public static string AppPackageName
		{
			get;
			private set;
		}

		protected override void OnCreate(Bundle bundle)
		{
			AppPackageName = ApplicationContext.PackageName;
			Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
			ZXing.Net.Mobile.Forms.Android.Platform.Init();
			Rg.Plugins.Popup.Popup.Init(this, bundle);

			Xamarin.Forms.Forms.Init(this, bundle);
			Xamarin.Forms.FormsMaterial.Init(this, bundle);
			base.OnCreate(bundle);

			XF.Material.Droid.Material.Init(this, bundle);

			CachedImageRenderer.Init(true);
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

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

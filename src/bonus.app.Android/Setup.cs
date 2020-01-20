using bonus.app.Core;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Application CreateFormsApplication() => new App();
	}
}

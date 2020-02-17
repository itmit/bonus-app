using bonus.app.Core;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace bonus.app.iOS
{
	public class Startup : MvxFormsIosSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Xamarin.Forms.Application CreateFormsApplication() => new App();
	}
}

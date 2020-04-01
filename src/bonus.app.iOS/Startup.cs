using bonus.app.Core;
using bonus.app.Core.Services;
using bonus.app.iOS.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.ViewModels;

namespace bonus.app.iOS
{
	public class Startup : MvxFormsIosSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Xamarin.Forms.Application CreateFormsApplication() => new App();

		protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
		{
			var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
			Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
			Mvx.IoCProvider.RegisterSingleton<ISettingsHelper>(new SettingsHelper());
			return formsPagePresenter;
		}
	}
}

using bonus.app.Core;
using bonus.app.Core.Services;
using bonus.app.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		#region Overrided
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Application CreateFormsApplication() => new App();

		protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
		{
			var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
			Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
			Mvx.IoCProvider.RegisterSingleton<ISettingsHelper>(new SettingsHelper());
			return formsPagePresenter;
		}
		#endregion
	}
}

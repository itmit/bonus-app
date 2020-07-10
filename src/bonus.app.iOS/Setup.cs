using bonus.app.Core;
using bonus.app.Core.Services;
using bonus.app.iOS.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace bonus.app.iOS
{
	public class Setup : MvxFormsIosSetup<CoreApp, App>
	{
		#region Overrided
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Xamarin.Forms.Application CreateFormsApplication() => new App();

		protected override IMvxIoCProvider CreateIocProvider()
		{
			var provider = base.CreateIocProvider();
			provider.RegisterType<IFirebaseService, IosFirebaseService>();
			provider.RegisterSingleton<IFacebookService>(new IosFacebookService());
			provider.RegisterSingleton<IVkService>(new IosVkService());
			provider.RegisterSingleton(typeof(IMessagingService), new MessagingService());
			return provider;
		}

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

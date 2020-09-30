using System.Threading.Tasks;
using bonus.app.Core;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.iOS.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Platforms.Ios.Presenters;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.ViewModels;
using UIKit;
using Xamarin.Forms;

namespace bonus.app.iOS
{
	public class Setup : MvxFormsIosSetup<CoreApp, App>
	{
		private MvxFormsIosViewPresenter _presenter;
		private CustomMvxFormsPagePresenter _formsPagePresenter;

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
			_formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
			Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(_formsPagePresenter);
			Mvx.IoCProvider.RegisterSingleton<ISettingsHelper>(new SettingsHelper());
			return _formsPagePresenter;
		}

		protected override IMvxIosViewPresenter CreateViewPresenter()
		{
			_presenter = new MvxFormsIosViewPresenter(ApplicationDelegate, Window, FormsApplication);
			_presenter.FormsPagePresenter = CreateFormsPagePresenter(_presenter);
			return _presenter;
		}
		#endregion
	}
}

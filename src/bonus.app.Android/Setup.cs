﻿using bonus.app.Core;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Droid.Services;
using Firebase.Messaging;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		#region Overrided
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Application CreateFormsApplication() => new App();

		protected override IMvxIoCProvider CreateIocProvider()
		{
			var provider = base.CreateIocProvider();
			provider.RegisterType<IFirebaseService, AndroidFirebaseService>();
			provider.RegisterSingleton<IFacebookService>(new AndroidFacebookService());
			provider.RegisterSingleton<IVkService>(new AndroidVkService());
			provider.RegisterSingleton<IMessagingService>(new MessagingService());
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

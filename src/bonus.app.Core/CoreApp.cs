using System;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Customer;
using bonus.app.Core.ViewModels.Manager;
using MonkeyCache.FileStore;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Realms;
using Xamarin.Essentials;

namespace bonus.app.Core
{
	public class CoreApp : MvxApplication
	{
		#region Overrided
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			CreatableTypes()
				.InNamespace("bonus.app.Core.Repositories")
				.EndingWith("Repository")
				.AsInterfaces()
				.RegisterAsDynamic();

			Barrel.ApplicationId = "itmit.bonus.app";
			RealmConfiguration.DefaultConfiguration.SchemaVersion = 3;

			var firstRun = Preferences.Get("FirstRun", "true");
			if (firstRun.Equals("true"))
			{
				Preferences.Set("FirstRun", "false");
				RegisterAppStart<BusinessmanAndCustomerViewModel>();
				return;
			}

			var authService = Mvx.IoCProvider.Resolve<IAuthService>();

			if (authService.UserIsAuthorized)
			{
				switch (authService.User.Role)
				{
					case UserRole.Businessman:
						RegisterAppStart<MainBusinessmanViewModel>();
						break;
					case UserRole.Customer:
						RegisterAppStart<MainCustomerViewModel>();
						break;
					case UserRole.Manager:
						RegisterAppStart<MainManagerViewModel>();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				return;
			}

			RegisterAppStart<AuthorizationViewModel>();
		}
		#endregion
	}
}

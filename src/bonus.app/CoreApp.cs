using System.Linq;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Customer;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace bonus.app.Core
{
	public class CoreApp : MvxApplication
	{
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

			var firstRun = Preferences.Get("FirstRun", "true");
			if (firstRun.Equals("true"))
			{
				Preferences.Set("FirstRun", "false");
				RegisterAppStart<BusinessmanAndCustomerViewModel>();
				return;
			}

			var userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();

			User user = userRepository.GetAll().SingleOrDefault();
			
			if (user?.AccessToken == null)
			{
				RegisterAppStart<AuthorizationViewModel>();
				return;
			}
			if (user.Role == UserRole.Businessman)
			{
				RegisterAppStart<MainBusinessmanViewModel>();
			}
			if (user.Role == UserRole.Customer)
			{
				RegisterAppStart<MainCustomerViewModel>();
			}
		}
	}
}

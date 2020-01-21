﻿using System.Linq;
using bonus.app.Core.Models;
using bonus.app.Core.Repositories;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

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

			var userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();

			User user = userRepository.GetUsers().SingleOrDefault();
			
			if (user?.AccessToken == null)
			{
				RegisterAppStart<AuthorizationViewModel>();
				return;
			}

			RegisterAppStart<MainViewModel>();
		}
	}
}
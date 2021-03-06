﻿using System;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
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
				.EndingWith("Repository")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			Barrel.ApplicationId = "itmit.bonus.app";
			RealmConfiguration.DefaultConfiguration.SchemaVersion = 4;

			// register the appstart object
			RegisterCustomAppStart<AppStart>();
		}
		#endregion
	}
}

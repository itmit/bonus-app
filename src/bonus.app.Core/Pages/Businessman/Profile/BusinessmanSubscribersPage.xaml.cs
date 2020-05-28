﻿using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusinessmanSubscribersPage : MvxContentPage<BusinessmanSubscribersViewModel>
	{
		#region .ctor
		public BusinessmanSubscribersPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

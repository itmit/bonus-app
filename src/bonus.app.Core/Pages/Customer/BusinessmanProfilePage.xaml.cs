﻿using bonus.app.Core.ViewModels.Customer;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(Icon = "ic_profile", Title = "Профиль")]
	public partial class BusinessmanProfilePage : MvxContentPage<BusinessmanProfileViewModel>
	{
		#region .ctor
		public BusinessmanProfilePage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
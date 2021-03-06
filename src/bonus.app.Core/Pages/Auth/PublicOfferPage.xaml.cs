﻿using System;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PublicOfferPage : MvxContentPage<PublicOfferViewModel>
	{
		#region .ctor
		public PublicOfferPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			Ch1.IsChecked = !Ch1.IsChecked;
		}

		private void TapGestureRecognizer_OnTapped_1(object sender, EventArgs e)
		{
			Ch2.IsChecked = !Ch2.IsChecked;
		}
		#endregion

		private void ShowPrivacy(object sender, EventArgs e)
		{
			Navigation.PushAsync(new PrivacyPage());
		}
	}
}

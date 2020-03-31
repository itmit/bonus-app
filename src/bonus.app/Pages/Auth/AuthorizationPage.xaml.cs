using System;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(NoHistory = true)]
	public partial class AuthorizationPage : MvxContentPage<AuthorizationViewModel>
	{
		#region .ctor
		public AuthorizationPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (Password.IsPassword)
			{
				((Image) sender).Source = new FileImageSource
				{
					File = "ic_hide_eye.png"
				};
			}
			else
			{
				((Image) sender).Source = new FileImageSource
				{
					File = "ic_eye.png"
				};
			}

			Password.IsPassword = !Password.IsPassword;
		}

		private void TapGestureRecognizer_OnTapped_1(object sender, EventArgs e)
		{
			IsRemember.IsChecked = !IsRemember.IsChecked;
		}

		private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AuthVkFcPage());
		}
		#endregion
	}
}

using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Views;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : MvxContentPage<AuthorizationViewModel>
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AuthVkFcPage());
        }

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
				((Image)sender).Source = new FileImageSource
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
	}
}
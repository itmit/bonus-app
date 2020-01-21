using System;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthVkFcPage : MvxContentPage<AuthVkFcViewModel>
    {
        public AuthVkFcPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuthorizationRecoveryPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntrepreneurAndBuyerPage());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuthorizationPage());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            if (Password.IsPassword)
            {
                ( (Image)sender ).Source = new FileImageSource
                {
                    File = "ic_hide_eye.png"
                };
            }
            else
            {
                ( (Image)sender ).Source = new FileImageSource
                {
                    File = "ic_eye.png"
                };
            }
            Password.IsPassword = !Password.IsPassword;
        }
    }
}
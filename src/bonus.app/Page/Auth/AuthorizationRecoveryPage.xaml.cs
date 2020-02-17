using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Page;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationRecoveryPage : MvxContentPage<AuthorizationRecoveryViewModel>
    {
        public AuthorizationRecoveryPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BusinessmanAndCustomerPage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecoveryDetailPage());
        }
    }
}
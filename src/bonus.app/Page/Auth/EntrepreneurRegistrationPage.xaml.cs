using System;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntrepreneurRegistrationPage : MvxContentPage<EntrepreneurRegistrationViewModel>
    {
        public EntrepreneurRegistrationPage()
        {
            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
	}
}
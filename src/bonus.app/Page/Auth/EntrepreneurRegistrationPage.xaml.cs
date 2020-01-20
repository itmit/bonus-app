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

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Вы успешно зарегистрировались", "Просим заполнить статистическую информацию, чтобы мы сделали сервис ориентированным на вас", "ОK");
            Navigation.PushAsync(new FillinDetailsPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Core.Page.AuthVKFC());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Core.Page.AuthVKFC());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaserRegistrationPage : ContentPage
    {
        public PurchaserRegistrationPage()
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
    }
}
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
    public partial class MyBonusPage : ContentPage
    {
        public MyBonusPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("Спасибо за посещение","Салон Бигуди\n\nСписано 200 бонусов,\nНачислено 200 бонусов","Перейти в профиль");
            await Navigation.PushAsync(new SubscribePage());
        }
    }
}
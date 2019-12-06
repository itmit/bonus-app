using bonus.app.Views;
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
    public partial class PublicOfferBusPage : ContentPage
    {
        public PublicOfferBusPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Вы успешно зарегестрировались!", "Пользуйтесь приложением и находите новых клиентов", "ОK");
            Application.Current.MainPage = new MainBusinessmanTabbedPage();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(Ch1.IsChecked && Ch2.IsChecked)
            {
                ButSave.IsEnabled = true;
            }
            else
            {
                ButSave.IsEnabled = false;
            }
        }
    }
}
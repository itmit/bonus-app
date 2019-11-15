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
    public partial class PublicOfferPage : ContentPage
    {
        public PublicOfferPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Добро пожаловать в Bonus &amp; Marketing", "Вы успешно зарегистрировались в приложении Bonus &amp; Marketing и получили личный QR-CODE с помощью которого вы можете получать бонусы", "ОK");
            Application.Current.MainPage = new MainPage();
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
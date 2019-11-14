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
    public partial class FillinDetailsPage : ContentPage
    {
        public FillinDetailsPage()
        {
            InitializeComponent();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            City.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PublicOfferPage());
        }
    }
}
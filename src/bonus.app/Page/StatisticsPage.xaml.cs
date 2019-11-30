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
    public partial class StatisticsPage : ContentPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SalesTypesPage());
        }

        private void ViewCell_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GenderAgePage());
        }

        private void ViewCell_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewsStockPage());
        }

        private void ViewCell_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewsProfilePage());
        }

        private void ViewCell_Tapped_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TransitionsProfilePage());
        }

        private void ViewCell_Tapped_5(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GeographyPage());
        }
    }
}
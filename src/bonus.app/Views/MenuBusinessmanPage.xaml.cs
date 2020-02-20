using bonus.app.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BusinessmanProfilePage = bonus.app.Core.Page.BusinessmanProfilePage;

namespace bonus.app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MenuBusinessmanPage : MvxContentPage<MenuBusinessmanViewModel>
    {
        public MenuBusinessmanPage()
        {
            InitializeComponent();

            BackgroundColor = Color.FromRgba(160, 150, 142, 235);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        { 
			Navigation.PushAsync(new BusinessmanProfilePage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PayTabbedPage());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TariffPage());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StatisticsPage());
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SupportPage());
        }
    }
}
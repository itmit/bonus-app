using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainBuyerProfilePage : MvxMasterDetailPage<MainCustomerViewModel>
    {
        public MainBuyerProfilePage()
        {
            InitializeComponent();
        }
    }
}
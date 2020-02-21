using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCustomerPage : MvxMasterDetailPage<MainCustomerViewModel>
    {
        public MainCustomerPage()
        {
            InitializeComponent();
        }
    }
}
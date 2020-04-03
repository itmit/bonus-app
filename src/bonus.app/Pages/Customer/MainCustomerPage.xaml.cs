using bonus.app.Core.ViewModels.Customer;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
    public partial class MainCustomerPage : MvxMasterDetailPage<MainCustomerViewModel>
    {
        public MainCustomerPage()
        {
            InitializeComponent();
        }
    }
}
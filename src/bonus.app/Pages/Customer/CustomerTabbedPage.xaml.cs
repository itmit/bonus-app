using bonus.app.Core.ViewModels.Customer;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = false)]
	[MvxTabbedPagePresentation(TabbedPosition.Root)]
    public partial class CustomerTabbedPage : MvxTabbedPage<MainTabbedCustomerViewModel>
    {
        public CustomerTabbedPage()
        {
            InitializeComponent();
			On<Android>()
				.SetToolbarPlacement(ToolbarPlacement.Bottom);
		}
    }
}
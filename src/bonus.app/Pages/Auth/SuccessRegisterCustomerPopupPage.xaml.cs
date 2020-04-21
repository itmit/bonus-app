using bonus.app.Core.ViewModels.Auth;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxPopupPagePresentation]
    public partial class SuccessRegisterCustomerPopupPage : MvxPopupPage<SuccessRegisterCustomerPopupViewModel>
    {
        public SuccessRegisterCustomerPopupPage()
        {
            InitializeComponent();
        }
    }
}
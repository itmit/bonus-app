using bonus.app.Core.ViewModels.Businessman.Popups;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxPopupPagePresentation(Animated = false)]
    public partial class SuccessCreateSharesPopupPage : MvxPopupPage<SuccessCreateSharesPopupViewModel>
    {
        public SuccessCreateSharesPopupPage()
        {
            InitializeComponent();
        }
    }
}
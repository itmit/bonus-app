using bonus.app.Core.ViewModels.Auth;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxPopupPagePresentation]
	public partial class SuccessRegisterPopupPage : MvxPopupPage<SuccessRegisterPopupViewModel>
	{
		public SuccessRegisterPopupPage()
		{
			InitializeComponent();
		}
	}
}
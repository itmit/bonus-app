using bonus.app.Core.ViewModels.Auth;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxPopupPagePresentation]
	public partial class SuccessRegisterCustomerPopupPage : MvxPopupPage<SuccessRegisterCustomerPopupViewModel>
	{
		#region .ctor
		public SuccessRegisterCustomerPopupPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

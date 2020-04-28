using bonus.app.Core.ViewModels.Businessman.Popups;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxPopupPagePresentation(Animated = false)]
	public partial class SuccessCreateSharesPopupPage : MvxPopupPage<SuccessCreateSharesPopupViewModel>
	{
		#region .ctor
		public SuccessCreateSharesPopupPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(WrapInNavigationPage = true)]
	public partial class BusinessmanAndCustomerPage : MvxContentPage<BusinessmanAndCustomerViewModel>
	{
		#region .ctor
		public BusinessmanAndCustomerPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

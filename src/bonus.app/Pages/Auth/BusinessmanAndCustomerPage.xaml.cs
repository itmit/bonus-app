using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
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

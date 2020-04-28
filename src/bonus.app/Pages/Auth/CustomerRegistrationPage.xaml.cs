using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerRegistrationPage : MvxContentPage<CustomerRegistrationViewModel>
	{
		#region Data
		#region Fields
		private bool _isFirstAppearing = true;
		#endregion
		#endregion

		#region .ctor
		public CustomerRegistrationPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

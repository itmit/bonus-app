using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_profile", Title = "Профиль")]
	public partial class CustomerProfilePage : MvxContentPage<CustomerProfileViewModel>
	{
		#region .ctor
		public CustomerProfilePage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

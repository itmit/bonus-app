using bonus.app.Core.ViewModels.Businessman.Profile;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_profile", Title = "Профиль")]
	public partial class BusinessmanProfilePage : MvxContentPage<BusinessmanProfileViewModel>
	{
		#region .ctor
		public BusinessmanProfilePage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

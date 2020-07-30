using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true)]
	public partial class ClientProfilePage : MvxContentPage<ClientProfileViewModel>
	{
		#region .ctor
		public ClientProfilePage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

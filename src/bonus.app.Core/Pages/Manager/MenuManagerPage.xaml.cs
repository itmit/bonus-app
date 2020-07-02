using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Manager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, Title = "Меню")]
	public partial class MenuManagerPage : MvxContentPage<MenuManagerViewModel>
	{
		#region .ctor
		public MenuManagerPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

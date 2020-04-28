using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, Title = "Меню")]
	public partial class MenuBusinessmanPage : MvxContentPage<MenuBusinessmanViewModel>
	{
		#region .ctor
		public MenuBusinessmanPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

using bonus.app.Core.Page;
using bonus.app.Core.ViewModels.Businessman;
using bonus.app.Core.ViewModels.Manager;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Manager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = false)]
	[MvxTabbedPagePresentation(TabbedPosition.Root)]
	public partial class ManagerTabbedPage : MvxTabbedPage<ManagerTabbedViewModel>
	{
		#region .ctor
		public ManagerTabbedPage()
		{
			InitializeComponent();
			On<Android>()
				.SetToolbarPlacement(ToolbarPlacement.Bottom);
		}
		#endregion

		#region Overrided
		/// <summary>Event that is raised when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed()
		{
			if (Navigation.ModalStack.Count != 1 || !(Navigation.ModalStack[0] is ScannerPage))
			{
				return base.OnBackButtonPressed();
			}

			Navigation.PopModalAsync();
			return true;

		}
		#endregion
	}
}

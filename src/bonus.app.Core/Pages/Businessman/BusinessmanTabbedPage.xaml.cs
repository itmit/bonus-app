using bonus.app.Core.Page;
using bonus.app.Core.ViewModels.Businessman;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = false)]
	[MvxTabbedPagePresentation(TabbedPosition.Root)]
	public partial class BusinessmanTabbedPage : MvxTabbedPage<MainTabbedBusinessmanViewModel>
	{
		private bool _firstTime = true;

		#region .ctor
		public BusinessmanTabbedPage()
		{
			InitializeComponent();
			On<Android>()
				.SetToolbarPlacement(ToolbarPlacement.Bottom);

			CurrentPageChanged += (sender, args) =>
			{
				if (CurrentPage is NavigationPage)
				{
					CurrentPage.Navigation.PopToRootAsync();
				}
			};
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			if (_firstTime)
			{
				ViewModel.ShowBusinessmanProfileViewModelCommand.Execute();
				ViewModel.ShowBusinessmanServicesViewModelCommand.Execute();
				ViewModel.ShowBusinessmanStocksViewModelCommand.Execute();
				ViewModel.ShowNewsViewModelCommand.Execute();
				ViewModel.ShowBusinessmanBonusAccrualViewModelCommand.Execute();

				_firstTime = false;
			}
			
			base.OnAppearing();
		}
	}
}

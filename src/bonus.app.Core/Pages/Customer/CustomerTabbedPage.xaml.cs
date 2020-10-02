using bonus.app.Core.ViewModels.Customer;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = false)]
	[MvxTabbedPagePresentation(TabbedPosition.Root)]
	public partial class CustomerTabbedPage : MvxTabbedPage<MainTabbedCustomerViewModel>
	{
		private bool _firstTime = true;

		#region .ctor
		public CustomerTabbedPage()
		{
			InitializeComponent();
			On<Android>()
				.SetToolbarPlacement(ToolbarPlacement.Bottom);
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			if (_firstTime)
			{
				ViewModel.ShowCustomerProfileViewModelCommand.Execute();
				ViewModel.ShowCustomerServicesViewModelCommand.Execute();
				ViewModel.ShowCustomerStocksViewModelCommand.Execute();
				ViewModel.ShowNewsViewModelCommand.Execute();
				ViewModel.ShowCustomerBonusAccrualViewModelCommand.Execute();

				_firstTime = false;
			}

			base.OnAppearing();
		}
	}
}

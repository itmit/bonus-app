using bonus.app.Core.ViewModels.Customer;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "Главная")]
	public partial class MainCustomerPage : MvxMasterDetailPage<MainCustomerViewModel>
	{
		private bool _firstTime  = true;

		#region .ctor
		public MainCustomerPage()
		{
			InitializeComponent();
		}
		#endregion


		/// <summary>Event that is raised when a detail appears.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			if (_firstTime)
			{
				ViewModel.ShowMainTabbedCustomerViewModelCommand.Execute();
				ViewModel.ShowMenuCustomerViewModelCommand.Execute();

				_firstTime = false;
			}

			base.OnAppearing();
		}
	}
}

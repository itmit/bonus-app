using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateStockPage : MvxContentPage<CreateStockViewModel>
	{
		#region .ctor
		public CreateStockPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Overrided
		/// <summary>
		/// When overridden, allows application developers to customize behavior immediately prior to the
		/// <see cref="T:Xamarin.Forms.Page" /> becoming visible.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			Layout.BindingContext = ViewModel;
			PicCountryAndCityContentView.ViewModel = ViewModel.PicCountryAndCityViewModel;

			base.OnAppearing();
		}
		#endregion
	}
}

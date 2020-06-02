using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditorStockPage : MvxContentPage<EditorStockViewModel>
	{
		#region .ctor
		public EditorStockPage()
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
			PicCountryAndCityContentView.ViewModel = ViewModel.PicCountryAndCityViewModel;
			Layout.BindingContext = ViewModel;
			MyServicesContentView.ViewModel = ViewModel.MyServicesViewModel;
			base.OnAppearing();
		}
		#endregion
	}
}

using bonus.app.Core.ViewModels.Customer.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectedStocksDetailPage : MvxContentPage<SelectedStocksDetailViewModel>
	{
		#region .ctor
		public SelectedStocksDetailPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_stock", Title = "Акции")]
	public partial class BusinessmanSharesPage : MvxContentPage<BusinessmanStocksViewModel>
	{
		#region .ctor
		public BusinessmanSharesPage()
		{
			InitializeComponent();
			BackgroundColor = Color.FromRgba(196, 196, 196, 0.2);
		}
		#endregion
	}
}

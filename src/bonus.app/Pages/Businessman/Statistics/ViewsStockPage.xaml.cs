using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewsStockPage : MvxContentPage<ViewsStockViewsStockViewModel>
	{
		#region .ctor
		public ViewsStockPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

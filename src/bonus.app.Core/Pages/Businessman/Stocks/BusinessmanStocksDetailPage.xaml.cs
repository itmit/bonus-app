using System;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusinessmanStocksDetailPage : MvxContentPage<BusinessmanStocksDetailViewModel>
	{
		#region .ctor
		public BusinessmanStocksDetailPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}

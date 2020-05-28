using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Validations;
using bonus.app.Core.ViewModels.Customer.Stocks;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Stocks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoriteStocksPage : MvxContentPage<FavoriteStocksViewModel>
	{
		public FavoriteStocksPage()
		{
			InitializeComponent();
		}

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}
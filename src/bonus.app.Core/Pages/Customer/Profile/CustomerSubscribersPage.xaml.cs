using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerSubscribersPage : MvxContentPage<CustomerSubscribersViewModel>
	{
		public CustomerSubscribersPage()
		{
			InitializeComponent();
		}

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}
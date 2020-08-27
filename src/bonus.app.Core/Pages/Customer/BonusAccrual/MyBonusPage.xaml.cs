using System;
using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.Pages.Customer.BonusAccrual
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyBonusPage : MvxContentPage<MyBonusViewModel>
	{
		#region .ctor
		public MyBonusPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
		#endregion
	}
}

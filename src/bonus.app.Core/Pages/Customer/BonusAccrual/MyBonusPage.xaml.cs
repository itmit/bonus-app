using System;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Customer.BonusAccrual;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
		private void Button_Clicked(object sender, EventArgs e)
		{
			Application.Current.MainPage.DisplayAlert("Спасибо за посещение", "Салон Бигуди\n\nСписано 200 бонусов,\nНачислено 200 бонусов", "Перейти в профиль");
			Navigation.PopAsync();
		}
		#endregion

		private void SelectableItemsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			((CollectionView) sender).SelectedItem = null;
		}
	}
}

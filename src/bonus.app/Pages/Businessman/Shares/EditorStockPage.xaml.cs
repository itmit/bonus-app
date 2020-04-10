using System;
using bonus.app.Core.ViewModels.Businessman.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorStockPage : MvxContentPage<EditorStockViewModel>
    {
        public EditorStockPage()
        {
            InitializeComponent();
        }

		#region Private
		private void Cell_OnTapped(object sender, EventArgs e)
		{
			if (Countries.IsEnabled)
			{
				Countries.IsEnabled = false;
				Countries.IsVisible = false;
				Grid.IsVisible = true;
				Shape.Rotation = 0;
				if (Countries.SelectedItem == null)
				{
					City.IsEnabled = false;
					City.IsVisible = false;
				}
				else
				{
					City.IsEnabled = true;
					City.IsVisible = true;
				}
			}
			else
			{
				Shape.Rotation = 180;
				Countries.IsEnabled = true;
				Countries.IsVisible = true;
				Grid.IsVisible = false;
			}
		}

		private void Cities_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			/*if (ViewModel.IsBusy || ViewModel.Cities.Count == 0)
			{
				return;
			}

			if (e.Item is City city &&
				city.Id ==
				ViewModel.Cities[ViewModel.Cities.Count - 1]
						 .Id)
			{
				ViewModel.LoadMoreCitiesCommand.Execute();
			}*/
		}

		private void Countries_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				Countries.IsEnabled = false;
				Countries.IsVisible = false;
				Grid.IsVisible = true;
				City.IsEnabled = true;
				City.IsVisible = true;
				Shape.Rotation = 0;
			}
		}

		private void OnSelectCity(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				Cities.IsEnabled = false;
				Cities.IsVisible = false;
				FieldsLayout.IsVisible = true;
				Shape1.Rotation = 0;
			}
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (Cities.IsEnabled)
			{
				Cities.IsEnabled = false;
				Cities.IsVisible = false;
				Shape1.Rotation = 0;
				FieldsLayout.IsVisible = true;
			}
			else
			{
				Shape1.Rotation = 180;
				Cities.IsEnabled = true;
				Cities.IsVisible = true;
				FieldsLayout.IsVisible = false;
			}
		}
		#endregion
	}
}
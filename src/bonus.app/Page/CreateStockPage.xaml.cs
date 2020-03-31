using System;
using bonus.app.Core.Models;
using bonus.app.Core.ViewModels;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateStockPage : MvxContentPage<CreateStockViewModel>
    {
        public CreateStockPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
		{
			var popup = new ViewSharesPopupPage();
			Navigation.PushPopupAsync(popup);
		}

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

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
	}
}
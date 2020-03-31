using System;
using bonus.app.Core.ViewModels.Customer.Services;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Customer.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_star",
		WrapInNavigationPage = false,
		Title = "Услуги")]
	public partial class CustomerServicesPage : MvxContentPage<CustomerServicesViewModel>
    {
        public CustomerServicesPage()
        {
            InitializeComponent();
        }

		private void MenuItem_OnClicked(object sender, EventArgs e)
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
			}
			else
			{
				Shape1.Rotation = 180;
				Cities.IsEnabled = true;
				Cities.IsVisible = true;
			}
		}

		private void Cities_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			//if (ViewModel.IsBusy || ViewModel.Cities.Count == 0)
			//{
			//	return;
			//}

			//if (e.Item is City city && city.Id == ViewModel.Cities[ViewModel.Cities.Count - 1].Id)
			//{
			//	ViewModel.LoadMoreCitiesCommand.Execute();
			//}
		}

		private void OnSelectCity(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				Cities.IsEnabled = false;
				Cities.IsVisible = false;
				Shape1.Rotation = 0;
			}
		}

		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			if (GridHeader.IsEnabled)
			{
				GridHeader.IsEnabled = false;
				GridHeader.IsVisible = false;
			}
			else
			{
				GridHeader.IsEnabled = true;
				GridHeader.IsVisible = true;
			}
		}

		private void TapServices_OnTapped(object sender, EventArgs e)
		{
			if (ViewServices.IsEnabled)
			{
				ViewServices.IsEnabled = false;
				ViewServices.IsVisible = false;
				Shape2.Rotation = 0;
			}
			else
			{
				Shape2.Rotation = 180;
				ViewServices.IsEnabled = true;
				ViewServices.IsVisible = true;
			}
		}
	}
}
using System;
using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBusinessmanServicesDetailsPage : MvxContentPage<EditBusinessmanServicesDetailsViewModel>
    {
        public EditBusinessmanServicesDetailsPage()
        {
            InitializeComponent();

			var collection = new ObservableCollection<string>
			{
				"0",
				"0",
				"0"
			};
			ViewServices.ItemsSource = collection;
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (ViewServices.IsEnabled)
			{
				ViewServices.IsEnabled = false;
				ViewServices.IsVisible = false;
				Shape.Rotation = 0;
			}
			else
			{
				Shape.Rotation = 180;
				ViewServices.IsEnabled = true;
				ViewServices.IsVisible = true;
			}
		}
	}
}
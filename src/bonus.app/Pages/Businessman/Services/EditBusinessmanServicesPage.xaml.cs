using System;
using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditBusinessmanServicesPage : MvxContentPage<EditBusinessmanServicesViewModel>
	{
		#region .ctor
		public EditBusinessmanServicesPage()
		{
			InitializeComponent();

			var source = new ObservableCollection<string>
			{
				"0",
				"0",
				"0"
			};
			RepeaterView.ItemsSource = source;
		}
		#endregion

		#region Private
		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new EditBusinessmanServicesDetailsPage());
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
		}
		#endregion
	}
}

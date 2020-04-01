using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyServicesContentView : ContentView
	{
		public MyServicesContentView()
		{
			InitializeComponent();
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (ViewServices.IsEnabled)
			{
				PlusServices.IsVisible = false;
				ViewServices.IsEnabled = false;
				ViewServices.IsVisible = false;
				Shape.Rotation = 0;
			}
			else
			{
				Shape.Rotation = 180;
				PlusServices.IsVisible = true;
				ViewServices.IsEnabled = true;
				ViewServices.IsVisible = true;
			}
		}

		private void EntryVisible_OnTapped(object sender, EventArgs e)
		{
			ServicesEntry.IsVisible = true;
			ServicesEntry.IsEnabled = true;
		}

		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			ServicesEntry.IsVisible = false;
			ServicesEntry.IsEnabled = false;
		}
	}
}
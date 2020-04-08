using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CountriesContentView : ContentView
	{
		public CountriesContentView()
		{
			InitializeComponent();
		}

		private void Cell_OnTapped(object sender, EventArgs e)
		{
			Countries.IsEnabled = true;
			Countries.IsVisible = true;
			SelectedCountryLayout.IsVisible = false;
			SelectedCountryLayout.IsEnabled = false;
		}

		private void Countries_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				Countries.IsEnabled = false;
				Countries.IsVisible = false;
				SelectedCountryLayout.IsVisible = true;
				SelectedCountryLayout.IsEnabled = true;
			}
		}

		private void Cell_OnTapped2(object sender, EventArgs e)
		{
			Countries.IsEnabled = false;
			Countries.IsVisible = false;
			SelectedCountryLayout.IsVisible = true;
			SelectedCountryLayout.IsEnabled = true;
		}
	}
}
using System;
using bonus.app.Core.Pages.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FillinDetailsPage : ContentPage
	{
		#region .ctor
		public FillinDetailsPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new PublicOfferPage());
		}

		private void Entry_Completed(object sender, EventArgs e)
		{
			City.IsVisible = true;
		}
		#endregion
	}
}

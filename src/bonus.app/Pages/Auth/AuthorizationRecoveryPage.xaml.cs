using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Page;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AuthorizationRecoveryPage : MvxContentPage<AuthorizationRecoveryViewModel>
	{
		#region .ctor
		public AuthorizationRecoveryPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new RecoveryDetailPage());
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new BusinessmanAndCustomerPage());
		}
		#endregion
	}
}

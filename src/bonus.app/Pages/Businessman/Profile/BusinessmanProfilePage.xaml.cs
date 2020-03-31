using System;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Page;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_profile", Title = "Профиль")]
	public partial class BusinessmanProfilePage : MvxContentPage<BusinessmanProfileViewModel>
	{
		#region .ctor
		public BusinessmanProfilePage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void ImageButton_OnClicked(object sender, EventArgs e)
		{
			if (Frame.IsEnabled)
			{
				Frame.IsVisible = false;
				Frame.IsEnabled = false;
			}
			else
			{
				Frame.IsVisible = true;
				Frame.IsEnabled = true;
			}
		}

		private void Subscribers_OnTapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SubscribersPage());
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			var app = Application.Current.MainPage.Navigation.NavigationStack;
		}

		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{
		}
		#endregion
	}
}

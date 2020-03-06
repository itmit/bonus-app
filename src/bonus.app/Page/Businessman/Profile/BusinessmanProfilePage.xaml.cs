using System;
using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels.Businessman.Profile;
using bonus.app.Core.Views.Popups;
using bonus.app.Page;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_profile",
		Title = "Профиль")]
    public partial class BusinessmanProfilePage : MvxContentPage<BusinessmanProfileViewModel>
    {
        public BusinessmanProfilePage()
        {
            InitializeComponent();
			
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			var app = Application.Current.MainPage.Navigation.NavigationStack;
		}

		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{

		}

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
	}
}
using System;
using bonus.app.Core.Models;
using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfileCustomerPage : MvxContentPage<EditProfileCustomerViewModel>
    {
        public EditProfileCustomerPage()
        {
            InitializeComponent();
			IsFemale.IsChecked = true;
		}

		/// <summary>Application developers can override this method to provide behavior when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed()
		{
			if (ViewModel.IsAuthorization)
			{
				return base.OnBackButtonPressed();
			}

			ViewModel.OpenAuthorization();
			return true;
		}

		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{
			IsFemale.IsChecked = !IsFemale.IsChecked;
		}
	}
}
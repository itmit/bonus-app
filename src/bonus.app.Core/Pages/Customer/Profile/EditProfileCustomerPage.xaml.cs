using System;
using bonus.app.Core.ViewModels.Customer.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfileCustomerPage : MvxContentPage<EditProfileCustomerViewModel>
	{
		#region .ctor
		public EditProfileCustomerPage()
		{
			InitializeComponent();
			IsFemale.IsChecked = true;
		}
		#endregion

		#region Overrided
		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			MainContent.BindingContext = ViewModel;
			base.OnAppearing();
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
		#endregion

		#region Private
		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{
			IsFemale.IsChecked = !IsFemale.IsChecked;
		}
		#endregion
	}
}

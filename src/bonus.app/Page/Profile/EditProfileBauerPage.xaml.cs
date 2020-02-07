using System;
using bonus.app.Core.ViewModels.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfileBauerPage : MvxContentPage<EditProfileBauerViewModel>
    {
        public EditProfileBauerPage()
        {
            InitializeComponent();
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
	}
}
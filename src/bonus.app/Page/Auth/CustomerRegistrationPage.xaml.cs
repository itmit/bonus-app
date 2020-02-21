using System;
using bonus.app.Core.ViewModels.Auth;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerRegistrationPage : MvxContentPage<CustomerRegistrationViewModel>
	{
		private bool _isFirstAppearing = true;

        public CustomerRegistrationPage()
        {
            InitializeComponent();
		}

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (_isFirstAppearing)
			{
				ViewModel.AfterRegister += ViewModelOnAfterRegister;
				_isFirstAppearing = false;
			}
		}

		private void ViewModelOnAfterRegister()
		{
			Navigation.PushPopupAsync(new SuccessRegisterPopupPage(ViewModel));
		}
	}
}
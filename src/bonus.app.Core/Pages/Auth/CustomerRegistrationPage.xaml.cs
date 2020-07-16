using System;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerRegistrationPage : MvxContentPage<CustomerRegistrationViewModel>
	{
		#region Data
		#region Fields
		private bool _isFirstAppearing = true;
		#endregion
		#endregion

		#region .ctor
		public CustomerRegistrationPage()
		{
			InitializeComponent();
		}
		#endregion

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (PasswordField.InputType == MaterialTextFieldInputType.Password)
			{
				((Image)sender).Source = new FileImageSource
				{
					File = "ic_hide_eye_gray"
				};
				PasswordField.InputType = MaterialTextFieldInputType.Default;
			}
			else
			{
				((Image)sender).Source = new FileImageSource
				{
					File = "ic_eye_gray"
				};
				PasswordField.InputType = MaterialTextFieldInputType.Password;
			}
		}

		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{
			if (ConfirmPasswordField.InputType == MaterialTextFieldInputType.Password)
			{
				((Image)sender).Source = new FileImageSource
				{
					File = "ic_hide_eye_gray"
				};
				ConfirmPasswordField.InputType = MaterialTextFieldInputType.Default;
			}
			else
			{
				((Image)sender).Source = new FileImageSource
				{
					File = "ic_eye_gray"
				};
				ConfirmPasswordField.InputType = MaterialTextFieldInputType.Password;
			}
		}
	}
}

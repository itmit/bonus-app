using System;
using bonus.app.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecoveryDetailPage : MvxContentPage<RecoveryDetailViewModel>
	{
		#region .ctor
		public RecoveryDetailPage()
		{
			InitializeComponent();
		}
		#endregion

		private void ShowHideConfirmPassword(object sender, EventArgs e)
		{
			if (!(sender is Image image))
			{
				return;
			}

			ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
			image.Source = ConfirmPasswordEntry.IsPassword ? "ic_eye" : "ic_hide_eye";
		}

		private void ShowHidePassword(object sender, EventArgs e)
		{
			if (!(sender is Image image))
			{
				return;
			}

			PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
			image.Source = PasswordEntry.IsPassword ? "ic_eye" : "ic_hide_eye";
		}
	}
}

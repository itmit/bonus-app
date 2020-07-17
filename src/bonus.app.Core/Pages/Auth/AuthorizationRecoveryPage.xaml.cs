using System;
using bonus.app.Core.ViewModels.Auth;
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
	}
}

using System;
using bonus.app.Core.Pages.Chats;
using bonus.app.Core.ViewModels.Customer;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, Title = "Меню")]
	public partial class MenuCustomerPage : MvxContentPage<MenuCustomerViewModel>
	{
		#region .ctor
		public MenuCustomerPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ChatPage());
		}
		#endregion
	}
}

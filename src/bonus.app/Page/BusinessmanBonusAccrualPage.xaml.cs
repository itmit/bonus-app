using System;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(WrapInNavigationPage = false)]
	public partial class BusinessmanBonusAccrualPage : MvxContentPage<BusinessmanBonusAccrualViewModel>
    {
        public BusinessmanBonusAccrualPage()
        {
            InitializeComponent();
        }

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new BusinessmanBonusAccrualDetails());
		}
	}
}
using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.BonusAccrual
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessmanBonusAccrualDetails : MvxContentPage<BusinessmanBonusAccrualDetailsViewModel>
    {
        public BusinessmanBonusAccrualDetails()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.AccrueAndWriteOffBonusesEventHandler += OnAccrueAndWriteOffBonuses;
        }

        private void OnAccrueAndWriteOffBonuses(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SuccessAccrualPopupPage(ViewModel.BonusesForAccrual));
        }

		private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
		{
			if (!e.IsFocused
				&& ViewModel.ServicePrice != null)
			{
				Task.Run(() =>
				{
					ViewModel.UpdateBonuses(ViewModel.SelectedService, ViewModel.ServicePrice.Value);
				});
			}
		}
	}
}
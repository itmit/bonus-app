﻿using System;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.BonusAccrual;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.BonusAccrual
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusinessmanBonusAccrualDetails : MvxContentPage<BusinessmanBonusAccrualDetailsViewModel>
	{
		#region .ctor
		public BusinessmanBonusAccrualDetails()
		{
			InitializeComponent();
		}
		#endregion

		#region Overrided
		protected override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.AccrueAndWriteOffBonusesEventHandler += OnAccrueAndWriteOffBonuses;
		}
		#endregion

		#region Private
		private void OnAccrueAndWriteOffBonuses(object sender, EventArgs e)
		{
			Navigation.PushPopupAsync(new SuccessAccrualPopupPage(ViewModel.AccrueBonuses));
		}

		private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
		{
			if (!e.IsFocused)
			{
				Task.Run(() =>
				{
					ViewModel.UpdateBonuses(ViewModel.SelectedService, ViewModel.ServicePrice);
				});
			}
		}

		private void VisualElement_OnUnfocused1(object sender, FocusEventArgs e)
		{
			if (!e.IsFocused)
			{
				Task.Run(() =>
				{
					ViewModel.UpdateBonusesForAccrual(ViewModel.SelectedService, ViewModel.ServicePrice);
				});
			}
		}

		private void VisualElement_OnUnfocused2(object sender, FocusEventArgs e)
		{
			if (!e.IsFocused)
			{
				Task.Run(() =>
				{
					ViewModel.UpdateAccrueBonuses(ViewModel.SelectedService);
				});
			}
		}
		#endregion
	}
}

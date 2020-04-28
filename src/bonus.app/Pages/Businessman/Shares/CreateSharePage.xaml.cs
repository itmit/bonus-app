using System;
using bonus.app.Core.Models;
using bonus.app.Core.ViewModels.Businessman.Shares;
using bonus.app.Core.Views.Popups;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateSharePage : MvxContentPage<CreateShareViewModel>
	{
		#region .ctor
		public CreateSharePage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			Layout.BindingContext = ViewModel;
			PicCountryAndCityContentView.ViewModel = ViewModel.PicCountryAndCityViewModel;

			base.OnAppearing();
		}
	}
}

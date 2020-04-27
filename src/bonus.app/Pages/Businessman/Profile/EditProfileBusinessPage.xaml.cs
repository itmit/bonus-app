using System;
using bonus.app.Core.Models;
using bonus.app.Core.ViewModels.Businessman.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfileBusinessPage : MvxContentPage<EditProfileBusinessmanViewModel>
	{
		#region .ctor
		public EditProfileBusinessPage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			Layout.BindingContext = ViewModel;
			PicCountryAndCityView.ViewModel = ViewModel.CountryAndCityViewModel;

			base.OnAppearing();
		}
	}
}

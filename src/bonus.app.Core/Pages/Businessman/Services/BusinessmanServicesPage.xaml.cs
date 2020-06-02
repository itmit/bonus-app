using System;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, Icon = "ic_star", Title = "Услуги")]
	public partial class BusinessmanServicesPage : MvxContentPage<BusinessmanServicesViewModel>
	{
		#region .ctor
		public BusinessmanServicesPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Overrided
		/// <summary>
		/// When overridden, allows application developers to customize behavior immediately prior to the
		/// <see cref="T:Xamarin.Forms.Page" /> becoming visible.
		/// </summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			MyServicesView.ViewModel = ViewModel.MyServicesViewModel;
			base.OnAppearing();
		}
		#endregion

		#region Private
		private void MenuItem_OnClicked(object sender, EventArgs e)
		{
			Scroll.ScrollToAsync(AddServiceLabel, ScrollToPosition.Start, false);
		}
		#endregion
	}
}

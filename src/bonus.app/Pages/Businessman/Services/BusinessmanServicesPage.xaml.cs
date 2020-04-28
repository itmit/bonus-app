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

			var toolBar = new ToolbarItem
			{
				Text = "Добавить свою услугу",
				Order = ToolbarItemOrder.Secondary,
				Priority = 0
			};

			var toolBar1 = new ToolbarItem
			{
				Text = "Редактировать список услуг",
				Order = ToolbarItemOrder.Secondary,
				Priority = 1
			};

			toolBar.Clicked += ToolbarItem_Clicked;
			toolBar1.Clicked += ToolBar1_Clicked;

			ToolbarItems.Add(toolBar);
			ToolbarItems.Add(toolBar1);
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
		private void ToolBar1_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new EditBusinessmanServicesPage());
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
		}
		#endregion
	}
}

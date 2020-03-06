using System;
using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels.Businessman.Services;
using bonus.app.Core.Views.ViewCells.Services;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab,
		Icon = "ic_star",
		Title = "Услуги")]
    public partial class BusinessmanServicesPage : MvxContentPage<BusinessmanServicesViewModel>
    {
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

        private void ToolBar1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditBusinessmanServicesPage());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			return;
        }

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (ViewServices.IsEnabled)
			{
				PlusServices.IsVisible = false;
				ViewServices.IsEnabled = false;
				ViewServices.IsVisible = false;
				Shape.Rotation = 0;
			}
			else
			{
				Shape.Rotation = 180;
				PlusServices.IsVisible = true;
				ViewServices.IsEnabled = true;
				ViewServices.IsVisible = true;
			}
        }

		private void TapGestureRecognizer_OnTapped1(object sender, EventArgs e)
		{

		}
	}
}
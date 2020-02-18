using System;
using System.Collections.ObjectModel;
using System.Linq;
using bonus.app.Core.ViewModels;
using bonus.app.Page;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

			var collection = new ObservableCollection<string>();
            collection.Add("0");
            collection.Add("1");

			ViewServices.ItemsSource = collection;

			ServicesName.Text = "Пока у вас нет услуг";
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

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Percentage.IsVisible = true;
			ServicesName.Text = "Массаж";
		}

		#region Charge
		#region Quantity
		private void VisualElement_OnFocused(object sender, FocusEventArgs e)
		{
			QuantityLabel.TextColor = Color.FromHex("#858585");
		}

		private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
		{
			QuantityLabel.TextColor = Color.FromHex("#858585");
		}

		#endregion

		#region Percentage
		private void VisualElement_OnFocused1(object sender, FocusEventArgs e)
		{
			PercentageLabel.TextColor = Color.FromHex("#858585");
		}

		private void VisualElement_OnUnfocused1(object sender, FocusEventArgs e)
		{
			PercentageLabel.TextColor = Color.FromHex("#858585");
		}
		#endregion
		#endregion

		#region Decommissioning
		#region Quantity
		private void DecommissioningQuantityLabel_OnFocused(object sender, FocusEventArgs e)
		{
			DecommissioningQuantityLabel.TextColor = Color.FromHex("#858585");
		}

		private void DecommissioningQuantityLabel_OnUnfocused(object sender, FocusEventArgs e)
		{
			DecommissioningQuantityLabel.TextColor = Color.FromHex("#858585");
		}
		#endregion

		#region Percentage
		private void DecommissioningPercentageLabel_OnFocused(object sender, FocusEventArgs e)
		{
			DecommissioningPercentageLabel.TextColor = Color.FromHex("#858585");
		}

		private void DecommissioningPercentageLabel_OnUnfocused(object sender, FocusEventArgs e)
		{
			DecommissioningPercentageLabel.TextColor = Color.FromHex("#858585");
		}
		#endregion
		#endregion


	}
}
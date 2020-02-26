using System;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ViewCells.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceTypeViewCell : ViewCell
    {
		private MvxViewModel _viewModel;

		public ServiceTypeViewCell()
        {
            InitializeComponent();
			
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (ViewServices.IsEnabled)
			{
				ViewServices.IsEnabled = false;
				ViewServices.IsVisible = false;
				Shape.Rotation = 0;
			}
			else
			{
				Shape.Rotation = 180;
				ViewServices.IsEnabled = true;
				ViewServices.IsVisible = true;
			}
		}
	}
}
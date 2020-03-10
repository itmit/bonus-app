using System;
using System.Collections.ObjectModel;
using bonus.app.Core.ViewModels;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page.Businessman.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBusinessmanServicesDetailsPage : MvxContentPage<EditBusinessmanServicesDetailsViewModel>
    {
        public EditBusinessmanServicesDetailsPage()
        {
            InitializeComponent();
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}
	}
}
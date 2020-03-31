using System;
using bonus.app.Core.ViewModels.Businessman.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditBusinessmanServicesDetailsPage : MvxContentPage<EditBusinessmanServicesDetailsViewModel>
	{
		#region .ctor
		public EditBusinessmanServicesDetailsPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_OnClicked(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}
		#endregion
	}
}

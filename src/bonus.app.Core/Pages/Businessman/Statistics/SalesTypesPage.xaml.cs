using System;
using System.Linq;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using bonus.app.Core.Views.ContentViews.Services;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SalesTypesPage : MvxContentPage<SalesTypesViewModel>
	{
		#region .ctor
		public SalesTypesPage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			LineChart.BindingContext = ViewModel;
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			var view = (View) sender;
			var service = (Service)view.BindingContext;
			if (ViewModel.SelectedItems.Any(s => s.Uuid.Equals(service.Uuid)))
			{
				ViewModel.SelectedItems.Remove(service);
				view.BackgroundColor = Color.Transparent;
			}
			else
			{
				ViewModel.SelectedItems.Add(service);
				view.BackgroundColor = Color.FromHex("#BB8D91");
			}

		}
	}
}

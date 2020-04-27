using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.GeoHelper;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Realms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicCountryAndCityContentView : MvxContentView<PicCountryAndCityViewModel>
	{
		private View _footer;

		public PicCountryAndCityContentView()
		{
			InitializeComponent();
		}

		public View Footer
		{
			get => _footer;
			set
			{
				_footer = value;
				FieldsLayout.Children.Clear();
				FieldsLayout.Children.Add(value);
			}
		}

		private void Cities_OnScrolled(object sender, ItemsViewScrolledEventArgs e)
		{
			if (ViewModel.IsBusy || ViewModel.Cities.Count == 0)
			{
				return;
			}


			if (e.LastVisibleItemIndex == ViewModel.Cities.Count - 1)
			{
				ViewModel.IsBusy = true;

				ViewModel.LoadMoreCitiesCommand.Execute();
				ViewModel.IsBusy = false;
			}
		}
	}
}
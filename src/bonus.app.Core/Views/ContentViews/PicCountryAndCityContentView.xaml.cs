using System;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicCountryAndCityContentView : MvxContentView<PicCountryAndCityViewModel>
	{
		#region Data
		#region Fields
		private View _footer;
		private CityCollectionContentView _cityCollectionContentView;
		private bool _firstViewModelSet = true;
		#endregion
		#endregion

		#region .ctor
		public PicCountryAndCityContentView()
		{
			InitializeComponent();
		}
		#endregion

		protected override void OnViewModelSet()
		{
			base.OnViewModelSet();

			if (!_firstViewModelSet)
			{
				return;
			}

			_firstViewModelSet = false;
			if (!ViewModel.CanPicCountryOrCity)
			{
				return;
			}

			_cityCollectionContentView = new CityCollectionContentView(OtherCityTabExecute);
			CityCollectionView.Children.Add(_cityCollectionContentView);
		}

		private void OtherCityTabExecute()
		{
			_cityCollectionContentView.CitySearchBar.Focus();
			ViewModel.Cities.Clear();
		}

		#region Properties
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
		#endregion
	}
}

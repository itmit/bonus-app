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
		#endregion
		#endregion

		#region .ctor
		public PicCountryAndCityContentView()
		{
			InitializeComponent();
		}
		#endregion

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

		private void OtherCityTabbed(object sender, EventArgs e)
		{
			CitySearchBar.Focus();
			ViewModel.Cities.Clear();
		}
	}
}

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

		#region Private
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
		#endregion
	}
}

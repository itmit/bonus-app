using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CityCollectionContentView : ContentView
	{
		private readonly Action _otherCityTabExecute;

		public SearchBar CitySearchBar
		{
			get;
		}

		public CityCollectionContentView(Action otherCityTabExecute)
		{
			_otherCityTabExecute = otherCityTabExecute;
			InitializeComponent();
			CitySearchBar = CityBar;
		}

		private void OtherCityTabbed(object sender, EventArgs e)
		{
			_otherCityTabExecute?.Invoke();
		}
	}
}
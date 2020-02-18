using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicesViewCell : ViewCell
    {
        public ServicesViewCell()
        {
            InitializeComponent();

			var collection = new ObservableCollection<string>();
			collection.Add("0");
			collection.Add("1");

			ViewServices.ItemsSource = collection;
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
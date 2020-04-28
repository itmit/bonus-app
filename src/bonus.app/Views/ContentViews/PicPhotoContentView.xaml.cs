using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicPhotoContentView : ContentView
	{
		#region .ctor
		public PicPhotoContentView()
		{
			InitializeComponent();
		}
		#endregion

		#region Properties
		public string Placeholder
		{
			get => PlaceholderLabel.Text;
			set => PlaceholderLabel.Text = value;
		}
		#endregion
	}
}

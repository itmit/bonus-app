using bonus.app.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CollectionView), typeof(CustomCollectionViewRenderer))]
namespace bonus.app.iOS.Renderers
{
	public class CustomCollectionViewRenderer : CollectionViewRenderer
	{
		protected override void Dispose(bool disposing)
		{
			ItemsView.SelectedItem = null;
			ItemsView.ItemsSource = null;
			base.Dispose(disposing);
		}
	}
}

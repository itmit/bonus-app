using bonus.app.Core.ViewModels.Businessman.Statistics;
using MvvmCross.Forms.Views;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewsProfilePage : MvxContentPage<ViewsProfileViewModel>
	{
		#region .ctor
		public ViewsProfilePage()
		{
			InitializeComponent();
		}
		#endregion

		private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			canvas.Clear();

			var axisPaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.FromHex("#838390").ToSKColor(),
				StrokeWidth = 2
			};

			canvas.DrawLine(0 ,0, 0, info.Height, axisPaint);
			canvas.DrawLine(0, 0, info.Width, 0, axisPaint);
			canvas.DrawLine(0, info.Height, info.Width, info.Height, axisPaint);

			var yLine = info.Height / 6f;

			canvas.DrawLine(0, yLine * 1, info.Width, yLine * 1, axisPaint);
			canvas.DrawLine(0, yLine * 2, info.Width, yLine * 2, axisPaint);
			canvas.DrawLine(0, yLine * 3, info.Width, yLine * 3, axisPaint);
			canvas.DrawLine(0, yLine * 4, info.Width, yLine * 4, axisPaint);
			canvas.DrawLine(0, yLine * 5, info.Width, yLine * 5, axisPaint);

		}
	}
}

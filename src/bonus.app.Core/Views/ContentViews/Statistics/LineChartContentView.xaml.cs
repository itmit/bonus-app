using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LineChartContentView : ContentView
	{
		public LineChartContentView()
		{
			InitializeComponent();
		}

		private void SKCanvasView_OnPaintSurfacew_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
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

			const int bottomMargin = 15;

			canvas.DrawLine(0, 0, 0, info.Height - bottomMargin, axisPaint);
			canvas.DrawLine(0, 0, info.Width, 0, axisPaint);
			canvas.DrawLine(0, info.Height - bottomMargin, info.Width, info.Height - bottomMargin, axisPaint);

			var yLine = (info.Height - bottomMargin) / 6f;

			canvas.DrawLine(0, yLine * 1, info.Width, yLine * 1, axisPaint);
			canvas.DrawLine(0, yLine * 2, info.Width, yLine * 2, axisPaint);
			canvas.DrawLine(0, yLine * 3, info.Width, yLine * 3, axisPaint);
			canvas.DrawLine(0, yLine * 4, info.Width, yLine * 4, axisPaint);
			canvas.DrawLine(0, yLine * 5, info.Width, yLine * 5, axisPaint);

			var dateCirclePaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.FromHex("#7A6457")
							 .ToSKColor(),
				StrokeWidth = 5
			};
			var dateTextPaint = new SKPaint
			{
				Color = Color.FromHex("#020203")
							 .ToSKColor(),
				TextSize = 10
			};

			var xLine = info.Width / 6f;

			for (var i = 0; i < 6; i++)
			{
				canvas.DrawCircle(xLine * i, info.Height, 5, dateCirclePaint);
				canvas.DrawText($"{10 + i}.06", xLine * i, info.Height, dateTextPaint);
			}
		}
	}
}
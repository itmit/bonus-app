using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Models.ServiceModels;
using bonus.app.Core.Models.Statistic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LineStatisticContentView : ContentView
	{
		public LineStatisticContentView()
		{
			InitializeComponent();
		}
		private void SKCanvasView_OnPaintSurfacew_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var model = (Line)BindingContext;

			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			canvas.Clear();

			var dateTextPaint = new SKPaint
			{
				Color = Color.FromHex("#020203")
							 .ToSKColor(),
				TextSize = 35,
				StrokeWidth = 100
			};

			canvas.DrawCircle(info.Width - 60, info.Height / 2f - 10, 20, new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = model.Color.ToSKColor()
			});
			canvas.DrawCircle(info.Width - 60, info.Height / 2f - 10, 10, new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.White.ToSKColor()
			});

			canvas.DrawText(model.Name, 15, info.Height / 2f, dateTextPaint);
			canvas.DrawLine(0, info.Height, info.Width, info.Height, new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.FromHex("#838390").ToSKColor(),
				StrokeWidth = 2
			});
		}
	}
}
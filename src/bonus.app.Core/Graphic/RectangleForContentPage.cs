using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.Graphic
{
	public class RectangleForContentPage : SKCanvasView
	{
		#region .ctor
		public RectangleForContentPage() => PaintSurface += RectangleForContentPagePaintSurface;
		#endregion

		#region Private
		private void RectangleForContentPagePaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			canvas.Clear();

			using (var paint = new SKPaint())
			{
				var rect = new SKRect(0, 0, info.Width, info.Height);

				paint.Shader = SKShader.CreateLinearGradient(new SKPoint(rect.Left, rect.Top),
															 new SKPoint(rect.Right, rect.Bottom),
															 new[]
															 {
																 SKColor.Parse("#aea59f"),
																 SKColor.Parse("#7b726c")
															 },
															 new float[]
															 {
																 0,
																 1
															 },
															 SKShaderTileMode.Repeat);

				canvas.DrawRect(rect, paint);
			}
		}
		#endregion
	}
}

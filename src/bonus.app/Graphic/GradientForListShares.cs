using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.Graphic
{
	public class GradientForListShares : SKCanvasView
	{
		#region .ctor
		public GradientForListShares() => PaintSurface += GradientForListSharesPaintSurface;
		#endregion

		#region Private
		private void GradientForListSharesPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			canvas.Clear();

			using (var paint = new SKPaint())
			{
				paint.Shader = SKShader.CreateLinearGradient(new SKPoint(info.Rect.Left, info.Rect.MidY),
															 new SKPoint(info.Rect.Right, info.Rect.MidY),
															 new[]
															 {
																 new SKColor(218, 205, 93, 0),
																 new SKColor(245, 227, 64, 125)
															 },
															 null,
															 SKShaderTileMode.Clamp);

				canvas.DrawRect(info.Rect, paint);
			}
		}
		#endregion
	}
}

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.Graphic
{
	public class GradientForBonusAccrual : SKCanvasView
	{
		#region .ctor
		public GradientForBonusAccrual() => PaintSurface += GradientForBonusAccrualPaintSurface;
		#endregion

		#region Private
		private void GradientForBonusAccrualPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			canvas.Clear();

			using (var paint = new SKPaint())
			{
				paint.Shader = SKShader.CreateLinearGradient(new SKPoint(0, 0),
															 new SKPoint(info.Width, info.Height),
															 new[]
															 {
																 new SKColor(156, 147, 141, 200),
																 new SKColor(156, 147, 141, 200)
															 },
															 null,
															 SKShaderTileMode.Mirror);

				canvas.DrawRect(info.Rect, paint);
			}
		}
		#endregion
	}
}

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.Graphic
{
	public class RectangleInProfile : SKCanvasView
	{
		#region .ctor
		public RectangleInProfile() => PaintSurface += RectangleInProfilePaintSurface;
		#endregion

		#region Private
		private void RectangleInProfilePaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			canvas.Clear();

			using (var paint = new SKPaint())
			{
				var rect = new SKRect(0, 0, info.Width, info.Size.Height);

				paint.Shader = SKShader.CreateLinearGradient(new SKPoint(rect.Left, rect.MidY),
															 new SKPoint(rect.Right, rect.MidY),
															 new[]
															 {
																 SKColor.Parse("#ada49e"),
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

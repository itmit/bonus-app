using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.Graphic
{
    public class GradientForBonusAccrual : SKCanvasView
    {
        public GradientForBonusAccrual()
        {
            PaintSurface += GradientForBonusAccrualPaintSurface;
        }

        private void GradientForBonusAccrualPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (var paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(info.Width, info.Height),
                    new SKColor[]
                    {
                        new SKColor(156,147,141,200),
                        new SKColor(156,147,141,200),
                    },
                    null,
                    SKShaderTileMode.Mirror);

                canvas.DrawRect(info.Rect, paint);
            }
        }
    }
}

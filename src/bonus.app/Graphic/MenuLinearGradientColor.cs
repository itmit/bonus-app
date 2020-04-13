using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace bonus.app.Core.Graphic
{
    public class MenuLinearGradientColor : SKCanvasView
    {
        public MenuLinearGradientColor()
        {
            PaintSurface += MenuLinearGradientColorPaintSurface;
        }

        private void MenuLinearGradientColorPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (var paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(0,0),
                    new SKPoint(info.Width, info.Height),
                    new SKColor[]
                    {
                        new SKColor(160,150,142,200),
                        new SKColor(0,0,0,150),
                        new SKColor(0,0,0,150),
                    },
                    null,
                    SKShaderTileMode.Mirror);

                canvas.DrawRect(info.Rect, paint);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.Models.Statistic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ContentViews.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LineChartContentView : ContentView
	{
		public LineChartContentView()
		{
			InitializeComponent();

			ChartView = Chart;
		}

		public SKCanvasView ChartView
		{
			get;
		}

		public static readonly BindableProperty LinesProperty =
			BindableProperty.Create(
				nameof(Lines), 
				typeof(IEnumerable<Line>), 
				typeof(LineChartContentView),
				defaultValue: new List<Line>(), 
				BindingMode.OneWay, 
				null,
				OnLinesPropertyPropertyChanged);

		private static void OnLinesPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue == null)
			{
				return;
			}

			var view = (LineChartContentView) bindable;
			var lines = ((IEnumerable<Line>) newValue).ToList();
			if (lines.Any())
			{
				view.ChartView.InvalidateSurface();
			}
		}

		public IEnumerable<Line> Lines
		{
			get => (IEnumerable<Line>)GetValue(LinesProperty);
			set => SetValue(LinesProperty, value);
		}

		private void SKCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var info = e.Info;
			var surface = e.Surface;
			var canvas = surface.Canvas;

			if (!Lines.Any())
			{
				return;
			}

			if (canvas == null)
			{
				return;
			}
			canvas.Clear();
			var list = Lines.ToList();
			var axisPaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.FromHex("#838390").ToSKColor(),
				StrokeWidth = 2
			};

			const int bottomMargin = 50;
			const int yOffset = 20;
			const int xOffset = 20;
			float yAxis = info.Height - bottomMargin + yOffset;

			canvas.DrawLine(xOffset, yOffset, xOffset, yAxis, axisPaint);
			canvas.DrawLine(xOffset, yOffset, info.Width, yOffset, axisPaint);
			canvas.DrawLine(xOffset, yAxis, info.Width, yAxis, axisPaint);

			var yLine = yAxis / 6f;

			canvas.DrawLine(xOffset, yLine * 1, info.Width, yLine * 1, axisPaint);
			canvas.DrawLine(xOffset, yLine * 2, info.Width, yLine * 2, axisPaint);
			canvas.DrawLine(xOffset, yLine * 3, info.Width, yLine * 3, axisPaint);
			canvas.DrawLine(xOffset, yLine * 4, info.Width, yLine * 4, axisPaint);
			canvas.DrawLine(xOffset, yLine * 5, info.Width, yLine * 5, axisPaint);

			var dateCirclePaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.FromHex("#7A6457")
							 .ToSKColor(),
			};
			var dateTextPaint = new SKPaint
			{
				Color = Color.FromHex("#020203")
							 .ToSKColor(),
				TextSize = 30
			};

			var dates = new List<DateTime>();
			var maxValue = 0f;
			foreach (var linePoint in list.SelectMany(line => line.Points))
			{
				if (!dates.Any(time => time.Equals(linePoint.Date)))
				{
					dates.Add(linePoint.Date);
				}

				maxValue = linePoint.Value > maxValue ? linePoint.Value : maxValue;
			}

			var pointPaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				StrokeWidth = 2
			};
			var point2Paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = Color.White.ToSKColor()
			};

			if (dates.Count == 0)
			{
				return;
			}

			var dc = dates.Count == 1 ? dates.Count : dates.Count - 1;
			var a = (info.Width - xOffset - (90 + xOffset)) / dc;
			foreach (var line in list)
			{
				pointPaint.Color = line
								   .Color.ToSKColor();
				var first = true;
				var py = 0f;
				var px = 0f;
				foreach (var point in line.Points)
				{
					var y = yAxis - yAxis / 100 * (point.Value / (maxValue / 100)) + yOffset;
					var x = a * dates.IndexOf(point.Date.Date) + xOffset;
					if (first)
					{
						first = false;
					}
					else
					{
						canvas.DrawLine(px,py, a * dates.IndexOf(point.Date.Date), y, pointPaint);
					}

					py = y;
					px = a * dates.IndexOf(point.Date.Date);
					canvas.DrawCircle(x, y, 20, pointPaint);
					canvas.DrawCircle(x, y, 10, point2Paint);
				}
			}

			for (var i = 0; i < dates.Count; i++)
			{
				canvas.DrawCircle(a * i + xOffset, info.Height - 10, 10, dateCirclePaint);
				canvas.DrawText(dates[i].ToString("MM.dd"), a * i + 25 + xOffset, info.Height, dateTextPaint);
			}
		}
	}
}
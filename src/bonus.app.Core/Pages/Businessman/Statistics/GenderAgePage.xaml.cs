using System.Drawing;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using Microcharts;
using Entry = Microcharts.Entry;
using MvvmCross.Forms.Views;
using SkiaSharp;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenderAgePage : MvxContentPage<GenderAgeViewModel>
	{
		#region .ctor
		public GenderAgePage()
		{
			InitializeComponent();
		}
		#endregion

		#region override
		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			var colorFemale = "#cc2084";
			var colorMale = "#482d86";

			var entriesColumn1 = new[]
			{
				new Entry(30)
				{
					Color = SKColor.Parse(colorFemale)
				},
				new Entry(30)
				{
					Color = SKColor.Parse(colorMale)
				}
			};
			var entriesColumn2 = new[]
			{
				new Entry(50)
				{
					Color = SKColor.Parse(colorFemale)
				},
				new Entry(40)
				{
					Color = SKColor.Parse(colorMale)
				}
			};
			var entriesColumn3 = new[]
			{
				new Entry(33)
				{
					Color = SKColor.Parse(colorFemale)
				},
				new Entry(95)
				{
					Color = SKColor.Parse(colorMale)
				}
			};
			var entriesColumn4 = new[]
			{
				new Entry(83)
				{
					Color = SKColor.Parse(colorFemale)
				},
				new Entry(65)
				{
					Color = SKColor.Parse(colorMale)
				}
			};
			var entriesColumn5 = new[]
			{
				new Entry(39)
				{
					Color = SKColor.Parse(colorFemale)
				},
				new Entry(53)
				{
					Color = SKColor.Parse(colorMale)
				}
			};
			var entriesColumn6 = new[]
			{
				new Entry(77)
				{
					Color = SKColor.Parse(colorFemale)
				},
				new Entry(85)
				{
					Color = SKColor.Parse(colorMale)
				}
			};

			var chart1 = new BarChart
			{
				Entries = entriesColumn1,
				BackgroundColor = SKColor.Empty,
				Margin = 0f,
				MaxValue = 100f,
				MinValue = 0f
			};
			var chart2 = new BarChart
			{
				Entries = entriesColumn2,
				BackgroundColor = SKColor.Empty,
				Margin = 0f,
				MaxValue = 100f,
				MinValue = 0f
			};
			var chart3 = new BarChart
			{
				Entries = entriesColumn3,
				BackgroundColor = SKColor.Empty,
				Margin = 0f,
				MaxValue = 100f,
				MinValue = 0f
			};
			var chart4 = new BarChart
			{
				Entries = entriesColumn4,
				BackgroundColor = SKColor.Empty,
				Margin = 0f,
				MaxValue = 100f,
				MinValue = 0f
			};
			var chart5 = new BarChart
			{
				Entries = entriesColumn5,
				BackgroundColor = SKColor.Empty,
				Margin = 0f,
				MaxValue = 100f,
				MinValue = 0f
			};
			var chart6 = new BarChart
			{
				Entries = entriesColumn6,
				BackgroundColor = SKColor.Empty,
				Margin = 0f,
				MaxValue = 100f,
				MinValue = 0f
			};

			ChartView1.Chart = chart1;
			ChartView2.Chart = chart2;
			ChartView3.Chart = chart3;
			ChartView4.Chart = chart4;
			ChartView5.Chart = chart5;
			ChartView6.Chart = chart6;
		}
		#endregion
	}
}

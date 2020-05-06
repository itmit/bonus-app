using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Businessman.Statistics;
using Microcharts;
using MvvmCross.Forms.Views;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;

namespace bonus.app.Core.Pages.Businessman.Statistics
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeographyDetailPage : MvxContentPage<GeographyDetailViewModel>
	{
		public GeographyDetailPage()
		{
			InitializeComponent();
		}

		#region override
		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			var entries = new[]
			{
				new Entry(80)
				{
					Color = SKColor.Parse("#482d86")
				},
				new Entry(8)
				{
					Color = SKColor.Parse("#cc2084")
				},
				new Entry(8)
				{
					Color = SKColor.Parse("#f28f25")
				},
				new Entry(4)
				{
					Color = SKColor.Parse("#26921d")
				}
			};

			var donutView = new DonutChart
			{
				MaxValue = 100f,
				MinValue = 0f,
				Entries = entries,
				HoleRadius = 0.3f,
				BackgroundColor = SKColor.Empty
			};

			donut.Chart = donutView;
		}
		#endregion
	}
}
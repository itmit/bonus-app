using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bonus.app.Core.Design
{
	public class CustomButton : Frame
	{
		#region .ctor
		public CustomButton()
		{
			var tapped = new TapGestureRecognizer();
			tapped.Tapped += TappedPressed;
			GestureRecognizers.Add(tapped);
		}
		#endregion

		#region Private
		private async Task<double> GetOpacity()
		{
			await Task.Delay(100);
			return 1;
		}

		private async void TappedPressed(object sender, EventArgs e)
		{
			Opacity = 0.5;
			Opacity = await GetOpacity();
		}
		#endregion
	}
}

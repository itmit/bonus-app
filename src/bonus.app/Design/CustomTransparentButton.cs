using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bonus.app.Core.Design
{
	public class CustomTransparentButton : Frame
	{
		#region .ctor
		public CustomTransparentButton()
		{
			var tapped = new TapGestureRecognizer();
			tapped.Tapped += EffectPress;
			GestureRecognizers.Add(tapped);
		}
		#endregion

		#region Private
		private async void EffectPress(object sender, EventArgs e)
		{
			BackgroundColor = Color.FromHex("#bab3af");
			BackgroundColor = await GetColor();
		}

		private async Task<Color> GetColor()
		{
			await Task.Delay(100);
			return Color.Transparent;
		}
		#endregion
	}
}

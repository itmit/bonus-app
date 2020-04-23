using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bonus.app.Core.Design
{
    public class CustomTransparentButton : Frame
    {
        public CustomTransparentButton()
        {
            var tapped = new TapGestureRecognizer();
            tapped.Tapped += EffectPress;
            GestureRecognizers.Add(tapped);
        }

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
    }
}

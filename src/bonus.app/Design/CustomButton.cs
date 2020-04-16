using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bonus.app.Core.Design
{
    public class CustomButton : Frame
    {
        public CustomButton()
        {
            var tapped = new TapGestureRecognizer();
            tapped.Tapped += TappedPressed;
            GestureRecognizers.Add(tapped);
        }

        private async void TappedPressed(object sender, EventArgs e)
        {
            Opacity = 0.5;
            Opacity = await GetOpacity();
        }

        private async Task<double> GetOpacity()
        {
            await Task.Delay(100);
            return 1;
        }
    }
}

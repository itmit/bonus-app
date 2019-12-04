using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using bonus.app.Controls;
using bonus.app.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(EntryAuthPage), typeof(EntryLogPageRenderer))]
namespace bonus.app.Droid.Renderers
{
    public class EntryLogPageRenderer : EntryRenderer
    {
        #region .ctor
        public EntryLogPageRenderer(Context context)
            :base(context)
        {
        }
        #endregion

        #region Overrided
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
            {
                return;
            }

            Control.Background.SetColorFilter(Color.ParseColor("#bab3af"), PorterDuff.Mode.SrcAtop);
        }
        #endregion
    }
}
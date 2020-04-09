using System;
using System.Collections.Generic;
using System.Linq;
using bonus.app.Core;
using Foundation;
using MvvmCross.Platforms.Ios.Core;
using UIKit;
using FFImageLoading.Forms.Platform;

namespace bonus.app.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            CachedImageRenderer.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}

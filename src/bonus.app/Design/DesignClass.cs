using System;
using Xamarin.Forms;

namespace bonus.app.Core.Design
{
    static class DesignClass
    {
        #region Fields
        private static string _latoBold,
                              _latoRegular,
                              _montserratBold,
                              _montserratLight,
                              _montserratMedium,
                              _montserratRegular;

        private static double _heightFrame;

        private static float _radius;

        private static LayoutOptions _horizontal;

        private static Color _borderColor;

        private static bool _shadow;
        private static string _tabIconProfile;


        #endregion

        static DesignClass()
        {
            if(Device.iOS == Device.RuntimePlatform)
            {
                LatoBold = "Lato-Bold";
                LatoRegular = "Lato-Regular";
                MontserratBold = "Montserrat-Bold";
                MontserratRegular = "Montserrat-Regular";
                MontserratMedium = "Montserrat-Medium";
                MontserratLight = "Montserrat-Light";
                HeightFrame = 45;
                Radius = 22;
                Horizontal = LayoutOptions.Center;
                BorderColor = Color.FromHex("#9A9A9A");
                Shadow = false;
            }
            else if(Device.Android == Device.RuntimePlatform)
            {
                LatoBold = "Lato-Bold.ttf#Lato-Bold";
                LatoRegular = "Lato-Regular.ttf#Lato-Regular";
                MontserratBold = "Montserrat-Bold.ttf#Montserrat-Bold";
                MontserratRegular = "Montserrat-Regular.ttf#Montserrat-Regular";
                MontserratMedium = "Montserrat-Medium.ttf#Montserrat-Medium";
                MontserratLight = "Montserrat-Light.ttf#Montserrat-Light";
                HeightFrame = 55;
                Radius = 27;
                Horizontal = LayoutOptions.Start;
                Shadow = true;
            }
        }

        #region Properties
        public static string LatoBold
        {
            get => _latoBold;
            set => _latoBold = value;
        }

        public static string LatoRegular
        {
            get => _latoRegular;
            set => _latoRegular = value;
        }

        public static string MontserratBold
        {
            get => _montserratBold;
            set => _montserratBold = value;
        }

        public static string MontserratRegular
        {
            get => _montserratRegular;
            set => _montserratRegular = value;
        }

        public static string MontserratMedium
        {
            get => _montserratMedium;
            set => _montserratMedium = value;
        }

        public static string MontserratLight
        {
            get => _montserratLight;
            set => _montserratLight = value;
        }

        public static double HeightFrame
        {
            get => _heightFrame;
            set => _heightFrame = value;
        }

        public static float Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public static LayoutOptions Horizontal
        {
            get => _horizontal;
            set => _horizontal = value;
        }

        public static Color BorderColor
        {
            get => _borderColor;
            set => _borderColor = value;
        }

        public static bool Shadow
        {
            get => _shadow;
            set => _shadow = value;
        }
        #endregion
    }
}

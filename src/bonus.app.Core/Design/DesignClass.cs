using Xamarin.Forms;

namespace bonus.app.Core.Design
{
	internal static class DesignClass
	{
		#region .ctor
		static DesignClass()
		{
			if (Device.iOS == Device.RuntimePlatform)
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
				Margin = new Thickness(25, 25, 5, 10);
				MarginForTitle = new Thickness(0, 50, 0, 10);
				MarginForImage = new Thickness(0, 45, 0, 55);
			}
			else if (Device.Android == Device.RuntimePlatform)
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
				Margin = new Thickness(5, 25, 5, 10);
				MarginForTitle = new Thickness(0, 35, 0, 10);
				MarginForImage = new Thickness(0, 35, 0, 55);
			}
		}
		#endregion

		#region Properties
		public static Color BorderColor
		{
			get;
			set;
		}

		public static Color ColorButtom
		{
			get;
			set;
		} = Color.FromRgba(255, 255, 255, 0.3);

		public static double HeightFrame
		{
			get;
			set;
		}

		public static LayoutOptions Horizontal
		{
			get;
			set;
		}

		public static string LatoBold
		{
			get;
			set;
		}

		public static string LatoRegular
		{
			get;
			set;
		}

		public static Thickness Margin
		{
			get;
			set;
		}

		public static Thickness MarginForImage
		{
			get;
			set;
		}

		public static Thickness MarginForTitle
		{
			get;
			set;
		}

		public static string MontserratBold
		{
			get;
			set;
		}

		public static string MontserratLight
		{
			get;
			set;
		}

		public static string MontserratMedium
		{
			get;
			set;
		}

		public static string MontserratRegular
		{
			get;
			set;
		}

		public static float Radius
		{
			get;
			set;
		}

		public static bool Shadow
		{
			get;
			set;
		}
		#endregion
	}
}

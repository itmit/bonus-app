using System;
using System.Globalization;
using Xamarin.Forms;

namespace bonus.app.Core.Converters
{
	public class InverseBoolConverter : IValueConverter
	{
		#region IValueConverter members
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
			{
				throw new InvalidOperationException("The target must be a boolean");
			}

			return !(bool) value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
		#endregion
	}
}

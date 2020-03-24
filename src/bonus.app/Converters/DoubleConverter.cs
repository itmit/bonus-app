using System;
using System.Globalization;
using MvvmCross.Converters;

namespace bonus.app.Core.Converters
{
	public class DoubleConverter : IMvxValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "0";
			}

			double doubleValue = (double)value;
			return $"{doubleValue:N}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strValue = value as string;
			if (string.IsNullOrEmpty(strValue))
			{
				return 0;
			}

			if (double.TryParse(strValue, out var resultDouble))
			{
				return resultDouble;
			}

			return 0;
		}
	}
}

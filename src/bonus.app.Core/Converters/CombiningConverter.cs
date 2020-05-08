﻿using System;
using Xamarin.Forms;

namespace bonus.app.Core.Converters
{
	public class CombiningConverter : IValueConverter
	{
		public IValueConverter Converter1 { get; set; }
		public IValueConverter Converter2 { get; set; }

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			object convertedValue = Converter1.Convert(value, targetType, parameter, culture);
			return Converter2.Convert(convertedValue, targetType, parameter, culture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			object convertedValue = Converter2.ConvertBack(value, targetType, parameter, culture);
			return Converter2.ConvertBack(convertedValue, targetType, parameter, culture);
		}

		#endregion
	}
}

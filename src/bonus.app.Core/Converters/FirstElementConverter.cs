using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MvvmCross.Binding.Extensions;
using Xamarin.Forms;

namespace bonus.app.Core.Converters
{
	public class FirstElementConverter : IValueConverter
	{
		#region IValueConverter members
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IEnumerable values)
			{
				var array = values as object[] ??
								 values.Cast<object>()
									   .ToArray();
				return array.Any() ? array.ElementAt(0) : null;
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}

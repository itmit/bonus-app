﻿using System;
using System.Globalization;
using bonus.app.Core.Models;
using Xamarin.Forms;

namespace bonus.app.Core.Converters
{
	public class BonusValueTypeConverter : IValueConverter
	{
		#region IValueConverter members
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <summary>
		/// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using
		/// <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is BonusValueType bonus)
			{
				switch (bonus)
				{
					case BonusValueType.Percent:
						return " %";
					case BonusValueType.Points:
						return " б.";
				}
			}

			return null;
		}

		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <summary>
		/// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using
		/// <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
		#endregion
	}
}

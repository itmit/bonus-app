using System;
using System.Linq.Expressions;
using MvvmCross.Base;

namespace bonus.app.Core.Validations
{
	public class IsValidConfirmPassword : IValidationRule<string>
	{
		private Func<string> _prop;

		public IsValidConfirmPassword(Func<string> property)
		{
			_prop = property;
		}

		public string ValidationMessage
		{
			get;
			set;
		}

		public bool Check(string value)
		{
			var str = _prop.Invoke();
			if (string.IsNullOrWhiteSpace(str) | string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			return str.Equals(value);
		}
	}
}

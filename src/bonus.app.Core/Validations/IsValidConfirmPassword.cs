using System;

namespace bonus.app.Core.Validations
{
	public class IsValidConfirmPassword : IValidationRule<string>
	{
		#region Data
		#region Fields
		private readonly Func<string> _prop;
		#endregion
		#endregion

		#region .ctor
		public IsValidConfirmPassword(Func<string> property) => _prop = property;
		#endregion

		#region IValidationRule<string> members
		public bool Check(string value)
		{
			var str = _prop.Invoke();
			if (string.IsNullOrWhiteSpace(str) | string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			return str.Equals(value);
		}

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

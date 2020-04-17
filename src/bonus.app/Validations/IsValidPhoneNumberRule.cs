using System.Text.RegularExpressions;

namespace bonus.app.Core.Validations
{
	public class IsValidPhoneNumberRule : IValidationRule<string>
	{
		private const string Pattern = @"^(\+[0-9]\ \([0-9][0-9][0-9]\)\ [0-9][0-9][0-9]\-[0-9][0-9]\-[0-9][0-9])$";

		public string ValidationMessage
		{
			get;
			set;
		}

		public bool Check(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}

			var regex = new Regex(Pattern);
			return regex.IsMatch(value);
		}
	}
}

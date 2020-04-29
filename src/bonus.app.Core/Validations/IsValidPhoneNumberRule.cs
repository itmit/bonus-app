using System.Text.RegularExpressions;

namespace bonus.app.Core.Validations
{
	public class IsValidPhoneNumberRule : IValidationRule<string>
	{
		#region Data
		#region Consts
		private const string Pattern = @"^(\+[0-9]\ \([0-9][0-9][0-9]\)\ [0-9][0-9][0-9]\-[0-9][0-9]\-[0-9][0-9])$";
		#endregion
		#endregion

		#region IValidationRule<string> members
		public bool Check(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}

			var regex = new Regex(Pattern);
			return regex.IsMatch(value);
		}

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

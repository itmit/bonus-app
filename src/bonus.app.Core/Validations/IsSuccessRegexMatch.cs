using System.Text.RegularExpressions;

namespace bonus.app.Core.Validations
{
	public class IsSuccessRegexMatch : IValidationRule<string>
	{
		#region Data
		#region Fields
		private readonly Regex _regex;
		#endregion
		#endregion

		#region .ctor
		public IsSuccessRegexMatch(Regex regex) => _regex = regex;
		#endregion

		#region IValidationRule<string> members
		public bool Check(string value) =>
			_regex.Match(value)
				  .Success;

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

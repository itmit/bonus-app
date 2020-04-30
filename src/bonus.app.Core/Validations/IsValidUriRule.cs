using System;

namespace bonus.app.Core.Validations
{
	public class IsValidUriRule : IValidationRule<string>
	{
		#region IValidationRule<string> members
		public bool Check(string value) => Uri.TryCreate(value, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}
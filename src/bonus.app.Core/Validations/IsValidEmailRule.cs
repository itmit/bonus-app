using System.Net.Mail;

namespace bonus.app.Core.Validations
{
	public class IsValidEmailRule : IValidationRule<string>
	{
		#region IValidationRule<string> members
		public bool Check(string value)
		{
			try
			{
				var address = new MailAddress(value);
				return address.Address == value;
			}
			catch
			{
				return false;
			}
		}

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace bonus.app.Core.Validations
{
	public class IsValidEmailRule : IValidationRule<string>
	{
		public string ValidationMessage
		{
			get;
			set;
		}

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
	}
}

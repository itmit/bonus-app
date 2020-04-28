namespace bonus.app.Core.Validations
{
	public class IsNotNullOrEmptyRule : IValidationRule<string>
	{
		#region IValidationRule<string> members
		public bool Check(string value) => !string.IsNullOrWhiteSpace(value);

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

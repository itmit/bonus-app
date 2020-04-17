namespace bonus.app.Core.Validations
{
	public class IsNotNullOrEmptyRule : IValidationRule<string>
	{
		public string ValidationMessage { get; set; }

		public bool Check(string value) => !string.IsNullOrWhiteSpace(value);
	}
}

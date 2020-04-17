namespace bonus.app.Core.Validations
{
	public class MinLengthRule : IValidationRule<string>
	{
		private readonly int _minLength;

		public MinLengthRule(int minLength)
		{
			_minLength = minLength;
		}

		public string ValidationMessage
		{
			get;
			set;
		}

		public bool Check(string value) =>
			value.Trim()
				 .Length >=
			_minLength;
	}
}

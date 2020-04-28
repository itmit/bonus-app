namespace bonus.app.Core.Validations
{
	public class MinLengthRule : IValidationRule<string>
	{
		#region Data
		#region Fields
		private readonly int _minLength;
		#endregion
		#endregion

		#region .ctor
		public MinLengthRule(int minLength) => _minLength = minLength;
		#endregion

		#region IValidationRule<string> members
		public bool Check(string value) =>
			value?.Trim()
				 .Length >=
			_minLength;

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

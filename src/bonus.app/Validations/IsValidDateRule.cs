using System;

namespace bonus.app.Core.Validations
{
	public class IsValidDateRule : IValidationRule<DateTime?>
	{
		public string ValidationMessage
		{
			get;
			set;
		}

		private readonly DateTime _minDate;
		private readonly DateTime _maxDate;

		public IsValidDateRule(DateTime minDate, DateTime maxDate)
		{
			_minDate = minDate;
			_maxDate = maxDate;
		}

		public bool Check(DateTime? value)
		{
			return value != null && value.Value >= _minDate & value.Value <= _maxDate;
		}
	}
}
 
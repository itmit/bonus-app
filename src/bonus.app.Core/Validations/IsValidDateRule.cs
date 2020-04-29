using System;

namespace bonus.app.Core.Validations
{
	public class IsValidDateRule : IValidationRule<DateTime?>
	{
		#region Data
		#region Fields
		private readonly DateTime _maxDate;

		private readonly DateTime _minDate;
		#endregion
		#endregion

		#region .ctor
		public IsValidDateRule(DateTime minDate, DateTime maxDate)
		{
			_minDate = minDate;
			_maxDate = maxDate;
		}
		#endregion

		#region IValidationRule<DateTime?> members
		public bool Check(DateTime? value) => value != null && (value.Value >= _minDate) & (value.Value <= _maxDate);

		public string ValidationMessage
		{
			get;
			set;
		}
		#endregion
	}
}

using System.Collections.Generic;
using System.Linq;
using MvvmCross.ViewModels;

namespace bonus.app.Core.Validations
{
	public class ValidatableObject<T> : MvxViewModel, IValidity
	{
		#region Data
		#region Fields
		private List<string> _errors;
		private bool _isValid;
		private T _value;
		#endregion
		#endregion

		#region .ctor
		public ValidatableObject()
		{
			_isValid = true;
			_errors = new List<string>();
			Validations = new List<IValidationRule<T>>();
		}
		#endregion

		#region Properties
		public List<string> Errors
		{
			get => _errors;
			set
			{
				_errors = value;
				RaisePropertyChanged(() => Errors);
			}
		}

		public List<IValidationRule<T>> Validations
		{
			get;
		}

		public T Value
		{
			get => _value;
			set
			{
				_value = value;
				RaisePropertyChanged(() => Value);
			}
		}
		#endregion

		#region Public
		public bool Validate()
		{
			Errors.Clear();

			var errors = Validations.Where(v => !v.Check(Value))
									.Select(v => v.ValidationMessage);

			Errors = errors.ToList();
			IsValid = !Errors.Any();

			return IsValid;
		}
		#endregion

		#region IValidity members
		public bool IsValid
		{
			get => _isValid;
			set
			{
				_isValid = value;
				RaisePropertyChanged(() => IsValid);
			}
		}
		#endregion
	}
}

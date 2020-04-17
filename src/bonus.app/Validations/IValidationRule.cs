﻿namespace bonus.app.Core.Validations
{
	public interface IValidationRule<in T>
	{
		string ValidationMessage { get; set; }

		bool Check(T value);
	}
}

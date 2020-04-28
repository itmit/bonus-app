namespace bonus.app.Core.Validations
{
	public interface IValidationRule<in T>
	{
		#region Properties
		string ValidationMessage
		{
			get;
			set;
		}
		#endregion

		#region Overridable
		bool Check(T value);
		#endregion
	}
}

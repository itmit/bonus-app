﻿namespace bonus.app.Core.Models.UserModels
{
	/// <summary>
	/// Представляет token доступа к api.
	/// </summary>
	public class AccessToken
	{
		#region Properties
		/// <summary>
		/// Возвращает или устанавливает тело token.
		/// </summary>
		public string Body
		{
			get;
			set;
		} = string.Empty;

		/// <summary>
		/// Возвращает или устанавливает тип token.
		/// </summary>
		public string Type
		{
			get;
			set;
		} = string.Empty;
		#endregion

		#region Overrided
		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => $"{Type} {Body}";
		#endregion
	}
}

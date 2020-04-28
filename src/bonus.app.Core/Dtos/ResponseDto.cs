using System.Collections.Generic;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class ResponseDto<T>
	{
		#region Properties
		/// <summary>
		/// Возвращает или устанавливает данные возвращаемые от сервиса.
		/// </summary>
		public T Data
		{
			get;
			set;
		}

		public string Error
		{
			get;
			set;
		}

		[JsonProperty("errors")]
		public Dictionary<string, string[]> ErrorDetails
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает возвращаемое сообщение сообщение.
		/// </summary>
		public string Message
		{
			get;
			set;
		} = "";

		/// <summary>
		/// Возвращает или устанавливает статус ответа.
		/// </summary>
		public bool Success
		{
			get;
			set;
		} = false;
		#endregion
	}
}

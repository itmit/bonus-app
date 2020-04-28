using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class CreateServiceDto
	{
		#region Properties
		[JsonProperty("accrual_method")]
		public string AccrualMethod
		{
			get;
			set;
		}

		[JsonProperty("accrual_value")]
		public int AccrualValue
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает guid вида сервиса.
		/// </summary>
		[JsonProperty("uuid")]
		public Guid Uuid
		{
			get;
			set;
		}

		[JsonProperty("writeoff_method")]
		public string WriteOffMethod
		{
			get;
			set;
		}

		[JsonProperty("writeoff_value")]
		public int WriteOffValue
		{
			get;
			set;
		}
		#endregion
	}
}

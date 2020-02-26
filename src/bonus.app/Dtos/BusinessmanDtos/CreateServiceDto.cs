using System;
using bonus.app.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class CreateServiceDto
	{
		/// <summary>
		/// Возвращает или устанавливает guid вида сервиса. 
		/// </summary>
		[JsonProperty("uuid")]
		public Guid Uuid
		{
			get;
			set;
		}

		[JsonProperty("accrual_method")]
		public string AccrualMethod
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

		[JsonProperty("accrual_value")]
		public int AccrualValue
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
	}
}

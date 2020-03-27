using System;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class AccrueAndWriteOffBonusesDto
	{
		[JsonProperty("client_uuid")]
		public Guid ClientUuid
		{
			get;
			set;
		}

		[JsonProperty("service_uuid")]
		public Guid ServiceUuid
		{
			get;
			set;
		}

		[JsonIgnore]
		public double Price
		{
			get;
			set;
		}


		[JsonProperty("price")]
		public int PriceInt
			=> (int)(Price * 100);

		[JsonIgnore]
		public BonusValueType AccrualMethod
		{
			get;
			set;
		}

		[JsonIgnore]
		public BonusValueType WriteOffMethod
		{
			get;
			set;
		}

		[JsonProperty("accrual_method")]
		public string AccrualMethodString =>
			AccrualMethod.ToString()
						 .ToLower();

		[JsonProperty("writeoff_method")]
		public string WriteOffMethodString =>
			WriteOffMethod.ToString()
						  .ToLower();

		[JsonIgnore]
		public double AccrualValue
		{
			get;
			set;
		}

		[JsonIgnore]
		public double WriteOffValue
		{
			get;
			set;
		}

		[JsonProperty("accrual_value")]
		public double AccrualIntValue
			=> (int)(AccrualValue * 100);

		[JsonProperty("writeoff_value")]
		public double WriteOffIntValue
			=> (int)(WriteOffValue * 100);
	}
}

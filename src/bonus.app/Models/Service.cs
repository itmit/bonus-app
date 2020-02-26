using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Service
	{
		public Guid Uuid
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		[JsonProperty("accrual_method")]
		public BonusValueType AccrualMethod
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

		public string AccrualValueString
		{
			get
			{
				switch (AccrualMethod)
				{
					case BonusValueType.Points:
						return (AccrualValue / 100).ToString();
					case BonusValueType.Percent:
						return AccrualValue.ToString();
					default:
						return string.Empty;
				}
			}
		}
	}
}

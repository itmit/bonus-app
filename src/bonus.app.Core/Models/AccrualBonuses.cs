using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class AccrualBonuses
	{
		#region Properties
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

		public User Client
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}

		[JsonProperty("writeoff_method")]
		public BonusValueType WhiteOffMethod
		{
			get;
			set;
		}

		[JsonProperty("writeoff_value")]
		public int WhiteOffValue
		{
			get;
			set;
		}

		public string AccrualValueString => (AccrualValue / 100).ToString();

		public string WhiteOffValueString => (AccrualValue / 100).ToString();
		#endregion
	}
}

using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Service
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

		public User Client
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

		public double AccrualFloatValue
		{
			get => AccrualMethod == BonusValueType.Points ? AccrualValue / 100 : AccrualValue;
			set => AccrualValue = (int) (AccrualMethod == BonusValueType.Points ? value * 100 : value);
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

		public float WhiteOffFloatValue
		{
			get => WhiteOffMethod == BonusValueType.Points ? WhiteOffValue / 100 : WhiteOffValue;
			set => WhiteOffValue = (int) (WhiteOffMethod == BonusValueType.Points ? value * 100 : value);
		}
		#endregion
	}
}

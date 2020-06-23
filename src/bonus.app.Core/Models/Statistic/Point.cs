using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models.Statistic
{
	public class Point
	{
		[JsonIgnore]
		public DateTime Date
		{
			get;
			set;
		}

		[JsonProperty("date")]
		public string DateString
		{
			get => Date.ToString("dd.MM");
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}

				var val = value.Split('.');
				Date = new DateTime(DateTime.Now.Year, int.Parse(val[1]), int.Parse(val[0]));
			}
		}

		[JsonProperty("count")]
		public float Value
		{
			get;
			set;
		}
	}
}

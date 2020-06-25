using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Rate
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Price
		{
			get;
			set;
		}

		public int Stocks
		{
			get;
			set;
		}

		[JsonProperty("stock_count")]
		public int CreatedStocks
		{
			get;
			set;
		}

		public int Duration
		{
			get;
			set;
		}

		[JsonProperty("expires_at")]
		public DateTime? ExpiresAt
		{
			get;
			set;
		}

		public int StocksAvailable => Stocks - CreatedStocks;

		public bool IsActive => ExpiresAt != null && ExpiresAt.Value >= DateTime.Now;
	}
}

using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Stock
	{
		#region Properties
		[JsonProperty("city")]
		public string City
		{
			get;
			set;
		}

		[JsonProperty("country")]
		public string Country
		{
			get;
			set;
		}

		[JsonProperty("description")]
		public string Description
		{
			get;
			set;
		}

		[JsonProperty("photo")]
		public string ImageSource
		{
			get;
			set;
		}

		[JsonProperty("sub_only")]
		public bool IsSubscriberOnly
		{
			get;
			set;
		}

		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("service_uuid")]
		public Guid Service
		{
			get;
			set;
		}

		[JsonProperty("expires_at")]
		public DateTime ShareTime
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}

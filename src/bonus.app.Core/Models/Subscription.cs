using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Subscription
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

		public string Login
		{
			get;
			set;
		}

		[JsonProperty("photo")]
		public string PhotoSource
		{
			get;
			set;
		}

		public int Amount
		{
			get;
			set;
		}

		public List<string> Services
		{
			get;
			set;
		}
	}
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bonus.app.Core.Models.UserModels
{
	public class Subscription
	{
		#region Properties
		public int Amount
		{
			get;
			set;
		}

		public string Login
		{
			get;
			set;
		}

		public string Name
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

		public List<string> Services
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

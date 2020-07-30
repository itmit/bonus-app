using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.CustomerDtos
{
	public class EditCustomerDto
	{
		#region Properties
		[JsonProperty("birthday")]
		public DateTime Birthday
		{
			get;
			set;
		}

		[JsonProperty("car")]
		public string Car
		{
			get;
			set;
		}

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

		[JsonProperty("password")]
		public string Password
		{
			get;
			set;
		}

		[JsonProperty("phone")]
		public string Phone
		{
			get;
			set;
		}

		[JsonProperty("sex")]
		public string Sex
		{
			get;
			set;
		}

		[JsonProperty("uuid")]
		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}

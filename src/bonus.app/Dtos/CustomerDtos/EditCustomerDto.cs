using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bonus.app.Core.Dtos.CustomerDtos
{
	public class EditCustomerDto
	{
		[JsonProperty("uuid")]
		public Guid Uuid
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

		[JsonProperty("city")]
		public string City
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

		[JsonProperty("birthday")]
		public string Birthday
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

		[JsonProperty("phone")]
		public string Phone
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

		[JsonProperty("address")]
		public string Address
		{
			get;
			set;
		}
	}
}

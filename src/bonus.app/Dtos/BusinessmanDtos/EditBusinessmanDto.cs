using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class EditBusinessmanDto
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

		[JsonProperty("address")]
		public string Address
		{
			get;
			set;
		}

		[JsonProperty("worktime")]
		public string WorkTime
		{
			get;
			set;
		}

		[JsonProperty("contact")]
		public string Contact
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

		[JsonProperty("description")]
		public string Description
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
	}
}

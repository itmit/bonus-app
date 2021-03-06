﻿using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos.BusinessmanDtos
{
	public class EditBusinessmanDto
	{
		#region Properties
		[JsonProperty("address")]
		public string Address
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

		[JsonProperty("contact")]
		public string Contact
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

		[JsonProperty("email")]
		public string Email
		{
			get;
			set;
		}

		public string FacebookLink
		{
			get;
			set;
		}

		public string InstagramLink
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

		public string Odnoklassniki
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

		[JsonProperty("uuid")]
		public Guid Uuid
		{
			get;
			set;
		}

		public string VkLink
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
		#endregion
	}
}

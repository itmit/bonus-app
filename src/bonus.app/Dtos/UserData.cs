using System;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class UserData
	{
		[JsonProperty("type")]
		public UserRole Role
		{
			get;
			set;
		}

		public Guid Uuid
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

		[JsonProperty("phone")]
		public string Phone
		{
			get;
			set;
		}

		public string Photo
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

		public string Login
		{
			get;
			set;
		}
	}
}

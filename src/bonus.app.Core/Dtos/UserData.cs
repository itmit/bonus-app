﻿using System;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class UserData
	{
		#region Properties
		[JsonProperty("email")]
		public string Email
		{
			get;
			set;
		}

		public string Login
		{
			get;
			set;
		}

		public int Id
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
		#endregion
	}
}

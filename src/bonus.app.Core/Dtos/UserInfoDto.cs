using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class UserInfoDto
	{
		#region Properties
		public string Address
		{
			get;
			set;
		}

		public DateTime? Birthday
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public string Contact
		{
			get;
			set;
		}

		public string Country
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		[JsonProperty("photo")]
		public string Photo
		{
			get;
			set;
		}

		[JsonProperty("work_time")]
		public string WorkTime
		{
			get;
			set;
		}
		#endregion
	}
}

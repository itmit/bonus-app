using System.Collections.Generic;
using Newtonsoft.Json;

namespace bonus.app.Core.Dto
{
	public class AuthErrorDto
	{
		[JsonProperty("email")]
		public List<string> Email
		{
			get;
			set;
		}

		[JsonProperty("password")]
		public List<string> Password
		{
			get;
			set;
		}
	}
}

using Newtonsoft.Json;

namespace bonus.app.Core.Dto
{
	public class AuthDto
	{
		[JsonProperty("email")]
		public string Email
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

		[JsonProperty("device_token")]
		public string DeviceToken
		{
			get;
			set;
		}
	}
}

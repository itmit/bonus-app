using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class AuthDto
	{
		#region Properties
		[JsonProperty("device_token")]
		public string DeviceToken
		{
			get;
			set;
		}

		[JsonProperty("login")]
		public string Login
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
		#endregion
	}
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace bonus.app.Core.Dtos
{
	public class RegisterDto
	{
		[JsonProperty("email")]
		public string Email
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

		[JsonProperty("type")]
		public string Type
		{
			get;
			set;
		}

		[JsonProperty("password_confirmation")]
		public string PasswordConfirm
		{
			get;
			set;
		}
	}
}

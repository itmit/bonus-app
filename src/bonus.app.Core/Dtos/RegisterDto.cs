using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class RegisterDto
	{
		#region Properties
		[JsonProperty("email")]
		public string Email
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

		[JsonProperty("name")]
		public string Name
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

		[JsonProperty("password_confirmation")]
		public string PasswordConfirm
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
		#endregion
	}
}

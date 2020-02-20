using System;
using bonus.app.Core.Models;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class UserDto
	{
		[JsonProperty("uuid")]
		public Guid Guid
		{
			get;
			set;
		}

		public UserInfoDto Client
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает имя, фамилию и отчество пользователя.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает почтовый адрес пользователя.
		/// </summary>
		public string Email
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

		/// <summary>
		/// Возвращает или устанавливает токен для авторизации.
		/// </summary>
		[JsonProperty("access_token")]
		public string Body
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает строковое представление даты, до которой действует токен.
		/// </summary>
		[JsonProperty("expires_at")]
		public string TokenExpiresAt
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает тип токена.
		/// </summary>
		[JsonProperty("token_type")]
		public string Type
		{
			get;
			set;
		}
	}
}

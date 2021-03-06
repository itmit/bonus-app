﻿using System;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using Newtonsoft.Json;

namespace bonus.app.Core.Dtos
{
	public class UserDto
	{
		#region Properties
		[JsonProperty("balance")]
		public int Balance
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

		public UserData Client
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		[JsonProperty("client_info")]
		public UserInfoDto ClientInfo
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

		/// <summary>
		/// Возвращает или устанавливает имя, фамилию и отчество пользователя.
		/// </summary>
		public string Name
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

		[JsonProperty("uuid")]
		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}

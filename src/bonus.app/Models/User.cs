using System;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	/// <summary>
	/// Представляет пользователя.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Возвращает или устанавливает id пользователя.
		/// </summary>
		public Guid Uuid
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает логин пользователя.
		/// </summary>
		public string Login
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает роль пользователя.
		/// </summary>
		public UserRole Role
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public string Country
		{
			get;
			set;
		}

		public string Address
		{
			get;
			set;
		}

		public string WorkTime
		{
			get;
			set;
		}

		public string Contact
		{
			get;
			set;
		}

		public DateTime Birthday
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string PhotoSource
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает token доступа пользователя.
		/// </summary>
		public AccessToken AccessToken
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public int PinCode
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Balance
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}
	}
}

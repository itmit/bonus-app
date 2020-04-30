﻿using System;

namespace bonus.app.Core.Models
{
	/// <summary>
	/// Представляет пользователя.
	/// </summary>
	public class User
	{
		#region Properties
		/// <summary>
		/// Возвращает или устанавливает token доступа пользователя.
		/// </summary>
		public AccessToken AccessToken
		{
			get;
			set;
		}

		public string Address
		{
			get;
			set;
		}

		public int Balance
		{
			get;
			set;
		}

		public DateTime Birthday
		{
			get;
			set;
		}

		public string Car
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

		public string Email
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

		public string Name
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public string PhotoSource
		{
			get;
			set;
		}

		public int PinCode
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

		public string Sex
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает id пользователя.
		/// </summary>
		public Guid Uuid
		{
			get;
			set;
		}

		public string WorkTime
		{
			get;
			set;
		}
		#endregion
	}
}
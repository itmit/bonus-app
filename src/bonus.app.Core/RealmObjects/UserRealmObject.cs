﻿using System;
using battery.app.Core.RealmObjects;
using Realms;

namespace bonus.app.Core.RealmObjects
{
	public class UserRealmObject : RealmObject
	{
		#region Properties
		public AccessTokenRealmObject AccessToken
		{
			get;
			set;
		}

		public int Id
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

		public DateTimeOffset Birthday
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

		public string ClassmatesLink
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

		public string FacebookLink
		{
			get;
			set;
		}

		[PrimaryKey]
		public string Guid
		{
			get;
			set;
		} = System.Guid.NewGuid()
				  .ToString();

		public string InstagramLink
		{
			get;
			set;
		}

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

		public string Role
		{
			get;
			set;
		}

		public string VkLink
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

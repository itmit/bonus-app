﻿using System;
using System.Threading.Tasks;
using bonus.app.Core.Models.UserModels;

namespace bonus.app.Core.Services.Interfaces
{
	public interface ICustomerService
	{
		#region Overridable
		Task<User> GetCustomerByLogin(string login);

		Task<User> GetCustomerByUuid(Guid uuid);
		#endregion
	}
}

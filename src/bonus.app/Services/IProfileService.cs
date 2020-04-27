﻿using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Dtos.BusinessmanDtos;
using bonus.app.Core.Dtos.CustomerDtos;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IProfileService
	{
		Task<User> Edit(EditBusinessmanDto arguments, string imagePath);

		Task<User> Edit(EditCustomerDto arguments, string imagePath);

		string Error
		{
			get;
		}

		Dictionary<string, string[]> ErrorDetails
		{
			get;
		}
	}
}

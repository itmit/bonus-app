using System;
using System.Collections.Generic;
using bonus.app.Core.Models;

namespace bonus.app.Core.Repositories
{
	public interface IUserRepository
	{
		void Add(User user);

		IEnumerable<User> GetAll();

		void Remove(User user);

		User Find(Guid uuid);

		void Update(User user);
	}
}

using System;
using System.Collections.Generic;
using bonus.app.Core.Models;

namespace bonus.app.Core.Repositories
{
	public interface IUserRepository
	{
		void Add(User user);

		IEnumerable<User> GetAll();

		bool Remove(User user);

		void RemoveAll();

		User Find(Guid uuid);

		bool Update(User user);
	}
}

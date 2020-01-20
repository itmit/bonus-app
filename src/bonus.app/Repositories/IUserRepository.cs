using System.Collections.Generic;
using bonus.app.Core.Models;

namespace bonus.app.Core.Repositories
{
	public interface IUserRepository
	{
		void Add(User user);

		IEnumerable<User> GetUsers();

		void Remove(User user);

		void Update(User user);
	}
}

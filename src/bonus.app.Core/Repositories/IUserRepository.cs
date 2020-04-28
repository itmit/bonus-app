using System;
using System.Collections.Generic;
using bonus.app.Core.Models;

namespace bonus.app.Core.Repositories
{
	public interface IUserRepository
	{
		#region Overridable
		void Add(User user);

		User Find(Guid uuid);

		IEnumerable<User> GetAll();

		bool Remove(User user);

		void RemoveAll();

		bool Update(User user);
		#endregion
	}
}

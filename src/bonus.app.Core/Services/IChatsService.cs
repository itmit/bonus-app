using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;

namespace bonus.app.Core.Services
{
	public interface IChatsService
	{
		Task<List<Dialog>> GetDialogs();

		Task<Dialog> CreateDialog(User user);

		Task<Message> SendMessage(int dialogId, string content, string file);

		Task<List<Message>> GetMessages(int dialogId);

		List<Dialog> SavedDialogs
		{
			get;
		}
	}
}

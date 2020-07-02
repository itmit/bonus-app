using System.Collections.Generic;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.ChatModels;
using bonus.app.Core.Models.UserModels;

namespace bonus.app.Core.Services
{
	public interface IChatsService
	{
		#region Properties
		List<Dialog> SavedDialogs
		{
			get;
		}
		#endregion

		#region Overridable
		Task<Dialog> CreateDialog(User user);

		Task<List<Dialog>> GetDialogs();

		Task<List<Message>> GetMessages(int dialogId);

		Task<Message> SendMessage(int dialogId, string content, string file);
		#endregion
	}
}

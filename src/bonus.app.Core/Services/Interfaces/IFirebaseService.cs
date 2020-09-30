using System.Threading.Tasks;

namespace bonus.app.Core.Services.Interfaces
{
	public interface IFirebaseService
	{
		Task<string> CreateToken(string senderId, string scope = "");

		void DeleteInstance(string senderId, string scope = "");

		void SubscribeToAllTopic();

		void UnsubscribeToAllTopic();
	}
}

using System.Threading.Tasks;
using Android.Gms.Extensions;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using Firebase.Iid;
using Firebase.Messaging;

namespace bonus.app.Droid.Services
{
	public class AndroidFirebaseService : IFirebaseService
	{
		public Task<string> CreateToken(string senderId, string scope = "")
		{
			if (string.IsNullOrEmpty(scope))
			{
				scope = FirebaseMessaging.InstanceIdScope;
			}

			return Task.Run(() => FirebaseInstanceId.Instance.GetToken(senderId, scope));
		}

		public void DeleteInstance(string senderId, string scope = "")
		{
			if (string.IsNullOrEmpty(scope))
			{
				scope = FirebaseMessaging.InstanceIdScope;
			}

			Task.Run(() =>
			{
				FirebaseInstanceId.Instance.DeleteToken(senderId, scope);

			});
		}

		private const string AllTopicName = "all";

		public async void SubscribeToAllTopic()
		{
			await FirebaseMessaging.Instance.SubscribeToTopic(AllTopicName);
		}

		public async void UnsubscribeToAllTopic()
		{
			await FirebaseMessaging.Instance.UnsubscribeFromTopic(AllTopicName);
		}
	}
}

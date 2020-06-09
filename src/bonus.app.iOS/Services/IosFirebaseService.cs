using System;
using System.Diagnostics;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using Firebase.CloudMessaging;
using Foundation;
using UIKit;

namespace bonus.app.iOS.Services
{
	public class IosFirebaseService : IFirebaseService
	{
		private static void Completion(string fcmToken, NSError error)
		{
			Console.WriteLine($"FCM Token: {fcmToken}");
			Console.WriteLine(error);
		}

		public Task<string> CreateToken(string senderId, string scope = "")
		{
			Messaging.SharedInstance.RetrieveFcmToken(senderId, Completion);
			Debug.WriteLine(Messaging.SharedInstance.FcmToken);
			return Task.FromResult(Messaging.SharedInstance.FcmToken);
		}

		public void DeleteInstance(string senderId, string scope = "")
		{
			Messaging.SharedInstance.DeleteFcmToken(senderId, Completion);
		}

		private static void Completion(NSError error)
		{
			Console.WriteLine(error);
		}

		public void SubscribeToAllTopic()
		{
			Messaging.SharedInstance.Subscribe(AllTopicName);

		}


		private const string AllTopicName = "all";

		public void UnsubscribeToAllTopic()
		{
			Messaging.SharedInstance.Unsubscribe(AllTopicName);

		}
	}
}

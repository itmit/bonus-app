using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using Firebase.Messaging;
using MvvmCross;

namespace bonus.app.Droid.Services
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class BonusFirebaseMessagingService : FirebaseMessagingService
	{
		private const string Tag = "BonusFirebaseMessagingService";

		public override void OnMessageReceived(RemoteMessage message)
		{
			Log.Debug(Tag, "From: " + message.From);

			if (message.Data.ContainsKey("dialog_id"))
			{
				(Mvx.IoCProvider.Resolve<IMessagingService>() as MessagingService)?.ReceiveMessage(message.GetNotification());
			}

			if (message.GetNotification() != null)
			{
				//These is how most messages will be received
				Log.Debug(Tag, "Notification Message Body: " + message.GetNotification().Body);
				var notification = message.GetNotification();
				SendNotification(notification.Body, $"Новое сообщение от: {notification.Title}");
			}
			else
			{
				//Only used for debugging payloads sent from the Azure portal
				SendNotification(message.Data.Values.First(), "Внимание");
			}
		}
		public override void OnNewToken(string token)
		{
			Log.Debug(Tag, "FCM token: " + token);
		}

		private void SendNotification(string messageBody, string title)
		{
			var intent = new Intent(this, typeof(FormsActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

			var notificationBuilder = new NotificationCompat.Builder(this, FormsActivity.ChannelId);

			notificationBuilder.SetContentTitle(title)
							   .SetSmallIcon(Resource.Drawable.pic_dragon)
							   .SetContentText(messageBody)
							   .SetAutoCancel(true)
							   .SetShowWhen(false)
							   .SetContentIntent(pendingIntent);

			var notificationManager = NotificationManager.FromContext(this);

			notificationManager.Notify(0, notificationBuilder.Build());
		}
	}
}

using System;
using System.Net;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using Foundation;
using Newtonsoft.Json.Linq;
using UIKit;
using Xamarin.Auth;
/*
using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using VKontakte.API.Models;
using VKontakte.Core;
using VKontakte.Views;
*/
using Xamarin.Forms;

namespace bonus.app.iOS.Services
{
	public class IosVkService : NSObject, IVkService//, IVKSdkDelegate, IVKSdkUIDelegate
    {
		private TaskCompletionSource<LoginResult> _completionSource;

		public Task<LoginResult> Login()
		{
			_completionSource = new TaskCompletionSource<LoginResult>();

			var auth = new OAuth2Authenticator("7511393",
											   "email",
											   new Uri("https://oauth.vk.com/authorize"),
											   new Uri("https://oauth.vk.com/blank.html"));
			

			auth.Completed += AuthOnCompleted;
			auth.Error += AuthOnError;

			auth.ClearCookiesBeforeLogin = true;
			auth.Title = "Vk";

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(auth.GetUI(), true, null);

			return _completionSource.Task;
		}

		private void AuthOnError(object sender, AuthenticatorErrorEventArgs e)
		{
		}

		private async void AuthOnCompleted(object sender, AuthenticatorCompletedEventArgs authCompletedArgs)
		{
			UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);

			if (!authCompletedArgs.IsAuthenticated || authCompletedArgs.Account == null)
			{
				SetResult(new LoginResult { LoginState = LoginState.Canceled });
			}
			else
			{
				var expInString = authCompletedArgs.Account.Properties.ContainsKey("expires_in")
									  ? authCompletedArgs.Account.Properties["expires_in"]
									  : null;

				var expireAt = DateTimeOffset.Now.AddSeconds(Convert.ToInt32(expInString));

				SetResult(new LoginResult
				{
					Token = authCompletedArgs.Account.Properties.ContainsKey("access_token")
								? authCompletedArgs.Account.Properties["access_token"]
								: null,
					ExpireAt = expireAt,
					LoginState = LoginState.Success,
					UserId = authCompletedArgs.Account.Properties.ContainsKey("user_id")
								 ? authCompletedArgs.Account.Properties["user_id"]
								 : null,
					Email = authCompletedArgs.Account.Properties.ContainsKey("email")
								? authCompletedArgs.Account.Properties["email"]
								: null
				});
			}
		}

		private void SetResult(LoginResult result)
		{
			_completionSource?.TrySetResult(result);
			_completionSource = null;
		}


		private async Task GetUserProfile(Account account, string token, DateTimeOffset expireAt)
		{
			var result = new LoginResult
			{
				Token = token,
				ExpireAt = expireAt
			};

			var request = new OAuth2Request("GET", new Uri($"https://api.vk.com/method/users.get?fields=email&access_token={token}&v=5.120"),
											null, account);
			var response = await request.GetResponseAsync();
			if (response != null && response.StatusCode == HttpStatusCode.OK)
			{
				var userJson = response.GetResponseText();

				var jObject = JObject.Parse(userJson);

				result.LoginState = LoginState.Success;
				result.Email = jObject["email"].ToString();

				var userId = jObject["id"].ToString();
				result.UserId = userId;
			}
			else
			{
				result.LoginState = LoginState.Failed;
				result.ErrorString = $"Error: Responce={response}, StatusCode = {response?.StatusCode}";
			}

			SetResult(result);
		}

		public void Logout()
		{
			_completionSource = null;
		}
	}
}

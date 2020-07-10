using System;
using System.Net;
using System.Threading.Tasks;
using bonus.app.Core.Services;
using CoreGraphics;
using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms.Platform.iOS;

namespace bonus.app.iOS.Services
{
	public class IosFacebookService : IFacebookService
	{
		private TaskCompletionSource<LoginResult> _completionSource;

        public Task<LoginResult> Login()
        {
			_completionSource = new TaskCompletionSource<LoginResult>();

			var auth = new OAuth2Authenticator("3865467926858251",
											   "email",
											   new Uri("https://www.facebook.com/v7.0/dialog/oauth"),
											   new Uri("https://www.facebook.com/connect/login_success.html"));

			auth.Completed += AuthOnCompleted;
			auth.Error += AuthOnError;

			auth.ClearCookiesBeforeLogin = true;
			auth.Title = "Facebook";

			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(auth.GetUI(), true, null);

			return _completionSource.Task;
		}

		private void AuthOnError(object sender, AuthenticatorErrorEventArgs e)
		{
			throw new NotImplementedException();
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
				var token = authCompletedArgs.Account.Properties.ContainsKey("access_token")
								? authCompletedArgs.Account.Properties["access_token"]
								: null;
				var expInString = authCompletedArgs.Account.Properties.ContainsKey("expires_in")
									  ? authCompletedArgs.Account.Properties["expires_in"]
									  : null;

				var expireIn = Convert.ToInt32(expInString);
				var expireAt = DateTimeOffset.Now.AddSeconds(expireIn);

				await GetUserProfile(authCompletedArgs.Account, token, expireAt);
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

			var request = new OAuth2Request("GET", new Uri($"https://graph.facebook.com/me?fields=email&access_token={token}"),
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

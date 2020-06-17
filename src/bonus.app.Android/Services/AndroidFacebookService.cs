using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using bonus.app.Core.Services;
using MvvmCross;
using Org.Json;
using Plugin.CurrentActivity;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using LoginResult = bonus.app.Core.Services.LoginResult;

namespace bonus.app.Droid.Services
{
	internal class AndroidFacebookService : Java.Lang.Object, IFacebookService, GraphRequest.IGraphJSONObjectCallback, GraphRequest.ICallback, IFacebookCallback
	{
		public static AndroidFacebookService Instance => Mvx.IoCProvider.Resolve<IFacebookService>() as AndroidFacebookService;

		private readonly ICallbackManager _callbackManager = CallbackManagerFactory.Create();
		private readonly string[] _permissions = { @"email", @"user_gender", @"user_hometown" };

		private LoginResult _loginResult;
		private TaskCompletionSource<LoginResult> _completionSource;

		public AndroidFacebookService()
		{
			LoginManager.Instance.RegisterCallback(_callbackManager, this);
		}

		public Task<LoginResult> Login()
		{
			_completionSource = new TaskCompletionSource<LoginResult>();
			
			LoginManager.Instance.LogInWithReadPermissions(CrossCurrentActivity.Current.Activity, _permissions);
			return _completionSource.Task;
		}

		public void Logout()
		{
			LoginManager.Instance.LogOut();
		}

		public void OnActivityResult(int requestCode, int resultCode, Intent data)
		{
			_callbackManager?.OnActivityResult(requestCode, resultCode, data);
		}

		public void OnCompleted(JSONObject data, GraphResponse response)
		{
			OnCompleted(response);
		}

		public void OnCompleted(GraphResponse response)
		{
			if (response?.JSONObject == null)
			{
				_completionSource?.TrySetResult(new LoginResult { LoginState = LoginState.Canceled });
			}
			else
			{
				_loginResult = new LoginResult
				{
					FirstName = Profile.CurrentProfile.FirstName,
					LastName = Profile.CurrentProfile.LastName,
					Email = response.JSONObject.Has("email") ? response.JSONObject.GetString("email") : string.Empty,
					ImageUrl = response.JSONObject.GetJSONObject("picture")?.GetJSONObject("data")?.GetString("url"),
					Token = AccessToken.CurrentAccessToken.Token,
					UserId = AccessToken.CurrentAccessToken.UserId,
					ExpireAt = FromJavaDateTime(AccessToken.CurrentAccessToken?.Expires?.Time),
					LoginState = LoginState.Success
				};

				_completionSource?.TrySetResult(_loginResult);
			}
		}

		public void OnCancel()
		{
			_completionSource?.TrySetResult(new LoginResult { LoginState = LoginState.Canceled });
		}

		public void OnError(FacebookException exception)
		{
			_completionSource?.TrySetResult(new LoginResult
			{
				LoginState = LoginState.Failed,
				ErrorString = exception?.Message
			});
		}

		public void OnSuccess(Java.Lang.Object result)
		{
			var facebookLoginResult = result.JavaCast<Xamarin.Facebook.Login.LoginResult>();
			if (facebookLoginResult == null)
			{
				return;
			}

			var parameters = new Bundle();
			parameters.PutString("fields", "id,email,picture.type(large)");
			var request = GraphRequest.NewMeRequest(facebookLoginResult.AccessToken, this);
			request.Parameters = parameters;
			request.ExecuteAsync();
		}

		private static DateTimeOffset FromJavaDateTime(long? longTimeMillis)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return longTimeMillis != null ? epoch.AddMilliseconds(longTimeMillis.Value) : DateTimeOffset.MinValue;
		}
	}
}

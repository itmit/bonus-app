using System.Threading.Tasks;
using Android.App;
using bonus.app.Core.Services;
using MvvmCross;
using Plugin.CurrentActivity;
using VKontakte;
using VKontakte.API;
using Xamarin.Forms;

namespace bonus.app.Droid.Services
{
	internal class AndroidVkService : Java.Lang.Object, IVkService
	{
		public static AndroidVkService Instance => Mvx.IoCProvider.Resolve<IVkService>() as AndroidVkService;

		private readonly string[] _permissions =
		{
			VKScope.Email,
			VKScope.Offline
		};

		private TaskCompletionSource<LoginResult> _completionSource;
		private LoginResult _loginResult;

		public Task<LoginResult> Login()
		{
			_completionSource = new TaskCompletionSource<LoginResult>();
			VKSdk.Login(CrossCurrentActivity.Current.Activity, _permissions);
			return _completionSource.Task;
		}

		public void Logout()
		{
			_loginResult = null;
			_completionSource = null;
			VKSdk.Logout();
		}

		public void SetUserToken(VKAccessToken token)
		{
			_loginResult = new LoginResult
			{
				Email = token.Email,
				Token = token.AccessToken,
				UserId = token.UserId,
				ExpireAt = Utils.FromMsDateTime(token.ExpiresIn)
			};

			Task.Run(GetUserInfo);
		}

		private async Task GetUserInfo()
		{
			var request = VKApi.Users.Get(VKParameters.From(VKApiConst.Fields, @"photo_400_orig,"));
			var response = await request.ExecuteAsync();
			var jsonArray = response.Json.OptJSONArray(@"response");
			var account = jsonArray?.GetJSONObject(0);
			if (account != null && _loginResult != null)
			{
				_loginResult.FirstName = account.OptString(@"first_name");
				_loginResult.LastName = account.OptString(@"last_name");
				_loginResult.ImageUrl = account.OptString(@"photo_400_orig");
				_loginResult.LoginState = LoginState.Success;
				SetResult(_loginResult);
			}
			else
			{
				SetErrorResult(@"Unable to complete the request of user info");
			}
		}

		public void SetErrorResult(string errorMessage)
		{
			SetResult(new LoginResult
			{
				LoginState = LoginState.Failed,
				ErrorString = errorMessage
			});
		}

		public void SetCanceledResult()
		{
			SetResult(new LoginResult
			{
				LoginState = LoginState.Canceled
			});
		}

		private void SetResult(LoginResult result)
		{
			_completionSource?.TrySetResult(result);
			_loginResult = null;
			_completionSource = null;
		}
	}
}

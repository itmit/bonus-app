using System.Threading.Tasks;
using bonus.app.Core.Services;
using CoreGraphics;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace bonus.app.iOS.Services
{
	public class IosFacebookService : IFacebookService

	{
		private readonly LoginManager _loginManager = new LoginManager();
		private readonly string[] _permissions = { @"public_profile", @"email", @"user_about_me" };

		private LoginResult _loginResult;
		private TaskCompletionSource<LoginResult> _completionSource;

        public Task<LoginResult> Login()
        {
            _completionSource = new TaskCompletionSource<LoginResult>();
            _loginManager.LogIn(_permissions, GetCurrentViewController(), LoginManagerLoginHandler);
            return _completionSource.Task;
        }

        public void Logout()
        {
            _loginManager.LogOut();
        }

		private void LoginManagerLoginHandler(LoginManagerLoginResult result, NSError error)
        {
            if (result.IsCancelled)
			{
				_completionSource.TrySetResult(new LoginResult { LoginState = LoginState.Canceled });
			}
			else if (error != null)
			{
				_completionSource.TrySetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = error.LocalizedDescription });
			}
			else
            {
                _loginResult = new LoginResult
                {
                    Token = result.Token.TokenString,
                    UserId = result.Token.UserId,
                    ExpireAt = result.Token.ExpirationDate.ToDateTime()
                };

                var request = new GraphRequest(@"me", new NSDictionary(@"fields", @"email"));
                request.Start(GetEmailRequestHandler);
            }
        }

		private void GetEmailRequestHandler(GraphRequestConnection connection, NSObject result, NSError error)
        {
            if (error != null)
                _completionSource.TrySetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = error.LocalizedDescription });
            else
            {
                _loginResult.FirstName = Profile.CurrentProfile.FirstName;
                _loginResult.LastName = Profile.CurrentProfile.LastName;
                _loginResult.ImageUrl = Profile.CurrentProfile.ImageUrl(ProfilePictureMode.Square, new CGSize()).ToString();

                var dict = result as NSDictionary;
                var emailKey = new NSString(@"email");
                if (dict != null && dict.ContainsKey(emailKey))
                    _loginResult.Email = dict[emailKey]?.ToString();

                _loginResult.LoginState = LoginState.Success;
                _completionSource.TrySetResult(_loginResult);
            }
        }

		private static UIViewController GetCurrentViewController()
        {
            var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (viewController.PresentedViewController != null)
			{
				viewController = viewController.PresentedViewController;
			}

			return viewController;
        }
    }
}

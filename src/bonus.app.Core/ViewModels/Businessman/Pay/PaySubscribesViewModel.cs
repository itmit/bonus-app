using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Pay
{
	public class PaySubscribesViewModel : MvxViewModel
	{
		#region .ctor
		public PaySubscribesViewModel(IRateService rateService, IMvxNavigationService navigationService, IMvxFormsViewPresenter platformPresenter)
		{
			_rateService = rateService;
			_navigationService = navigationService;
			_platformPresenter = platformPresenter;
		}
		#endregion
		

		private IRateService _rateService;
		private Rate _myRate;
		private IMvxNavigationService _navigationService;
		private MvxObservableCollection<Rate> _rates;
		private MvxCommand<Rate> _changeRateCommand;

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Rates = new MvxObservableCollection<Rate>(await _rateService.GetRates());
				MyRate = await _rateService.GetMyRate();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private readonly IMvxFormsViewPresenter _platformPresenter;

		private Application _formsApplication;

		private Application FormsApplication => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);

		public MvxObservableCollection<Rate> Rates
		{
			get => _rates;
			set => SetProperty(ref _rates, value);
		}

		public Rate MyRate
		{
			get => _myRate;
			private set => SetProperty(ref _myRate, value);
		}

		private MvxCommand _paymentCommand;
		public MvxCommand PaymentCommand
		{
			get
			{
				_paymentCommand = _paymentCommand ??
								  new MvxCommand(async () =>
								  {
									  var webView = new WebView
									  {
										  Source = new UrlWebViewSource
										  {
											  Url = await _rateService.GetHtmlPayment()
										  }
									  };
									  webView.Navigated += async (sender, args) =>
									  {
										  if (args.Result == WebNavigationResult.Success && args.Url.StartsWith(_rateService.PaySuccessUrl))
										  {
											  await FormsApplication.MainPage.Navigation.PopModalAsync();
											  await FormsApplication.MainPage.DisplayAlert("Внимание", "Оплата прошла успешно", "Ок");
											  await Initialize();
										  }

										  if (args.Result == WebNavigationResult.Success && args.Url.StartsWith(_rateService.PayErrorUrl))
										  {
											  await FormsApplication.MainPage.Navigation.PopModalAsync();
											  await FormsApplication.MainPage.DisplayAlert("Внимание", "Платеж не прошел", "Ок");
										  }
									  };
									  await FormsApplication.MainPage.Navigation.PushModalAsync(new ContentPage
									  {
										  Content = webView
									  });
								  });
				return _paymentCommand;
			}
		}

		public MvxCommand<Rate> ChangeRateCommand
		{
			get
			{
				_changeRateCommand = _changeRateCommand ?? new MvxCommand<Rate>((rate) =>
				{
					_navigationService.Navigate<TariffViewModel>();
				});
				return _changeRateCommand;
			}
		}
	}
}

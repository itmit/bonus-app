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
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Businessman.Pay
{
	public class PaySubscribesViewModel : MvxViewModel
	{
		#region .ctor
		public PaySubscribesViewModel(IRateService rateService, IMvxNavigationService navigationService)
		{
			_rateService = rateService;
			_navigationService = navigationService;
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
											  await Application.Current.MainPage.Navigation.PopModalAsync();
											  await MaterialDialog.Instance.AlertAsync("Оплата прошла успешно", "Внимание", "Ок");
											  await Initialize();
										  }

										  if (args.Result != WebNavigationResult.Success || !args.Url.StartsWith(_rateService.PayErrorUrl))
										  {
											  return;
										  }

										  await Application.Current.MainPage.Navigation.PopModalAsync();
										  await MaterialDialog.Instance.AlertAsync("Платеж не прошел", "Внимание", "Ок");
									  };
									  await Application.Current.MainPage.Navigation.PushModalAsync(new ContentPage
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

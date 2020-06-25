using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Pay;
using Microsoft.AppCenter;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace bonus.app.Core.ViewModels.Businessman
{
	public class TariffViewModel : MvxViewModel<Rate>
	{
		#region Data
		#region Fields
		private IMvxNavigationService _navigationService;
		private readonly IRateService _rateService;
		private Rate _myRate;
		private MvxObservableCollection<Rate> _rates;
		private MvxCommand _changeRateCommand;
		private Rate _selectedRate;
		#endregion
		#endregion

		#region .ctor
		public TariffViewModel(IMvxNavigationService navigationService, IRateService rateService, IMvxFormsViewPresenter platformPresenter)
		{
			_rateService = rateService;
			_platformPresenter = platformPresenter;
			_navigationService = navigationService;
		}
		#endregion

		public override void Prepare(Rate parameter)
		{
			SelectedRate = parameter;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Rates = new MvxObservableCollection<Rate>(await _rateService.GetRates());
				_myRate = SelectedRate = await _rateService.GetMyRate();
				if (SelectedRate == null)
				{
					SelectedRate = _myRate;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			await RaisePropertyChanged(() => IsVisiblePayment);
			await RaisePropertyChanged(() => IsEnabledChangeRate);
		}

		public bool IsVisiblePayment =>  _myRate != null && SelectedRate != null  && _myRate.Id == SelectedRate.Id && !_myRate.IsActive;

		public MvxObservableCollection<Rate> Rates
		{
			get => _rates;
			private set => SetProperty(ref _rates, value);
		}

		public Rate SelectedRate
		{
			get => _selectedRate;
			set
			{
				SetProperty(ref _selectedRate, value);
				RaisePropertyChanged(() => IsVisiblePayment);
				RaisePropertyChanged(() => IsEnabledChangeRate);
			}
		}

		public bool IsEnabledChangeRate => _myRate == null || (SelectedRate != null && _myRate != null && SelectedRate.Id != _myRate.Id);

		private readonly IMvxFormsViewPresenter _platformPresenter;

		private Application _formsApplication;
		private MvxCommand _paymentCommand;

		private Application FormsApplication => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);

		public MvxCommand PaymentCommand
		{
			get
			{
				_paymentCommand = _paymentCommand ?? new MvxCommand(async () =>
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

		public MvxCommand ChangeRateCommand
		{
			get
			{
				_changeRateCommand = _changeRateCommand ?? new MvxCommand(async () =>
				{
					try
					{
						if (SelectedRate == null)
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								FormsApplication.MainPage.DisplayAlert("Внимание", "Выберите тариф.", "Ок");
							});
							return;
						}

						if (_myRate != null && SelectedRate.Id == _myRate.Id)
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								FormsApplication.MainPage.DisplayAlert("Внимание", "Выбранный тариф уже подключен.", "Ок");
							});
						}

						if (!await FormsApplication.MainPage.DisplayAlert("Внимание", "Вы уверены, что хотите сменить тариф?", "Да", "Нет"))
						{
							return;
						}

						var rate = await _rateService.ChangeRate(SelectedRate);

						if (rate)
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								FormsApplication.MainPage.DisplayAlert("Внимание", "Вы успешно сменили тариф.", "Ок");
							});
							_myRate = await _rateService.GetMyRate();
							SelectedRate = _myRate;
						}
						else
						{
							Device.BeginInvokeOnMainThread(() =>
							{
								FormsApplication.MainPage.DisplayAlert("Внимание", "Не удалось сменить тариф.", "Ок");
							});
						}
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
				});
				return _changeRateCommand;
			}
		}
	}
}

using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using bonus.app.Core.ViewModels.Businessman.Pay;
using Microsoft.AppCenter;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
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
		public TariffViewModel(IMvxNavigationService navigationService, IRateService rateService)
		{
			_rateService = rateService;
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

		private MvxCommand _paymentCommand;

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
							await MaterialDialog.Instance.AlertAsync("Выберите тариф", "Внимание", "Ок");
							return;
						}

						if (_myRate != null && SelectedRate.Id == _myRate.Id)
						{
							await MaterialDialog.Instance.AlertAsync("Выбранный тариф уже подключен", "Внимание", "Ок");
						}
						
						var confirm = await MaterialDialog.Instance.ConfirmAsync("Вы уверены, что хотите сменить тариф?", "Внимание", "Да", "Нет");
						if (confirm == null)
						{
							return;
						}
						if (!confirm.Value)
						{
							return;
						}

						var rate = await _rateService.ChangeRate(SelectedRate);

						if (rate)
						{
							await MaterialDialog.Instance.AlertAsync("Вы успешно сменили тариф", "Внимание", "Ок");
							_myRate = await _rateService.GetMyRate();
							SelectedRate = _myRate;
						}
						else
						{
							await MaterialDialog.Instance.AlertAsync("Не удалось сменить тариф", "Внимание", "Ок");
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

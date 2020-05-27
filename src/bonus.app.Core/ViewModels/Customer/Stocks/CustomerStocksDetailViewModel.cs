using bonus.app.Core.Models;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Customer.Stocks
{
	public class CustomerStocksDetailViewModel : MvxViewModel<Stock>
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openArchivePageCommand;
		private Color _shareColor;
		private Stock _stock;
		private User _user;
		private MvxCommand _addToFavoriteCommand;
		private IStockService _stockService;
		#endregion
		#endregion

		#region .ctor
		public CustomerStocksDetailViewModel(IAuthService authService, IMvxNavigationService navigationService, IStockService stockService, IMvxFormsViewPresenter platformPresenter)
		{
			User = authService.User;
			_navigationService = navigationService;
			_stockService = stockService;
			_platformPresenter = platformPresenter;
		}
		#endregion

		#region Properties
		public MvxCommand OpenArchivePageCommand
		{
			get
			{
				_openArchivePageCommand = _openArchivePageCommand ??
													 new MvxCommand(() =>
													 {
														 _navigationService.Navigate<StockArchiveViewModel>();
													 });
				return _openArchivePageCommand;
			}
		}

		public MvxCommand OpenFavoriteStocksCommand
		{
			get
			{
				_openFavoriteStocksCommand = _openFavoriteStocksCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<CustomerStocksViewModel, bool>(true);
				});
				return _openFavoriteStocksCommand;
			}
		}
		private Application _formsApplication;
		private readonly IMvxFormsViewPresenter _platformPresenter;
		private MvxCommand _openFavoriteStocksCommand;

		public Application FormsApplication
		{
			get => _formsApplication ?? (_formsApplication = _platformPresenter.FormsApplication);
			set => _formsApplication = value;
		}

		public MvxCommand AddToFavoriteCommand
		{
			get
			{
				_addToFavoriteCommand = _addToFavoriteCommand ?? new MvxCommand(async () =>
				{
					if (!await _stockService.AddToFavorite(Stock.Uuid))
					{
						await FormsApplication.MainPage.DisplayAlert("Внимание", "Не удалось добавить акцию избранное.", "Ок");
					}
				});
				return _addToFavoriteCommand;
			}
		}

		public Color ShareColor
		{
			get => _shareColor;
			private set => SetProperty(ref _shareColor, value);
		}

		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
			switch (Stock.Status)
			{
				case null:
					ShareColor = Color.Transparent;
					return;
				case "Завершена":
					ShareColor = Color.FromHex("#807D746D");
					break;
				case "Отклонена":
					ShareColor = Color.FromHex("#80BB8D91");
					break;
			}
		}
		#endregion
	}
}

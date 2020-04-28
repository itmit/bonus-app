using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class BusinessmanStocksDetailViewModel : MvxViewModel<Stock>
	{
		private Stock _stock;
		private User _user;
		private Color _shareColor;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openCreateStockPageCommand;
		private MvxCommand _openCreateStockArchivePageCommand;
		private MvxCommand _openEditStockArchivePageCommand;
		private IStockService _stockService;

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public Color ShareColor
		{
			get => _shareColor;
			private set => SetProperty(ref _shareColor, value);
		}

		public BusinessmanStocksDetailViewModel(IAuthService authService, IMvxNavigationService navigationService, IStockService stockService)
		{
			User = authService.User;
			_navigationService = navigationService;
			_stockService = stockService;
			_stockService.EditedStock += StockServiceOnEditedStock;
		}

		private void StockServiceOnEditedStock(Stock stock)
		{
			if (stock != null)
			{
				Stock = stock;
				RaiseAllPropertiesChanged();
			}
		}

		public MvxCommand OpenCreateStockPageCommand
		{
			get
			{
				_openCreateStockPageCommand = _openCreateStockPageCommand ??
											  new MvxCommand(() =>
											  {
												  _navigationService.Navigate<CreateStockViewModel>();
											  });
				return _openCreateStockPageCommand;
			}
		}

		public MvxCommand OpenArchivePageCommand
		{
			get
			{
				_openCreateStockArchivePageCommand = _openCreateStockArchivePageCommand ??
													 new MvxCommand(() =>
													 {
														 _navigationService.Navigate<StockArchiveViewModel>();
													 });
				return _openCreateStockArchivePageCommand;
			}
		}

		public MvxCommand OpenEditStockArchivePageCommand
		{
			get
			{
				_openEditStockArchivePageCommand = _openEditStockArchivePageCommand ??
												   new MvxCommand(() =>
												   {
													   _navigationService.Navigate<EditorStockViewModel, Stock>(Stock);
												   });
				return _openEditStockArchivePageCommand;
			}
		}

		public Stock Stock
		{
			get => _stock;
			private set => SetProperty(ref _stock, value);
		}

		public override void Prepare(Stock parameter)
		{
			Stock = parameter;
			if (Stock.Status == null)
			{
				ShareColor = Color.Transparent;
				return;
			}

			if (Stock.Status.Equals("Завершена"))
			{
				ShareColor = Color.FromHex("#807D746D");
			}
			else if (Stock.Status.Equals("Отклонена"))
			{
				ShareColor = Color.FromHex("#80BB8D91");
			}
		}
	}
}

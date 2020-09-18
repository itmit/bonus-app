using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.ViewModels.Businessman.Stocks;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace bonus.app.Core.ViewModels.Customer.Stocks
{
	public class CustomerStocksDetailViewModel : MvxViewModel<Guid>
	{
		#region Data
		#region Fields
		private MvxCommand _addToFavoriteCommand;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openArchivePageCommand;
		private MvxCommand _openFavoriteStocksCommand;
		private Color _shareColor;
		private Stock _stock;
		private readonly IStockService _stockService;
		private Guid _guid;
		private MvxCommand _showBusinessmanProfileCommand;
		#endregion
		#endregion

		#region .ctor
		public CustomerStocksDetailViewModel(IMvxNavigationService navigationService,
											 IStockService stockService)
		{
			_navigationService = navigationService;
			_stockService = stockService;
		}
		#endregion

		#region Properties
		public MvxCommand AddToFavoriteCommand
		{
			get
			{
				_addToFavoriteCommand = _addToFavoriteCommand ??
										new MvxCommand(async () =>
										{
											if (await _stockService.AddToFavorite(Stock.Uuid))
											{
												await MaterialDialog.Instance.AlertAsync("Акция добавлена в избранное", "Внимание", "Ок");
											}
											else
											{
												await MaterialDialog.Instance.AlertAsync("Не удалось добавить акцию избранное", "Внимание", "Ок");
											}
										});
				return _addToFavoriteCommand;
			}
		}

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
				_openFavoriteStocksCommand = _openFavoriteStocksCommand ??
											 new MvxCommand(() =>
											 {
												 _navigationService.Navigate<FavoriteStocksViewModel>();
											 });
				return _openFavoriteStocksCommand;
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
		#endregion

		#region Overrided
		public override void Prepare(Guid parameter)
		{
			_guid = parameter;
		}
		#endregion

		public MvxCommand ShowBusinessmanProfileCommand
		{
			get
			{
				_showBusinessmanProfileCommand = _showBusinessmanProfileCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<BusinessmanProfileViewModel, BusinessmanProfileViewModelArgs>(new BusinessmanProfileViewModelArgs(Stock.Client.Uuid, Stock.Id, null));
				});
				return _showBusinessmanProfileCommand;
			}
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			var stock = await _stockService.GetDetail(_guid);
			switch (stock.Status)
			{
				case "Завершена":
					ShareColor = Color.FromHex("#807D746D");
					break;
				case "Отклонена":
					ShareColor = Color.FromHex("#80BB8D91");
					break;
				default:
					ShareColor = Color.Transparent;
					break;
			}

			Stock = stock;
		}
	}
}

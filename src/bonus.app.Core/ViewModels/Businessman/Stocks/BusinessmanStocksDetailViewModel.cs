using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Models.UserModels;
using bonus.app.Core.Services;
using bonus.app.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Stocks
{
	public class BusinessmanStocksDetailViewModel : MvxViewModel<Guid>
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openArchivePageCommand;
		private MvxCommand _openCreateStockPageCommand;
		private MvxCommand _openEditStockArchivePageCommand;
		private Color _shareColor;
		private Stock _stock;
		private readonly IStockService _stockService;
		private User _user;
		private Guid _guid;
		#endregion
		#endregion

		#region .ctor
		public BusinessmanStocksDetailViewModel(IAuthService authService, IMvxNavigationService navigationService, IStockService stockService)
		{
			User = authService.User;
			_navigationService = navigationService;
			_stockService = stockService;
			_stockService.EditedStock += StockServiceOnEditedStock;
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
		public override void Prepare(Guid parameter)
		{
			_guid = parameter;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			Stock = await _stockService.Detail(_guid);
		}
		#endregion

		#region Private
		private void StockServiceOnEditedStock(Stock stock)
		{
			if (stock == null)
			{
				return;
			}

			Stock = stock;
			RaiseAllPropertiesChanged();
		}
		#endregion
	}
}

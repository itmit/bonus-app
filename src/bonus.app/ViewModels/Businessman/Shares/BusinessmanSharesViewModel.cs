using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Shares
{
	public class BusinessmanSharesViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<Share> _shares;
		private Share _selectedShare;
		private readonly IShareService _shareService;
		private MvxCommand _openCreateSharePageCommand;
		private MvxCommand _openCreateShareArchivePageCommand;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;

		public BusinessmanSharesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService,  IShareService shareService)
			: base(logProvider, navigationService)
		{
			_shareService = shareService;
			_shareService.CreatedShareEventHandler += ShareServiceOnCreatedShareEventHandler;
		}

		private void ShareServiceOnCreatedShareEventHandler(object sender, EventArgs e)
		{
			RefreshCommand.Execute();
		}

		public MvxObservableCollection<Share> Shares
		{
			get => _shares;
			set => SetProperty(ref _shares, value);
		}
		
		public override async Task Initialize()
		{
			await base.Initialize();

			Shares = new MvxObservableCollection<Share>(await _shareService.GetAll());
		}

		public MvxCommand OpenCreateSharePageCommand
		{
			get
			{
				_openCreateSharePageCommand = _openCreateSharePageCommand ??
											  new MvxCommand(() =>
											  {
												  NavigationService.Navigate<CreateShareViewModel>();
											  });
				return _openCreateSharePageCommand;
			}
		}

		public MvxCommand OpenCreateShareArchivePageCommand
		{
			get
			{
				_openCreateShareArchivePageCommand = _openCreateShareArchivePageCommand ??
													 new MvxCommand(() =>
													 {
														 NavigationService.Navigate<ShareArchiveViewModel>();
													 });
				return _openCreateShareArchivePageCommand;
			}
		}
		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ??
								  new MvxCommand(async () =>
								  {
									  IsRefreshing = true;
									  Shares = new MvxObservableCollection<Share>(await _shareService.GetAll());
									  IsRefreshing = false;
								  });
				return _refreshCommand;
			}
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			 private set => SetProperty(ref _isRefreshing, value);
		}

		public Share SelectedShare
		{
			get => _selectedShare;
			set
			{
				SetProperty(ref _selectedShare, value);
				if (value != null)
				{
					NavigationService.Navigate<BusinessmanSharesDetailViewModel, Share>(value);
				}
			}
		}
	}
}

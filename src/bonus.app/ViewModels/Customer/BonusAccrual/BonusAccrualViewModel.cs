using System;
using System.Linq;
using System.Threading.Tasks;
using bonus.app.Core.Repositories;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.BonusAccrual
{
	public class BonusAccrualViewModel : MvxViewModel
	{
		private IMvxNavigationService _navigationService;
		private IUserRepository _userRepository;
		private Guid _userUuid;

		public BonusAccrualViewModel(IUserRepository repository, IMvxNavigationService navigationService)
		{
			_userRepository = repository;
			_navigationService = navigationService;
		}

		public Guid UserUuid
		{
			get => _userUuid;
			set => SetProperty(ref _userUuid, value);
		}

		public override Task Initialize()
		{
			UserUuid = _userRepository.GetAll()
								  .Single().Guid;
			return base.Initialize();
		}
	}
}

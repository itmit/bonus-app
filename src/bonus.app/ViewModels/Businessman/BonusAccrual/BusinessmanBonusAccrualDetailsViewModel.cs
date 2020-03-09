using System;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.BonusAccrual
{
	public class BusinessmanBonusAccrualDetailsViewModel : MvxNavigationViewModel<Guid>
	{
		private Guid _guid;
		private ICustomerService _customerService;

		public BusinessmanBonusAccrualDetailsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ICustomerService customerService)
			: base(logProvider, navigationService)
		{
			_customerService = customerService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			User = await _customerService.GetCustomerByUuid(_guid);
		}

		public User User
		{
			get;
			set;
		}

		public override void Prepare(Guid parameter)
		{
			_guid = parameter;
		}
	}
}

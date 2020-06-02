using System;
using System.Globalization;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.BonusAccrual
{
	public class MyBonusViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IBonusService _bonusService;
		private MvxObservableCollection<AccrualBonuses> _myBonuses;
		private readonly IMvxNavigationService _navigationService;
		private AccrualBonuses _selectedBusinessman;
		private double _sum;
		#endregion
		#endregion

		#region .ctor
		public MyBonusViewModel(IBonusService bonusService, IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
			_bonusService = bonusService;
		}
		#endregion

		#region Properties
		public MvxObservableCollection<AccrualBonuses> MyBonuses
		{
			get => _myBonuses;
			private set => SetProperty(ref _myBonuses, value);
		}

		public AccrualBonuses SelectedBusinessman
		{
			get => _selectedBusinessman;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedBusinessman, value);
				_navigationService.Navigate<BusinessmanProfileViewModel, Guid>(value.Uuid);
			}
		}

		public string Sum => _sum.ToString(CultureInfo.InvariantCulture);
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				MyBonuses = new MvxObservableCollection<AccrualBonuses>(await _bonusService.GetMyBonuses());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			_sum = 0;
			foreach (var bonus in MyBonuses)
			{
				_sum += (double) bonus.AccrualValue / 100;
			}

			await RaisePropertyChanged(() => Sum);
		}
		#endregion
	}
}

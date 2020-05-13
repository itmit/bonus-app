using System;
using System.Globalization;
using System.Threading.Tasks;
using bonus.app.Core.Models;
using bonus.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Customer.BonusAccrual
{
	public class MyBonusViewModel : MvxViewModel
	{
		private IBonusService _bonusService;
		private MvxObservableCollection<AccrualBonuses> _myBonuses;
		private double _sum;

		#region .ctor
		public MyBonusViewModel(IBonusService bonusService)
		{
			_bonusService = bonusService;
		}
		#endregion

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
				_sum += (double)bonus.AccrualValue / 100;
			}

			await RaisePropertyChanged(() => Sum);
		}

		public MvxObservableCollection<AccrualBonuses> MyBonuses
		{
			get => _myBonuses;
			private set => SetProperty(ref _myBonuses, value);
		}

		public string Sum => _sum.ToString(CultureInfo.InvariantCulture);
	}
}

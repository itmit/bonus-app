using System;
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
		private MvxObservableCollection<Service> _myBonuses;

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
				MyBonuses = new MvxObservableCollection<Service>(await _bonusService.GetMyBonuses());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public MvxObservableCollection<Service> MyBonuses
		{
			get => _myBonuses;
			private set => SetProperty(ref _myBonuses, value);
		}
	}
}

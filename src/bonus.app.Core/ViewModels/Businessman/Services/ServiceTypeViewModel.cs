using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class ServiceTypeViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private bool _isVisibleServices;
		private string _name;
		private int _rotation;
		private MvxObservableCollection<ServiceViewModel> _services;
		private MvxCommand _showOrHideServicesCommand;
		private Guid _uuid;
		#endregion
		#endregion

		#region Properties
		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
		}

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public int Rotation
		{
			get => _rotation;
			set => SetProperty(ref _rotation, value);
		}

		public MvxObservableCollection<ServiceViewModel> Services
		{
			get => _services;
			set => SetProperty(ref _services, value);
		}

		public MvxCommand ShowOrHideServicesCommand
		{
			get
			{
				_showOrHideServicesCommand = _showOrHideServicesCommand ??
											 new MvxCommand(() =>
											 {
												 IsVisibleServices = !IsVisibleServices;
												 Rotation = IsVisibleServices ? 180 : 0;
											 });
				return _showOrHideServicesCommand;
			}
		}

		public Guid Uuid
		{
			get => _uuid;
			set => SetProperty(ref _uuid, value);
		}
		#endregion
	}
}

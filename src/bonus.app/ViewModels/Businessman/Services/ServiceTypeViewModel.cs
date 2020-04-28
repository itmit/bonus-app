using System;
using System.Collections.Generic;
using bonus.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class ServiceTypeViewModel : MvxViewModel
	{
		private string _name;
		private Guid _uuid;
		private MvxObservableCollection<ServiceViewModel> _services;
		private MvxCommand _showOrHideServicesCommand;
		private int _rotation;
		private bool _isVisibleServices;

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public Guid Uuid
		{
			get => _uuid;
			set => SetProperty(ref _uuid, value);
		}

		public MvxCommand ShowOrHideServicesCommand
		{
			get
			{
				_showOrHideServicesCommand = _showOrHideServicesCommand ?? new MvxCommand(() =>
				{
					IsVisibleServices = !IsVisibleServices;
					Rotation = IsVisibleServices ? 180 : 0;
				});
				return _showOrHideServicesCommand;
			}
		}

		public bool IsVisibleServices
		{
			get => _isVisibleServices;
			set => SetProperty(ref _isVisibleServices, value);
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
	}
}

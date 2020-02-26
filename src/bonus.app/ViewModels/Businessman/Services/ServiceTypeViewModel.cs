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

		public MvxObservableCollection<ServiceViewModel> Services
		{
			get => _services;
			set => SetProperty(ref _services, value);
		}
	}
}

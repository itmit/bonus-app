using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class ServiceViewModel : MvxViewModel
	{
		private string _name;
		private MvxCommand _selectService;
		private Color _color = Color.Transparent;

		public Guid Uuid
		{
			get;
			set;
		}

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		public MvxCommand SelectServiceCommand
		{
			get
			{
				_selectService = _selectService ?? new MvxCommand(Execute);
				return _selectService;
			}
		}

		public IServiceParentViewModel ParentViewModel
		{
			get;
			set;
		}

		public Color Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
		}

		private void Execute()
		{
			ParentViewModel.SelectedService = this;
		}
	}
}

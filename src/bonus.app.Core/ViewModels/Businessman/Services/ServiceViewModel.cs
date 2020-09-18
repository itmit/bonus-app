using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.ViewModels.Businessman.Services
{
	public class ServiceViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private Color _color = Color.Transparent;
		private string _name;
		private MvxCommand _selectService;
		#endregion
		#endregion

		#region Properties
		public IServiceParentViewModel ParentViewModel
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}

		public Color Color
		{
			get => _color;
			set => SetProperty(ref _color, value);
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

		public int Id
		{
			get;
			set;
		}
		#endregion

		#region Private
		private void Execute()
		{
			ParentViewModel.SelectedService = this;
		}
		#endregion
	}
}

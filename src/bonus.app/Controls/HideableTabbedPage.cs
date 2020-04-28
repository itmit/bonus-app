using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.Controls
{
	public class HideableTabbedPage : MvxTabbedPage
	{
		#region Data
		#region Static
		public static readonly BindableProperty IsHiddenProperty = BindableProperty.Create(nameof(IsHidden), typeof(bool), typeof(HideableTabbedPage), false);
		#endregion
		#endregion

		#region Properties
		public bool IsHidden
		{
			get => (bool) GetValue(IsHiddenProperty);
			set => SetValue(IsHiddenProperty, value);
		}
		#endregion
	}

	public class HideableTabbedPage<TViewModel> : HideableTabbedPage, IMvxPage<TViewModel> where TViewModel : class, IMvxViewModel
	{
		#region Data
		#region Static
		public new static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create(nameof(ViewModel), typeof(TViewModel), typeof(IMvxElement<TViewModel>), default(TViewModel), BindingMode.Default, null, ViewModelChanged);
		#endregion
		#endregion

		#region Public
		public static void ViewModelChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			if (newvalue != null)
			{
				if (bindable is IMvxElement element)
				{
					element.DataContext = newvalue;
				}
				else
				{
					bindable.BindingContext = newvalue;
				}
			}
		}
		#endregion

		#region IMvxView<TViewModel> members
		public new TViewModel ViewModel
		{
			get => (TViewModel) base.ViewModel;
			set => base.ViewModel = value;
		}
		#endregion
	}
}

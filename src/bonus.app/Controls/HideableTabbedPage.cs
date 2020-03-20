using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace bonus.app.Core.Controls
{
	public class HideableTabbedPage : MvxTabbedPage
	{
		public static readonly BindableProperty IsHiddenProperty =
			BindableProperty.Create(nameof(IsHidden), typeof(bool), typeof(HideableTabbedPage), false);

		public bool IsHidden
		{
			get { return (bool)GetValue(IsHiddenProperty); }
			set { SetValue(IsHiddenProperty, value); }
		}
	}

	public class HideableTabbedPage<TViewModel>
		: HideableTabbedPage
		  , IMvxPage<TViewModel> where TViewModel : class, IMvxViewModel
	{
		public new static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel), typeof(TViewModel), typeof(IMvxElement<TViewModel>), default(TViewModel), BindingMode.Default, null, ViewModelChanged, null, null);

		public new TViewModel ViewModel
		{
			get => (TViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

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
	}
}

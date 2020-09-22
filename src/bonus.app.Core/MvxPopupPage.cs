using System;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Base;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace bonus.app.Core
{
	public class MvxPopupPage : PopupPage, IMvxPage, IMvxEventSourcePage
	{
		#region Delegates and events
		public event EventHandler AppearingCalled;

		public event EventHandler BindingContextChangedCalled;

		public event EventHandler DisappearingCalled;

		public event EventHandler ParentSetCalled;
		#endregion

		#region Data
		#region Static
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create(nameof(ViewModel), typeof(IMvxViewModel), typeof(IMvxElement), default(MvxViewModel), BindingMode.Default, null, ViewModelChanged);
		#endregion

		#region Fields
		private IMvxBindingContext _bindingContext;
		#endregion
		#endregion

		#region Public
		internal static void ViewModelChanged(BindableObject bindable, object oldvalue, object newvalue)
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

		#region Overridable
		protected virtual void OnViewModelSet()
		{
			ViewModel?.ViewCreated();
		}
		#endregion

		#region IMvxBindingContextOwner members
		public new IMvxBindingContext BindingContext
		{
			get
			{
				if (_bindingContext == null)
				{
					BindingContext = new MvxBindingContext(base.BindingContext);
				}

				return _bindingContext;
			}
			set
			{
				_bindingContext = value;
				base.BindingContext = _bindingContext.DataContext;
			}
		}
		#endregion

		#region IMvxDataConsumer members
		public object DataContext
		{
			get => BindingContext.DataContext;
			set
			{
				if (value != null && !(_bindingContext != null && ReferenceEquals(DataContext, value)))
				{
					BindingContext = new MvxBindingContext(value);
				}
			}
		}
		#endregion

		#region IMvxView members
		public IMvxViewModel ViewModel
		{
			get => DataContext as IMvxViewModel;
			set
			{
				DataContext = value;
				SetValue(ViewModelProperty, value);
				OnViewModelSet();
			}
		}
		#endregion

		#region Overrided
		protected override void OnAppearing()
		{
			base.OnAppearing();
			AppearingCalled.Raise(this);
			ViewModel?.ViewAppearing();
			ViewModel?.ViewAppeared();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			BindingContextChangedCalled.Raise(this);
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			DisappearingCalled.Raise(this);
			ViewModel?.ViewDisappearing();
			ViewModel?.ViewDisappeared();
			ViewModel?.ViewDestroy();
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();
			ParentSetCalled.Raise(this);
		}
		#endregion
	}

	public class MvxPopupPage<TViewModel> : MvxPopupPage, IMvxPage<TViewModel> where TViewModel : class, IMvxViewModel
	{
		#region Data
		#region Static
		public new static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create(nameof(ViewModel), typeof(TViewModel), typeof(IMvxElement<TViewModel>), default(TViewModel), BindingMode.Default, null, ViewModelChanged);
		#endregion
		#endregion

		#region IMvxView<TViewModel> members
		public MvxFluentBindingDescriptionSet<IMvxElement<TViewModel>, TViewModel> CreateBindingSet() => this.CreateBindingSet<IMvxElement<TViewModel>, TViewModel>();

		public new TViewModel ViewModel
		{
			get => (TViewModel) base.ViewModel;
			set => base.ViewModel = value;
		}
		#endregion
	}
}

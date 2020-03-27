using bonus.app.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace bonus.app.Core.Page
{
	[MvxModalPresentation(Animated = true, WrapInNavigationPage = false)]
	public class ScannerPage : ZXingScannerPage, IMvxPage<ScannerViewModel>
	{
		private IMvxBindingContext _bindingContext;

		/// <summary>Application developers can override this method to provide behavior when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed()
		{
			Navigation.PopModalAsync();
			return true;
		}

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			ViewModel?.ViewAppearing();
			ViewModel?.ViewAppeared();

			NavigationPage.SetHasNavigationBar(this, false);
			if (ViewModel != null)
			{
				OnScanResult += ViewModel.OnScanResult;
			}
		}

		private static void ViewModelChanged(BindableObject bindable, object oldvalue, object newvalue)
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

		public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel), typeof(IMvxViewModel), typeof(IMvxElement), default(MvxViewModel), BindingMode.Default, null, ViewModelChanged, null, null);

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
		protected virtual void OnViewModelSet()
		{
			ViewModel?.ViewCreated();
		}

		IMvxViewModel IMvxView.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as ScannerViewModel;
		}

		public new IMvxBindingContext BindingContext
		{
			get => _bindingContext;
			set => _bindingContext = value;
		}

		public ScannerViewModel ViewModel
		{
			get;
			set;
		}
	}
}

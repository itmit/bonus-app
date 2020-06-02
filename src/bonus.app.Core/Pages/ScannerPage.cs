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
		#region Data
		#region Static
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create(nameof(ViewModel), typeof(IMvxViewModel), typeof(IMvxElement), default(MvxViewModel), BindingMode.Default, null, ViewModelChanged);
		#endregion
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
			get;
			set;
		}
		#endregion

		#region IMvxDataConsumer members
		public object DataContext
		{
			get => BindingContext.DataContext;
			set
			{
				if (value != null && !(BindingContext != null && ReferenceEquals(DataContext, value)))
				{
					BindingContext = new MvxBindingContext(value);
				}
			}
		}
		#endregion

		#region IMvxView members
		IMvxViewModel IMvxView.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as ScannerViewModel;
		}
		#endregion

		#region IMvxView<ScannerViewModel> members
		public ScannerViewModel ViewModel
		{
			get;
			set;
		}
		#endregion

		#region Overrided
		/// <summary>
		/// When overridden, allows application developers to customize behavior immediately prior to the
		/// <see cref="T:Xamarin.Forms.Page" /> becoming visible.
		/// </summary>
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

		/// <summary>Application developers can override this method to provide behavior when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed()
		{
			Navigation.PopModalAsync();
			return true;
		}
		#endregion

		#region Private
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
		#endregion
	}
}

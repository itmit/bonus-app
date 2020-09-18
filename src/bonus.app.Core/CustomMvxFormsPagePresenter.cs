using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Logging;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace bonus.app.Core
{
	public class CustomMvxFormsPagePresenter : MvxFormsPagePresenter
	{
		private IMvxLog _logger;

		#region .ctor
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MvvmCross.Forms.Views.MvxFormsPagePresenter" /> class.
		/// </summary>
		/// <param name="platformPresenter">The native platform presenter from where the MvxFormsPagePresenter is created</param>
		public CustomMvxFormsPagePresenter(IMvxFormsViewPresenter platformPresenter)
			: base(platformPresenter)
		{
		}
		#endregion

		public IMvxLog Logger
		{
			get
			{
				_logger = _logger ?? Mvx.IoCProvider.Resolve<IMvxLogProvider>()
								  .GetLogFor(GetType());
				return _logger;
			}
		} 

		#region Overrided
		public override Task<bool> CloseContentPage(IMvxViewModel viewModel, MvxContentPagePresentationAttribute attribute)
		{
			if ((FormsApplication.MainPage as TabbedPage)?.CurrentPage is NavigationPage root)
			{
				return FindAndCloseViewFromViewModel(viewModel, root, attribute);
			}

			return base.CloseContentPage(viewModel, attribute);
		}
		public override Task<bool> Show(MvxViewModelRequest request)
		{
#if DEBUG // Only wrap in try-finally when in debug
			try
			{
#endif
				return base.Show(request);
#if DEBUG // Only showing this when debugging MVX
			}
			catch (Exception exception)
			{
				Logger.Log(MvxLogLevel.Debug, () => exception.ToString());
				throw;
			}
			finally
			{
				Logger.Log(MvxLogLevel.Debug, () => FormsApplication.Hierarchy());
			}
#endif
		}
		public override TPage GetPageOfType<TPage>(Xamarin.Forms.Page rootPage = null)
		{
			if (rootPage == null)
			{
				rootPage = FormsApplication.MainPage;
			}

			switch (rootPage)
			{
				case TPage page:
					return page;
				case TabbedPage tabbedPage:
				{
					var currentPage = GetPageOfType<TPage>(tabbedPage.CurrentPage);
					return currentPage;
				}
				default:
					return base.GetPageOfType<TPage>(rootPage);
			}
		}

		public override void RegisterAttributeTypes()
		{
			base.RegisterAttributeTypes();
			AttributeTypesToActionsDictionary.Register<MvxPopupPagePresentationAttribute>(ShowPopupPage, ClosePopupPage);
		}

		public override async Task<bool> ShowTabbedPage(Type view, MvxTabbedPagePresentationAttribute attribute, MvxViewModelRequest request)
		{
			if (attribute.Position != TabbedPosition.Tab)
			{
				return await base.ShowTabbedPage(view, attribute, request);
			}

			var page = await CloseAndCreatePage(view, request, attribute);
			var tabHost = GetPageOfType<TabbedPage>();
			if (tabHost == null)
			{
				tabHost = new TabbedPage();
				await PushOrReplacePage(FormsApplication.MainPage, tabHost, attribute);
			}

			if (tabHost.Children.Any(p => p.GetType() == page.GetType() || (p as NavigationPage)?.RootPage.GetType() == page.GetType()))
			{
				return true;
			}

			if (attribute.WrapInNavigationPage)
			{
				tabHost.Children.Add(new NavigationPage(page)
				{
					Title = page.Title,
					IconImageSource = page.IconImageSource
				});
				return true;
			}

			tabHost.Children.Add(page);
			return true;

		}

		public override NavigationPage TopNavigationPage(Xamarin.Forms.Page rootPage = null)
		{
			if (rootPage == null)
			{
				rootPage = FormsApplication.MainPage;
			}

			if (!(rootPage is TabbedPage tabbedPage))
			{
				return base.TopNavigationPage(rootPage);
			}

			if (tabbedPage.CurrentPage == null)
			{
				return base.TopNavigationPage(rootPage);
			}

			var navTabbedPage = TopNavigationPage(tabbedPage.CurrentPage);
			return navTabbedPage ?? base.TopNavigationPage(rootPage);
		}
		#endregion

		#region Private
		private async Task<bool> ClosePopupPage(IMvxViewModel viewModel, MvxPopupPagePresentationAttribute attribute)
		{
			var popup = PopupNavigation.Instance.PopupStack?.OfType<IMvxPage>()
									   .FirstOrDefault(x => x.ViewModel == viewModel);
			if (!(popup is PopupPage page))
			{
				await FormsApplication.MainPage.Navigation.PopPopupAsync(attribute.Animated);
				return true;
			}

			await FormsApplication.MainPage.Navigation.RemovePopupPageAsync(page, attribute.Animated);
			return true;
		}

		private async Task<bool> ShowPopupPage(Type view, MvxPopupPagePresentationAttribute attribute, MvxViewModelRequest request)
		{
			var page = CreatePage(view, request, attribute) as PopupPage;

			await FormsApplication.MainPage.Navigation.PushPopupAsync(page, attribute.Animated);

			return true;
		}
		#endregion
	}
}

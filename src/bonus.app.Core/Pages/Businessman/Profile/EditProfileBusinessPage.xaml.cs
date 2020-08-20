using System;
using System.Linq;
using bonus.app.Core.ViewModels.Businessman.Profile;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;

namespace bonus.app.Core.Pages.Businessman.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfileBusinessPage : MvxContentPage<EditProfileBusinessmanViewModel>
	{
		#region .ctor
		public EditProfileBusinessPage()
		{
			InitializeComponent();
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
			Layout.BindingContext = ViewModel;
			PicCountryAndCityView.ViewModel = ViewModel.CountryAndCityViewModel;

			base.OnAppearing();

			ViewModel.FailedEdit += FailedEdit;
		}

		private void FailedEdit(string[] propertyNames)
		{
			Element elementMinY = null;
			foreach (var name in propertyNames)
			{
				var element = FindByName(name);
				if (elementMinY == null)
				{
					elementMinY = (Element)element;
				}
				if (element != null && ((VisualElement)element).ScaleY < ((VisualElement)elementMinY).ScaleY)
				{
					elementMinY = (Element)element;
				}
			}

			try
			{
				EditProfileBusinessmanScroll.ScrollToAsync(elementMinY, ScrollToPosition.Start, true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}

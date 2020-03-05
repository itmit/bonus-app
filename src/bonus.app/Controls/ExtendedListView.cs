using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace bonus.app.Core.Controls
{
	public class ExtendedListView : ListView
	{
		#region Data
		#region Static
		public static readonly BindableProperty ItemAppearingCommandProperty =
			BindableProperty.Create(nameof(ItemAppearingCommand),
									typeof(ICommand),
									typeof(ExtendedListView),
									default(ICommand));

		public static readonly BindableProperty ItemDisappearingCommandProperty =
			BindableProperty.Create(nameof(ItemDisappearingCommand),
									typeof(ICommand),
									typeof(ExtendedListView),
									default(ICommand));

		public static readonly BindableProperty TappedCommandProperty =
			BindableProperty.Create(nameof(TappedCommand),
									typeof(ICommand),
									typeof(ExtendedListView),
									default(ICommand));
		#endregion
		#endregion

		#region .ctor
		public ExtendedListView()
			: this(ListViewCachingStrategy.RecycleElement)
		{
		}

		public ExtendedListView(ListViewCachingStrategy cachingStrategy)
			: base(cachingStrategy)
		{
			ItemSelected += OnItemSelected;
			ItemTapped += OnItemTapped;
			ItemAppearing += OnItemAppearing;
			ItemDisappearing += OnItemDisappering;
		}
		#endregion

		#region Properties
		public ICommand ItemAppearingCommand
		{
			get => (ICommand)GetValue(ItemAppearingCommandProperty);
			set => SetValue(ItemAppearingCommandProperty, value);
		}

		public ICommand ItemDisappearingCommand
		{
			get => (ICommand)GetValue(ItemDisappearingCommandProperty);
			set => SetValue(ItemDisappearingCommandProperty, value);
		}

		public ICommand TappedCommand
		{
			get => (ICommand)GetValue(TappedCommandProperty);
			set => SetValue(TappedCommandProperty, value);
		}
		#endregion

		#region Public
		public void ScrollToFirst()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				try
				{
					if (ItemsSource != null &&
						ItemsSource.Cast<object>().Any())
					{
						var msg = ItemsSource.Cast<object>()
											 .FirstOrDefault();
						if (msg != null)
						{
							ScrollTo(msg, ScrollToPosition.Start, false);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
				}
			});
		}

		public void ScrollToLast()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				try
				{
					if (ItemsSource != null &&
						ItemsSource.Cast<object>().Any())
					{
						var msg = ItemsSource.Cast<object>()
											 .LastOrDefault();
						if (msg != null)
						{
							ScrollTo(msg, ScrollToPosition.End, false);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
				}
			});
		}
		#endregion

		#region Private
		private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
			=>ItemAppearingCommand?.Execute(e.Item);

		private void OnItemDisappering(object sender, ItemVisibilityEventArgs e)
			=>ItemDisappearingCommand?.Execute(e.Item);

		private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var listView = (ExtendedListView)sender;
			if (e == null)
			{
				return;
			}

			listView.SelectedItem = null;
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			TappedCommand?.Execute(e.Item);

			SelectedItem = null;
		}
		#endregion
	}
}

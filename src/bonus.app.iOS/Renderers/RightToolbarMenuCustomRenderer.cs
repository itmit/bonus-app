using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using bonus.app.Core;
using bonus.app.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(RightToolbarMenuCustomRenderer))]
namespace bonus.app.iOS.Renderers
{
	public class RightToolbarMenuCustomRenderer : PageRenderer
	{
		private List<ToolbarItem> _secondaryItems;
		private UITableView _table;

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			// Get all secondary toolbar items and fill it to the gloabal list variable and remove from the content page.
			if (e.NewElement is ContentPage page)
			{
				_secondaryItems = page.ToolbarItems.Where(i => i.Order == ToolbarItemOrder.Secondary).ToList();
				_secondaryItems.ForEach(t => page.ToolbarItems.Remove(t));

				// If global secondary toolbar items are not null, I created and added a primary toolbar item with image(Overflow) I         
				// want to show.
				if (_secondaryItems != null && _secondaryItems.Count > 0)
				{
					page.ToolbarItems.Add(new ToolbarItem()
					{
						Order = ToolbarItemOrder.Primary,
						IconImageSource = "more.png",
						Priority = 1,
						Command = new Command(ToolClicked)
					});
				}
			}
			base.OnElementChanged(e);
		}

		// Create a table instance and added it to the view.
		private void ToolClicked()
		{
			if (_table == null)
			{
				// Set the table position to right side. and set height to the content height.
				var childRect = new RectangleF((float)View.Bounds.Width - 250, 0, 250, _secondaryItems.Count() * 56);
				_table = new UITableView(childRect)
				{
					// Created Table Source Class as Mentioned in the 
					Source = new TableSource(_secondaryItems)
				};
				Add(_table);
				return;
			}
			if (View.Subviews.Any(subview => subview.Equals(_table)))
			{
				_table.RemoveFromSuperview();
				return;
			}
			Add(_table);
		}
    }
}

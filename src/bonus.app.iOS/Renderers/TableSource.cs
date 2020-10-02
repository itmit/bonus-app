using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace bonus.app.iOS.Renderers
{
	public class TableSource : UITableViewSource
	{
		private readonly List<ToolbarItem> _tableItems;
		private readonly string[] _tableItemTexts;
		private const string CellIdentifier = "TableCell";

		public TableSource(List<ToolbarItem> items)
		{
			// Set the secondary toolbar items to global variables and get all text values from the toolbar items
			_tableItems = items;
			_tableItemTexts = items.Select(a => a.Text).ToArray();
		}

		public override nint RowsInSection(UITableView tableview, nint section) => _tableItemTexts.Length;

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var item = _tableItemTexts[indexPath.Row];

			var cell = tableView.DequeueReusableCell(CellIdentifier);
			cell = cell ?? new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
			cell.TextLabel.Text = item;

			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => 56; // Set default row height.

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			// Used command to excute and deselct the row and removed the table.
			var command = _tableItems[indexPath.Row].Command;
			command.Execute(_tableItems[indexPath.Row].CommandParameter);
			tableView.DeselectRow(indexPath, true);
			tableView.RemoveFromSuperview();
		}
	}
}

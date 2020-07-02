using System;
using bonus.app.Core.Models.UserModels;

namespace bonus.app.Core.Models.ChatModels
{
	public class Dialog
	{
		#region Properties
		public DateTime? CreatedAt
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public Message LastMessage
		{
			get;
			set;
		}

		public DateTime? UpdatedAt
		{
			get;
			set;
		}

		public User UserTo
		{
			get;
			set;
		}
		#endregion
	}
}

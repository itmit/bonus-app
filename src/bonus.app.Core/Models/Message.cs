using System;

namespace bonus.app.Core.Models
{
	public class Message
	{
		#region Prop
		public string Text
		{
			get;
			set;
		}

		public bool IsTextOut
		{
			get;
			set;
		}

		public DateTime? CreatedAt
		{
			get;
			set;
		}

		public DateTime? UpdatedAt
		{
			get;
			set;
		}
		#endregion
	}
}

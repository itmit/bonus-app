using System;
using bonus.app.Core.ViewModels.Chats;
using MvvmCross.ViewModels;
using Newtonsoft.Json;

namespace bonus.app.Core.Models
{
	public class Dialog
	{
		public int Id
		{
			get;
			set;
		}

		public User UserTo
		{
			get;
			set;
		}

		public Message LastMessage
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
	}
}

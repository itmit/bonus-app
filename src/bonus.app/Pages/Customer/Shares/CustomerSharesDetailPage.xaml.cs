using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels.Customer.Shares;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Pages.Customer.Shares
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomerSharesDetailPage : MvxContentPage<CustomerSharesDetailViewModel>
	{
		public CustomerSharesDetailPage()
		{
			InitializeComponent();
		}
	}
}
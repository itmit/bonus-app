using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bonus.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : MvxContentPage<ChatViewModel>
    {
        public ChatPage()
        {
            InitializeComponent();

			Grid.BackgroundColor = Color.FromRgba(160,150,142,76);
		}

		public void OnListTapped(object sender, ItemTappedEventArgs e)
		{
			ChatInput.UnFocusEntry();
		}
	}
}
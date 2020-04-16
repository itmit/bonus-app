using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonus.app.Core.Views.ViewCells.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();

            Frame.BackgroundColor = Color.FromRgba(160, 150, 142, 76);
        }

		public void UnFocusEntry()
		{
			ChatTextInput?.Unfocus();
		}
    }
}
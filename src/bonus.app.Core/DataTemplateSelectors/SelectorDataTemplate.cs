using bonus.app.Core.Models;
using bonus.app.Core.Views.ContentViews.Chats;
using Xamarin.Forms;

namespace bonus.app.Core.Views.ViewCells.Chat
{
	public class SelectorDataTemplate : DataTemplateSelector
	{
		#region Data
		#region Fields
		private readonly DataTemplate _textInDataTemplate;
		private readonly DataTemplate _textOutDataTemplate;
		#endregion
		#endregion

		#region .ctor
		public SelectorDataTemplate()
		{
			_textInDataTemplate = new DataTemplate(typeof(TextInContentView));
			_textOutDataTemplate = new DataTemplate(typeof(TextOutContentView));
		}
		#endregion

		#region Overrided
		/// <param name="item">The data for which to return a template.</param>
		/// <param name="container">
		/// An optional container object in which the developer may have opted to store
		/// <see cref="T:Xamarin.Forms.DataTemplateSelector" /> objects.
		/// </param>
		/// <summary>
		/// The developer overrides this method to return a valid data template for the specified <paramref name="item" />
		/// . This method is called by
		/// <see cref="M:Xamarin.Forms.DataTemplateSelector.SelectTemplate(System.Object,Xamarin.Forms.BindableObject)" />.
		/// </summary>
		/// <returns>
		/// A developer-defined <see cref="T:Xamarin.Forms.DataTemplate" /> that can be used to display
		/// <paramref name="item" />.
		/// </returns>
		/// <remarks>
		///     <para>
		///     This method causes
		///     <see cref="M:Xamarin.Forms.DataTemplateSelector.SelectTemplate(System.Object,Xamarin.Forms.BindableObject)" /> to
		///     throw an exception if it returns an instance of <see cref="T:Xamarin.Forms.DataTemplateSelector" />.
		///     </para>
		/// </remarks>
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			var messageVm = item as Message;
			if (messageVm == null)
			{
				return null;
			}

			return messageVm.IsTextOut ? _textOutDataTemplate : _textInDataTemplate;
		}
		#endregion
	}
}

using Xamarin.Forms;

namespace bonus.app.Core.Controls
{
	public class ExtendedEditorControl : Editor
	{
		#region Data
		#region Static
		public static readonly BindableProperty HasRoundedCornerProperty = BindableProperty.Create(nameof(HasRoundedCorner), typeof(bool), typeof(ExtendedEditorControl), false);

		public static readonly BindableProperty IsExpandableProperty = BindableProperty.Create(nameof(IsExpandable), typeof(bool), typeof(ExtendedEditorControl), false);
		#endregion
		#endregion

		#region .ctor
		public ExtendedEditorControl() => TextChanged += OnTextChanged;
		#endregion

		#region .dtor
		~ExtendedEditorControl() => TextChanged -= OnTextChanged;
		#endregion

		#region Properties
		public bool HasRoundedCorner
		{
			get => (bool) GetValue(HasRoundedCornerProperty);
			set => SetValue(HasRoundedCornerProperty, value);
		}

		public bool IsExpandable
		{
			get => (bool) GetValue(IsExpandableProperty);
			set => SetValue(IsExpandableProperty, value);
		}
		#endregion

		#region Private
		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (IsExpandable)
			{
				InvalidateMeasure();
			}
		}
		#endregion
	}
}

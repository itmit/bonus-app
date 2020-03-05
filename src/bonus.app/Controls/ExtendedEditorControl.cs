using Xamarin.Forms;

namespace bonus.app.Core.Controls
{
	public class ExtendedEditorControl : Editor
	{
		public static readonly BindableProperty HasRoundedCornerProperty
			= BindableProperty.Create(nameof(HasRoundedCorner), 
				typeof(bool), 
				typeof(ExtendedEditorControl), 
				false);

		public static readonly BindableProperty IsExpandableProperty
			= BindableProperty.Create(nameof(IsExpandable), 
									  typeof(bool), 
									  typeof(ExtendedEditorControl), 
									  false);

		public bool IsExpandable
		{
			get => (bool)GetValue(IsExpandableProperty);
			set => SetValue(IsExpandableProperty, value);
		}
		public bool HasRoundedCorner
		{
			get => (bool)GetValue(HasRoundedCornerProperty);
			set => SetValue(HasRoundedCornerProperty, value);
		}

		public ExtendedEditorControl() => TextChanged += OnTextChanged;

		~ExtendedEditorControl() => TextChanged -= OnTextChanged;

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			if (IsExpandable)
			{
				InvalidateMeasure();
			}
		}
	}
}

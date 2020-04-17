using System.Collections.Generic;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace bonus.app.Core.Behaviors
{
	public class MaterialTextFieldMaskedBehavior : Behavior<MaterialTextField>
	{
		#region Data
		#region Fields
		private string _mask = "";

		private IDictionary<int, char> _positions;
		#endregion
		#endregion

		#region Properties
		public string Mask
		{
			get => _mask;
			set
			{
				_mask = value;
				SetPositions();
			}
		}
		#endregion

		#region Overrided
		protected override void OnAttachedTo(MaterialTextField materialTextField)
		{
			materialTextField.TextChanged += OnMaterialTextFieldTextChanged;
			base.OnAttachedTo(materialTextField);
		}

		protected override void OnDetachingFrom(MaterialTextField MaterialTextField)
		{
			MaterialTextField.TextChanged -= OnMaterialTextFieldTextChanged;
			base.OnDetachingFrom(MaterialTextField);
		}
		#endregion

		#region Private
		private void OnMaterialTextFieldTextChanged(object sender, TextChangedEventArgs args)
		{
			if (sender is MaterialTextField materialTextField)
			{
				var text = materialTextField.Text;

				if (string.IsNullOrWhiteSpace(text) || _positions == null)
				{
					return;
				}

				if (text.Length > _mask.Length)
				{
					materialTextField.Text = text.Remove(text.Length - 1);
					return;
				}

				foreach (var position in _positions)
				{
					if (text.Length >= position.Key + 1)
					{
						var value = position.Value.ToString();
						if (text.Substring(position.Key, 1) != value)
						{
							text = text.Insert(position.Key, value);
						}
					}
				}

				if (materialTextField.Text != text)
				{
					materialTextField.Text = text;
				}
			}
		}

		private void SetPositions()
		{
			if (string.IsNullOrEmpty(Mask))
			{
				_positions = null;
				return;
			}

			var list = new Dictionary<int, char>();
			for (var i = 0; i < Mask.Length; i++)
			{
				if (Mask[i] != 'X')
				{
					list.Add(i, Mask[i]);
				}
			}

			_positions = list;
		}
		#endregion
	}
}

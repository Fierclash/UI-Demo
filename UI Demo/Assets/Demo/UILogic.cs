/// Logic Script
/// Implements specific logic actions for UI handling.

using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Demo
{
	public static class UILogic
	{
		public static void SetDropdownUIField(DropdownUIField field, string label, 
			string[] options, int index)
		{
			field.labelText.SetText(label);
			field.dropdown.ClearOptions();
			field.dropdown.AddOptions(options.ToList());
			field.dropdown.value = index;
		}

		public static void SetToggleUIField(ToggleUIField field, string label, bool value,
			string trueLabel, string falseLabel)
		{
			field.labelText.SetText(label);
			field.toggle.isOn = value;
			field.valueText.SetText(value ? trueLabel : falseLabel);
		}

		public static void SetToggleUIFieldValueText(ToggleUIField field, bool value,
			string trueLabel, string falseLabel)
		{
			field.valueText.SetText(value ? trueLabel : falseLabel);
		}

		public static void SetSpinBoxUIField(SpinBoxUIField field, string label,
			string[] options, int index)
		{
			field.labelText.SetText(label);
			field.valueText.SetText(options[index]);
			field.decrementButton.interactable = index > 0;
			field.incrementButton.interactable = index < options.Length - 1;
		}

		public static void DecrementSpinBoxValue(SpinBoxUIField field, string[] options,
			ref int index)
		{
			index = Mathf.Max(index - 1, 0);
			field.valueText.SetText(options[index]);
			field.decrementButton.interactable = index > 0;
			field.incrementButton.interactable = index < options.Length - 1;
		}

		public static void IncrementSpinBoxValue(SpinBoxUIField field, string[] options,
			ref int index)
		{
			index = Mathf.Min(index + 1, options.Length - 1);
			field.valueText.SetText(options[index]);
			field.decrementButton.interactable = index > 0;
			field.incrementButton.interactable = index < options.Length - 1;
		}
	}
}

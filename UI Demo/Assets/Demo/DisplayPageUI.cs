/// #DataStructScript

using UnityEngine;

namespace Demo
{
	public class DisplayPageUI : MonoBehaviour
	{
		[Header("Components")]
		public DropdownUIField displayModeDropdown;
		public DropdownUIField screenResolutionDropdown;
		public ToggleUIField antiAliasingToggle;
		public ToggleUIField vsyncToggle;
		public SpinBoxUIField maxFrameRateSpinBox;
	}
}

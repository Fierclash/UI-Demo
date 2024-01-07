/// UI Component Reference Script
/// Holds references to specific components of the UI.

using UnityEngine;
using TMPro;

namespace Demo
{
	public class DropdownUIField : MonoBehaviour
	{
		[Header("Components")]
		public RectTransform host;
		public CanvasGroup canvasGroup;
		public TextMeshProUGUI labelText;
		public TMP_Dropdown dropdown;
	}
}

/// UI Component Reference Script
/// Holds references to specific components of the UI.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo
{
	public class SpinBoxUIField : MonoBehaviour
	{
		[Header("Components")]
		public RectTransform host;
		public CanvasGroup canvasGroup;
		public TextMeshProUGUI labelText;
		public TextMeshProUGUI valueText;
		public Button decrementButton;
		public Button incrementButton;
	}
}

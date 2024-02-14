/// #DataStructScript

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo
{
	public class ConfirmDialogUI : MonoBehaviour
	{
		[Header("Components")]
		public RectTransform host;
		public CanvasGroup canvasGroup;
		public Image blockerImage;
		public Image panelImage;
		public TextMeshProUGUI labelText;
		public Button confirmButton;
		public Button declineButton;
	}
}

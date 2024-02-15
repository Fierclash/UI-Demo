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
		public Button acceptButton;
		public TextMeshProUGUI acceptText;
		public Button declineButton;
		public TextMeshProUGUI declineText;
	}
}

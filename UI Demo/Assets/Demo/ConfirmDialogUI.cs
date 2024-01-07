/// 

using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

	public class QuitGameDialogUIHandler
	{
		public ConfirmDialogUI dialog;
		
		public void Link()
		{
			dialog.confirmButton.onClick.AddListener(HandleOnConfirm);
			dialog.declineButton.onClick.AddListener(HandleOnDecline);
		}

		public void Unlink()
		{
			dialog.confirmButton.onClick.RemoveListener(HandleOnConfirm);
			dialog.declineButton.onClick.RemoveListener(HandleOnDecline);
		}

		private void HandleOnConfirm()
		{
			Application.Quit();
		}

		private void HandleOnDecline()
		{
			dialog.gameObject.SetActive(false);
		}
	}
}

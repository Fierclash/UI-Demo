/// #LogicScript

using UnityEngine;

namespace Demo
{
	public class QuitGameDialogUIHandler
	{
		public Settings settings;
		public ConfirmDialogUIConfig config;
		public ConfirmDialogUI dialog;

		public void Link()
		{
			dialog.labelText.SetText(config.messageLabel);
			dialog.acceptText.SetText(config.acceptLabel);
			dialog.declineText.SetText(config.declineLabel);

			dialog.acceptButton.onClick.AddListener(HandleOnConfirm);
			dialog.declineButton.onClick.AddListener(HandleOnDecline);
		}

		public void Unlink()
		{
			dialog.acceptButton.onClick.RemoveListener(HandleOnConfirm);
			dialog.declineButton.onClick.RemoveListener(HandleOnDecline);
		}

		private void HandleOnConfirm()
		{
			SettingsPortLogic.ExportSettingsToJson(settings, Application.streamingAssetsPath + "/Settings.json", true);
			Application.Quit();
		}

		private void HandleOnDecline()
		{
			dialog.gameObject.SetActive(false);
		}
	}
}

/// #LogicScript

using UnityEngine;

namespace Demo
{
	public class QuitGameDialogUIHandler
	{
		public Settings settings;
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
			SettingsPortLogic.ExportSettingsToJson(settings, Application.streamingAssetsPath + "/Settings.json", true);
			Application.Quit();
		}

		private void HandleOnDecline()
		{
			dialog.gameObject.SetActive(false);
		}
	}
}

/// 

using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
	public class GeneralPageUI : MonoBehaviour
	{
		[Header("Components")]
		public Button quitButton;
		public GameObject dialogGameObject;
		public ConfirmDialogUI dialogUI;

		GeneralPageUIExternalHandler handler;
		QuitGameDialogUIHandler quitHandler;

		private void Awake()
		{
			handler = new();
			handler.quitButton = quitButton;
			handler.dialogGameObject = dialogGameObject;
			handler.Link();

			quitHandler = new();
			quitHandler.dialog = dialogUI;
			quitHandler.Link();

		}
	}
}

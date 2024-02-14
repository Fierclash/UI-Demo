/// #LogicScript

using UnityEngine;

namespace Demo
{
	public class SettingsUIController : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private SettingsUI UI;

		private TabUIHandler tabUIHandler;

		public void InitController()
		{
			_InitTabUIHandler();

			void _InitTabUIHandler()
			{
				if (tabUIHandler == null) tabUIHandler = new();
				tabUIHandler.generalPageButton = UI.generalPageButton;
				tabUIHandler.displayButton = UI.displayButton;
				tabUIHandler.audioPageButton = UI.audioPageButton;
				tabUIHandler.generalPageGameObject = UI.generalPageGameObject;
				tabUIHandler.displayPageGameObject = UI.displayPageGameObject;
				tabUIHandler.audioPageGameObject = UI.audioPageGameObject;
			}
		}

		public void EnableUI()
		{
			UI.gameObject.SetActive(true);
			tabUIHandler.Link();
		}

		public void DisableUI()
		{
			UI.gameObject.SetActive(false);
			tabUIHandler.Unlink();
		}

		public void ToggleUI()
		{
			if (UI.gameObject.activeSelf) DisableUI();
			else EnableUI();
		}
	}
}

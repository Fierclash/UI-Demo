/// 

using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
	public class GeneralPageUIExternalHandler
	{
		public Button quitButton;
		public GameObject dialogGameObject;

		public void Link()
		{
			quitButton.onClick.AddListener(HandleOnQuitClicked);
		}

		public void Unlink()
		{
			quitButton.onClick.RemoveListener(HandleOnQuitClicked);
		}

		private void HandleOnQuitClicked()
		{
			dialogGameObject.SetActive(true);
		}
	}
}

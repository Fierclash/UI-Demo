/// #LogicScript

using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
	public class TabUIHandler
	{
		public Button generalPageButton;
		public Button displayButton;
		public Button audioPageButton;
		public GameObject generalPageGameObject;
		public GameObject displayPageGameObject;
		public GameObject audioPageGameObject;

		public void Link()
		{
			generalPageButton.onClick.AddListener(HandleOnGeneralClick);
			displayButton.onClick.AddListener(HandleOnDisplayClick);
			audioPageButton.onClick.AddListener(HandleOnAudioClick);
		}

		public void Unlink()
		{
			generalPageButton.onClick.RemoveListener(HandleOnGeneralClick);
			displayButton.onClick.RemoveListener(HandleOnDisplayClick);
			audioPageButton.onClick.RemoveListener(HandleOnAudioClick);
		}

		private void HandleOnGeneralClick()
		{
			DisableAllPages();
			generalPageGameObject.SetActive(true);
		}

		private void HandleOnDisplayClick()
		{
			DisableAllPages();
			displayPageGameObject.SetActive(true);
		}

		private void HandleOnAudioClick()
		{
			DisableAllPages();
			audioPageGameObject.SetActive(true);
		}

		private void DisableAllPages()
		{
			generalPageGameObject.SetActive(false);
			displayPageGameObject.SetActive(false);
			audioPageGameObject.SetActive(false);
		}
	}
}

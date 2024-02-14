/// #LogicScript

using UnityEngine;

namespace Demo
{
	public class SettingsUIController : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private SettingsUI UI;

		public void EnableUI()
		{
			UI.gameObject.SetActive(true);
		}

		public void DisableUI()
		{
			UI.gameObject.SetActive(false);
		}

		public void ToggleUI()
		{
			UI.gameObject.SetActive(!UI.gameObject.activeSelf);
		}
	}
}

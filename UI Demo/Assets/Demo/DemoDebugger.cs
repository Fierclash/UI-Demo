/// #DebugScript

using System.Collections;
using UnityEngine;

namespace Demo
{
	public class DemoDebugger : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private SettingsUIController controller;

		[Header("Settings")]
		[SerializeField] private bool inputEnabled;
		[SerializeField] private KeyCode toggleUIKey;

		[ContextMenu("T")]
		private void OnEnable()
		{
			Debug.Log("[DEMO_DEBUGGER] Starting Debugger.");

			StartCoroutine(_Load()); // Coroutine since audio initialization has issues when calle during OnEnable

			IEnumerator _Load()
			{
				yield return null;
				controller.InitController();
				controller.DisableUI();
			}
		}

		private void Update()
		{
			if (inputEnabled) DetectToggleUIInput();
		}

		private void DetectToggleUIInput()
		{
			if (Input.GetKeyDown(toggleUIKey))
			{
				Debug.Log("[DEMO_DEBUGGER] Toggling UI.");
				controller.ToggleUI();
			}
		}
	}
}

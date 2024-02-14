/// #DebugScript

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
		
		private void Start()
		{
			Debug.Log("[DEMO_DEBUGGER] Starting Debugger.");
			controller.DisableUI();
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

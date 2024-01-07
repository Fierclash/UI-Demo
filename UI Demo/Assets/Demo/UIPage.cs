/// UI Component Reference Script
/// Holds references to specific components of the UI.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Demo
{
	public interface IUIState
	{
		public void EnableUI();
		public void DisableUI();
	}
}

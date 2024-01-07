/// Data Struct Script
/// Scriptable object for all settings data.

using UnityEngine;

namespace Demo
{
	[CreateAssetMenu(fileName = "Settings_", menuName = "Demo/Data/Settings")]
	public class Settings : ScriptableObject
	{
		public DisplaySettings displaySettings;
	}
}
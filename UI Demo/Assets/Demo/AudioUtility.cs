/// #UtilityScript

using UnityEngine;

namespace Demo
{
	public static class AudioUtility
	{
		public static readonly float MIN_VOLUME = 0.00001f;
		public static readonly float MAX_VOLUME = 1f;

		public static float ConvertPercentToDecibels(float value)
		{
			return Mathf.Log10(value) * 20f;
		}

		public static float ClampVolumePercent(float value)
		{
			return Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME);
		}
	}
}

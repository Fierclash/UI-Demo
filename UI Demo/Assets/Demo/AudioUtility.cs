/// #UtilityScript

using UnityEngine;

namespace Demo
{
	public static class AudioUtility
	{
		public static float ConvertPercentToDecibels(float value)
		{
			return Mathf.Log10(value) * 20f;
		}
	}
}

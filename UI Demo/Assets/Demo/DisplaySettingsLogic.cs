/// #LogicScript

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Demo
{
	public static class DisplaySettingsLogic
	{
		public static void SetDisplayMode(int displayModeIndex)
		{
			FullScreenMode fullscreenMode;
			switch (displayModeIndex)
			{
				case 0: fullscreenMode = FullScreenMode.MaximizedWindow; break;
				case 1: fullscreenMode = FullScreenMode.FullScreenWindow; break;
				case 2: fullscreenMode = FullScreenMode.Windowed; break;
				default: fullscreenMode = FullScreenMode.Windowed; break;
			}
			Screen.SetResolution(Screen.width, Screen.height, fullscreenMode);
		}

		public static void SetAntiAliasing(bool antiAliasing)
		{
			Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = antiAliasing ?
																					AntialiasingMode.SubpixelMorphologicalAntiAliasing :
																					AntialiasingMode.None;
		}

		public static void SetVSync(bool vsync)
		{
			QualitySettings.vSyncCount = vsync ? 1 : 0;
		}
	}
}

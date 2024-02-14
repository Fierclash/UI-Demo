/// #DataStructScript
/// Holds data for game display settings.

namespace Demo
{
	[System.Serializable]
	public struct DisplaySettings // Avoid ref issues, pass-by-value only
	{
		public int displayMode; // Index for an ordered set of display modes
		public int screenResolutionWidth; // Number of pixels for screen width
		public int screenResolutionHeight; // Number of pixels for screen height
		public bool antiAliasing; // True if camera should render with anti-aliasing
		public bool vsync; // True if V-Sync should be enabled
		public int maxFrameRate; // Maximum number of frames to render per second (<= 0 implies limitless)
	}
}

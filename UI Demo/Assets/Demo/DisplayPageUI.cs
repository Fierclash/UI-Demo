/// UI Component Reference Script
/// Holds references to specific components of the UI.

using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Demo
{
	public class DisplayPageUI : MonoBehaviour
	{
		[Header("Components")]
		public Settings settings;
		public DropdownUIField displayModeDropdown;
		public DropdownUIField screenResolutionDropdown;
		public ToggleUIField antiAliasingToggle;
		public ToggleUIField vsyncToggle;
		public SpinBoxUIField maxFrameRateSpinBox;

		DisplayPageUIHandler handler;

		private void Awake()
		{
			handler = new();
			handler.settings = settings;
			handler.displayPage = this;
			handler.config.displayModeLabel = "Display Mode";
			handler.config.screenResolutionLabel = "Screen Resolution";
			handler.config.antiAliasingLabel = "Anti-Aliasing";
			handler.config.vsyncLabel = "V-Sync";
			handler.config.maxFrameRateLabel = "Frame Rate Limit";
			handler.config.enableLabel = "ENABLED";
			handler.config.disableLabel = "DISABLED";
			handler.config.displayModes = new string[]
			{ 
				"Fullscreen", 
				"Borderless", 
				"Windowed" 
			};
			handler.config.screenResolutions = new Vector2Int[]
			{ 
				new Vector2Int(1920, 1080),
				new Vector2Int(1366, 768),
				new Vector2Int(1440, 900),
				new Vector2Int(1280, 720),
				new Vector2Int(1280, 1024),
			};
			handler.config.maxFrameRates = new int[] 
			{ 
				30,
				60,
				120,
				-1,
			};
			handler.config.maxFrameRateLabels = new string[] 
			{ 
				"30 FPS", 
				"60 FPS",
				"120 FPS",
				"Unlimited FPS"
			};
			handler.Link();
		}

		private void OnDestroy()
		{
			handler.Unlink();
		}
	}

	public struct DisplayPageConfig
	{
		public string displayModeLabel;
		public string[] displayModes;
		public string screenResolutionLabel;
		public Vector2Int[] screenResolutions;
		public string antiAliasingLabel;
		public string vsyncLabel;
		public string maxFrameRateLabel;
		public int[] maxFrameRates;
		public string[] maxFrameRateLabels;
		public string enableLabel;
		public string disableLabel;
	}

	public class DisplayPageUIHandler
	{
		public Settings settings;
		public DisplayPageConfig config;
		public DisplayPageUI displayPage;

		private int displayModeIndex;
		private int screenResolutionIndex;
		private int maxFrameRatesIndex;

		public void Link()
		{
			// Load data from Settings and update runtime data
			var displaySettings = settings.displaySettings;
			displayModeIndex = displaySettings.displayMode;
			Vector2Int resolution = new Vector2Int(displaySettings.screenResolutionWidth,
													displaySettings.screenResolutionHeight);
			screenResolutionIndex = Mathf.Max(config.screenResolutions.ToList().FindIndex(x => x == resolution), 0); // Ensure index is non-negative
			maxFrameRatesIndex = Mathf.Max(config.maxFrameRates.ToList().FindIndex(x => x == displaySettings.maxFrameRate), 0); // Ensure index is non-negative
			
			// Update UI to match Config and Settings data
			UILogic.SetDropdownUIField(displayPage.displayModeDropdown,
										config.displayModeLabel,
										config.displayModes,
										displayModeIndex);
			var resolutionLabels = config.screenResolutions.Select(x => string.Concat(x.x, "x", x.y))
															.ToArray();
			UILogic.SetDropdownUIField(displayPage.screenResolutionDropdown,
										config.screenResolutionLabel,
										resolutionLabels,
										screenResolutionIndex);
			UILogic.SetToggleUIField(displayPage.antiAliasingToggle,
										config.antiAliasingLabel,
										displaySettings.antiAliasing,
										config.enableLabel,
										config.disableLabel);
			UILogic.SetToggleUIField(displayPage.vsyncToggle,
										config.vsyncLabel,
										displaySettings.vsync,
										config.enableLabel,
										config.disableLabel);
			UILogic.SetSpinBoxUIField(displayPage.maxFrameRateSpinBox,
										config.maxFrameRateLabel,
										config.maxFrameRateLabels,
										maxFrameRatesIndex);

			// Subscribe to UI events
			displayPage.displayModeDropdown.dropdown.onValueChanged.AddListener(HandleOnDisplayModeChanged);
			displayPage.screenResolutionDropdown.dropdown.onValueChanged.AddListener(HandleOnScreenResolutionChanged);
			displayPage.antiAliasingToggle.toggle.onValueChanged.AddListener(HandleOnAntiAliasingChanged);
			displayPage.vsyncToggle.toggle.onValueChanged.AddListener(HandleOnVSyncChanged);
			displayPage.maxFrameRateSpinBox.decrementButton.onClick.AddListener(HandleOnMaxFrameRateDecrement);
			displayPage.maxFrameRateSpinBox.incrementButton.onClick.AddListener(HandleOnMaxFrameRateIncrement);
		}

		public void Unlink()
		{
			// Unsubscribe from UI events
			displayPage.displayModeDropdown.dropdown.onValueChanged.RemoveListener(HandleOnDisplayModeChanged);
			displayPage.screenResolutionDropdown.dropdown.onValueChanged.RemoveListener(HandleOnScreenResolutionChanged);
			displayPage.antiAliasingToggle.toggle.onValueChanged.RemoveListener(HandleOnAntiAliasingChanged);
			displayPage.vsyncToggle.toggle.onValueChanged.RemoveListener(HandleOnVSyncChanged);
			displayPage.maxFrameRateSpinBox.decrementButton.onClick.RemoveListener(HandleOnMaxFrameRateDecrement);
			displayPage.maxFrameRateSpinBox.incrementButton.onClick.RemoveListener(HandleOnMaxFrameRateIncrement);
		}

		private void HandleOnDisplayModeChanged(int value)
		{
			displayModeIndex = value;
			settings.displaySettings.displayMode = value;
			FullScreenMode fullscreenMode;
			switch (value)
			{
				case 0: fullscreenMode = FullScreenMode.MaximizedWindow; break;
				case 1: fullscreenMode = FullScreenMode.FullScreenWindow; break;
				case 2: fullscreenMode = FullScreenMode.Windowed; break;
				default: fullscreenMode = FullScreenMode.Windowed; break;
			}
			Screen.SetResolution(Screen.width, Screen.height, fullscreenMode);
		}

		private void HandleOnScreenResolutionChanged(int value)
		{
			Vector2Int resolution = config.screenResolutions[value];
			settings.displaySettings.screenResolutionWidth = resolution.x;
			settings.displaySettings.screenResolutionHeight = resolution.y;
			Screen.SetResolution(resolution.x, resolution.y, Screen.fullScreen);
		}

		private void HandleOnAntiAliasingChanged(bool value)
		{
			UILogic.SetToggleUIFieldValueText(displayPage.antiAliasingToggle,
												value,
												config.enableLabel,
												config.disableLabel);
			settings.displaySettings.antiAliasing = value;
			Camera.main.GetComponent<UniversalAdditionalCameraData>().antialiasing = value ? 
																					AntialiasingMode.SubpixelMorphologicalAntiAliasing :
																					AntialiasingMode.None;
		}

		private void HandleOnVSyncChanged(bool value)
		{
			UILogic.SetToggleUIFieldValueText(displayPage.vsyncToggle,
												value,
												config.enableLabel,
												config.disableLabel);
			settings.displaySettings.vsync = value;
			QualitySettings.vSyncCount = value ? 1 : 0;
		}

		private void HandleOnMaxFrameRateDecrement()
		{
			UILogic.DecrementSpinBoxValue(displayPage.maxFrameRateSpinBox,
											config.maxFrameRateLabels,
											ref maxFrameRatesIndex);
			int frameRate = config.maxFrameRates[maxFrameRatesIndex];
			settings.displaySettings.maxFrameRate = frameRate;
			Application.targetFrameRate = frameRate;
		}

		private void HandleOnMaxFrameRateIncrement()
		{
			UILogic.IncrementSpinBoxValue(displayPage.maxFrameRateSpinBox,
											config.maxFrameRateLabels,
											ref maxFrameRatesIndex);
			int frameRate = config.maxFrameRates[maxFrameRatesIndex];
			settings.displaySettings.maxFrameRate = frameRate;
			Application.targetFrameRate = frameRate;
		}
	}
}

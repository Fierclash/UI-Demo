/// UI Component Reference Script
/// Holds references to specific components of the UI.

using System.Linq;
using UnityEngine;

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
			displayPage.displayModeDropdown.dropdown.ClearOptions();
			displayPage.displayModeDropdown.dropdown.AddOptions(config.displayModes.ToList());
			displayPage.displayModeDropdown.dropdown.value = displayModeIndex;
			displayPage.displayModeDropdown.labelText.SetText(config.displayModeLabel);
			displayPage.screenResolutionDropdown.dropdown.ClearOptions();
			displayPage.screenResolutionDropdown.dropdown.AddOptions(config.screenResolutions.Select(x => string.Concat(x.x, "x", x.y)).ToList());
			displayPage.screenResolutionDropdown.dropdown.value = screenResolutionIndex;
			displayPage.screenResolutionDropdown.labelText.SetText(config.screenResolutionLabel);
			displayPage.antiAliasingToggle.toggle.isOn = displaySettings.antiAliasing;
			displayPage.antiAliasingToggle.valueText.SetText(displaySettings.antiAliasing ? config.enableLabel : config.disableLabel);
			displayPage.antiAliasingToggle.labelText.SetText(config.antiAliasingLabel);
			displayPage.vsyncToggle.toggle.isOn = displaySettings.vsync;
			displayPage.vsyncToggle.valueText.SetText(displaySettings.vsync ? config.enableLabel : config.disableLabel);
			displayPage.vsyncToggle.labelText.SetText(config.vsyncLabel);
			displayPage.maxFrameRateSpinBox.valueText.SetText(config.maxFrameRateLabels[maxFrameRatesIndex]);
			displayPage.maxFrameRateSpinBox.decrementButton.interactable = maxFrameRatesIndex > 0;
			displayPage.maxFrameRateSpinBox.incrementButton.interactable = maxFrameRatesIndex < config.maxFrameRateLabels.Length - 1;
			displayPage.maxFrameRateSpinBox.labelText.SetText(config.maxFrameRateLabel);

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
		}

		private void HandleOnScreenResolutionChanged(int value)
		{
			Vector2Int resolution = config.screenResolutions[value];
			settings.displaySettings.screenResolutionWidth = resolution.x;
			settings.displaySettings.screenResolutionHeight = resolution.y;
		}

		private void HandleOnAntiAliasingChanged(bool value)
		{
			settings.displaySettings.antiAliasing = value;
			displayPage.antiAliasingToggle.valueText.SetText(value ? config.enableLabel : config.disableLabel);
		}

		private void HandleOnVSyncChanged(bool value)
		{
			settings.displaySettings.vsync = value;
			displayPage.vsyncToggle.valueText.SetText(value ? config.enableLabel : config.disableLabel);
		}

		private void HandleOnMaxFrameRateDecrement()
		{
			maxFrameRatesIndex = Mathf.Max(maxFrameRatesIndex - 1, 0);
			settings.displaySettings.maxFrameRate = config.maxFrameRates[maxFrameRatesIndex];
			displayPage.maxFrameRateSpinBox.valueText.SetText(config.maxFrameRateLabels[maxFrameRatesIndex]);
			displayPage.maxFrameRateSpinBox.decrementButton.interactable = maxFrameRatesIndex > 0;
			displayPage.maxFrameRateSpinBox.incrementButton.interactable = maxFrameRatesIndex < config.maxFrameRateLabels.Length - 1;
		}

		private void HandleOnMaxFrameRateIncrement()
		{
			maxFrameRatesIndex = Mathf.Min(maxFrameRatesIndex + 1, config.maxFrameRates.Length - 1);
			settings.displaySettings.maxFrameRate = config.maxFrameRates[maxFrameRatesIndex];
			displayPage.maxFrameRateSpinBox.valueText.SetText(config.maxFrameRateLabels[maxFrameRatesIndex]);
			displayPage.maxFrameRateSpinBox.decrementButton.interactable = maxFrameRatesIndex > 0;
			displayPage.maxFrameRateSpinBox.incrementButton.interactable = maxFrameRatesIndex < config.maxFrameRateLabels.Length - 1;
		}
	}
}

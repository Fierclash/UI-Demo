/// #LogicScript

using UnityEngine;
using UnityEngine.Audio;

namespace Demo
{
	public class SettingsUIController : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private SettingsUI settingsUI;
		[SerializeField] private GeneralPageUI generalPageUI;
		[SerializeField] private ConfirmDialogUI quitGameDialogUI;
		[SerializeField] private DisplayPageUI displayPageUI;
		[SerializeField] private AudioPageUI audioPageUI;

		[Header("Data")]
		[SerializeField] private Settings settings;
		[SerializeField] private AudioMixerGroup mixerGroup;

		private TabUIHandler tabUIHandler;
		private GeneralPageUIHandler generalPageUIHandler;
		private QuitGameDialogUIHandler quitGameDialogUIHandler;
		private DisplayPageUIHandler displayPageUIHandler;
		private AudioPageUIHandler audioPageUIHandler;

		public void InitController()
		{
			_LoadSettings();
			_LoadDisplaySettings();
			_LoadAudioSettings();

			_InitTabUIHandler();
			_InitGeneralPageUIHandler();
			_InitQuitGameDialogUIHandler();
			_InitDisplayPageUIHandler();
			_InitAudioPageUIHandler();

			void _LoadSettings()
			{
				SettingsPortLogic.ImportSettingsFromJson(ref settings, Application.streamingAssetsPath + "/Settings.json");
			}

			void _LoadDisplaySettings()
			{
				DisplaySettingsLogic.SetDisplayMode(settings.displaySettings.displayMode);
				Screen.SetResolution(settings.displaySettings.screenResolutionWidth,
									settings.displaySettings.screenResolutionHeight,
									Screen.fullScreen);
				DisplaySettingsLogic.SetAntiAliasing(settings.displaySettings.antiAliasing);
				DisplaySettingsLogic.SetVSync(settings.displaySettings.vsync);
				Application.targetFrameRate = settings.displaySettings.maxFrameRate;
			}
			
			void _LoadAudioSettings()
			{
				float masterVolume = AudioUtility.ClampVolumePercent(settings.audioSettings.masterVolume);
				mixerGroup.audioMixer.SetFloat("MasterVolume", 
												AudioUtility.ConvertPercentToDecibels(masterVolume));
				float SFXVolume = AudioUtility.ClampVolumePercent(settings.audioSettings.SFXVolume);
				mixerGroup.audioMixer.SetFloat("SFXVolume", 
												AudioUtility.ConvertPercentToDecibels(SFXVolume));
				float BGMVolume = AudioUtility.ClampVolumePercent(settings.audioSettings.BGMVolume);
				mixerGroup.audioMixer.SetFloat("BGMVolume",
												AudioUtility.ConvertPercentToDecibels(BGMVolume));
			}

			void _InitTabUIHandler()
			{
				if (tabUIHandler == null) tabUIHandler = new();
				tabUIHandler.generalPageButton = settingsUI.generalPageButton;
				tabUIHandler.displayButton = settingsUI.displayButton;
				tabUIHandler.audioPageButton = settingsUI.audioPageButton;
				tabUIHandler.generalPageGameObject = settingsUI.generalPageGameObject;
				tabUIHandler.displayPageGameObject = settingsUI.displayPageGameObject;
				tabUIHandler.audioPageGameObject = settingsUI.audioPageGameObject;
			}

			void _InitGeneralPageUIHandler()
			{
				if (generalPageUIHandler == null) generalPageUIHandler = new();
				generalPageUIHandler = new();
				generalPageUIHandler.quitButton = generalPageUI.quitButton;
				generalPageUIHandler.dialogGameObject = generalPageUI.dialogGameObject;
			}

			void _InitQuitGameDialogUIHandler()
			{
				if (quitGameDialogUIHandler == null) quitGameDialogUIHandler = new();
				quitGameDialogUIHandler.settings = settings;
				quitGameDialogUIHandler.dialog = quitGameDialogUI;

				quitGameDialogUIHandler.config.messageLabel = "Are you sure you want to quit the game?";
				quitGameDialogUIHandler.config.acceptLabel = "Quit";
				quitGameDialogUIHandler.config.declineLabel = "Cancel";
			}

			void _InitDisplayPageUIHandler()
			{
				if (displayPageUIHandler == null) displayPageUIHandler = new();

				displayPageUIHandler.settings = settings;
				displayPageUIHandler.displayPage = displayPageUI;
				displayPageUIHandler.config.displayModeLabel = "Display Mode";
				displayPageUIHandler.config.screenResolutionLabel = "Screen Resolution";
				displayPageUIHandler.config.antiAliasingLabel = "Anti-Aliasing";
				displayPageUIHandler.config.vsyncLabel = "V-Sync";
				displayPageUIHandler.config.maxFrameRateLabel = "Frame Rate Limit";
				displayPageUIHandler.config.enableLabel = "ENABLED";
				displayPageUIHandler.config.disableLabel = "DISABLED";
				displayPageUIHandler.config.displayModes = new string[]
				{
					"Fullscreen",
					"Borderless",
					"Windowed"
				};
				displayPageUIHandler.config.screenResolutions = new Vector2Int[]
				{
					new Vector2Int(1920, 1080),
					new Vector2Int(1366, 768),
					new Vector2Int(1440, 900),
					new Vector2Int(1280, 720),
					new Vector2Int(1280, 1024),
				};
				displayPageUIHandler.config.maxFrameRates = new int[]
				{
					30,
					60,
					120,
					-1,
				};
				displayPageUIHandler.config.maxFrameRateLabels = new string[]
				{
					"30 FPS",
					"60 FPS",
					"120 FPS",
					"Unlimited FPS"
				};
			}

			void _InitAudioPageUIHandler()
			{
				if (audioPageUIHandler == null) audioPageUIHandler = new();
				audioPageUIHandler.settings = settings;
				audioPageUIHandler.mixerGroup = mixerGroup;
				audioPageUIHandler.audioPage = audioPageUI;

				audioPageUIHandler.config.masterLabel = "Master";
				audioPageUIHandler.config.SFXLabel = "SFX";
				audioPageUIHandler.config.BGMLabel = "BGM";
			}
		}

		public void EnableUI()
		{
			_Link();
			settingsUI.gameObject.SetActive(true);

			void _Link()
			{
				tabUIHandler.Link();
				generalPageUIHandler.Link();
				quitGameDialogUIHandler.Link();
				displayPageUIHandler.Link();
				audioPageUIHandler.Link();
			}
		}

		public void DisableUI()
		{
			_Unlink();
			settingsUI.gameObject.SetActive(false);

			void _Unlink()
			{
				tabUIHandler.Unlink();
				generalPageUIHandler.Unlink();
				quitGameDialogUIHandler.Unlink();
				displayPageUIHandler.Unlink();
				audioPageUIHandler.Unlink();
			}
		}

		public void ToggleUI()
		{
			if (settingsUI.gameObject.activeSelf) DisableUI();
			else EnableUI();
		}
	}
}

/// 

using UnityEngine;
using UnityEngine.Audio;

namespace Demo
{
	public class AudioPageUI : MonoBehaviour
	{
		[Header("Components")]
		public Settings settings;
		public AudioMixerGroup mixerGroup;
		public SliderUIField masterSlider;
		public SliderUIField sfxSlider;
		public SliderUIField bgmSlider;

		AudioPageUIHandler handler;

		private void Awake()
		{
			handler = new();
			handler.settings = settings;
			handler.mixerGroup = mixerGroup;
			handler.audioPage = this;
			handler.config.masterLabel = "Master";
			handler.config.sfxLabel = "SFX";
			handler.config.bgmLabel = "BGM";
			handler.Link();
		}
	}

	public struct AudioPageConfig
	{
		public string masterLabel;
		public string sfxLabel;
		public string bgmLabel;
	}

	[System.Serializable]
	public struct AudioSettings
	{
		public float masterVolume;
		public float sfxVolume;
		public float bgmVolume;
	}

	public static class AudioUtility
	{
		public static float ConvertPercentToDecibels(float value)
		{
			return Mathf.Log10(value) * 20f;
		}
	}

	public class AudioPageUIHandler
	{
		public Settings settings;
		public AudioPageConfig config;
		public AudioPageUI audioPage;
		public AudioMixerGroup mixerGroup;

		public void Link()
		{
			// Update UI to stored Config and Settings data
			float minVolume = .0001f; // 0% as decibels does not create zero volume, use very small number as min instead
			float maxVolume = 1f;
			float masterVolume = settings.audioSettings.masterVolume;
			UILogic.SetSliderUIField(audioPage.masterSlider,
									config.masterLabel,
									masterVolume,
									minVolume,
									maxVolume,
									masterVolume.ToString("0%"));
			float sfxVolume = settings.audioSettings.sfxVolume;
			UILogic.SetSliderUIField(audioPage.sfxSlider,
									config.sfxLabel,
									sfxVolume,
									minVolume,
									maxVolume,
									sfxVolume.ToString("0%"));
			float bgmVolume = settings.audioSettings.bgmVolume;
			UILogic.SetSliderUIField(audioPage.bgmSlider,
									config.bgmLabel,
									bgmVolume,
									minVolume,
									maxVolume,
									bgmVolume.ToString("0%"));

			// Subscribe to UI events
			audioPage.masterSlider.slider.onValueChanged.AddListener(HandleOnMasterChanged);
			audioPage.sfxSlider.slider.onValueChanged.AddListener(HandleOnSFXChanged);
			audioPage.bgmSlider.slider.onValueChanged.AddListener(HandleOnBGMChanged);
		}

		public void Unlink()
		{
			// Unsubscribe from UI events
			audioPage.masterSlider.slider.onValueChanged.RemoveListener(HandleOnMasterChanged);
			audioPage.sfxSlider.slider.onValueChanged.RemoveListener(HandleOnSFXChanged);
			audioPage.bgmSlider.slider.onValueChanged.RemoveListener(HandleOnBGMChanged);
		}

		private void HandleOnMasterChanged(float value)
		{
			audioPage.masterSlider.valueText.SetText(value.ToString("0%"));
			mixerGroup.audioMixer.SetFloat("MasterVolume", AudioUtility.ConvertPercentToDecibels(value));
			settings.audioSettings.masterVolume = value;
		}

		private void HandleOnSFXChanged(float value)
		{
			audioPage.sfxSlider.valueText.SetText(value.ToString("0%"));
			mixerGroup.audioMixer.SetFloat("SFXVolume", AudioUtility.ConvertPercentToDecibels(value));
			settings.audioSettings.sfxVolume = value;
		}

		private void HandleOnBGMChanged(float value)
		{
			audioPage.bgmSlider.valueText.SetText(value.ToString("0%"));
			mixerGroup.audioMixer.SetFloat("BGMVolume", AudioUtility.ConvertPercentToDecibels(value));
			settings.audioSettings.bgmVolume = value;
		}
	}
}

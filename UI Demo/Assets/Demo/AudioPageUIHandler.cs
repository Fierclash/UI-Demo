/// #LogicScript

using UnityEngine.Audio;

namespace Demo
{
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
			float sfxVolume = settings.audioSettings.SFXVolume;
			UILogic.SetSliderUIField(audioPage.SFXSlider,
									config.SFXLabel,
									sfxVolume,
									minVolume,
									maxVolume,
									sfxVolume.ToString("0%"));
			float bgmVolume = settings.audioSettings.BGMVolume;
			UILogic.SetSliderUIField(audioPage.BGMSlider,
									config.BGMLabel,
									bgmVolume,
									minVolume,
									maxVolume,
									bgmVolume.ToString("0%"));

			// Subscribe to UI events
			audioPage.masterSlider.slider.onValueChanged.AddListener(HandleOnMasterChanged);
			audioPage.SFXSlider.slider.onValueChanged.AddListener(HandleOnSFXChanged);
			audioPage.BGMSlider.slider.onValueChanged.AddListener(HandleOnBGMChanged);
		}

		public void Unlink()
		{
			// Unsubscribe from UI events
			audioPage.masterSlider.slider.onValueChanged.RemoveListener(HandleOnMasterChanged);
			audioPage.SFXSlider.slider.onValueChanged.RemoveListener(HandleOnSFXChanged);
			audioPage.BGMSlider.slider.onValueChanged.RemoveListener(HandleOnBGMChanged);
		}

		private void HandleOnMasterChanged(float value)
		{
			audioPage.masterSlider.valueText.SetText(value.ToString("0%"));
			mixerGroup.audioMixer.SetFloat("MasterVolume", AudioUtility.ConvertPercentToDecibels(value));
			settings.audioSettings.masterVolume = value;
		}

		private void HandleOnSFXChanged(float value)
		{
			audioPage.SFXSlider.valueText.SetText(value.ToString("0%"));
			mixerGroup.audioMixer.SetFloat("SFXVolume", AudioUtility.ConvertPercentToDecibels(value));
			settings.audioSettings.SFXVolume = value;
		}

		private void HandleOnBGMChanged(float value)
		{
			audioPage.BGMSlider.valueText.SetText(value.ToString("0%"));
			mixerGroup.audioMixer.SetFloat("BGMVolume", AudioUtility.ConvertPercentToDecibels(value));
			settings.audioSettings.BGMVolume = value;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("Master volume")]
    public string exposedMasterParameter;
    public Slider masterSlider;

    [Header("Music volume")]
    public string exposedMusicParameter;
    public Slider musicSlider;

    [Header("Sound effects")]
    public string exposedEffectsParameter;
    public Slider effectsSlider;

    private void Start()
    {
        SetUpSliders();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(exposedMasterParameter, volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(exposedMusicParameter, volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat(exposedEffectsParameter, volume);
    }

    public void SetUpSliders()
    {
        float sliderValue;
        bool result = audioMixer.GetFloat(exposedMasterParameter, out sliderValue);
        if (result)
        {
            masterSlider.value = sliderValue;
        }

        result = audioMixer.GetFloat(exposedMusicParameter, out sliderValue);
        if (result)
        {
            musicSlider.value = sliderValue;
        }

        result = audioMixer.GetFloat(exposedEffectsParameter, out sliderValue);
        if (result)
        {
            effectsSlider.value = sliderValue;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeController : MonoBehaviour
{
    [SerializeField] private string audioParameter;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private float sliderMultiplier;

    public void SetupVolumeSlider()
    {        
        slider.onValueChanged.AddListener(SliderValue);
        slider.minValue = 0.001f;
        slider.value = PlayerPrefs.GetFloat(audioParameter, slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(audioParameter, slider.value);
    }

    private void SliderValue(float _value)
    {
        audioMixer.SetFloat(audioParameter, Mathf.Log10(_value) * sliderMultiplier);
    }
}

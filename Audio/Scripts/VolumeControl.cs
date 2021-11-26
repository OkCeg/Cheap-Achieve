using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private void Start()
    {
        //defaults to 0, but otherwise returns saved volume
        slider.value = PlayerPrefs.GetFloat("MusicVol", 0.0001f);

        SetVolume(slider.value);
    }

    public void SetVolume(float sliderValue)
    {
        //from -80 dB to 0 dB (slider set from 0.0001 to 1)
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);

        //saved to playerPrefs
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }
}

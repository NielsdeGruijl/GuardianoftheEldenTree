using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider MusicSlider;

    private void Start()
    {
        audioManager = AudioManager.manager;

        SetAudioVolume();
    }

    private void Update()
    {
        SetAudioVolume();
    }

    void SetAudioVolume()
    {
        audioManager.SFXVolume = SFXSlider.value;
        audioManager.MusicVolume = MusicSlider.value;
    }
}

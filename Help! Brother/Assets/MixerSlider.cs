using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MixerSlider : MonoBehaviour
{
    [SerializeField] private string _soundParameterName;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _slider;
    [SerializeField] private float _multipier = 30f;


    private void Awake()
    {
        if (PlayerPrefs.HasKey(_soundParameterName))
        {
            var volume = PlayerPrefs.GetFloat(_soundParameterName);
            _mixer.SetFloat(_soundParameterName, GetMixerValue(volume));
            _slider.value = volume;
        }   
    }

    public void SliderChanged(float value)
    {
        _mixer.SetFloat(_soundParameterName, GetMixerValue(value));
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_soundParameterName,_slider.value);
    }

    private float GetMixerValue(float value)
    {
        return Mathf.Log10(value) * _multipier;
    }
}
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerToggle : MonoBehaviour
{
    [SerializeField] private string _soundParameterName;
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Toggle _toggle;


    private void Awake()
    {
        if (PlayerPrefs.HasKey(_soundParameterName))
        {
            var volume = PlayerPrefs.GetInt(_soundParameterName);
            _toggle.isOn = volume == 1;
            _mixer.SetFloat(_soundParameterName, _toggle.isOn?-80f:00f);
        }
        else
        {
            _toggle.isOn = false;
            _toggle.onValueChanged.Invoke(_toggle.isOn);
        }
    }

    public void ToggleChange(bool isOn)
    {
        _mixer.SetFloat(_soundParameterName, isOn?-80f:00f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(_soundParameterName,_toggle.isOn?1:0);
    }
}
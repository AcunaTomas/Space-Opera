using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private Slider _sliderMusic;
    [SerializeField]
    private Slider _sliderSFX;

    private void Start()
    {

        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = GameManager.INSTANCE.MUSIC_VOLUME;
        try
        {
            volume = _sliderMusic.value;
        }
        catch (System.Exception e)
        {

             
        }
        _mixer.SetFloat("music", Mathf.Log10(volume)*20);
        GameManager.INSTANCE.MUSIC_VOLUME = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = GameManager.INSTANCE.SFX_VOLUME;
        try
        {
            volume = _sliderSFX.value;
        }
        catch (System.Exception e)
        {

            
        }
        _mixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        GameManager.INSTANCE.SFX_VOLUME = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        if (SceneManager.GetActiveScene().name == "SceneMainMenu")
        {
            _sliderMusic.value = PlayerPrefs.GetFloat("musicVolume");
            _sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        SetMusicVolume();
        SetSFXVolume();
    }
}

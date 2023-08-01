using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager INSTANCE;

    [SerializeField]
    private AudioSource _audioUI;
    [SerializeField]
    private AudioSource _audioPlayer;
    [SerializeField]
    private AudioSource _audioAlarm;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioClip _buttonConfirm;
    [SerializeField]
    private AudioClip _musicIntro;

    private void Awake()
    {
        INSTANCE = this;
    }

    //ALL_SOUNDS
    public void PlayUI()
    {
        _audioUI.clip = _buttonConfirm;
        _audioUI.Play();
    }

    public void PlayPlayer()
    {
        _audioPlayer.clip = _buttonConfirm;
        _audioPlayer.Play();
    }

    public void PlayAlarm()
    {
        _audioAlarm.clip = _buttonConfirm;
        _audioAlarm.Play();
    }


    //ALL_MUSIC
    public void PlayMusic()
    {
        _musicSource.clip = _musicIntro;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }

}

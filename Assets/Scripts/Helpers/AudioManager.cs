using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager INSTANCE;

    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioClip _sound;
    [SerializeField]
    private AudioClip _musicIntro;

    private void Awake()
    {
        INSTANCE = this;
    }

    public void PlaySound()
    {
        _audio.clip = _sound;
        _audio.Play();
    }

    public void PlayMusic()
    {
        _musicSource.clip = _musicIntro;
        _musicSource.Play();
    }

}

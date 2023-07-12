using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _sound;

    public void PlaySound()
    {
        _audio.clip = _sound;
        _audio.Play();
    }

}

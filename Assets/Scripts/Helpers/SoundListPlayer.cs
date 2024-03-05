using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListPlayer : SoundList
{
   public Object[] Pasos;
   public Object[] Melees;
   public Object[] Landings;
   public Object[] Brody; 

    void Awake()
    {
        Pasos = Resources.LoadAll(BasePath + "/Pasos/Pasos lvl2", typeof(AudioClip));
        Melees = Resources.LoadAll(BasePath + "/Attack/Melee", typeof(AudioClip));
        Landings = Resources.LoadAll(BasePath + "/Landings", typeof(AudioClip));
        Brody = Resources.LoadAll(BasePath + "/Brody", typeof(AudioClip));
        Source = GetComponent<AudioSource>();
    }

    public override void QueueSound(string soundToQueue)
    {
        Source.PlayOneShot(ClipTypeSearch(soundToQueue), Source.volume);
    }

    private AudioClip ClipTypeSearch(string parameter)
    {
        if (parameter == "PlayPlayer")
        {
            return (AudioClip)Pasos[Random.Range(0, Pasos.Length - 1)];
        }
        if (parameter == "PlayBrodyGun")
        {
            return (AudioClip)Brody[0];
        }
        if (parameter == "PlayBrodyScan")
        {
            return (AudioClip)Brody[1];
        }
        return null;
    }
}

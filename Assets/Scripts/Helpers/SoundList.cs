using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    public Object[] Sounds;
    public string BasePath;
    public AudioSource Source;

    void Awake()
    {
        Sounds = Resources.LoadAll(BasePath, typeof(AudioClip));
        Source = GetComponent<AudioSource>();
    }

    public virtual void QueueSound(string soundToQueue)
    {
        Source.PlayOneShot(SearchAudioClip(soundToQueue), Source.volume) ;
    }

    public AudioClip SearchAudioClip(string filter) //Replace this with Collection later
    {
        foreach (AudioClip AC in Sounds )
        {
            if (filter == AC.name)
            {
                return AC;
            }
        }
        print("Sound Not Found");
        return null;
    }
}

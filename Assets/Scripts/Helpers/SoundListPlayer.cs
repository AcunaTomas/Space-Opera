using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundListPlayer : SoundList
{
    public List<AudioClip> sounds;
    private Object[] snds;
    void Awake()
    {

        sounds = Resources.LoadAll(BasePath, typeof(AudioClip)).Cast<AudioClip>().ToList();
        
        Source = GetComponent<AudioSource>();
    }

    public override void QueueSound(string soundToQueue)
    {
        Source.PlayOneShot(ClipTypeSearch(soundToQueue), Source.volume);
    }

    private AudioClip ClipTypeSearch(string parameter)
    {
        
        return sounds.Find(x => x.name == parameter);
    }
}

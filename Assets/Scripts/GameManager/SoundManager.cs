using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager INSTANCE;
    public AudioSource PlayerAudio;
    public AudioSource Music;
    public AudioSource FX;
    public AudioSource Ambient;

    public struct SoundInfo
    {
        public string clip;
        public int target;

        public SoundInfo(string a, int b)
        {
            clip = a;
            target = b;
        }
    }


    private void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
        }
        INSTANCE = this;

    }
    
    private void PlaySound(SoundInfo SoundInfo)
    {

        switch (SoundInfo.target)
        {
            case 0:
                PlayerAudio.SendMessage("QueueSound",SoundInfo.clip);
                break;
            case 2:
                FX.SendMessage("QueueSound",SoundInfo.clip);
                break;
            case 3:
                Ambient.Play();
                break;
            default:
                print("Playback failed");
                break;
        }
    }
    public void StartMusic(string SongID)
    {
        Music.SendMessage("QueueSound", SongID);
    }

}

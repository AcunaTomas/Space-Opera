using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public GameObject PANEL_DIALOGUE;


    private void FixedUpdate()
    {
        if (PANEL_DIALOGUE.activeSelf)
        {
            GetComponent<PlayableDirector>().Pause();
        }
        else
        {
            GetComponent<PlayableDirector>().Resume();
        }
    }

    // Es simplemente increible que esto ande tan bien :D

}

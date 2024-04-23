using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private UIManager PANEL_DIALOGUE;



    private void FixedUpdate()
    {
        if (PANEL_DIALOGUE == null)
        {
            PANEL_DIALOGUE = UIManager.INSTANCE;
        }
        if (PANEL_DIALOGUE.DialogueBoxStatus())
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCutscene : MonoBehaviour
{
    void Start()
    {
        AudioManager.INSTANCE.PlayMusic();
        try
        {
            gameObject.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
        }
        catch (System.Exception e)
        {
            Debug.Log("no hay error xd " + e);
        }
    }

}

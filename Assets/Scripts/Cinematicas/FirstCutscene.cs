using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCutscene : MonoBehaviour
{

    void Start()
    {
        AudioManager.INSTANCE.PlayMusic();
    }

}

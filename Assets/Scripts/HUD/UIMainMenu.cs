using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    private void Update()
    {
        if (LoadJson.DEBUG_MODE)
        {
            if (Input.GetKeyDown("1"))
            {
                ScenesManager.Instance.LoadNextScene("Tutorial");
            }

            if (Input.GetKeyDown("2"))
            {
                ScenesManager.Instance.LoadNextScene("Lvl2_Radar");
            }

            if (Input.GetKeyDown("3"))
            {
                ScenesManager.Instance.LoadNextScene("NewLevel3");
            }
        }
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

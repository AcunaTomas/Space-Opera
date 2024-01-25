using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsEnd : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(EndMySuffering());

        AudioManager.INSTANCE.PlayMusic();
    }


    IEnumerator EndMySuffering()
    {
        yield return new WaitForSeconds(40f);
        ScenesManager.Instance.LoadNextScene("SceneMainMenu");
    }
}

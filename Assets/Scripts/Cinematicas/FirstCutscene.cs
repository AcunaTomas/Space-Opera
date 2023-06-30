using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstCutscene : MonoBehaviour
{

    public Animator animator;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("akfjasjf");
            SceneManager.LoadScene("Tutorial");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        ScenesManager.Instance.LoadNewGame();
    }

}

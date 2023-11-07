using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemoShowcase : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(EndMySuffering());
    }


    IEnumerator EndMySuffering()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Credits");
    }
}

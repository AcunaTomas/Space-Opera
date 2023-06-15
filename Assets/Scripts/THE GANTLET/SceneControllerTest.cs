using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerTest : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gantlet"))
        {
            SceneManager.LoadScene("Tutorial");
        }
        if (other.CompareTag("Tutorial"))
        {
            SceneManager.LoadScene("THE GANTLET");
        }

    }


}

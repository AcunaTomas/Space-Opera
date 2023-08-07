using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AlphaMovingCamera : MonoBehaviour
{
    float i = 4f;
    float iIncrement = 0.0007f;

    void Update()
    {
        i += iIncrement;

        transform.position = new Vector3(transform.position.x + iIncrement , transform.position.y, 0);

        if (transform.position.x >= 28 || transform.position.x <= 3)
        {
            iIncrement = iIncrement * -1;
        }

        if (Input.GetAxis("Submit") > 0 || Input.GetAxis("Jump") > 0)
        {
            SceneManager.LoadScene("SceneMainMenu");
        }
    }
}

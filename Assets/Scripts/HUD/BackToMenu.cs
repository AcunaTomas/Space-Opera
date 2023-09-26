using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            ScenesManager.Instance.LoadMainMenu();
        }
    }
}

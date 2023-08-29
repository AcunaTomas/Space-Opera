using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CHANGESCENE : MonoBehaviour
{
    public void LoadScene2(string input)
    {
        SceneManager.LoadScene("Coleccionable");
    }
}

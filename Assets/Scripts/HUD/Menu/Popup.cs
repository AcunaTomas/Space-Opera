using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void StartBackButton()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        try
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        catch (System.Exception e)
        {

        }
    }
}

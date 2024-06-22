using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void StartBackButton()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if(UIManager.INSTANCE != null)
        {
            if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
            {
                Debug.Log("BRODY");
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
            }
            if(UIManager.INSTANCE.CURRENTPLAYER == "Bito")
            {
                Debug.Log("BITO");
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        try
        {
            if(transform.GetChild(1).gameObject.name != "ControllerButtons1") transform.GetChild(1).gameObject.SetActive(true);
            if(transform.GetChild(2).gameObject.name != "BackgroundButtons2")transform.GetChild(2).gameObject.SetActive(true);
        }
        catch (System.Exception e)
        {
                print("PopUp null Reference Exception" + e);
        }
    }
}

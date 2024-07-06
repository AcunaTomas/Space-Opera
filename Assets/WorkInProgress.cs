using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInProgress : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            this.gameObject.SetActive(false);
        }
        
    }
}

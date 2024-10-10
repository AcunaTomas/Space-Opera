using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInProgress : MonoBehaviour
{
    void Update()
    {
        if(InputManager.INSTANCE.SubmitInput)
        {
            this.gameObject.SetActive(false);
        }
        
    }
}

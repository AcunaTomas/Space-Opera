using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInProgress : MonoBehaviour
{
    void Update()
    {
        InputManager.INSTANCE.UIInputs();
        if(InputManager.INSTANCE.SubmitInput)
        {
            InputManager.INSTANCE.InGameInputs();
            this.gameObject.SetActive(false);
        }
        
    }
}
